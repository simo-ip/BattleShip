using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DomainModels
{
    public static class Constants
    {
        public static char[] charArr = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        public static char NoShot = '.';
        public static char Miss = '-';
        public static char Hit = 'X';
        public static char Ship = 'S';
    }

    public static class ErrorMsg
    {
        public static string NotValid = "Ivalid!";
        public static string NotValidRow = "Not valid letter!";
        public static string NotValidCol = "Not valid integer!";
    }
}
