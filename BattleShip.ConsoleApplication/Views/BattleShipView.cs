using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.ConsoleApplication.Views
{
    public class BattleShipView 
    {
        public int ShotCounter { get; set; }
        public string ErrMessage { get; set; }
        public string Message { get; set; }
        
        public BattleShipView()
        {
            ErrMessage = string.Empty;
            Message = string.Empty;
        }
        
        public List<string> Text { get; set; }

        public void Show()
        {
            Console.Clear();
            foreach (var line in Text)
            {
                Console.WriteLine(line);
            }            
        }

        public void GameOver()
        {
            Console.WriteLine();
            Console.WriteLine("Well done! You completed the game in {0} shots", ShotCounter);
            Console.WriteLine("Enter 'q' and press 'Enter' key to quit");
            string line = Console.ReadLine();
            if (line.ToLower() == "q")
            {
                Environment.Exit(0);
            }
        }

        public void ShowMessages()
        {
            Console.WriteLine("\n");
            if (ErrMessage.Length > 0)
            {
                Console.WriteLine(ErrMessage);
                ErrMessage = "";
            }
            else
            {
                Console.WriteLine(Message);
            }
            Console.WriteLine("Enter coordinates (row, col), e.g. A5");
        }

    }
}
