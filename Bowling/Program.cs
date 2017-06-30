using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    class Program
    {
        private static List<Frame> _frames = new List<Frame>(10)
        {
            new Frame(),
            new Frame(),
            new Frame(),
            new Frame(),
            new Frame(),
            new Frame(),
            new Frame(),
            new Frame(),
            new Frame(),
            new Frame()
        };
        private static SortedDictionary<int, int> _additionalBowlsByFrameId = new SortedDictionary<int, int>();

        static void Main(string[] args)
        {
            int frameScore = 0;

            //Frame 1 - 9
            for (int i = 0; i < _frames.Count - 1; i++)
            {
                PrintFrames();

                if (i != 0)
                {
                    _frames[i].TotalScore = _frames[i - 1].TotalScore;
                }

                bool strike = false;
                frameScore = 0;
                Console.WriteLine("Enter first bowl:");
                _frames[i].Attempt1Score = Console.ReadLine();

                if (_frames[i].Attempt1Score == "X" || _frames[i].Attempt1Score == "x")
                {
                    frameScore = 10;
                    _frames[i].TotalScore += frameScore;
                    CheckPreviousBowls(frameScore, i);
                    _additionalBowlsByFrameId.Add(i, 2);
                    strike = true;
                }
                else
                {
                    frameScore = int.Parse(_frames[i].Attempt1Score);
                    CheckPreviousBowls(frameScore, i);
                    _frames[i].TotalScore += frameScore;
                }

                if (!strike)
                {
                    Console.WriteLine("Enter second bowl:");
                    _frames[i].Attempt2Score = Console.ReadLine();
                    frameScore = 0;

                    if (_frames[i].Attempt2Score == "/")
                    {
                        frameScore = (10 - int.Parse(_frames[i].Attempt1Score));
                        _frames[i].TotalScore += frameScore;
                        CheckPreviousBowls(frameScore, i);
                        _additionalBowlsByFrameId.Add(i, 1);
                    }
                    else
                    {
                        frameScore = int.Parse(_frames[i].Attempt2Score);
                        _frames[i].TotalScore += frameScore;
                        CheckPreviousBowls(frameScore, i);
                    }
                }
            }

            PrintFrames();

            //Frame 10
            bool strikeOrSpare = false;
            int index = _frames.Count - 1;
            frameScore = 0;

            _frames[index].TotalScore = _frames[index - 1].TotalScore;

            Console.WriteLine("Enter first bowl:");
            _frames[index].Attempt1Score = Console.ReadLine();

            if (_frames[index].Attempt1Score == "X" || _frames[index].Attempt1Score == "x")
            {
                frameScore = 10;
                _frames[index].TotalScore += frameScore;
                CheckPreviousBowls(frameScore, index);
                strikeOrSpare = true;
            }
            else
            {
                frameScore = int.Parse(_frames[index].Attempt1Score);
                CheckPreviousBowls(frameScore, index);
                _frames[index].TotalScore += frameScore;
            }

            Console.WriteLine("Enter second bowl:");
            _frames[index].Attempt2Score = Console.ReadLine();
            frameScore = 0;

            if (_frames[index].Attempt2Score == "/")
            {
                frameScore = (10 - int.Parse(_frames[index].Attempt1Score));
                _frames[index].TotalScore += frameScore;
                CheckPreviousBowls(frameScore, index);
                strikeOrSpare = true;
            }
            else if (_frames[index].Attempt2Score == "X" || _frames[index].Attempt2Score == "x")
            {
                frameScore = 10;
                _frames[index].TotalScore += frameScore;
                CheckPreviousBowls(frameScore, index);
                strikeOrSpare = true;
            }
            else
            {
                frameScore = int.Parse(_frames[index].Attempt2Score);
                _frames[index].TotalScore += frameScore;
                CheckPreviousBowls(frameScore, index);
            }

            if (strikeOrSpare)
            {
                Console.WriteLine("Enter third bowl:");
                _frames[index].Attempt3Score = Console.ReadLine();
                frameScore = 0;

                if (_frames[index].Attempt3Score == "/")
                {
                    frameScore = (10 - int.Parse(_frames[index].Attempt2Score));
                    _frames[index].TotalScore += frameScore;
                    CheckPreviousBowls(frameScore, index);
                }
                else if (_frames[index].Attempt2Score == "X" || _frames[index].Attempt2Score == "x")
                {
                    frameScore = 10;
                    _frames[index].TotalScore += frameScore;
                    CheckPreviousBowls(frameScore, index);
                    strikeOrSpare = true;
                }
                else
                {
                    frameScore = int.Parse(_frames[index].Attempt3Score);
                    _frames[index].TotalScore += frameScore;
                    CheckPreviousBowls(frameScore, index);
                }
            }

            PrintFrames();
            Console.ReadLine();
        }

        private static void PrintFrames()
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("-  1  -  2  -  3  -  4  -  5  -  6  -  7  -  8  -  9  -  10   -");
            Console.WriteLine("---------------------------------------------------------------");
            Console.Write("-");
            
            for (int i = 0; i < _frames.Count - 1; i++)
            {
                Console.Write(" ");
                Console.Write(_frames[i].Attempt1Score);
                Console.Write("-");
                Console.Write(_frames[i].Attempt2Score);
                Console.Write(" -");
            }

            Console.Write(" ");
            Console.Write(_frames[_frames.Count - 1].Attempt1Score);
            Console.Write("-");
            Console.Write(_frames[_frames.Count - 1].Attempt2Score);
            Console.Write("-");
            Console.Write(_frames[_frames.Count - 1].Attempt3Score);
            Console.Write(" -\n");

            Console.Write("-");
            for (int i = 0; i < _frames.Count - 1; i++)
            {
                if (_frames[i].TotalScore < 10)
                {
                    Console.Write("  ");
                    Console.Write(_frames[i].TotalScore);
                    Console.Write("  -");
                }
                else if (_frames[i].TotalScore < 100)
                {
                    Console.Write("  ");
                    Console.Write(_frames[i].TotalScore);
                    Console.Write(" -");
                }
                else
                {
                    Console.Write(" ");
                    Console.Write(_frames[i].TotalScore);
                    Console.Write(" -");
                }
            }

            Console.Write("  ");
            Console.Write(_frames[_frames.Count - 1].TotalScore);
            Console.Write("  -\n");

            Console.WriteLine("---------------------------------------------------------------");
        }

        private static void CheckPreviousBowls(int frameScore, int currentFrame)
        {
            List<int> framesToAdd = new List<int>(_additionalBowlsByFrameId.Keys);

            for (int i = 0; i < framesToAdd.Count; i++)
            {
                _frames[framesToAdd[i]].TotalScore += frameScore;
                _frames[currentFrame].TotalScore += frameScore;

                _additionalBowlsByFrameId[framesToAdd[i]] -= 1;

                if (_additionalBowlsByFrameId[framesToAdd[i]] == 0)
                {
                    _additionalBowlsByFrameId.Remove(framesToAdd[i]);
                }

                if (currentFrame - framesToAdd[i] > 1)
                {
                    _frames[framesToAdd[i + 1]].TotalScore += frameScore;
                }
            }
        }
    }
}
