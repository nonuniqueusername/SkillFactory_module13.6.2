using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Введи путь к файлу:");
        string path = Console.ReadLine();
        var wordCount = CountWords(ReadFile(path));
        PrintTopUsableWords(wordCount, 10);
    }

    private static void PrintTopUsableWords(Dictionary<string,int> wordDict, int top)
    {
        Dictionary<string, int> SortedDict = wordDict.OrderByDescending(word => word.Value)
            .Take(top)
            .ToDictionary(pair=>pair.Key, pair=>pair.Value);

        foreach (var pair in SortedDict)
        {
            Console.WriteLine($"Слово \"{pair.Key}\" встречается {pair.Value} раз");
        }
    }

    private static string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }

    private static Dictionary<string, int> CountWords(string text)
    {
        Dictionary<string, int> wordCount = new Dictionary<string, int>();
        string[] delimiters = { " ", "\t", "\r", "\n" };
        string[] words = RemoveSpecialCharacters(text).Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in words)
        {
            if (wordCount.ContainsKey(word))
            {
                wordCount[word] += 1;
            }
            else
            {
                wordCount[word] = 1;
            }
        }
        return wordCount;
    }

    private static string RemoveSpecialCharacters(string input)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in input)
        {
            if (!char.IsPunctuation(c))
            {
                stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString();
    }
}