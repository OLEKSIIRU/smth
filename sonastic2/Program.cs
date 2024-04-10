using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TARgv23_C_põhikonstruktsioonid
{
    public class Funktisioonid
    {
        public static Dictionary<string, string> FailistToDict(string f)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                string filePath = f; // Прямое указание пути к файлу
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Файл {f} не найден.");
                    return dictionary;
                }

                string[] lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
                foreach (string line in lines)
                {
                    string[] parts = line.Trim().Split('-');
                    if (parts.Length == 2)
                    {
                        string word = parts[0].Trim();
                        string translation = parts[1].Trim();
                        dictionary[word] = translation;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
            return dictionary;
        }

        public static void AddWord(Dictionary<string, string> dictionary, string word, string translation)
        {
            dictionary[word] = translation;
            Console.WriteLine("Слово успешно добавлено в словарь.");
        }

        public static void EditWord(Dictionary<string, string> dictionary, string word, string newTranslation)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary[word] = newTranslation;
                Console.WriteLine("Перевод слова успешно отредактирован.");
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }

        public static void RemoveWord(Dictionary<string, string> dictionary, string word)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary.Remove(word);
                Console.WriteLine("Слово успешно удалено из словаря.");
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }

        public static void Translate(Dictionary<string, string> dictionary, string word)
        {
            if (dictionary.ContainsKey(word))
            {
                Console.WriteLine($"{word}: {dictionary[word]}");
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }

        public static void TestKnowledge(Dictionary<string, string> dictionary)
        {
            int score = 0;
            int totalWords = Math.Min(dictionary.Count, 5);
            if (totalWords == 0)
            {
                Console.WriteLine("Словарь пустой. Невозможно провести тест.");
                return;
            }

            List<string> wordsToTest = new List<string>(dictionary.Keys);
            Random random = new Random();
            wordsToTest = new List<string>(wordsToTest.OrderBy(_ => random.Next()));
            wordsToTest = wordsToTest.GetRange(0, totalWords);

            foreach (string word in wordsToTest)
            {
                Console.WriteLine($"Переведите слово '{word}': ");
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == dictionary[word].ToLower())
                {
                    Console.WriteLine("Правильно!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Неправильно. Правильный перевод: {dictionary[word]}");
                }
            }

            Console.WriteLine($"Вы получили {score}/{totalWords} баллов. ({(score * 100.0 / totalWords):F2}%)");
        }
    }

    public class ClassMain
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Dictionary<string, string> rusDict = Funktisioonid.FailistToDict(@"C:\Users\Пользователь\source\repos\sonastic2\sonastic2\rus.txt");
            Dictionary<string, string> estDict = Funktisioonid.FailistToDict(@"C:\Users\Пользователь\source\repos\sonastic2\sonastic2\est.txt");

            Console.WriteLine("Словарь с русского на эстонский: " + string.Join(", ", rusDict));
            Console.WriteLine("Словарь с эстонского на русский: " + string.Join(", ", estDict));

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1: Перевод слова");
                Console.WriteLine("2: Добавить слово в словарь");
                Console.WriteLine("3: Редактировать перевод слова");
                Console.WriteLine("4: Удалить слово из словаря");
                Console.WriteLine("5: Проверить знание слов");
                Console.WriteLine("0: Выйти из программы");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Выберите язык (rus/est): ");
                    string language = Console.ReadLine().ToLower();
                    Console.Write("Введите слово: ");
                    string word = Console.ReadLine();
                    if (language == "rus")
                    {
                        Funktisioonid.Translate(rusDict, word);
                    }
                    else if (language == "est")
                    {
                        Funktisioonid.Translate(estDict, word);
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор языка.");
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Выберите язык (rus/est): ");
                    string language = Console.ReadLine().ToLower();
                    Console.Write("Введите слово: ");
                    string word = Console.ReadLine();
                    Console.Write("Введите перевод: ");
                    string translation = Console.ReadLine();
                    if (language == "rus")
                    {
                        Funktisioonid.AddWord(rusDict, word, translation);
                    }
                    else if (language == "est")
                    {
                        Funktisioonid.AddWord(estDict, word, translation);
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор языка.");
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Выберите язык (rus/est): ");
                    string language = Console.ReadLine().ToLower();
                    Console.Write("Введите слово: ");
                    string word = Console.ReadLine();
                    Console.Write("Введите новый перевод: ");
                    string newTranslation = Console.ReadLine();
                    if (language == "rus")
                    {
                        Funktisioonid.EditWord(rusDict, word, newTranslation);
                    }
                    else if (language == "est")
                    {
                        Funktisioonid.EditWord(estDict, word, newTranslation);
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор языка.");
                    }
                }
                else if (choice == "4")
                {
                    Console.Write("Выберите язык (rus/est): ");
                    string language = Console.ReadLine().ToLower();
                    Console.Write("Введите слово: ");
                    string word = Console.ReadLine();
                    if (language == "rus")
                    {
                        Funktisioonid.RemoveWord(rusDict, word);
                    }
                    else if (language == "est")
                    {
                        Funktisioonid.RemoveWord(estDict, word);
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор языка.");
                    }
                }
                else if (choice == "5")
                {
                    Console.Write("Выберите язык для тестирования (rus/est): ");
                    string testLanguage = Console.ReadLine().ToLower();
                    if (testLanguage == "rus")
                    {
                        Funktisioonid.TestKnowledge(rusDict);
                    }
                    else if (testLanguage == "est")
                    {
                        Funktisioonid.TestKnowledge(estDict);
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор языка.");
                    }
                }
                else if (choice == "0")
                {
                    Console.WriteLine("Программа завершена.");
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                }
            }
        }
    }
}
