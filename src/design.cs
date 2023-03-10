/* 
    design.cs - Made by Taran Nagra
    ---
    This is the file to make the designing
    in the program. It's quite cool :).
*/

using System;
using System.Text;

namespace Design {
    public class Design {
        public static void welcome_screen() {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t██╗  ██╗██╗   ██╗██████╗ ███████╗██████╗ ██╗ ██████╗ ███╗   ██╗");
            Console.WriteLine("\t██║  ██║╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██║██╔═══██╗████╗  ██║");
            Console.WriteLine("\t███████║ ╚████╔╝ ██████╔╝█████╗  ██████╔╝██║██║   ██║██╔██╗ ██║");
            Console.WriteLine("\t██╔══██║  ╚██╔╝  ██╔═══╝ ██╔══╝  ██╔══██╗██║██║   ██║██║╚██╗██║");
            Console.WriteLine("\t██║  ██║   ██║   ██║     ███████╗██║  ██║██║╚██████╔╝██║ ╚████║");
            Console.WriteLine("\t╚═╝  ╚═╝   ╚═╝   ╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝ ╚═════╝ ╚═╝  ╚═══╝");
            Console.ResetColor();

            Console.Write("\t\t\tWelcome to ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Hyperion ");
            Console.ResetColor();
            Console.WriteLine(":)");
        }
    }
}