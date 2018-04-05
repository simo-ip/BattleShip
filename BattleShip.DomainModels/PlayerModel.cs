using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DomainModels
{
    public class PlayerModel
    {
        /// <summary>count successful hits</summary>
        public int HitCounter = 0;
        /// <summary>to report the number of shots taken once game complete</summary>
        public int ShotCounter = 0;
        public string Message { get; set; }
        
        /// <summary> Process player shoot </summary>
        /// <param name="Grid"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void Shoot(char[,] Grid, int row, int col)
        {
            if (Grid[row, col].Equals(Constants.Ship))
            {
                Grid[row, col] = Constants.Hit;
                Message = "Hit!";
                HitCounter += 1;
                ShotCounter += 1;
                
                foreach(var s in ComputerModel.Ships)
                {
                    var a = (from b in s.Positions
                             where b.row == row && b.col == col
                             select b).FirstOrDefault();
                    
                    if (a != null)
                    {
                        s.Positions.Remove(a);

                        if (s.Positions.Count == 0)
                        {
                            Message = string.Format("Congratulations! {0} sank", s.Name);
                        }
                    }
                }
            }
            else if (Grid[row, col].Equals(Constants.NoShot))
            {
                Grid[row, col] = Constants.Miss;
                Message = "Miss!";
                ShotCounter += 1;
            }
            else
            {
                Message = "Already played!";
            }
        }        
    }
}
