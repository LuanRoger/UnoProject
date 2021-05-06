using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnoProject
{
    class Program
    {
        private static readonly int COOLDOWN_SLEEP0 = 1000;
        private static readonly int COOLDOWN_SLEEP1 = 500;
        static void Main(string[] args)
        {
            Config.SetConfig();

            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine($"+  Uno Project v{Config.GetProgramVersion()}      +");
            Console.WriteLine("+      por Luan Roger        +");
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");

            Console.WriteLine("Jogadores:");
            foreach(string playersShow in args) Console.WriteLine(">" + playersShow);

            Verificardores.VerificarPlayers(args);

            Console.WriteLine("Deseja continuar ? (S/N)");
            if (!Verificardores.VerificarInputChar(Console.ReadKey().KeyChar)) AppManager.ExitProgram();
            Console.Write("\n");

            #region Configure Deck and Players
            Deck starderDeck = new();

            List<Player> players = new();
            foreach (var playerArgs in args)
            {
                Player player = new(playerArgs, starderDeck);
                players.Add(player);
            }
            #endregion

            while (players.Any(p => !p.isWinner))
            {
                int playingNow = 0;
                foreach (var player in players)
                {
                    Console.WriteLine($"Cartas do {player.name}:");
                    foreach (var card in player.hand) card.SeeCard();
                    Console.WriteLine("=====================================");
                    Thread.Sleep(COOLDOWN_SLEEP0);
                }

                while (playingNow != players.Count)
                {
                    Thread.Sleep(COOLDOWN_SLEEP1);
                    Console.WriteLine($"\nÉ a vez de {players[playingNow].name}");
                    Console.WriteLine("Carta de cima:");
                    starderDeck.deckHsitory.Last().SeeCard();
                    Thread.Sleep(COOLDOWN_SLEEP0);

                    short playerMove;
                    while (true)
                    {
                        Console.WriteLine("O que deseja fazer?");
                        Console.WriteLine(
                            "[ 1 ] - Jogar uma carta.   [ 2 ] - Puxar carta.   [ 3 ] - Sair do jogo.   [ 4 ] - Encerrar jogo.");
                        try
                        {
                            playerMove = Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                            break;
                        }
                        catch { Console.WriteLine("Digite um item válido"); }
                    }

                    Thread.Sleep(COOLDOWN_SLEEP0);

                    switch (playerMove)
                    {
                        case 1:
                            while (true)
                            {
                                while (true)
                                {
                                    Console.WriteLine("\nDigite o index da carta que deseja jogar:");
                                    Console.WriteLine("[0] - Puxar uma carta");
                                    for (int c = 1; c != players[playingNow].hand.Count; c++)
                                    {
                                        Console.Write($"[{c}] - ");
                                        players[playingNow].hand[c].SeeCard();

                                        Thread.Sleep(COOLDOWN_SLEEP1);
                                    }

                                    try
                                    {
                                        playerMove = Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                                        break;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Digite um index de uma carta valida");
                                    }
                                }

                                if (playerMove == 0) { players[playingNow].DrawCard(starderDeck, 1); break;} 
                                else if (players[playingNow].PlayCard(playerMove, starderDeck, players, playingNow)) break;

                                Console.WriteLine("Está carta não pode ser jogada.");
                            }

                            break;
                        case 2:
                            while (true)
                            {
                                Console.WriteLine("\nQuantas cartas deseja puxar?");
                                try
                                {
                                    playerMove = Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Digite um número válido.");
                                }
                            }

                            players[playingNow].DrawCard(starderDeck, playerMove);

                            Thread.Sleep(COOLDOWN_SLEEP0);
                            break;
                        case 3:
                            while (true)
                            {
                                Console.WriteLine($"{players[playingNow].name}, deseja realmente sair do jogo? (S/N)");
                                if (!Verificardores.VerificarInputChar(Console.ReadKey().KeyChar))
                                {
                                    Console.WriteLine($"{players[playingNow].name} foi removido do jogo.");
                                    players.RemoveAt(playingNow);
                                    break;
                                }

                                Console.WriteLine("Digite um charater válido");
                            }

                            break;
                        case 4:
                            AppManager.ExitProgram();
                            break;
                    }

                    Verificardores.VerificarVitoria(players);
                    playingNow++;
                }
            }
            Console.WriteLine("Parabés pela vitória!");
        }
    }
}
