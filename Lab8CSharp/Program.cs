using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace lab8
{
    internal class Program
    {
        static void Main(string[] args)
        {
             //task1();
             //task2();
           // task3();
           // task4();
          task5();
        }

        public static void task1()
        {
            Regex regex = new Regex(@"\b[0-9A-F][0-9A-F]\.[0-9A-F][0-9A-F]\.[0-9A-F][0-9A-F]\.[0-9A-F][0-9A-F]\b", RegexOptions.IgnoreCase);

            string inputPath = "D:\\C#\\lab8\\data_task1.txt";
            string outputPath = "D:\\C#\\lab8\\result_task1.txt";

            using (FileStream readstream = File.OpenRead(inputPath))
            {
                byte[] b_array = new byte[readstream.Length];
                readstream.Read(b_array, 0, b_array.Length);

                string data_text = Encoding.Default.GetString(b_array);
                Console.WriteLine("Текст з файлу:\n" + data_text);

                MatchCollection matches = regex.Matches(data_text);
                Console.WriteLine("Знайдено IP-адрес: " + matches.Count);

                // Відкриваємо файл один раз, для всіх записів
                using (StreamWriter writer = new StreamWriter(outputPath, false, Encoding.Default))
                {
                    foreach (Match m in matches)
                    {
                        Console.WriteLine(m.Value);
                        writer.WriteLine(m.Value);  // Запис у файл
                    }
                }

                Console.WriteLine("Всі знайдені IP-адреси записані у result_task1.txt");
            }

            Console.ReadKey();
        }

        public static void task2()
        {
            Regex regex = new Regex(@"\b(?![abcde])[a-z]{2,}\b", RegexOptions.IgnoreCase);
            MatchCollection matches;

            string inputPath = "D:\\C#\\lab8\\data_task2.txt";
            string outputPath = "D:\\C#\\lab8\\result_task2.txt";

            using (FileStream readstream = File.OpenRead(inputPath))
            {
                byte[] array = new byte[readstream.Length];
                readstream.Read(array);
                string data_text = Encoding.Default.GetString(array);

                matches = regex.Matches(data_text);



                using (StreamWriter writer = new StreamWriter(outputPath, false, Encoding.Default))
                {
                    foreach (Match m in matches)
                    {
                        Console.WriteLine(m.Value);
                        writer.WriteLine(m.Value);  // Запис у файл
                    }
                }
                Console.WriteLine("Всі знайдені cлова записано в result_task2.txt");
            }

            Console.ReadKey();
        }

        public static void task3()
        {
            string filePath = "D:\\C#\\lab8\\data_task3.txt";
            string text = File.ReadAllText(filePath);
            string[] words = Regex.Split(text, @"\W+");

            string wordWithMaxRepeatingChars = "";
            int maxRepeatingCount = 0;

            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word)) continue;
                var charCount = word.GroupBy(c => c)
                                    .ToDictionary(g => g.Key, g => g.Count());

                
                int maxInWord = charCount.Values.Max();

                
                if (maxInWord > maxRepeatingCount)
                {
                    maxRepeatingCount = maxInWord;
                    wordWithMaxRepeatingChars = word;
                }
            }

            
            Console.WriteLine($"Слово з найбільшою кількістю однакових символів: {wordWithMaxRepeatingChars}");
        }

        public static void task4()
        {
            string filePath = "D:\\C#\\lab8\\data_task4.bin";
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string text = Encoding.UTF8.GetString(fileBytes);
            string[] words = Regex.Split(text, @"\W+");
            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word)) continue;

                // Перевірка умови: перша та остання буква однакові
                if (char.ToLower(word[0]) == char.ToLower(word[word.Length - 1]))
                {
                    Console.WriteLine(word);
                }
            }

        }

        public static void task5()
        {
            string baseDir = @"D:\temp";
            string dir1 = Path.Combine(baseDir, "Chupa_Ilona1");
            string dir2 = Path.Combine(baseDir, "Chupa_Ilona2");

            //Створення папок
            Directory.CreateDirectory(dir1);
            Directory.CreateDirectory(dir2);

            //Створення та запис у файли t1.txt і t2.txt
            string t1Path = Path.Combine(dir1, "t1.txt");
            string t2Path = Path.Combine(dir1, "t2.txt");

            File.WriteAllText(t1Path, "<Чупа Ілона, 2006> року народження, місце проживання м.Чернівці");
            File.WriteAllText(t2Path, "Шевченко Марія 2003 року народження, місце проживання м. Київ");

            //Створення t3.txt у Chupa_Ilona2 і додавання тексту з t1.txt та t2.txt
            string t3Path = Path.Combine(dir2, "t3.txt");
            string contentT1 = File.ReadAllText(t1Path);
            string contentT2 = File.ReadAllText(t2Path);
            File.WriteAllText(t3Path, contentT1 + Environment.NewLine + contentT2);

            //Виведення інформації про t1.txt, t2.txt, t3.txt
            Console.WriteLine("Інформація про створені файли:");
            PrintFileInfo(t1Path);
            PrintFileInfo(t2Path);
            PrintFileInfo(t3Path);

            //Переміщення t2.txt у Chupa_Ilona2
            string t2NewPath = Path.Combine(dir2, "t2.txt");
            File.Move(t2Path, t2NewPath);

            //Копіювання t1.txt у Chupa_Ilona2
            string t1CopyPath = Path.Combine(dir2, "t1.txt");
            File.Copy(t1Path, t1CopyPath, true);

            //Перейменування Chupa_Ilona2 у ALL та видалення Chupa_Ilona1
            string allDir = Path.Combine(baseDir, "ALL");
            Directory.Move(dir2, allDir);
            Directory.Delete(dir1, true); // true = видалити рекурсивно

            //Виведення повної інформації про файли в папці ALL
            Console.WriteLine("\nФайли у папці ALL:");
            foreach (string file in Directory.GetFiles(allDir))
            {
                PrintFileInfo(file);
            }
        }

        static void PrintFileInfo(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            Console.WriteLine($"Файл: {fileInfo.Name}");
            Console.WriteLine($"Розмір: {fileInfo.Length} байт");
            Console.WriteLine($"Дата створення: {fileInfo.CreationTime}");
            Console.WriteLine($"Остання зміна: {fileInfo.LastWriteTime}");
            Console.WriteLine($"Повний шлях: {fileInfo.FullName}");
            Console.WriteLine(new string('-', 40));
        }

    }
}


