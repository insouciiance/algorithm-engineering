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
        private TextBlock _currentCardUI = null;

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
                ColumnDefinition def = new()
                {
                    Width = new GridLength(110)
                };

                PlayerHand.ColumnDefinitions.Add(def);
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
                ColumnDefinition def = new()
                {
                    Width = new GridLength(110)
                };

                OpponentHand.ColumnDefinitions.Add(def);
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
                Margin = new Thickness(5),
                Width = 100,
                Height = 140,
            };

            ImageBrush brush = new();
            string imageUrl = CardImageHelper.GetCardImageURL(card);

            if (imageUrl is not null)
            {
                brush.ImageSource = new BitmapImage(new Uri(imageUrl, UriKind.Relative));
                cardBlock.Background = brush;
                cardBlock.Cursor = Cursors.Hand;
            }
            else
            {
                cardBlock.Background = Brushes.DimGray;
            }

            cardBlock.SetValue(Grid.RowProperty, row);
            cardBlock.SetValue(Grid.ColumnProperty, column);

            return cardBlock;
        }

        private void OnPlayerCardMouseDown(object obj, MouseEventArgs _)
        {
            if (_currentCardUI is not null)
            {
                _currentCardUI.Opacity = 1;
            }

            _currentCardUI = (TextBlock)obj;
            object columnObj = _currentCardUI.GetValue(Grid.ColumnProperty);
            int columnIndex = Convert.ToInt32(columnObj);

            if (_currentCardIndex == columnIndex)
            {
                _currentCardIndex = -1;
                _currentCardUI = null;
                return;
            }

            _currentCardUI.Opacity = 0.5;
            _currentCardIndex = columnIndex;

            ColorPossibleMoves();
        }

        private async void OnBoardCardMouseDown(object obj, MouseEventArgs _)
        {
            _currentCardUI = null;

            object columnObj = ((TextBlock)obj).GetValue(Grid.ColumnProperty);
            int columnIndex = Convert.ToInt32(columnObj);
            object rowObj = ((TextBlock)obj).GetValue(Grid.RowProperty);
            int rowIndex = Convert.ToInt32(rowObj);

            if (_nightAi.CurrentBoard.Board[rowIndex, columnIndex] is null && _currentCardIndex < 0)
            {
                return;
            }

            MoveInput input = new(rowIndex, columnIndex, _currentCardIndex);

            _nightAi.TakePlayerMove(input);

            _currentCardIndex = -1;

            ClearField();
            InitializeField();

            if (_nightAi.IsGameFinished())
            {
                MessageBox.Show("You won!");
                return;
            }

            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    StatusBar.Text = "AI thinking...";
                });

                _nightAi.TakeAIMove();

                Dispatcher.Invoke(() =>
                {
                    ClearField();
                    InitializeField();

                    if (_nightAi.IsGameFinished())
                    {
                        MessageBox.Show("AI won!");
                    }

                    StatusBar.Text = "Your turn";
                });
            });
        }

        private void ColorPossibleMoves()
        {
            foreach (UIElement card in Board.Children)
            {
                card.Opacity = 1;
            }

            Card currentCard = _nightAi.CurrentBoard.PlayerHand[_currentCardIndex];

            var cardIndexes = _nightAi.CurrentBoard.GetCardIndexes(currentCard);

            foreach (Tuple<int, int> cardIndex in cardIndexes)
            {
                object cardUI = Board.Children[cardIndex.Item1 * 9 + cardIndex.Item2];
                ((TextBlock)cardUI).Opacity = 0.75;
            }
        }
    }
}
