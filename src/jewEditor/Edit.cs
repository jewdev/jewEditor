using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace jewEditor
{
    internal class Edit
    {
        public static void Start()
        {
            Console.Clear();
            Console.Title = $"jewEditor | v{Globals.Version} | Edit";
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
            Messages.PrintWithPrefix("Warning", "Don't edit the combolist before fixing it, it could CRASH the program!", "Yellow");
            Console.WriteLine();
            Messages.PrintWithPrefix("Info", $"Loaded {Globals.Lines.Count} lines from the file!", "Crimson");
            Messages.PrintWithPrefix("Continue", "Press any key to continue.", "Crimson");
            Console.ReadKey();
            Process();
        }

        //TODO: Optimize the code, set splits to 2 variables instead of 2 EACH function.
        private static void Process()
        {
            Console.Clear();
            //Messages.PrintWithPrefix("Process", "I'm working on it! (If the file's size is BIG it will take more time)", "Crimson");
            Messages.Logo();
            Messages.PrintWithPrefix("Editing", $"[{Globals.NewLines.Count} / {Globals.Lines.Count * 9}]", "Crimson");

            Timer aTimer = new Timer();
            aTimer.Interval = 750;
            aTimer.Elapsed += TimerHandle;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            foreach (string line in Globals.Lines)
            {
                string[] splitLine = line.Split(':');

                // email:Pass
                string result1 = Regex.Replace(splitLine[1], "^[a-z]", m => m.Value.ToUpper());
                Globals.NewLines.Add(splitLine[0] + ":" + result1);

                // email:pass!
                string result2 = splitLine[1] + "!";
                Globals.NewLines.Add(splitLine[0] + ":" + result2);

                // email:Pass!
                string result3Regex = Regex.Replace(splitLine[1], "^[a-z]", m => m.Value.ToUpper());
                string result3 = result3Regex + "!";
                Globals.NewLines.Add(splitLine[0] + ":" + result3);

                // email:pass1
                string result4 = splitLine[1] + "1";
                Globals.NewLines.Add(splitLine[0] + ":" + result4);

                // email:Pass1
                string result5Regex = Regex.Replace(splitLine[1], "^[a-z]", m => m.Value.ToUpper());
                string result5 = result5Regex + "1";
                Globals.NewLines.Add(splitLine[0] + ":" + result5);

                // email:pass123
                string result6 = splitLine[1] + "123";
                Globals.NewLines.Add(splitLine[0] + ":" + result6);

                // email:Pass123
                string result7Regex = Regex.Replace(splitLine[1], "^[a-z]", m => m.Value.ToUpper());
                string result7 = result7Regex + "123";
                Globals.NewLines.Add(splitLine[0] + ":" + result7);

                // email(passnum):pass(num)
                string takeNumbersFromPw = Regex.Replace(splitLine[1], @"[^\d]", "");
                string[] splitEmailLine1 = splitLine[0].Split('@');
                string emailResult = splitEmailLine1[0] + takeNumbersFromPw;
                Globals.NewLines.Add(emailResult + "@" + splitEmailLine1[1] + ":" + splitLine[1]);

                // email(num):pass(emailnum)
                string[] splitEmailLine2 = splitLine[0].Split('@');
                string takeNumbersFromEmail = Regex.Replace(splitEmailLine2[0], @"[^\d]", "");
                string passResult = splitLine[1] + takeNumbersFromEmail;

                Globals.NewLines.Add(splitLine[0] + ":" + passResult);
            }

            aTimer.Enabled = false;

            Globals.Lines.Clear();
            RemovedDuplicates();
        }

        private static void TimerHandle(Object source, ElapsedEventArgs e)
        {
            Console.Clear();
            Messages.PrintWithPrefix("Editing", $"[{Globals.NewLines.Count} / {Globals.Lines.Count * 9}]", "Crimson");
        }

        private static void RemovedDuplicates()
        {
            Console.Clear();
            Messages.PrintWithPrefix("Question", "Would you like to remove duplicates in the combolist? (y / n)", "Crimson");

            string removeAnswer = Console.ReadLine();

            if (removeAnswer != null && removeAnswer.StartsWith("Y".ToLower()))
            {
                Console.Clear();
                Messages.PrintWithPrefix("Process", "Removing duplicates...", "Crimson");

                var removedLines = Globals.NewLines.Distinct().ToList();
                Globals.NewLines.Clear();
                foreach (string line in removedLines)
                {
                    Globals.NewLines.Add(line);
                }
                Randomize();
            }
            else
            {
                Randomize();
            }
        }

        private static void Randomize()
        {
            Console.Clear();
            Messages.PrintWithPrefix("Question", "Would you like to randomize the combolist? (y / n)", "Crimson");
            string randomizeAnswer = Console.ReadLine();

            if (randomizeAnswer != null && randomizeAnswer.StartsWith("Y".ToLower()))
            {
                Console.Clear();
                Messages.PrintWithPrefix("Process", "Randomizing Lines...", "Crimson");
                var randomizedList = Globals.NewLines.OrderBy(a => Guid.NewGuid()).ToList();
                Globals.NewLines.Clear();
                foreach (string line in randomizedList)
                {
                    Globals.NewLines.Add(line);
                }
                SaveToFile();
            }
            else
            {
                SaveToFile();
            }
        }

        private static void SaveToFile()
        {
            Console.Clear();
            SaveFileDialog saveFile = new SaveFileDialog
            {
                FileName = $"jewEditor - Edit - {Globals.NewLines.Count} Lines.txt",
                Filter = "Text Files|*.txt"
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName))
                {
                    foreach (string line in Globals.NewLines)
                    {
                        sw.WriteLine(line);
                    }
                }

                Globals.NewLines.Clear();
            }
            else
            {
                Program.Menu();
            }

            Console.Clear();
            Messages.PrintWithPrefix("Info", $"Saved the file! File location: {saveFile.FileName}", "Crimson");
            Messages.PrintWithPrefix("Done", "Press any key to go back to the menu.", "Crimson");
            Console.ReadKey();
            Program.Menu();
        }
    }
}