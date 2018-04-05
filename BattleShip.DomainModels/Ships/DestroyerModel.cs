using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DomainModels.Ships
{
    public class DestroyerModel : BaseShipModel
    {
        public DestroyerModel()
        {
            Lenght = 4;
            Name = "Destroyer";
        }
    }
}
