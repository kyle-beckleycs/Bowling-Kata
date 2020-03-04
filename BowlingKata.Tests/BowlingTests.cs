using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling_Kata;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace BowlingKata.Tests
{
    [TestClass]
    public class BowlingTests
    {
        private readonly Game Bowling = new Game();

        public BowlingTests()
        {
        }

        [TestMethod]
        public void CalculateGame_PerfectGame()
        {
            string TestFile = @"C:\Projects\BowlingKata\BowlingKata\TestFiles\\PerfectGame.csv";
            int TestScore = Bowling.CalculateGame(TestFile);

            Assert.AreEqual(300, TestScore);
        }

        [TestMethod]
        public void CalculateGame_PerfectGame_ReturnFalse()
        {
            string TestFile = @"C:\Projects\BowlingKata\BowlingKata\TestFiles\\NormalGame.csv";
            int TestScore = Bowling.CalculateGame(TestFile);

            Assert.AreNotEqual(300, TestScore);
        }

        [TestMethod]
        public void CalculateGame_AllSpares()
        {
            string TestFile = @"C:\Projects\BowlingKata\BowlingKata\TestFiles\\AllSpares.csv";
            int TestScore = Bowling.CalculateGame(TestFile);

            Assert.AreEqual(150, TestScore);
        }

        [TestMethod]
        public void CalculateGame_AllGutters()
        {
            string TestFile = @"C:\Projects\BowlingKata\BowlingKata\TestFiles\\AllGutters.csv";
            int TestScore = Bowling.CalculateGame(TestFile);

            Assert.AreEqual(0, TestScore);
        }

        [TestMethod]
        public void CalculateGame_IncompleteGame()
        {
            string TestFile = @"C:\Projects\BowlingKata\BowlingKata\TestFiles\\IncompleteGame.csv";
            int TestScore = Bowling.CalculateGame(TestFile);

            Assert.AreEqual(160, TestScore);
        }

        [TestMethod]
        public void PlayGame_PerfectScore()
        {
            const string InputString = "10\n10\n10\n10\n10\n10\n10\n10\n10\n10\n10\n10\n";
            var stringReader = new StringReader(InputString);
            Console.SetIn(stringReader);
            Bowling.PlayGame();

            Assert.AreEqual(300, Bowling.GetFinalScore());
        }

        [TestMethod]
        public void PlayGame_AllGutters()
        {
            const string InputString = "0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n";
            var stringReader = new StringReader(InputString);
            Console.SetIn(stringReader);
            Bowling.PlayGame();
          
            Assert.AreEqual(0, Bowling.GetFinalScore());
        }

        [TestMethod]
        public void PlayGame_AllSpares()
        {
            const string InputString = "5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n5\n";
            var stringReader = new StringReader(InputString);
            Console.SetIn(stringReader);
            Bowling.PlayGame();

            Assert.AreEqual(150, Bowling.GetFinalScore());
        }

        [TestMethod]
        public void PlayGame_RegularGame()
        {
            const string InputString = "10\n5\n2\n4\n3\n5\n5\n5\n4\n6\n4\n3\n5\n5\n4\n5\n5\n5\n5\n0\n";
            var stringReader = new StringReader(InputString);
            Console.SetIn(stringReader);
            Bowling.PlayGame();

            Assert.AreEqual(125, Bowling.GetFinalScore());
        }
    }
}
