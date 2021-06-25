using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangmanMotorola
{
    class Program
    {
        readonly static Random random = new();

        const string HighScoreFilePath = "../../../highscore.txt";
        const string HighScoreHeader = "name | date | guessing_time | guessing_tries | guessed_word";
        const string DataFilePath = "../../../countries_and_capitals.txt";
        public const string DataDelimiter = " | ";
        const string HintFormat = "The capital of ";
        public const string DateTimeFormat = "dd/MM/yyyy";
        const int InitialHealth = 5;
        const int NumberOfHighscores = 10;

        static List<Score> highScoreList = InitHighScore();
        static int health = InitialHealth;
        static int guessCount = 0;
        static int gameTime = 0;
        static DateTime startTime;
        static DateTime endTime;
        static Dictionary<string, string> gameDictionary = InitGameDictionary();
        static KeyValuePair<string, string> target = GetRandomKeyValuePair(gameDictionary);
        static string targetWord = target.Value.ToLower();
        static string targetHint = target.Key;
        static string guess = "";
        static string guessType = "";
        static HashSet<char> goodGuesses = new HashSet<char>();
        static HashSet<char> badGuesses = new HashSet<char>();
        static bool correctGuess = false;
        static void Main(string[] args)
        {
            while (true)
            {
                startTime = DateTime.Now.ToUniversalTime();
                do
                {
                    Console.Clear();
                    HangmanArt.PrintHangmanLogo();
                    HangmanArt.PrintHangmanFigure(health);
                    PrintTargetWord();
                    ShowCurrentHealth();
                    ShowHint();
                    ShowBadGusses();
                    ReadPlayerGuess();
                    CheckGuess();
                }
                while (!IsGameOver());
                
                endTime = DateTime.Now.ToUniversalTime();
                gameTime = endTime.Subtract(startTime).Seconds;
                PrintGameResult();
                
                // save highscore
                if (correctGuess && AskTrueOrFalseQuestion("Do you want to save your score?"))
                {
                    Console.WriteLine("What is your name?");
                    string name = Console.ReadLine();
                    SaveScore(name);
                }

                // print highscores
                PrintHighscoreList();

                // replay
                if (AskTrueOrFalseQuestion("Do you want to play again?"))
                {
                    PrepareNewGame();
                    continue;
                } 
                else
                {
                    Console.WriteLine("Thank you for the time spent together :) \nTerminating game.");
                    break;
                }
            }
        }
        static void PrintTargetWord()
        {
            StringBuilder builder = new StringBuilder(targetWord.Length);
            for (int i = 0; i < targetWord.Length; i++)
            {
                if (goodGuesses.Contains(targetWord[i]))
                {
                    builder.Append(targetWord[i]);
                }
                else
                {
                    if (targetWord[i] == ' ')
                    {
                        builder.Append(" ");
                    }
                    else
                    {
                        builder.Append("_");
                    }
                }
                builder.Append(" ");
            }
            Console.WriteLine("\n" + builder.ToString() + "\n");
        }
        static void ReadPlayerGuess()
        {
            while (true)
            {
                Console.WriteLine("\nDo you wish to guess a letter or whole word? (L - letter; W - Word)");
                guessType = Console.ReadLine().Trim().ToLower();
                if (guessType == "l")
                {
                    Console.Write("Guess letter: ");
                    guess = Console.ReadLine().Trim().ToLower();
                    if (guess.Length != 1)
                    {
                        Console.WriteLine("Invalid input. Expected a letter.");
                        continue;
                    }
                    if (goodGuesses.Contains(char.Parse(guess)) ||
                        badGuesses.Contains(char.Parse(guess)))
                    {
                        Console.WriteLine("You've already tried this letter. Try again.");
                        continue;
                    }
                    break;
                }
                else if (guessType == "w")
                {
                    Console.Write("Guess word: ");
                    guess = Console.ReadLine().Trim().ToLower();
                    break;

                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }
        static void ShowHint()
        {
            if (IsLastChance())
            {
                Console.WriteLine("Hint:" + HintFormat + targetHint);
            }
        }
        static void ShowCurrentHealth()
        {
            Console.WriteLine(string.Format("\nYou have {0} chances left.", health));
        }
        static void ShowBadGusses()
        {
            if (badGuesses.Count > 0)
            {
                Console.WriteLine("Wrong guesses: " + string.Join(", ", badGuesses.ToArray()));
            }
        }
        static void CheckGuess()
        {
            guessCount++;

            if (guessType == "l")
            {
                if (IsCorrectLetter(guess))
                {
                    goodGuesses.Add(char.Parse(guess));
                    if (HasGuessedAllLetters())
                    {
                        correctGuess = true;
                    }
                } 
                else
                {
                    badGuesses.Add(char.Parse(guess));
                    health -= 1;
                }
            } 
            else if (guessType == "w")
            {
                if (IsCorrectWord(guess))
                {
                    correctGuess = true;
                } else
                {
                    health -= 2;
                }
            }
        }
        static bool HasGuessedAllLetters()
        {
            HashSet<char> targetWordChars = new HashSet<char>();
            foreach (char c in targetWord.ToCharArray())
            {
                if (c == ' ') continue;
                targetWordChars.Add(c);
            }
            return goodGuesses.SetEquals(targetWordChars);
        }
        static bool IsLastChance()
        {
            return health == 1;
        }
        static bool IsCorrectWord(string guess) 
        {
            return guess.Trim().ToLower() == targetWord.Trim().ToLower();
        }
        static bool IsCorrectLetter(string guess)
        {
            return targetWord.Contains(guess);
        }
        static bool IsGameOver()
        {
            return health <= 0 || correctGuess;
        }
        static void PrintGameResult()
        {
            if (correctGuess)
            {
                Console.WriteLine("You won!!!");
                Console.WriteLine("Your guessed: " + targetWord);
                Console.WriteLine("You guessed after " + guessCount + ". It took you " + gameTime + " seconds.");

            }
            else
            {
                Console.Clear();
                HangmanArt.PrintHangmanLogo();
                HangmanArt.PrintHangmanFigure(0);
                Console.WriteLine("You Lost :(");
                Console.WriteLine("Correct answer was: " + targetWord);
            }
        }
        static bool AskTrueOrFalseQuestion(string question)
        {
            bool answer;
            while (true)
            {
                Console.WriteLine(question + " [Y/N]");
                string response = Console.ReadLine().Trim().ToLower();
                if (response == "y")
                {
                    answer = true;
                    break;
                }
                else if (response == "n")
                {
                    answer = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
            }
            return answer;
        }
        static void PrepareNewGame()
        {
            health = InitialHealth;
            target = GetRandomKeyValuePair(gameDictionary);
            targetWord = target.Value.ToLower();
            targetHint = target.Key;
            guessCount = 0;
            correctGuess = false;
            goodGuesses.Clear();
            badGuesses.Clear();
        }
        static Dictionary<string, string> InitGameDictionary()
        {
            Dictionary<string, string> gameDictionary = new Dictionary<string, string>();
            List<string> list = ReadFromFileAsList(DataFilePath);
            
            foreach (string line in list)
            {
                string[] pair = line.Split(DataDelimiter);
                gameDictionary.Add(pair[0], pair[1]);
            }

            return gameDictionary;
        }
        static KeyValuePair<string, string> GetRandomKeyValuePair(Dictionary<string, string> dictionary)
        {
            return dictionary.ElementAt(random.Next(dictionary.Count));
        }
        static List<Score> InitHighScore()
        {
            List<Score> highScoreList = new List<Score>();

            if (File.Exists(HighScoreFilePath))
            {
                List<string> recordList = ReadFromFileAsList(HighScoreFilePath);
                foreach (string record in recordList.Skip(1))
                {
                    highScoreList.Add(new Score(record));
                }
            }
            else
            {
                StreamWriter sw = new StreamWriter(HighScoreFilePath);
                sw.WriteLine(HighScoreHeader);
                sw.Close();
            }

            return highScoreList;
        }
        static void SaveScore(string name)
        {
            Score score = new Score(name, endTime, gameTime, guessCount, targetWord);
            highScoreList.Add(score);
            SaveHighScoreListToFile();
        }
        static void SaveHighScoreListToFile()
        {
            StreamWriter sw = new StreamWriter(HighScoreFilePath);
            sw.WriteLine(HighScoreHeader);
            foreach (Score score in highScoreList
                .OrderBy(s => s.GuessCount)
                .ThenBy(s => s.GameTime)
                .Take(NumberOfHighscores)) sw.WriteLine(score.ToString());
            sw.Close();
        }
        static void PrintHighscoreList()
        {
            if (highScoreList.Count() != 0)
            {
            Console.WriteLine(string.Format("\nTop {0} scores", NumberOfHighscores));
            Console.WriteLine(HighScoreHeader);
            foreach (Score score in highScoreList
                .OrderBy(s => s.GuessCount)
                .ThenBy(s => s.GameTime)
                .Take(NumberOfHighscores)) Console.WriteLine(score);
            }
        }
        static List<string> ReadFromFileAsList(string path) 
        {
            try
            {
            StreamReader sr = new StreamReader(path);
            List<string> list = new List<string>();
            string line = sr.ReadLine();
           
            while (line != null)
            {
                list.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();

            return list;

            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Application will close in 5 secods.");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
            return null;
        }
    }
}
