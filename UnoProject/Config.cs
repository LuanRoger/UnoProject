using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnoProject
{
    public static class Config
    {
        public static void SetConfig() => Console.Title = $"Uno Console por Luan Roger v{GetProgramVersion()}";

            public static Version GetProgramVersion() => Assembly.GetExecutingAssembly().GetName().Version;
    }
}
