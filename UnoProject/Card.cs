using System;
using System.Collections.Generic;
using System.Linq;

namespace UnoProject
{
    public enum CardColors { Vermelho, Amarelo, Verde, Azul, Preto}
    public class Card
    {
        private static readonly string[] BLACK_CARDS = {"+4", "Mudar cor"};
        private static readonly string[] CARDS = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+2", "Reverso", "Bloqueio"};
        public CardColors cardColor { get; private set; }
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

        public void ActivateEffect(Card card, List<Player> players, Deck deckNow, int playingNow)
        {
            switch (card.number)
            {
                case "+4":
                    PlusFourEffect(players, deckNow, playingNow);
                    ChangeColor();
                    break;
                case "Mudar cor":
                    deckNow.deckHsitory.Last().cardColor = ChangeColor();
                    break;
                case "+2":
                    PlusTowEffect(players, deckNow, playingNow);
                    break;
                case "Reverso":
                    ReverseEffect(players);
                    break;
                case "Bloqueio":
                    BlockEffect(players, playingNow);
                    break;
            }
        }
        private int PlayerEffected(IReadOnlyList<Player> players, int playingNow)
        {
            try
            { 
                _ = players[playingNow + 1]; //Verificar se é somável
                return ++playingNow;
            }
            catch (IndexOutOfRangeException) { return playingNow = 0; }
        }
        private void PlusTowEffect(List<Player> players, Deck deckNow, int playingNow) => 
            players[PlayerEffected(players, playingNow)].DrawCard(deckNow, 2);
        private void PlusFourEffect(List<Player> players, Deck deckNow, int playingNow) => 
            players[PlayerEffected(players, playingNow)].DrawCard(deckNow, 4);
        private void ReverseEffect(List<Player> players) => players.Reverse();
        private int BlockEffect(List<Player> players, int playingNow) => PlayerEffected(players, playingNow) + 1;
        private CardColors ChangeColor()
        {
            int colorChoice;
            while (true)
            {
                Console.WriteLine("Qual será a cor?");
                Console.WriteLine("[ 1 ] - Vermelho.   [ 2 ] - Amarelo.   [ 3 ] - Verde.   [ 4 ] - Azul");
                try
                {
                    colorChoice = Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                    break;
                }
                catch { Console.WriteLine("Digite um index de uma carta valida"); }
            }
            return (CardColors)colorChoice - 1;
        }

        public static List<Card> CardFactory(int times)
        {
            List<Card> cardList = new();
            for (; times != 0; times--)
            {
                CardColors colorPicker = (CardColors)(new Random().Next(5));
                string cardNumber = CARDS[new Random().Next(CARDS.Length)];
                bool isSpecial = colorPicker is CardColors.Preto || cardNumber == CARDS[10] || cardNumber == CARDS[11] || cardNumber == CARDS[12];

                cardNumber = colorPicker switch
                {
                    CardColors.Preto => BLACK_CARDS[new Random().Next(BLACK_CARDS.Length)],
                    _ => cardNumber
                };

                Card cardGenerated = new Card(cardNumber, colorPicker, isSpecial);
                cardList.Add(cardGenerated);
            }

            return cardList;
        }
    }
}
