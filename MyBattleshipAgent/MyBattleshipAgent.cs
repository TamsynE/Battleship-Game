using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;

        char searchMode; // hunt mode
        int xHit; // to store x co-ordinate of first hit of a boat
        int yHit; //to store y co-ordinate of first hit of a boat
        char searchDirection; // attack mode direction around xHit, yHit 

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
        }

        public override void Initialize()
        {
            searchMode = 'H';

            for (int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for (int j = 0; j < attackHistory.GetLength(1); j++)
                {
                    attackHistory[i, j] = 'U'; // spots that haven't been hit yet
                }
            }

            // first attack co-ordinates
            attackGrid.x = 0;
            attackGrid.y = 0;

            xHit = -1;
            yHit = -1;
            searchDirection = 'N';
            searchMode = 'H';

            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Tam Evezard";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            //- ATTACK MODE -//
            //when a ship is hit
            if (searchMode == 'A')
            {
                //-MISSING IN ATTACK MODE-//
                if(attackHistory[attackGrid.x, attackGrid.y] == 'M') // if a miss occurs when circle-attacking a ship, remember original hit co-ords and change direction of attack 
                {
                    if (searchDirection == 'N') // if a miss occurs attacking up
                    {
                        //-1-//
                        if(attackGrid.x < 9) // check in range (is there a block to hit to the right?)
                        {
                            searchDirection = 'E'; // change direction of attack to East when missed once in North direction
                            attackGrid.x = xHit + 1; //reset from original hit position
                            attackGrid.y = yHit;
                        }
                        //-2-//
                        else if(attackGrid.y < 9) // check in range (is there a block to hit downwards?)
                        {
                            searchDirection = 'S'; // change direction of attack to South 
                            attackGrid.x = xHit;
                            attackGrid.y = yHit + 1;
                        }
                        //-3-//
                        else if(attackGrid.x > 0) // check in range (is there a block to hit to the left?)
                        {
                            searchDirection = 'W'; // change direction of attack to West
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }
                        //-4-//
                        else
                        {
                            searchMode = 'H'; // reset to hunt mode once ship has sunk (missed in final attack direction)
                            attackGrid.x = xHit; //reset attackHistory
                            attackGrid.y = yHit; //reset attackHistory
                            searchDirection = 'N'; // reset attack direction to North
                        }
                    }
                    else if (searchDirection == 'E') // if a miss occurs attacking right - repeats code sections 2, 3, and 4 above
                    {
                        if(attackGrid.y < 9)
                        {
                            searchDirection = 'S';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit + 1;
                        }
                        else if(attackGrid.x > 0)
                        {
                            searchDirection = 'W';
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }
                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit;
                            searchDirection = 'N';
                        }
                    }
                    else if (searchDirection == 'S') // if a miss occurs attacking down - repeats code sections 3 and 4 above
                    {
                        if (attackGrid.x > 0)
                        {
                            searchDirection = 'W';
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }
                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit;
                            searchDirection = 'N';
                        }
                    }
                    else if (searchDirection == 'W') // if a miss occurs attacking left - repeats code section 4 above
                    {
                        searchMode = 'H';
                        attackGrid.x = xHit;
                        attackGrid.y = yHit;
                        searchDirection = 'N';
                    }
                }

                //-HITTING A SHIP IN ATTACKMODE-//
                else
                {
                    if (searchDirection == 'N')
                    {
                        if (attackGrid.y > 0) // if we can attack up...
                        {
                            attackGrid.y -= 1; // ...attack up
                        }
                        else // if we can't attack up...
                        {
                            searchDirection = 'E'; // ...change attack direction to East
                            attackGrid.x = xHit + 1; //go back to original hit
                            attackGrid.y = yHit;
                        }
                    }
                    else if (searchDirection == 'E')
                    {
                        if (attackGrid.x < 9) // if we can attack right...
                        {
                            attackGrid.x += 1; // ...attack right
                        }
                        else // if we can't...
                        {
                            searchDirection = 'S'; // ...change attack direction to South
                            attackGrid.x = xHit; //go back to original hit
                            attackGrid.y = yHit + 1;
                        }
                    }
                    else if (searchDirection == 'S')
                    {
                        if(attackGrid.y < 9) // if we can attack down...
                        {
                            attackGrid.y += 1; // ... attack down
                        }
                        else
                        {
                            searchDirection = 'W'; //...West
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }
                    }
                    else if (searchDirection == 'W')
                    {
                        if (attackGrid.x > 0)// if we can attack West...
                        {
                            attackGrid.x -= 1; // ... attack West
                        }
                        else
                        {
                            searchMode = 'H'; // go back to hunt mode (searching in a checkerboard-like fashion
                            attackGrid.x = xHit; // from original hit
                            attackGrid.y = yHit;
                            searchDirection = 'N'; //reset direction for next time
                        }
                    }
                }
            }
            
            if (searchMode == 'H') // hunt
            {
                while (attackHistory[attackGrid.x, attackGrid.y]  != 'U')
                {
                    if (attackGrid.y % 2 == 0) // every other row
                    {
                        if (attackGrid.x < 8) // in range
                        {
                            attackGrid.x += 2; // every second block
                        }
                        else // go 
                        {
                            attackGrid.x = 1; 
                            attackGrid.y += 1; // next row
                        }
                    }
                    else // the other "every other" rows
                    {
                        if (attackGrid.x < 8)
                        {
                            attackGrid.x += 2; // every second block
                        }
                        else
                        {
                            attackGrid.x = 0;
                            attackGrid.y += 1; // next row
                        }
                    }
                }
            }
            
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if (report =='\0')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'M'; // miss
            }

            else if (report == 'S' || report == 'P' || report == 'D' || report == 'C' || report == 'B')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'H'; // hit
                if (searchMode != 'A')
                {
                    xHit = attackGrid.x; // log original hit x value for after ship sinks
                    yHit = attackGrid.y; // log original hit y value "
                }
                searchMode = 'A'; // change to attack mode when hit
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            Random rng = new Random();

            if (rng.Next() % 2 == 0)
            {
                myFleet.Submarine = new ShipPosition(3, 2, ShipRotation.Horizontal);
                myFleet.Destroyer = new ShipPosition(0, 5, ShipRotation.Vertical);
                myFleet.PatrolBoat = new ShipPosition(6, 0, ShipRotation.Horizontal);
                myFleet.Battleship = new ShipPosition(6, 3, ShipRotation.Vertical);
                myFleet.Carrier = new ShipPosition(4, 8, ShipRotation.Horizontal);
            }

            else
            {
                myFleet.Submarine = new ShipPosition(3, 2, ShipRotation.Vertical);
                myFleet.Destroyer = new ShipPosition(0, 5, ShipRotation.Horizontal);
                myFleet.PatrolBoat = new ShipPosition(6, 0, ShipRotation.Vertical);
                myFleet.Battleship = new ShipPosition(6, 3, ShipRotation.Horizontal);
                myFleet.Carrier = new ShipPosition(0, 1, ShipRotation.Horizontal);

            }
            return myFleet;
        }
    }
}
