/****************************************************************************************************
* Programming Test: Battle ships                                                                    *
* --------------------------------------------------------------------------------------------------*
* REQUIREMENTS                                                                                      * 
*---------------------------------------------------------------------------------------------------*
* 1. write the game as a simple console application in the language requested                       *
* 2. Comment your code as necessary                                                                 *
* 3. Application should allow a single human player to play a one-sided game against the computer   *
* 4. The program should create:                                                                     *
*	a. a 10x10 grid                                                                                 *
*	b. 1 x Battleship (5 squares)                                                                   *
*	c. 2 x Destroyers (4 squares)                                                                   *
* 5. Application should accept input from the user in the format “A5”                               *
* 6. feedback to the user whether the shot was success, miss                                        *
* 7. report on the sinking of any vessels                                                           *
*   a.  . = no shot                                                                                 *
*   b.  - = miss                                                                                    *
*   c.  X = hit                                                                                     *
* 8. You should implement a show command to aid debugging and backdoor cheat                        *
* 9. Please report the number of shots taken once game complete                                     *
 ****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DomainModels;
using BattleShip.ConsoleApplication.Presenters;
using Microsoft.Practices.Unity;

namespace BattleShip.ConsoleApplication
{
    class Program
    {                    
        static void Main(string[] args)
        {
            bool debug = false;
            setParam(args, out debug);

            var unitycontainer = new UnityContainer();
            unitycontainer.RegisterTypes(
                            AllClasses.FromLoadedAssemblies(),
                            WithMappings.FromMatchingInterface,
                            WithName.Default);
            

            var presenter = unitycontainer.Resolve<BattleShipPresenter>();
            presenter.Init(debug);
            
            while (presenter.HitCounter <= presenter.TotalHits)
            {
                presenter.ShowBoard();
                if (presenter.HitCounter == presenter.TotalHits)
                {
                    presenter.GameOver();
                }
                else
                {
                    presenter.ShowMessages();
                    presenter.Line = Console.ReadLine();
                }
            }            
        }


        #region private methods

        private static void setParam(string[] args, out bool debug)
        {
            debug = false;
            for (int i = 0; i < args.Length; i++)
            {
                string flag = args.GetValue(i).ToString().ToLower();
                if (flag == "show")
                {
                    debug = true;
                }
            }
        }

        #endregion

    }
}
