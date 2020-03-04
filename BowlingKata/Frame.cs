using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling_Kata
{
    class Frame
    {
        private
        int Score = 0;
        bool spare;
        bool strike;
        int BallOne = 0;
        int BallTwo = 0;    
        int FinalFrameThree = 0;

        public
        bool IsStrike()
        {
            return strike;
        }

        public
        void SetStrike()
        {
            strike = true;
        }

        public
        void SetSpare()
        {
            spare = true;
        }

        public
        bool IsSpare()
        {
            return spare;
        }

        public 
        void SetBallOne(int ball)
        {
            BallOne = ball;
        }

        public
        void SetBallTwo(int ball)
        {
            BallTwo = ball;
        }

        public
        void SetFinalFrameThree(int ball)
        {
            Score += ball;
            FinalFrameThree = ball;
        }

        public
        int GetBallOne()
        {
            return BallOne;
        }

        public
        int GetBallTwo()
        {
            return BallTwo;
        }

        public
        int GetFinalFrameThree()
        {
            return FinalFrameThree;
        }


        public
        int GetScore()
        {
            return Score;
        }

        public
        void AddScore(int ball)
        {
            if(ball == 10 && (GetBallOne() == 0))
            {
                Score += ball;
                SetBallOne(ball);
                SetStrike();
                Console.WriteLine("STRIKE!!!");
            }
            else if(Score + ball == 10 && (GetBallTwo() == 0))
            {
                Score += ball;
                SetBallTwo(ball);
                SetSpare();
                Console.WriteLine("SPARE!!!");
            }
            else
            {
                if(BallOne == 0)
                {
                    SetBallOne(ball);
                    Score += ball;
                }
                else
                {
                    SetBallTwo(ball);
                    Score += ball;
                }
                
            }
        }
    }
}
