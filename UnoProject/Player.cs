using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoProject
{
    public class Player
    {
        public string name { get; init; }
        public List<Card> hand { get; init; }
        public bool isWinner { get; private set; } = false;
        public Player(string name, Deck deck)
        {
            this.name = name;
            this.hand = DrawInitialCards(deck);
        }

        public void DrawCard(Deck deck, int times)
        {
            for (int t = 0 ; t != times; t++)
            {
                hand.Add(deck.deck.First());
                deck.deck.Remove(deck.deck.First());
            }
        }

        /// <summary>
        /// Joga uma carta de acordo com o inedx
        /// </summary>
        /// <param name="cardIndex"></param>
        /// <param name="deckNow"></param>
        /// <returns>Retorna <c>sucesso</c></returns>
        public bool PlayCard(int cardIndex, Deck deckNow)
        {
            if (Verificardores.VerificarCartaParaJogar(hand[cardIndex], deckNow))
            {
                deckNow.deckHsitory.Add(hand[cardIndex]);
                hand.RemoveAt(cardIndex);
                return true;
            }
            else return false;
        }

        private List<Card> DrawInitialCards(Deck deck)
        {
            List<Card> initialCardHand = new List<Card>();
            for (int c = 0; c != 7; c++)
            {
                initialCardHand.Add(deck.deck.First());
                deck.deck.Remove(deck.deck.First());
            }

            return initialCardHand;
        }

        public void PlayerWinner() => isWinner = true;
    }
}
