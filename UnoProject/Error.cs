using System;

namespace UnoProject
{
    public class Error
    {
        public Error() => Console.ForegroundColor = ConsoleColor.Red;
        public void Error1() 
        {
            Console.WriteLine("Para começar a jogar você precisa pasar o nome dos jogadores como argumento.");
            AppManager.ExitProgram();
        }
        public void Error2()
        {
            Console.WriteLine("São suportados somente 4 jogadores.");
            AppManager.ExitProgram();
        }
        public void Error3() 
        {
            Console.WriteLine("Jogadores insufucientes, minimo 2");
            AppManager.ExitProgram();
        }
        public void Error4()
        {
            Console.WriteLine("Valor inválido.");
            AppManager.ExitProgram();
        }
    }
}
