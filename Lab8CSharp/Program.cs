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
            //task3();
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
    
    }
}

