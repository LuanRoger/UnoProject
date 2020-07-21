using System;

namespace ErrorNS{
    public class Error{
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
}