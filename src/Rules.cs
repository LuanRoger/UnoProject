using System;
using System.Collections.Generic;
using PlayerNS;
using src; //Remover

namespace RulesNS{
    internal class Rules {
        internal string[] playerOrder;
        internal static List<string> stack = new List<string>(); //Historico de cartas jogadas

        internal Rules(int playersAmount, List<Player> players){
            playerOrder = new string[playersAmount];
            for(int c = 0; c != playersAmount; c++){
                playerOrder[c] = players[c].name;
            }
            
           /*
            *Preferi criar um novo Random ao inves de pergar do takeCard() pois não teria que remover o "+4" e "Mudar cor".
            *Além de ter que instanciar Deck ou tornar takeCard() static.
            */
            Random random = new Random();
            string[] cardColor = new string[] {"Vermelho", "Amarelo", "Verde", "Azul"};
            int firstCard = random.Next(0, 4);
            stack.Add("0" + cardColor[firstCard]);
           /*"0" para que não ocorra problemas em playCard().
            *Isto apenas para a primeira carta.
            */
        }
        internal void showHistoric(){ Console.WriteLine($"Carta atual: {stack[stack.Count - 1]}"); }
        internal static string chageColor(){
            Console.WriteLine("Escolha a nova cor: ");
            Console.WriteLine("[ 1 ] - Vermelho. [ 2 ] - Amarelo.\n[ 3 ] - Verde.    [ 4 ] - Azul.");
            int colorChoice = Convert.ToInt32(Console.ReadLine());
            
            switch(colorChoice){
                case 1:
                Console.WriteLine("Agora a nova cor é Vermelho.");
                return "0Vermelho";

                case 2:
                Console.WriteLine("Agora a nova cor é Amarelo.");
                return "0Amarelo";

                case 3: 
                Console.WriteLine("Agora a nova cor é Verde.");
                return "0Verde";

                case 4:
                Console.WriteLine("Agora a nova cor é Azul.");
                return "0Azul";

                default:
                Error error = new Error();
                error.error4();
                break;
            }
            return "error"; //return fora da conticional para que tenha um retorno string para o metodo.
        }
        public bool IsWin(List<Player> players, int playingNow) => players[(playingNow >= 1 ? playingNow -= 1 : playingNow)].hand.Count == 0 ? true : false;
        public int PlayerTime(List<Player> players, int playingNow) => players.Count == 2 && playingNow == 2 || players.Count == 3 && playingNow == 3 ||
        players.Count == 4 && playingNow == 4 ? 0 : playingNow;
        public string IsWinner(List<Player> players) {
            if(players[0].hand.Count == 0){ return players[0].name; }
            else if(players[1].hand.Count == 0){ return players[1].name; }
            else if(players[2].hand.Count == 0){ return players[2].name; }
            else if(players[3].hand.Count == 0){ return players[3].name; }
            else { return null; }
        }
    }
}