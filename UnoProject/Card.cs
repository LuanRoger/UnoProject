using System;
using System.Collections.Generic;

namespace UnoProject
{
    public enum CardColors { Vermelho, Amarelo, Verde, Azul, Preto}
    public class Card
    {
        private static readonly string[] BLACKCARDS = {"+4", "Mudar cor"};
        private static readonly string[] CARDS = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+2", "Reverso", "Bloqueio"};
        public CardColors cardColor { get; }
        public string number { get; }
        public bool isSpecial { get; }

        private Card(string cardNumb, CardColors cardColor, bool isSpecial)
        {
            this.cardColor = cardColor;
            number = cardNumb;
            this.isSpecial = isSpecial;
        }

        public void SeeCard()
        {
            var consleForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = cardColor switch
            {
                CardColors.Vermelho => ConsoleColor.Red,
                CardColors.Amarelo => ConsoleColor.Yellow,
                CardColors.Verde => ConsoleColor.Green,
                CardColors.Azul => ConsoleColor.Blue,
                CardColors.Preto => ConsoleColor.White,
            };
            Console.WriteLine($"{number} {cardColor}");

            Console.ForegroundColor = consleForegroundColor;
        }

        public static List<Card> CardFactory(int times)
        {
            List<Card> cardList = new();
            for (; times != 0; times--)
            {
                CardColors colorPicker = (CardColors)(new Random().Next(5));
                string cardNumber = new Random().Next(CARDS.Length).ToString();
                bool isSpecial = colorPicker is CardColors.Preto;

                cardNumber = colorPicker switch
                {
                    CardColors.Preto => BLACKCARDS[new Random().Next(BLACKCARDS.Length)],
                    _ => cardNumber
                };

                Card cardGenerated = new Card(cardNumber, colorPicker, isSpecial);
                cardList.Add(cardGenerated);
            }

            return cardList;
        }
    }
}
