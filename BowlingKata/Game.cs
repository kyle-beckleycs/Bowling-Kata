using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace Bowling_Kata
{
    public
    class Game
    {
        private int TotalScore;
        private readonly Frame[] ScoreBoard = new Frame[10];
        int Round = 0;

        public 
        Game()
        {
            for(int i = 0; i < 10; i++)
            {
                ScoreBoard[i] = new Frame();
            }
        }

        public
        void PlayGame()
        {
            while (CurrentRound() < 10)
            {
               int ball = Bowl();                                                   //Player Bowls
               CurrentFrame().AddScore(ball);                                       //Add ball to frame

               if (Round == 9)                                                      //if final frame
               {
                    if(CurrentFrame().GetBallOne() == 10)                           //if first ball is strike
                    {
                        CurrentFrame().AddScore(ball);
                        Console.WriteLine("Please go again");
                        ball = Bowl();                                              //Go again
                        if (ball == 10)                                             // if second ball is strike or spare
                        {
                            CurrentFrame().SetBallTwo(ball);                     
                            Console.WriteLine("Please go again");
                            ball = Bowl();                                          //Go again
                            CurrentFrame().SetFinalFrameThree(ball);                //Calculate final ball
                        }
                    }
                    else                                                            //first ball isn't a strike, check for second ball to be a spare or end the game
                    {
                        ball = Bowl();                                              //bowl second ball
                        CurrentFrame().AddScore(ball);
                        if (CurrentFrame().IsSpare())
                        {
                            ball = Bowl();                                          //bowl final ball
                            CurrentFrame().SetFinalFrameThree(ball);
                        }
                    }
               }
               else if (CurrentFrame().IsStrike())                                  //if first ball of frame is a strike
               {
                    Console.WriteLine("Moving to next frame");
               }
               else
               {
                    ball = Bowl();                                                  //bowl second ball of the frame
                    CurrentFrame().AddScore(ball);
               }
               //Calculate score at the end of every frame
               Console.WriteLine("Current Score:" + CurrentScore());
               NextFrame();
            }
            Console.WriteLine("GAME OVER! \nFinal Score:" + CurrentScore());
        }

        public
        int CalculateGame(string inputfile)
        {
            Queue<int> ScoreCard = ParseCSV(inputfile);
            try { 
            while (CurrentRound() < 10)
            {
                //Player Bowls
                CurrentFrame().AddScore(ScoreCard.Dequeue());                        //Add ball to frame

                if (Round == 9)                                                      //if final frame
                {
                    if (CurrentFrame().GetBallOne() == 10)                           //if first ball is strike
                    {
                        CurrentFrame().AddScore(ScoreCard.Dequeue());
                        if (CurrentFrame().GetBallTwo() == 10)                       // if second ball is strike or spare
                        {
                            CurrentFrame().SetFinalFrameThree(ScoreCard.Dequeue()); //Calculate final ball
                        }
                    }
                    else                                                            //first ball isn't a strike, check for second ball to be a spare or end the game
                    {
                        CurrentFrame().AddScore(ScoreCard.Dequeue());
                        if (CurrentFrame().IsSpare())
                        {
                            CurrentFrame().SetFinalFrameThree(ScoreCard.Dequeue());
                        }
                    }
                }
                else if (CurrentFrame().IsStrike())                                  //if first ball of frame is a strike
                {
                    Console.WriteLine("Moving to next frame");
                }
                else
                {
                    CurrentFrame().AddScore(ScoreCard.Dequeue());
                }
                NextFrame();
            }

            Console.WriteLine("The Results Are In! \nFinal Score:" + CurrentScore());
            return CurrentScore();
        }
            catch
            {
                Console.WriteLine("The scorecard was an incomplete game.\nIncomplete Score:" + CurrentScore());
                return CurrentScore();
            }
        }

        private
        int Bowl()
        {
            int ball;
            //ask what the player bowled
            Console.WriteLine("How many pins did you knock down?");
            try
            {
                ball = Convert.ToInt32(Console.ReadLine());     //take user input
                
            }
            catch
            {
                Console.WriteLine("Invalid entry, ball is void and counted as a gutter ball");
                ball = 0;
            }

            if (ball > 10 || ball < 0)                      //check if bowl is a valid input
            {
                Console.WriteLine(ball + " is an nvalid score, ball is void and counted as a gutter ball");
                ball = 0;
            }
            return ball;
        } 

        private
        int CurrentRound()
        {
            return Round;
        }

        private
        void NextFrame()
        {
            Round++;
        }

        private
        Frame CurrentFrame()
        {
            return ScoreBoard[Round];
        }

        private
        void CalcScore()
        {
            for (int i = 0; i < 10; i++)
            {
                if (ScoreBoard[i].IsStrike() && i < 8)   // if before frame 8
                {
                    TotalScore += (ScoreBoard[i].GetScore() + ScoreBoard[i + 1].GetBallOne());
                    if (ScoreBoard[i + 1].IsStrike()) {
                        TotalScore += ScoreBoard[i + 2].GetBallOne();
                    }
                    else{
                        TotalScore += (ScoreBoard[i].GetScore() + ScoreBoard[i + 1].GetBallOne() + ScoreBoard[i + 1].GetBallTwo());
                    }
                }
                else if (ScoreBoard[i].IsStrike() && i > 8) //if frame 9 or 10
                {
                    if (i == 8) //9th frame calculations
                    {
                        TotalScore += (ScoreBoard[i].GetScore() + ScoreBoard[i + 1].GetBallOne());
                        if (ScoreBoard[i + 1].IsStrike())
                        {
                            TotalScore += ScoreBoard[i].GetBallTwo();
                        }
                        else
                        {
                            TotalScore += (ScoreBoard[i].GetScore() + ScoreBoard[i + 1].GetBallOne() + ScoreBoard[i + 1].GetBallTwo());
                        }
                    }
                    else  //10th frame calculations
                    {
                        TotalScore += (ScoreBoard[i].GetScore() + ScoreBoard[i].GetBallTwo() + ScoreBoard[i].GetFinalFrameThree());
                    }
                }
                else if(ScoreBoard[i].IsSpare())
                {
                    if(i == 9)
                    {
                        TotalScore += (10 + ScoreBoard[i].GetFinalFrameThree());
                    }
                    else
                    {
                        TotalScore += (10 + ScoreBoard[i + 1].GetBallOne());
                    }
                }
                else
                {
                    TotalScore += ScoreBoard[i].GetScore();
                }
            }
        }

        private
        int CurrentScore()
        {
            TotalScore = 0;
            CalcScore();
            return TotalScore;
        }

        public 
        int GetFinalScore()
        {
            return TotalScore;
        }

        private
        Queue<int> ParseCSV(string inputfile)
        {
            Queue<int> Scores = new Queue<int>();
            using (var reader = new StreamReader(inputfile))
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                int i = 0;
                while((i < values.Length) && i < 22)
                { 
                    try
                    {
                        Scores.Enqueue(Convert.ToInt32(values[i]));     //attempt to parse csv as an int

                    }
                    catch                                           //failed parse results in 0 for the ball
                    {
                        Scores.Enqueue(0);
                    }
                    i++;
                }
            }
            if (Scores.Count > 21)
            {
                Console.WriteLine("There are more values than possible balls in a game of bowling.\nWe will count up to the maximum number of balls in a game.\n ");
                //read maximum values for a perfect game, even if the user didn't populate the full final frame
            }
            else
            {                                                           //how do i verify if this isn't a complete game
                Console.WriteLine("There is not enough data to complete the game.\n ");
                //int array = new Array[Scores.Count];
                //array = Scores.ToArray();
            }
            return Scores;
        }
    }
}
