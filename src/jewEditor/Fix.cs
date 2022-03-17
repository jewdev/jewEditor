using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace jewEditor
{
    internal class Fix
    {
        public static void Start()
        {
            Console.Clear();
            Console.Title = string.Format("jewEditor | v{0} | Fix", Globals.Version);
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
            Messages.PrintWithPrefix("Process", "I'm working on it! (If the file's size is BIG it will take more time)", "Crimson");

            Messages.PrintWithPrefix("Process", "Fixing Lines...", "Crimson");
            foreach (string line in Globals.Lines)
            {
                if (!line.Contains(" ") && line.Contains("@") && line.Contains(":"))
                {
                    string[] splitLine = line.Split(':');
                    if (splitLine[0].Contains("@"))
                    {
                        _fixedLines.Add(line);
                    }
                }
            }

            Messages.PrintWithPrefix("Process", "Removing Duplicates...", "Crimson");
            _fixedLines = _fixedLines.Distinct().ToList();

            Globals.Lines.Clear();
            SaveToFile();
        }

        private static void SaveToFile()
        {
            Console.Clear();
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = $"jewEditor - Fix - {_fixedLines.Count} Lines.txt";
            saveFile.Filter = "Text Files|*.txt";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName))
                {
                    foreach (string line in _fixedLines)
                    {
                        sw.WriteLine(line);
                    }
                }

                _fixedLines.Clear();
            }
            else
            {
                Program.Menu();
            }

            Console.Clear();
            Messages.Logo();
            Messages.PrintWithPrefix("Info", $"Saved the file! File location: {saveFile.FileName}", "Crimson");
            Messages.PrintWithPrefix("Done", "Press any key to go back to the menu.", "Crimson");
            Console.ReadKey();
            Program.Menu();
        }

        private static List<string> _fixedLines = new List<string>();
    }
}