using Pastel;
using System;
using System.Drawing;
using System.Text;

namespace jewEditor
{
    internal class Messages
    {
        public static void Logo()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.OutputEncoding = Encoding.Unicode;

            string[] logoList = 
            {
                " ▄▄▄██▀▀▀▓█████  █     █░▓█████ ▓█████▄  ██▓▄▄▄█████▓ ▒█████   ██▀███  ",
                "   ▒██   ▓█   ▀ ▓█░ █ ░█░▓█   ▀ ▒██▀ ██▌▓██▒▓  ██▒ ▓▒▒██▒  ██▒▓██ ▒ ██▒",
                "   ░██   ▒███   ▒█░ █ ░█ ▒███   ░██   █▌▒██▒▒ ▓██░ ▒░▒██░  ██▒▓██ ░▄█ ▒",
                "▓██▄██▓  ▒▓█  ▄ ░█░ █ ░█ ▒▓█  ▄ ░▓█▄   ▌░██░░ ▓██▓ ░ ▒██   ██░▒██▀▀█▄  ",
                " ▓███▒   ░▒████▒░░██▒██▓ ░▒████▒░▒████▓ ░██░  ▒██▒ ░ ░ ████▓▒░░██▓ ▒██▒",
                " ▒▓▒▒░   ░░ ▒░ ░░ ▓░▒ ▒  ░░ ▒░ ░ ▒▒▓  ▒ ░▓    ▒ ░░   ░ ▒░▒░▒░ ░ ▒▓ ░▒▓░",
                " ▒ ░▒░    ░ ░  ░  ▒ ░ ░   ░ ░  ░ ░ ▒  ▒  ▒ ░    ░      ░ ▒ ▒░   ░▒ ░ ▒░",
                " ░ ░ ░      ░     ░   ░     ░    ░ ░  ░  ▒ ░  ░      ░ ░ ░ ▒    ░░   ░ ",
                " ░   ░      ░  ░    ░       ░  ░   ░     ░               ░ ░     ░     ",
                "                                 ░                                     ",
                "◄ Coded by jewdev ►"
            };
            int red = 255;
            int green = 40;
            int blue = 90;
            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - logoList[i].Length) / 2, Console.CursorTop);
                Console.WriteLine(logoList[i].Pastel(Color.FromArgb(red, green, blue)));
                red -= 8;
                blue -= 9;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.ResetColor();
        }

        public static void PrintWithPrefix(string prefix, string msg, string color)
        {
            object locked = Locked;
            object obj = locked;
            lock (obj)
            {
                Console.Write("[".Pastel(Color.White));
                Console.Write(prefix.Pastel(Color.FromName(color)));
                Console.WriteLine(("] " + msg).Pastel(Color.White));
            }
        }

        public static void WritePrintWithPrefix(string prefix, string msg, string color)
        {
            object locked = Locked;
            lock (locked)
            {
                Console.Write("[".Pastel(Color.White));
                Console.Write(prefix.Pastel(Color.FromName(color)));
                Console.Write(("] " + msg).Pastel(Color.White));
            }
        }

        public static void Print(string msg)
        {
            object locked = Locked;
            lock (locked)
            {
                Console.WriteLine(msg.Pastel(Color.White));
            }
        }

        private static readonly object Locked = new object();
    }
}