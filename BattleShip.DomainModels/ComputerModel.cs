using BattleShip.DomainModels.Ships;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DomainModels
{
    public class ComputerModel
    {
        #region Dependency
        
        [Dependency]
        public BattleshipModel BattleshipModel { get; set; }

        [Dependency]
        public DestroyerModel DestroyerOneModel { get; set; }

        [Dependency]
        public DestroyerModel DestroyerTwoModel { get; set; }

        #endregion

        #region private properties

        Random generator = null;
        
        #endregion

        /// <summary>The program should create a 10x10 grid</summary>
        public static char[,] Sea = new char[10, 10];
        /// <summary>list of ships</summary>
        public static List<BaseShipModel> Ships;
        /// <summary> Number of hits to finish game  </summary>
        public int TotalHits { get; set; }
        
        public ComputerModel()
        {
            
        }

        /// <summary>
        /// Initialise list of ships, random number generator, 
        /// fill board with "no shot" sign and finally place ships on sea
        /// </summary>
        public void Init()
        {
            Ships = new List<BaseShipModel>();
            generator = new Random(DateTime.Now.Millisecond);
            
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    Sea[row, column] = Constants.NoShot;
                }
            }

            placeShips();
        }

        #region private methods

        /// <summary>
        /// place one Battleship and too Destroyers on the see(grid)
        /// at random position
        /// </summary>
        private void placeShips()
        {
            // 2 x Destroyers (4 squares)
            PlaseOneShip(DestroyerOneModel);
            Ships.Add(DestroyerOneModel);
            TotalHits += DestroyerOneModel.Lenght;

            PlaseOneShip(DestroyerTwoModel);
            Ships.Add(DestroyerTwoModel);
            TotalHits += DestroyerTwoModel.Lenght;
            
            // 1 x Battleship (5 squares)
            PlaseOneShip(BattleshipModel);
            Ships.Add(BattleshipModel);
            TotalHits += BattleshipModel.Lenght; 
        }
                
        private void PlaseOneShip(BaseShipModel ship)
        {
            var r = generator.Next();
            int orientation = r % 2;
            Debug.WriteLine(" orientation " + orientation.ToString());

            switch (orientation)
            {
                case (int)Orientation.down:
                    {
                        var row = generator.Next(0, 9 - ship.Lenght);
                        var col = generator.Next(0, 9);
                        
                        if (checkForOverlap(row, col, ship.Lenght, Orientation.down))
                        {
                            for (var i = 0; i < ship.Lenght; i++)
                            {
                                ship.Positions.Add(new Cell {row = row + i,col = col });
                                setOnBoard(row + i, col);
                            }
                        }
                        else
                        {
                            PlaseOneShip(ship);
                        }
                        break;
                    }
                case (int)Orientation.right:
                    {
                        var col = generator.Next(0, 9 - ship.Lenght);
                        var row = generator.Next(0, 9);
                        if (col < 0)
                        {
                            var a = 1;
                        }
                        if (checkForOverlap(row, col, ship.Lenght, Orientation.right))
                        {
                            for (var i = 0; i < ship.Lenght; i++)
                            {
                                ship.Positions.Add(new Cell { row = row, col = col + i });
                                setOnBoard(row, col + i);
                            }
                        }
                        else
                        {
                            PlaseOneShip(ship);
                        }
                        break;
                    }
            }
        }

        private bool checkForOverlap(int row, int col, int lenght, Orientation orientation)
        {
            for (var i = 0; i < lenght; i++)
            {
                switch (orientation)
                {
                    case Orientation.down:
                        {
                            if (!isEmptyCell(row + i, col))
                            {
                                return false;
                            }
                            break;
                        }
                    case Orientation.right:
                        {
                            if (!isEmptyCell(row , col + i))
                            {
                                return false;
                            }
                            break;
                        }
                }
            }
            return true;
        }

        private bool isEmptyCell(int row, int col)
        {
            if (Sea[row, col].Equals(Constants.Ship))
            {
                return false;
            }
            return true;
        }

        private void setOnBoard(int row, int col)
        {
            Sea[row, col] = Constants.Ship;
        }

        #endregion
    }

    enum Orientation
    {
        down, right
    }
}
