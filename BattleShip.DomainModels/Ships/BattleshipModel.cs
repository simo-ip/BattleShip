using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DomainModels.Ships
{
    public class BattleshipModel: BaseShipModel
    {
        public BattleshipModel()
        {
            Lenght = 5;
            Name = "Battleship";
        }
    }    
}
