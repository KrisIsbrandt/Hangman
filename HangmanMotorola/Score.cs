using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanMotorola
{
    class Score
    {
        public string PlayerName { get; set; }
        public DateTime GameDate { get; set; }
        public int GameTime { get; set; }
        public int GuessCount { get; set; }
        public string TargetWord  { get; set; }

        public Score(string playerName, DateTime gameDate, int gameTime, int guessCount, string targetWord)
        {
            PlayerName = playerName;
            GameDate = gameDate;
            GameTime = gameTime;
            GuessCount = guessCount;
            TargetWord = targetWord;
        }

        public Score(string record)
        {
            string[] arr = record.Split(Program.DataDelimiter);
            PlayerName = arr[0];
            GameDate = DateTime.Parse(arr[1]);
            GameTime = Int32.Parse(arr[2]);
            GuessCount = Int32.Parse(arr[3]);
            TargetWord = arr[4];
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(PlayerName).Append(Program.DataDelimiter)
                .Append(GameDate.ToString(Program.DateTimeFormat)).Append(Program.DataDelimiter)
                .Append(GameTime).Append(Program.DataDelimiter)
                .Append(GuessCount).Append(Program.DataDelimiter)
                .Append(TargetWord);
             
            return builder.ToString();
        }
    }
}
