using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleShip.DomainModels;
using BattleShip.ConsoleApplication.Views;
using BattleShip.ConsoleApplication.Presenters;
using BattleShip.DomainModels.Ships;

namespace BattleShipTest
{
    [TestClass]
    public class UnitTest1
    {
        BattleShipPresenter Presenter;

        [TestInitialize]
        public void SetUp()
        {
            Presenter = new BattleShipPresenter()
            {
                BattleShipView = new BattleShipView(),
                ComputerModel = new ComputerModel() { 
                    BattleshipModel = new BattleshipModel(),
                    DestroyerOneModel = new DestroyerModel(),
                    DestroyerTwoModel = new DestroyerModel()
                },
                PlayerModel = new PlayerModel()
            };
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var expectedTotalHits = 13 ;
            // Act
            Presenter.Init(false);
            var actualTotalHits = Presenter.TotalHits;
            // Assert
            Assert.AreEqual(expectedTotalHits, actualTotalHits);
        }
    }
}
