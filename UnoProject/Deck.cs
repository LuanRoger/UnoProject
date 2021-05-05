using System;
using System.Collections.Generic;
using System.Linq;

namespace UnoProject
{
    public class Deck
    {
        private readonly int DECK_SIZE = 200;
        public List<Card> deck { get; }
        public List<Card> deckHsitory { get; private set; }

        public Deck()
        {
            deck = Card.CardFactory(DECK_SIZE);

            Card starterCard = Card.CardFactory(1).First();
            while (starterCard.isSpecial) starterCard = Card.CardFactory(1).First();

            deckHsitory = new List<Card>();
            deckHsitory.Add(starterCard);
        }
    }
}
