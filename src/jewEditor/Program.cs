using System;
using System.Windows.Forms;

namespace jewEditor
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            Welcome();
        }

        public static void Welcome()
        {
            Console.Clear();
            Console.Title = $"jewEditor | v{Globals.Version}";
            Messages.Logo();
            Messages.PrintWithPrefix("Info", "Press any key to continue.", "Crimson");
            Console.ReadKey();
            Menu();
        }

        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.Title = $"jewEditor | v{Globals.Version} | Menu";
                Messages.Logo();
                Messages.PrintWithPrefix("1", "Edit", "Crimson");
                Messages.PrintWithPrefix("2", "Fix", "Crimson");
                Messages.PrintWithPrefix("3", "Domain Sorter", "Crimson");
                Console.WriteLine();
                Messages.PrintWithPrefix("0", "Exit", "Crimson");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Edit.Start();
                        break;

                    case "2":
                        Fix.Start();
                        break;

                    case "3":
                        Sorter.Start();
                        break;

                    case "0":
                        DialogResult askForFixDialog = MessageBox.Show("Do you really want to leave me?", "Exit? (ಥ﹏ಥ)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (askForFixDialog == DialogResult.Yes)
                            Environment.Exit(1);
                        else
                            continue;
                        break;
                }

                break;
            }
        }
    }
}