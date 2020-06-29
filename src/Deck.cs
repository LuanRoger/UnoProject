using System;
using System.Text;

namespace DeckNS{
    public class Deck{
        //Cartas e números
        private int[] nunbCards = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        private string[] cardColor = new string[] {"Vermelho", "Amarelo", "Verde", "Azul", "Mudar cor"};

        //Pegar uma carta
        internal string takeCard(){
            Random rand = new Random();
            StringBuilder card = new StringBuilder();

            int nCardNunb = rand.Next(0,10);
            int nCardColorNunb = rand.Next(0, 5);

            card.Append(nunbCards[nCardNunb]);
            card.Append(cardColor[nCardColorNunb]);
            if(nCardColorNunb == 4) { card.Remove(0, 1); } //Remover o número de "Mudar cor"

            return Convert.ToString(card);
        }
    }
}