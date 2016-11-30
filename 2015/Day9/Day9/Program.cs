using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        #region LineSegment
        public struct LineSegment
        {
            public string[] Points;
            public int Distance;
        }
        #endregion LineSegment

        #region Member Variables
        public static List<LineSegment> _Map = new List<LineSegment>();
        private static List<string> _Cities = new List<string>();

        public static List<string> Cities
        {
            get
            {
                if (_Cities.Count == 0)
                {
                    _Cities = _Map.SelectMany(x => x.Points).Distinct().ToList();
                }
                return _Cities;
            }
            private set { _Cities = value; }
        }
        #endregion Member Variables

        #region Main
        static void Main(string[] args)
        {
            int Result = -1;
            string FilePath = string.Empty;

            Console.Write("Merry Christmas, Santa! What's the file path for your map?  ");
            FilePath = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                BuildMap(FilePath);
                Result = GetShortestRouteDistance();
            }

            if (Result > 0)
            {
                Console.WriteLine(string.Format("The shortest distance you'll need to travel is {0} unspecified units.", Result));
            }

            Console.Read();
        }
        #endregion Main

        #region GetShortestRouteDistance
        private static int GetShortestRouteDistance()
        {
            int Result = -1;
            foreach (string city in Cities)
            {
                LineSegment[] PossibleStartingLines = _Map.Where(x => x.Points.Contains(city)).ToArray();
                foreach (LineSegment line in PossibleStartingLines)
                {
                    List<string> Route = new List<string>() { line.Points[0], line.Points[1] };
                    string CurrentStop = city;
                    string NextStop = line.Points.Where(x => !x.Equals(city)).FirstOrDefault();
                    int RouteDistance = line.Distance;
                    int CityCount = Cities.Count();

                    while (Route.Count < CityCount)
                    {
                        LineSegment[] PossibleNextStops = _Map.Where(x => x.Points.Contains(NextStop) && !x.Points.Contains(CurrentStop)).ToArray();

                        if (PossibleNextStops.Count() == 1)
                        {
                            CurrentStop = NextStop;
                            NextStop = PossibleNextStops[0].Points.Where(x => !x.Equals(CurrentStop)).FirstOrDefault();
                            if (!Route.Contains(NextStop))
                            {
                                Route.Add(NextStop);
                                RouteDistance += PossibleNextStops[0].Distance;
                            }
                        }
                        else
                        {
                            var test = "crap.";
                        }
                    }

                    if (Result < 0 || RouteDistance < Result)
                    {
                        Result = RouteDistance;
                    }
                }
            }
            return Result;

        }
        #endregion GetShortestRouteDistance
        

        #region BuildMap
        private static void BuildMap(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while(!reader.EndOfStream)
                {
                    string[] Line = reader.ReadLine().Split(new string[] { " to ", " = " }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
                    if(Line.Length == 3)
                    {
                        LineSegment Segment = new LineSegment()
                        {
                            Points =  new string[] {Line[0], Line[1]},
                            Distance = Convert.ToInt32(Line[2])
                        };
                        _Map.Add(Segment);
                    }
                }
            }
        }
        #endregion BuildMap

    }
}
