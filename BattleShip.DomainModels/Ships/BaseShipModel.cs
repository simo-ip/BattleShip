using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DomainModels.Ships
{

    public abstract class BaseShipModel
    {
        public int Lenght {get;set;}
        public List<Cell> Positions {get;set;}
        public string Name {get;set;}

        public BaseShipModel()
        {
            Positions = new List<Cell>();
        }
    }
}
