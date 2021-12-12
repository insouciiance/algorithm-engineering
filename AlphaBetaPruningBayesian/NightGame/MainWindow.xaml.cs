using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlphaBetaPruningBayesian;
using Night;
using Night.Services;
using NightGame.Services;

namespace NightGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NightAI _nightAi;
        private int _currentCardIndex = -1;

        private Board GameBoard => _nightAi?.CurrentBoard;

        public MainWindow()
        {
            Board board = BoardGenerator.BoardGenerator.Generate();
            _nightAi = new NightAI(board);
            InitializeComponent();
            InitializeField();
        }

        private void ClearField()
        {
            Board.Children.Clear();
            PlayerHand.Children.Clear();
            PlayerHand.ColumnDefinitions.Clear();
            OpponentHand.Children.Clear();
            OpponentHand.ColumnDefinitions.Clear();
        }

        private void InitializeField()
        {
            for (int i = 0; i < GameBoard.Rows; i++)
            {
                for (int j = 0; j < GameBoard.Cols; j++)
                {
                    Card card = GameBoard.Board[i, j];
                    TextBlock cardBlock = CreateCard(i, j, card);
                    cardBlock.PreviewMouseDown += OnBoardCardMouseDown;
                    Board.Children.Add(cardBlock);
                }
            }

            InitializePlayerHand(GameBoard);
            InitializeOpponentHand(GameBoard);
        }

        private void InitializePlayerHand(Board board)
        {
            foreach (Card _ in board.PlayerHand)
            {
                PlayerHand.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < board.PlayerHand.Length; i++)
            {
                Card card = board.PlayerHand[i];
                TextBlock cardBlock = CreateCard(0, i, card);

                cardBlock.PreviewMouseDown += OnPlayerCardMouseDown;

                PlayerHand.Children.Add(cardBlock);
            }
        }

        private void InitializeOpponentHand(Board board)
        {
            foreach (Card _ in board.OpponentHand)
            {
                OpponentHand.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < board.OpponentHand.Length; i++)
            {
                Card card = board.OpponentHand[i];
                TextBlock cardBlock = CreateCard(0, i, card);
                OpponentHand.Children.Add(cardBlock);
            }
        }

        private TextBlock CreateCard(int row, int column, Card card)
        {
            TextBlock cardBlock = new()
            {
                Margin = new Thickness(5)
            };

            ImageBrush brush = new();
            string imageUrl = CardImageHelper.GetCardImageURL(card);

            if (imageUrl is not null)
            {
                brush.ImageSource = new BitmapImage(new Uri(imageUrl, UriKind.Relative));
                cardBlock.Background = brush;
            }

            cardBlock.SetValue(Grid.RowProperty, row);
            cardBlock.SetValue(Grid.ColumnProperty, column);

            return cardBlock;
        }

        private void OnPlayerCardMouseDown(object obj, MouseEventArgs _)
        {
            object columnObj = ((TextBlock)obj).GetValue(Grid.ColumnProperty);
            int columnIndex = Convert.ToInt32(columnObj);
            _currentCardIndex =  columnIndex;
        }

        private async void OnBoardCardMouseDown(object obj, MouseEventArgs _)
        {
            object columnObj = ((TextBlock)obj).GetValue(Grid.ColumnProperty);
            int columnIndex = Convert.ToInt32(columnObj);
            object rowObj = ((TextBlock)obj).GetValue(Grid.RowProperty);
            int rowIndex = Convert.ToInt32(rowObj);

            MoveInput input = new(rowIndex, columnIndex, _currentCardIndex);
            _nightAi.TakePlayerMove(input);

            _currentCardIndex = -1;

            ClearField();
            InitializeField();

            await Task.Run(() =>
            {
                _nightAi.TakeAIMove();

                Dispatcher.Invoke(() =>
                {
                    ClearField();
                    InitializeField();
                });
            });
        }
    }
}
