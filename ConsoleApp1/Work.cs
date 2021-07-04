using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]
    public class Work
    {
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
        [JsonPropertyName("fileSize")]
        public string FileSize { get; set; }
        [JsonPropertyName("lettersCount")]
        public string LettersCount { get; set; }
        [JsonPropertyName("letters")]
        public List<string> Letters { get; set; } = new List<string>();
        [JsonPropertyName("words")]
        [JsonInclude]
        public Dictionary<string, int> words = new Dictionary<string, int>();

        public void Letter(string str)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = str.GroupBy(x => x).OrderBy(x => x.Key);
            foreach (var item in result)
            {
                string s = item.Key + " = " + item.Count();
                for (int i = 0; i < alphabet.Length; i++)
                    if (item.Key == alphabet[i])
                        Letters.Add(s);
            }
        }
        [JsonPropertyName("wordsCount")]
        public string WordsCount { get; set; }

        public void Word(string str)
        {
            string[] aa = new string[] { };
            aa = Regex.Matches(str, @"[a-zA-Z]+").Cast<Match>().Select(m => m.Value).ToArray();
            foreach (var item in aa)
            {
                words.TryGetValue(item, out int count);
                words[item] = count + 1;
            }
            words = words.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        [JsonPropertyName("linesCount")]
        public string LinesCount { get; set; }
        [JsonPropertyName("digitsCount")]
        public string DigitsCount { get; set; }
        [JsonPropertyName("numbersCount")]
        public string NumbersCount { get; set; }
        [JsonPropertyName("longestWord")]
        public string LongestWord { get; set; }
        [JsonPropertyName("wordsWithHyphen")]
        public string WordsWithHyphen { get; set; }
        [JsonPropertyName("punctuation")]
        public string Punctuation { get; set; }
    }
}
