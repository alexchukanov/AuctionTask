using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Test task: (http://qlogic.io/)
// At auction there are possible the case when a participant can flip (turn up down) his poster with a number.
// You need to check this case to avoid such kind of mistakes.
namespace AuctionTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxNumParticipants = 1000;

            Auction auction = new Auction();

            //register participants
            for (int i = 1; i < maxNumParticipants; i++)
            {
                Board board = new Board(i);
                auction.AddParticipant(new Participant(board));
            }

            //check participants numbers
            for (int p = 1; p < maxNumParticipants; p++)
            {
                int realNumber = auction.CheckParticipantNumber(p);

                if (p == realNumber)
                {
                    Console.WriteLine($"Shown number: {p} real");
                }
                else
                {
                    Console.WriteLine($"Shown number: {p} = {realNumber} flipped");
                }
            }

            Console.ReadLine();
        }

        public class Auction
        {
            public List<Participant> ParticipantList { get; set; } = new List<Participant>();

            public void AddParticipant(Participant participant)
            {
                if (participant.BoardBadge.NumberId == 69)
                {
                    {
                    }
                }


                int flippedNumber = participant.BoardBadge.GetFlippedNumber();



                if (flippedNumber == 0 || participant.BoardBadge.NumberId <= flippedNumber)
                {
                    ParticipantList.Add(participant);
                }
            }

            public int CheckParticipantNumber(int numberId)
            {
                int participantNumber = 0; // alien

                if (ParticipantList.Exists(p => p.BoardBadge.NumberId == numberId))
                {
                    participantNumber = numberId;
                }
                else
                {
                    var participant = ParticipantList.FirstOrDefault(p => p.ShowFlippedNumber() == numberId);

                    if (participant != null)
                    {
                        participantNumber = participant.BoardBadge.NumberId;
                    }
                }

                return participantNumber;
            }
        }


        public class Participant
        {
            public Board BoardBadge
            {
                get;
            }

            public Participant(Board board)
            {
                BoardBadge = board;
            }

            public int ShowNumber()
            {
                return BoardBadge.NumberId;
            }

            public int ShowFlippedNumber()
            {
                return BoardBadge.GetFlippedNumber();
            }
        }

        public class Board
        {
            public Board(int number)
            {
                NumberId = number;
            }

            public int NumberId
            {
                get;
            }

            public int GetFlippedNumber()
            {
                int flippedNumber = 0;

                string numStr = NumberId.ToString();

                if (CanNumberFlip(NumberId))
                {
                    char temp = '_';
                    char[] numArr = numStr.ToCharArray();

                    //reverse
                    for (int i = 0; i < numArr.Length / 2; i++)
                    {
                        temp = numArr[i];
                        numArr[i] = numArr[numArr.Length - 1 - i];
                        numArr[numArr.Length - 1 - i] = temp;
                    }

                    //flipping 6 and 9
                    for (int i = 0; i < numArr.Length; i++)
                    {
                        if (numArr[i] == '9')
                        {
                            numArr[i] = '6';
                        }
                        else if (numArr[i] == '6')
                        {
                            numArr[i] = '9';
                        }
                    }

                    flippedNumber = int.Parse(new string(numArr));
                }

                return flippedNumber;
            }

            private bool CanNumberFlip(int number)
            {
                string numStr = number.ToString();

                bool canFlip = !(numStr.Contains('2') || numStr.Contains('3') || numStr.Contains('4') || numStr.Contains('5') || numStr.Contains('7') || number % 10 == 0);

                return canFlip;
            }
        }
    }
}
