using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfRounds = 3;
            int roundDelay = 3000; // milliseconds
            int shotDelay = 10; // milliseconds

            BattleshipAgent myBattleshipAgent = new SuperCoolAgent();
            //BattleshipAgent myTestingOpponent = new HumanPlayer();
            BattleshipAgent myTestingOpponent = new BozoTheClown();
            //BattleshipAgent myTestingOpponent = new LarryTheLine();

            BattleshipEngine gameEngine = new BattleshipEngine(myBattleshipAgent, myTestingOpponent);
            for (int i = 0; i < numberOfRounds; i++)
            {
                BattleshipAgent winner = gameEngine.PlaySingleGame(true, shotDelay);
                System.Threading.Thread.Sleep(roundDelay);
            }
        }
    }
}
