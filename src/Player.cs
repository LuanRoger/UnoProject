using System.Collections.Generic;
using System;
using RulesNS;

namespace PlayerNS{
    public class Player{
        public string name {get; set;}
        internal List<string> hand = new List<string>();

        public static void seeHand(List<Player> players, int playingNow){
                Console.WriteLine($"Cartas do {players[playingNow].name}:"); //Mostrar nome do player no momento
                
                foreach(string pCards in players[playingNow].hand) {
                    Console.WriteLine(pCards);
                }
        }
        public static void playCard(string cardCode, List<Player> players, int pNow, int cPlayed){
            //Tirar o numero, deixando apenas a cor
            string processingCardCode = cardCode.Remove(0, 1); 
            string lastCardPlayed = Rules.stack[Rules.stack.Count - 1].Remove(0, 1);

             //Fazer a verificação para sabre se o número ou a cor da carta são iguais
            if(processingCardCode == lastCardPlayed || cardCode.Substring(0,1) == Rules.stack[Rules.stack.Count - 1].Substring(0, 1) || cardCode == "Mudar cor"){
                Rules.stack.Add(cardCode);//Adicionar a pilha de cartas
                Console.WriteLine($"Você jogou: {players[pNow].hand[cPlayed]}");

                players[pNow].hand.RemoveAt(cPlayed);//Remover da mão

                if(cardCode == "Mudar cor"){
                    Rules.stack.Add(Rules.chageColor());
                }
            }else{
                Console.WriteLine("Está carta não pode ser jogada.");
                Console.WriteLine("Você perdeu a vez.");
            }
        }
    }
}

