using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016Day4
{    class Day4
    {
        struct Room
        {
            public string EncryptedName;
            public string Checksum;
            public int SectorId;
        }
        static void Main(string[] args)
        {
            string FilePath = string.Empty;
            List<string> InputList = new List<string>();
            List<Room> RoomList = new List<Room>();
            int Result = 0;

            //Get Input
            Console.Write("Merry Christmas! What's the file path of the rooms?  ");
            FilePath = Console.ReadLine();
            InputList = GetInputFromFile(FilePath);

            if (InputList.Count > 0)
            {
                char[] Integers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                foreach (string roomString in InputList)
                {
                    if (!string.IsNullOrWhiteSpace(roomString))
                    {
                        int SectorIdCharIndex = roomString.IndexOfAny(Integers);
                        int ChecksumCharIndex = roomString.IndexOf('[');

                        Room room = new Room();
                        room.EncryptedName = roomString.Substring(0, SectorIdCharIndex).Replace("-", string.Empty);
                        room.Checksum = roomString.Substring(ChecksumCharIndex + 1, 5);
                        room.SectorId = Convert.ToInt32(roomString.Substring(SectorIdCharIndex, ChecksumCharIndex - SectorIdCharIndex));

                        RoomList.Add(room);
                    }
                }

                RemoveInvalidRoomsFromList(ref RoomList);

                Result = RoomList.Sum(r => r.SectorId);

                Console.WriteLine("The Sector Id Sum is " + Result.ToString());
            }
            else
            {
                Console.WriteLine("You've been led on a wild east egg hunt.");
            }

            Console.Read();
        }

        private static void RemoveInvalidRoomsFromList(ref List<Room> roomList)
        {
            for(int i= 0; i < roomList.Count(); i++)
            {
                Room room = roomList[i];
                string CalculatedChecksum = new string(room.EncryptedName.GroupBy(c => c).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Take(5).Select(g => g.Key).ToArray());
                if(!room.Checksum.Equals(CalculatedChecksum, StringComparison.OrdinalIgnoreCase))
                {
                    roomList.Remove(room);
                    i--;
                }
            }
        }

        private static List<string> GetInputFromFile(string path)
        {
            List<string> Result = new List<string>();

            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string Input = reader.ReadToEnd();
                        Result = Input.Split(new string[] { Environment.NewLine, "\r", "\n" }, StringSplitOptions.None).ToList();
                    }
                }
            }
            catch
            {
                //pretend nothing happened, because that's totally a great idea.
            }

            return Result;
        }
    }
}
