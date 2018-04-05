using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DomainModels;
using BattleShip.ConsoleApplication.Views;
using Microsoft.Practices.Unity;

namespace BattleShip.ConsoleApplication.Presenters
{
    public class BattleShipPresenter : IDisposable
    {
        [Dependency]
        public PlayerModel PlayerModel { get; set; }
        [Dependency]
        public ComputerModel ComputerModel { get; set; }
        [Dependency]
        public BattleShipView BattleShipView { get; set; }
        
        public bool Show { get; set; }

        public int HitCounter { get; set; }
        public int TotalHits { get; set; }
        
        string line = string.Empty;
        public string Line
        {
            get
            {
                return line;
            }
            set
            {
                line = value;
                OnInputLineChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnInputLineChanged = delegate { };

        public BattleShipPresenter()
        {
            
        }

        public void Init(bool show)
        {
            Show = show;
            ComputerModel.Init();
            TotalHits = ComputerModel.TotalHits;
            OnInputLineChanged += new EventHandler(inputLineChanged);
        }
        
        public void inputLineChanged(object sender, EventArgs e)
        {
            ProcessShoot();
        }

        public void ShowBoard()
        {
            BattleShipView.Text = DisplayBoard(ComputerModel.Sea, Show);
            BattleShipView.Show();
        }

        public void GameOver()
        {
            BattleShipView.ShotCounter = PlayerModel.ShotCounter;
            BattleShipView.GameOver();
        }

        public void ShowMessages()
        {
            BattleShipView.ShowMessages();
        }

        #region private methods

        private List<string> DisplayBoard(char[,] Board, bool debug)
        {
            List<string> result = new List<string>();
            int Row;
            int Column;
            StringBuilder line = new StringBuilder();

            result.Add("  | 1 2 3 4 5 6 7 8 9 0");
            result.Add("--+--------------------");
            for (Row = 0; Row < 10; Row++)
            {
                line.Clear();
                line.Append(Constants.charArr[Row] + " | ");
                for (Column = 0; Column < 10; Column++)
                {
                    if (debug)
                    {
                        line.Append(Board[Row, Column] + " ");
                    }
                    else
                    {
                        char ch = Board[Row, Column];
                        if (ch != Constants.Ship && ch != Constants.NoShot)
                        {
                            line.Append(Board[Row, Column] + " ");
                        }
                        else
                        {
                            line.Append(Constants.NoShot + " ");
                        }
                    }
                }
                result.Add(line.ToString());
            }
            result.Add(string.Empty);
            return result;
        }


        private void ProcessShoot()
        {
            int row = 0;
            int col = 0;
            bool valid = ValidateUserInput(ComputerModel.Sea, out row, out col);
            if (valid)
            {
                try
                {
                    PlayerModel.Shoot(ComputerModel.Sea, row, col);
                    HitCounter = PlayerModel.HitCounter;
                    BattleShipView.Message = PlayerModel.Message;
                    BattleShipView.ShotCounter = PlayerModel.ShotCounter;
                }
                catch
                {
                    BattleShipView.ErrMessage = ErrorMsg.NotValid;
                }
            }
        }


        private bool ValidateUserInput(char[,] Grid, out int row, out int col)
        {
            row = -1;
            col = -1;

            if (Line.Length < 1)
            {
                BattleShipView.ErrMessage = string.Format("{0} {1}", ErrorMsg.NotValid, line);
                return false;
            }
            else if (Line.Length > 3)
            {
                BattleShipView.ErrMessage = string.Format("{0} {1}", ErrorMsg.NotValid, line);
                return false;
            }


            if (!validateRow(out row))
            {
                return false;
            }

            if (!validateCol(out col))
            {
                return false;
            }

            return true;

        }

        private bool validateCol(out int col)
        {
            col = -1;
            string l = Line.Substring(1);
            int x;
            if (int.TryParse(l, out x))
            {
                if (x > 10)
                {
                    BattleShipView.ErrMessage = ErrorMsg.NotValidCol;
                    return false;
                }
                col = x - 1;
            }
            else
            {
                BattleShipView.ErrMessage = ErrorMsg.NotValidCol;
                return false;
            }
            return true;
        }

        private bool validateRow(out int row)
        {
            char v = Line[0].ToString().ToUpper()[0];
            int y = Array.IndexOf(Constants.charArr, v);
            if (y > -1)
            {
                row = y;
                return true;
            }
            else
            {
                row = -1;
                BattleShipView.ErrMessage = ErrorMsg.NotValidRow;
                return false;
            }
        }

        #endregion

        public void Dispose()
        {
            this.OnInputLineChanged -= new EventHandler(inputLineChanged);
        }
    }
}
