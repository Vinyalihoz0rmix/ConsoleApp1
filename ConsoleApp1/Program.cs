using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Work w = new Work();
            Console.WriteLine("Enter filename: ");
            string st = null;
            try
            {
                string str = File.ReadAllText(st = Console.ReadLine(), Encoding.Default);
                w.FileName = st;
                FileInfo file = new FileInfo(st);
                w.FileSize = file.Length.ToString();
                // Количество букв всего без пробелов
                int lettersCount = str.Trim().Where(ch => !char.IsWhiteSpace(ch)).Count();
                Console.WriteLine("Количество букв всего - " + lettersCount);
                w.LettersCount = lettersCount.ToString();
                // Количества каждой буквы
                w.Letter(str);
                string digits = Regex.Replace(str, @"\D", "");
                Console.WriteLine("Количество цифр - " + digits.Length);
                w.DigitsCount = digits.Length.ToString();
                int number = Regex.Matches(str, @"\d+").Count;
                Console.WriteLine("Количество чисел - " + number);
                w.NumbersCount = number.ToString();
                int wc = Regex.Matches(str, @"[a-zA-Z]+").Count;
                Console.WriteLine("Количество слов всего - " + wc);
                w.WordsCount = wc.ToString();
                w.Word(str);
                var lines = 0;
                using (var reader = File.OpenText(st))
                {
                    while (reader.ReadLine() != null)
                    {
                        lines++;
                    }
                }
                Console.WriteLine("Количество строк - " + lines);
                w.LinesCount = lines.ToString();
                int hyphen = Regex.Matches(str, @"([A-zА-я]+[\-][A-zА-я]+)").Count;
                Console.WriteLine("Количество слов с дефисом - " + hyphen);
                w.WordsWithHyphen = hyphen.ToString();
                int p = Regex.Matches(str, @"[?()!.,;:""'']").Count;
                Console.WriteLine("Количество знаков препинания - " + p);
                w.Punctuation = p.ToString();
                string[] a = new string[] { };
                string longestWord = string.Empty;
                a = Regex.Matches(str, @"[a-zA-Z]+").Cast<Match>().Select(m => m.Value).ToArray();
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].Length > longestWord.Length)
                        longestWord = a[i];
                }
                Console.WriteLine("Самое длинное слово - " + longestWord);
                w.LongestWord = longestWord;
                // Сериализация
                // var json = JsonConvert.SerializeObject(w, Formatting.Indented);
                // Console.WriteLine(json);
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                string json = System.Text.Json.JsonSerializer.Serialize(w, options);
                File.WriteAllText("serialized.json", json);
                Console.WriteLine(json);
                Console.WriteLine("Сохранено!");

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

    }
}
