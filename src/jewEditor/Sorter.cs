using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace jewEditor
{
    internal class Sorter
    {
        public static void Start()
        {
            Console.Clear();
            Console.Title = $"jewEditor | v{Globals.Version} | Domain Sorter";
            Messages.Logo();
            Messages.PrintWithPrefix("Input", "Please choose a file.", "Crimson");
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Choose a text file!";
            file.Filter = "Text Files|*.txt";
            file.FilterIndex = 2;
            file.RestoreDirectory = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                string path = file.FileName;
                Globals.Lines = File.ReadLines(path).ToList();
            }
            else
            {
                Program.Menu();
            }

            Console.Clear();
            Messages.Logo();
            Messages.PrintWithPrefix("Input", "Please choose the folder you want the files in.", "Crimson");

            using (var fdb = new FolderBrowserDialog())
            {
                DialogResult result = fdb.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrEmpty(fdb.SelectedPath))
                {
                    _folder = fdb.SelectedPath;
                }
                else
                {
                    Program.Menu();
                }
            }

            ProcessInfo();
        }

        private static void ProcessInfo()
        {
            Console.Clear();
            Messages.Logo();
            Messages.PrintWithPrefix("Info", $"Loaded {Globals.Lines.Count} lines from the file!", "Crimson");
            Messages.PrintWithPrefix("Continue", "Press any key to continue.", "Crimson");
            Console.ReadKey();
            Process();
        }

        private static void Process()
        {
            Console.Clear();
            Messages.Logo();
            Messages.PrintWithPrefix("Sorting Domains", $"[{_sortedLines} / {Globals.Lines.Count}]", "Crimson");

            Timer aTimer = new Timer();
            aTimer.Interval = 750;
            aTimer.Elapsed += TimerHandle;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            foreach (string line in Globals.Lines)
            {
                var value = new Regex("@([^:]+)").Match(line.Replace(";", ":").Replace("|", ":")).Value;
                try
                {
                    lock (Locked)
                    {
                        using (FileStream fileStream = File.Open($"{_folder}\\{value}.txt", FileMode.Append))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream))
                            {
                                streamWriter.WriteLine(line);
                                _sortedLines++;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Console.WriteLine("exp");
                }
            }

            aTimer.Enabled = false;
            Globals.Lines.Clear();

            Done();
        }

        private static void TimerHandle(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            Messages.Logo();
            Messages.PrintWithPrefix("Sorting Domains", $"[{_sortedLines} / {Globals.Lines.Count}]", "Crimson");
        }

        private static void Done()
        {
            Console.Clear();
            Globals.Lines.Clear();
            Messages.Logo();
            Messages.PrintWithPrefix("Info", $"Saved all the files! File location: {_folder}", "Crimson");
            Messages.PrintWithPrefix("Done", "Press any key to go back to the menu.", "Crimson");
            Console.ReadKey();
            Program.Menu();
        }

        private static string _folder;
        private static int _sortedLines;
        private static readonly object Locked = new object();
    }
}