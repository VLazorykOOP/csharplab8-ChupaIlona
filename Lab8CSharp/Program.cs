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
            // task1();
            // task2();
            //task3();
            task4();
           // task5();
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
            bool IsPalindrome(string input)
            {
                var cleaned = new string(input.Where(c => char.IsLetterOrDigit(c)).ToArray()).ToLower();
                var reversed = new string(cleaned.Reverse().ToArray());
                return cleaned == reversed;
            }

            string inputPath = "D:\\C#\\lab8\\data_task3.txt";
            string outputPath = "D:\\C#\\lab8\\result_task3.txt";

            // Зчитуємо весь файл
            string data_text = File.ReadAllText(inputPath);

            // Розділяємо текст за пробілами та комами
            string[] split_data_text = data_text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Список для збереження знайдених паліндромів
            List<string> result = new List<string>();

            // Перевіряємо кожне слово на паліндромність
            foreach (string s in split_data_text)
            {
                if (IsPalindrome(s))
                {
                    result.Add(s);
                }
            }

            // Записуємо знайдені паліндроми в файл
            using (StreamWriter writer = new StreamWriter(outputPath, false, Encoding.Default))
            {
                foreach (string palindrome in result)
                {
                    writer.WriteLine(palindrome);
                    Console.WriteLine(palindrome);  // Виведення на консоль
                }
            }

            Console.WriteLine("Всі знайдені слова паліндроми записано в result_task3.txt");
        }

        public static void task4()
        {
            string inputPath = "D:\\C#\\lab8\\data_task4.txt";
            List<String> result = new List<string>();

            Console.Write("Введіть букву: ");
            char letter = Char.Parse(Console.ReadLine());

            using (FileStream readstream = File.OpenRead(inputPath))
            {
                byte[] array = new byte[readstream.Length];
                readstream.Read(array);
                string data_text = Encoding.Default.GetString(array);

                string [] data_text_array = data_text.Split(' ');
                
                foreach(string s in data_text_array)
                {
                    if(!string.IsNullOrEmpty(s) && s.StartsWith(letter))
                    {
                        result.Add(s);
                    }
                }
            }

            foreach(string s in result)
            {
                Console.Write(s+" ");
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
