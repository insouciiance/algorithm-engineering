using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Night;

namespace NightGame.Services
{
    internal static class CardImageHelper
    {
        public static string GetCardImageURL(Card card)
        {
            if (card is null)
            {
                return null;
            }

            if (!card.Open)
            {
                return "./Assets/closed.png";
            }

            CardNominal nominal = card.Nominal;
            CardSuit suit = card.Suit;

            string nominalString = nominal.ToString().ToLowerInvariant();
            string suitString = suit.ToString().ToLowerInvariant();

            return $"./Assets/{nominalString}_of_{suitString}.png";
        }
    }
}
