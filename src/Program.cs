using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = new Config(); //set Configs

            // Boas vindas
            Console.WriteLine($"===Uno Console v0.01===");
            Console.WriteLine("Jogadores:");
            foreach(string playersShow in args) { //Mostrar jogadores
                Console.WriteLine(playersShow);
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
                players[0].name = args[0];
                players[1].name = args[1];
                break;
                case 3:
                players.Add(new Player());
                players.Add(new Player());
                players.Add(new Player());
                players[0].name = args[0];
                players[1].name = args[1];
                players[2].name = args[2];
                break;
                case 4:
                players.Add(new Player());
                players.Add(new Player());
                players.Add(new Player());
                players.Add(new Player());
                players[0].name = args[0];
                players[1].name = args[1];
                players[2].name = args[2];
                players[3].name = args[3];
                break;
            }
            Console.WriteLine($"Existem {players.Count} jogadores."); //Quantos jogadores existem
            

            //Menu
            Console.WriteLine("===Menu===");
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
                case 2:
                for(int cardAmmount = 0; cardAmmount != 8; cardAmmount++){
                    players[0].hand.Add(deck.takeCard());
                }
                for(int cardAmmount = 0; cardAmmount != 8; cardAmmount++){
                    players[1].hand.Add(deck.takeCard());
                }
                Player.seeHand(players, 0);
                Thread.Sleep(2000); //Limitar o tempo em que aparecem as cartas
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
                Player.seeHand(players, 1);
                Thread.Sleep(2000);
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
                Player.seeHand(players, 1);
                Thread.Sleep(2000);
                Player.seeHand(players, 2);
                Thread.Sleep(2000);
                Player.seeHand(players, 3);
                Thread.Sleep(2000);
                break;
            }
            
            //Começar jogo
            Rules rules = new Rules(players.Count, players);
            int playingNow = 0; //Variavel que dirá qual jogador estará jogando no momento
            while(players[0].hand.Count != 0 || players[1].hand.Count != 0 || players[2].hand.Count != 0 || players[3].hand.Count != 0){ //Jogo não acaba até alguem ficar sem carta
                //Resetar playingNow de acordo com a quantidade de players
                if(players.Count == 2 && playingNow == 2){
                    playingNow = 0;
                }else if(players.Count == 3 && playingNow == 3){
                    playingNow = 0;
                }else if(players.Count == 4 && playingNow == 4){
                    playingNow = 0;
                }
                Console.WriteLine($"Agora: {rules.playerOrder[playingNow]}");
                rules.showHistoric(); //Mostrar carta atual.
                
                //playerActions
                System.Console.WriteLine("Escolha o que deseja fazer:");
                System.Console.WriteLine("[ 1 ] - Jogar uma carta.     [ 2 ] - Puxar uma carta.    [ 3 ] - Sair do jogo.");

                int gameChoice = Convert.ToInt32(Console.ReadLine());
                switch(gameChoice) {
                    case 1:
                    Player.seeHand(players, playingNow); //Ver cartas na mão.
                    Console.WriteLine("Selecione a carta por meio do número da posição da carta, sendo a primeira carta o número 0: ");

                    int cardChoised = Convert.ToInt32(Console.ReadLine());
                
                    Player.playCard(players[playingNow].hand[cardChoised], players, playingNow, cardChoised);

                    playingNow += 1; //Incremento para o proximo jogador
                    break;

                    case 2:
                    string cTaked = deck.takeCard();
                    players[playingNow].hand.Add(cTaked);
                    Console.WriteLine($"Você pegou: {cTaked}");
                    break;

                    case 3:
                    Environment.Exit(0);
                    break;

                    default:
                    Error error = new Error();
                    error.error4();
                    break;
                }
            }

            //Indentificar o ganhador
            string winner = "null";
            if(players[0].hand.Count != 0){
                winner = players[0].name;
            }else if(players[1].hand.Count != 0){
                winner = players[1].name;
            }else if(players[2].hand.Count != 0){
                winner = players[2].name;
            }else if(players[3].hand.Count != 0){
                winner = players[3].name;
            }
            Console.WriteLine($"Temos um vecedor: {winner}");
            Console.WriteLine("Obrigado por jogar.");
        }
    }
#region Classes
    // Config class
    internal class Config{
        internal Config() {
            Console.Title = "Uno Console by Luan Roger v0.01";
            Console.Beep();
        }
    }

    // Deck class
    internal class Deck{
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

    // Player class
    internal class Player{
        internal string name {get; set;}
        internal List<string> hand = new List<string>();

        internal static void seeHand(List<Player> players, int playingNow){
                Console.WriteLine($"Cartas do {players[playingNow].name}:"); //Mostrar nome do player no momento
                
                foreach(string pCards in players[playingNow].hand) {
                    Console.WriteLine(pCards);
                }
        }
        internal static void playCard(string cardCode, List<Player> players, int pNow, int cPlayed){
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

    //Rule class
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
        internal void showHistoric(){
            Console.WriteLine($"Carta atual: {stack[stack.Count - 1]}");
        }
        internal static string chageColor(){
            Console.WriteLine("Escolha a nova cor: ");
            Console.WriteLine("[ 1 ] - Vermelho.    [ 2 ] - Amarelo.\n[ 3 ] - Verde.    [ 4 ] - Azul.");
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

    }

    // Error class
    internal class Error{
        internal Error() { // Texto vermlho
            Console.ForegroundColor = ConsoleColor.Red;
        }
        internal void error1() {
            Console.WriteLine("Para começar a jogar você precisa pasar o nome dos jogadores como argumento.");
            Environment.Exit(1);
        }
        internal void error2(){
            Console.WriteLine("São suportados somente 4 jogadores.");
            Environment.Exit(2);
        }
        internal void error3() {
            Console.WriteLine("Jogadores insufucientes, minimo 2");
            Environment.Exit(3);
        }
        internal void error4(){
            Console.WriteLine("Valor inválido.");
            Environment.Exit(4);
        }
    }
#endregion
}
