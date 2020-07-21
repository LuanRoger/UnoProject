using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using PlayerNS;
using DeckNS;
using RulesNS;
using ErrorNS;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = new Config(); //set Configs

            // Boas vindas
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine("+    Uno Project v1.0.0      +");
            Console.WriteLine("+      por Luan Roger        +");
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine("Jogadores:");
            foreach(string playersShow in args) { //Mostrar jogadores
                Console.WriteLine(">" + playersShow);
            }
            
            // Verificar jogadores e mostrar posivel error
            if(args.Length == 0) {
                Error error = new Error();
                error.error1();
            }else if(args.Length > 4) {
                Error error = new Error();
                error.error2();
            }else if(args.Length == 1) {
                Error error = new Error();
                error.error3();
            }

            //Adicionar jogadores
            List<Player> players = new List<Player>();
            switch(args.Length){
                case 2:
                players.Add(new Player());
                players.Add(new Player());
                players[0].SetName(args[0]);
                players[1].SetName(args[1]);
                break;
                case 3:
                players.Add(new Player());
                players.Add(new Player());
                players.Add(new Player());
                players[0].SetName(args[0]);
                players[1].SetName(args[1]);
                players[2].SetName(args[2]);
                break;
                case 4:
                players.Add(new Player());
                players.Add(new Player());
                players.Add(new Player());
                players.Add(new Player());
                players[0].SetName(args[0]);
                players[1].SetName(args[1]);
                players[2].SetName(args[2]);
                players[3].SetName(args[3]);
                break;
            }
            Console.WriteLine($"Existem {players.Count} jogadores."); //Quantos jogadores existem
            

            //Menu
            Console.WriteLine("=+=+=+=+=+= Menu =+=+=+=+=+=+=+=+");
            Console.WriteLine("[ 1 ] - Começar.    [ 2 ] - Sair.");
            int menuChoice = Convert.ToInt32(Console.ReadLine());
            switch(menuChoice){
                case 1: break;
                case 2: 
                Environment.Exit(0);
                break;
                default:
                Error error = new Error();
                error.error4();
                break;
            }

            //Dar as cartas a todos os players, 7 cartas para cada
            Deck deck = new Deck();
            switch(args.Length){//Dar as carta apenas para os players que estão jogando
                case 2: //Número de players
                for(int cardAmmount = 0; cardAmmount != 8; cardAmmount++){
                    players[0].hand.Add(deck.takeCard());
                }
                for(int cardAmmount = 0; cardAmmount != 8; cardAmmount++){
                    players[1].hand.Add(deck.takeCard());
                }
                Player.seeHand(players, 0);
                Thread.Sleep(2000); //Limitar o tempo em que aparecem as cartas
                Console.WriteLine("=======================================");
                Player.seeHand(players, 1);
                Thread.Sleep(2000);
                break;
                case 3:
                for (int cardAmmount = 0; cardAmmount != 8; cardAmmount++) {
                    players[0].hand.Add(deck.takeCard());
                }
                for(int cardAmmount = 0; cardAmmount != 8; cardAmmount++){
                    players[1].hand.Add(deck.takeCard());
                }
                for(int cardAmmount = 0; cardAmmount != 8; cardAmmount++){
                    players[2].hand.Add(deck.takeCard());
                }
                //Mostrar mão
                Player.seeHand(players, 0);
                Thread.Sleep(2000);
                Console.WriteLine("=======================================");
                Player.seeHand(players, 1);
                Thread.Sleep(2000);
                Console.WriteLine("=======================================");
                Player.seeHand(players, 2);
                Thread.Sleep(2000);
                break;
                case 4:
                for (int qc = 0; qc != 8; qc++) {
                    players[0].hand.Add(deck.takeCard());
                }
                for(int qc = 0; qc != 8; qc++){
                    players[1].hand.Add(deck.takeCard());
                }
                for(int qc = 0; qc != 8; qc++){
                    players[2].hand.Add(deck.takeCard());
                }
                for(int qc = 0; qc != 8; qc++){
                    players[3].hand.Add(deck.takeCard());
                }
                Player.seeHand(players, 0);
                Thread.Sleep(2000);
                Console.WriteLine("=======================================");
                Player.seeHand(players, 1);
                Thread.Sleep(2000);
                Console.WriteLine("=======================================");
                Player.seeHand(players, 2);
                Thread.Sleep(2000);
                Console.WriteLine("=======================================");
                Player.seeHand(players, 3);
                Thread.Sleep(2000);
                break;
            }
            
            //Começar jogo
            Rules rules = new Rules(players.Count, players);
            int playingNow = 0; //Variavel que dirá qual jogador estará jogando no momento

            while(rules.IsWin(players, playingNow) == false){ //Jogo não acaba até alguem ficar sem carta

                playingNow = rules.PlayerTime(players, playingNow);
                Console.ForegroundColor = ConsoleColor.DarkBlue; //Mudar cor para destaque
                Console.WriteLine($"Agora: {rules.playerOrder[playingNow]}");
                Console.ForegroundColor = ConsoleColor.Black;

                rules.showHistoric(); //Mostrar carta atual.
                
                //playerActions
                System.Console.WriteLine("Escolha o que deseja fazer:");
                System.Console.WriteLine("[ 1 ] - Jogar uma carta.     [ 2 ] - Puxar uma carta.    [ 3 ] - Sair do jogo.");

                int gameChoice = Convert.ToInt32(Console.ReadLine());
                switch(gameChoice) {
                    case 1:
                    Player.seeHand(players, playingNow); //Ver cartas na mão.
                    Console.WriteLine("Selecione a carta por meio do número da posição:");

                    int cardChoised = Convert.ToInt32(Console.ReadLine());
                
                    Player.playCard(players[playingNow].hand[cardChoised], players, playingNow, cardChoised);

                    playingNow++; //Incremento para o proximo jogador
                    break;

                    case 2:
                    string cardTaked = deck.takeCard();
                    players[playingNow].hand.Add(cardTaked);
                    Console.WriteLine($"Você pegou: {cardTaked}");

                    Thread.Sleep(1200);
                    break;

                    case 3:
                    Environment.Exit(0);
                    break;
                }
            }

            //Indentificar o ganhador
            Console.WriteLine($"Temos um vecedor: {rules.IsWinner(players)}");
            Console.WriteLine("Obrigado por jogar.");
        }
    }
    // Config class
    internal class Config{
        internal Config() {
            Console.Title = "Uno Console por Luan Roger v0.0.3";
            Console.Beep();
        }
    }
}
