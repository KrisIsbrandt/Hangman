using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanMotorola
{
    public static class HangmanArt
    {
        public static void PrintHangmanLogo()
        {
            Console.WriteLine(" _                                               ");
            Console.WriteLine("| |                                              ");
            Console.WriteLine("| | __   __ _ _ __   __ _ _ __ ___   __ _ _ __   ");
            Console.WriteLine("| '_  \\ / _` | '_ \\ / _` | '_ ` _ \\ / _` | '_ \\");
            Console.WriteLine("| | | |  (_| | | | | (_| | | | | | | (_| | | | | ");
            Console.WriteLine("|_| |_|\\__, _|_| |_|\\__, |_| |_| |_|\\__,_|_| |_|");
            Console.WriteLine("                    __/ |                        ");
            Console.WriteLine("                   |___/                         ");
        }
        public static void PrintHangmanFigure(int health)
        {
            switch(health)
            {
                case 0:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine(" / \\  |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 1:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine(" /    |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 2:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|\\  |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 3:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine(" /|   |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                case 4:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("  O   |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
                default:
                    Console.WriteLine("  +---+");
                    Console.WriteLine("  |   |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("      |");
                    Console.WriteLine("=========");
                    break;
            }
        }
    }
}
