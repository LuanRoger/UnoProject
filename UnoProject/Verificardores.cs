using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoProject
{
    public static class Verificardores
    {
        public static bool VerificarInputChar(char character) => character.ToString().ToUpper().Equals("S");

        public static void VerificarPlayers(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                {
                    Error error = new Error();
                    error.Error1();
                    break;
                }
                case > 4:
                {
                    Error error = new Error();
                    error.Error2();
                    break;
                }
                case 1:
                {
                    Error error = new Error();
                    error.Error3();
                    break;
                }
            }
        }

        public static bool VerificarCartaParaJogar(Card cardToPlay, Deck deckNow) =>
            deckNow.deckHsitory.First().cardColor == cardToPlay.cardColor ||
            deckNow.deckHsitory.First().number == cardToPlay.number;

        public static void VerificarVitoria(List<Player> players)
        {
            try
            {
                var winner = players.First(p => p.hand.Count <= 0);
                winner.PlayerWinner();
            }
            catch { /*Nothing*/ }
        }
    }
}
