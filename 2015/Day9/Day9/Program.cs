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
        private static int _LongestRouteDistance = -1;
        private static int _ShortestRouteDistance = -1;

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
            string FilePath = string.Empty;

            Console.Write("Merry Christmas, Santa! What's the file path for your map?  ");
            FilePath = Console.ReadLine();

            try
            {
                BuildMap(FilePath);
                List<LineSegment> Route = GetSantasRoute();
                Console.WriteLine(string.Format("The shortest distance you'll need to travel is {0} unspecified units.", _ShortestRouteDistance));
                Route = GetSantasScenicRoute();
                Console.WriteLine(string.Format("The longest distance you can travel is {0} unspecified units.", _LongestRouteDistance));
            }
            catch(Exception ex)
            {
                Console.WriteLine("Who gave you these directions?");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
        #endregion Main

        #region AddNextStop
        private static void AddNextStop(ref List<LineSegment> route, ref List<LineSegment> map, bool useScenicCalculation)
        {
            string StartingCity = route.FirstOrDefault().Points.FirstOrDefault();
            string CurrentCity = route.LastOrDefault().Points.LastOrDefault();
            string[] TakenCities = route.SelectMany(s => s.Points).Distinct().ToArray();
            IEnumerable<LineSegment> PossibleNextStops = map.Where(x => IsValidNextStop(x, CurrentCity, TakenCities)); //and other point isn't already in the map.             
           
            if(PossibleNextStops.Count() > 0)
            {
                LineSegment NextStop = new LineSegment();
                if(useScenicCalculation)
                {
                    NextStop = PossibleNextStops.OrderByDescending(c => c.Distance).FirstOrDefault();
                }
                else
                {
                    NextStop = PossibleNextStops.OrderBy(c => c.Distance).FirstOrDefault();
                }
                string NextCity = NextStop.Points.Where(c => !c.Equals(CurrentCity)).FirstOrDefault();
                route.Add(new LineSegment()
                                    {
                                        Points =  new string[] {CurrentCity, NextCity},
                                        Distance = NextStop.Distance
                                    }); 
                map.Remove(NextStop);
            }
        }
        #endregion AddNextStop

        #region CloneMap
        private static List<LineSegment> CloneMap(List<LineSegment> map)
        {
            List<LineSegment> Result = new List<LineSegment>();
            foreach(LineSegment l in map)
            {
                Result.Add(new LineSegment()
                                    {
                                        Points =  new string[] {l.Points[0], l.Points[1]},
                                        Distance = l.Distance
                                    });
            }
            return Result;
        }
        #endregion CloneMap

        #region GetSantasRoute
        private static List<LineSegment> GetSantasRoute()
        {
            List<LineSegment> Result = new List<LineSegment>();

            foreach(LineSegment firstStop in _Map)
            {
                List<LineSegment> Map = CloneMap(_Map);
                List<LineSegment> Route = new List<LineSegment>();
                Route.Add(firstStop);
                Map.Remove(firstStop);
                GetRoute(ref Route, ref Map, Cities.Count());

                if(_ShortestRouteDistance < 0 || Route.Sum(x => x.Distance) < _ShortestRouteDistance)
                {
                    Result = Route;
                    _ShortestRouteDistance = Result.Sum(x => x.Distance);
                }
            }

            return Result;
        }
        #endregion GetSantasRoute

        #region GetSantasScenicRoute
        private static List<LineSegment> GetSantasScenicRoute()
        {
            List<LineSegment> Result = new List<LineSegment>();

            foreach(LineSegment firstStop in _Map)
            {
                List<LineSegment> Map = CloneMap(_Map);
                List<LineSegment> Route = new List<LineSegment>();
                Route.Add(firstStop);
                Map.Remove(firstStop);
                GetRoute(ref Route, ref Map, Cities.Count());

                if(_LongestRouteDistance < 0 || _LongestRouteDistance < Route.Sum(x => x.Distance) )
                {
                    Result = Route;
                    _LongestRouteDistance = Result.Sum(x => x.Distance);
                }
            }

            return Result;
        }
        #endregion GetSantasScenicRoute

        #region GetRoute
        private static void GetRoute(ref List<LineSegment> route, ref List<LineSegment> map, int numberOfStops, bool isScenic = false)
        {
            int LastStopNumber = numberOfStops-1;
            if(route.Count < numberOfStops)
            {
                //add a stop
                GetRoute(ref route, ref map, LastStopNumber, isScenic);
            }
            
            AddNextStop(ref route, ref map, isScenic);            
            
        }
        #endregion GetRoute
        
        #region IsValidNextStop
        private static bool IsValidNextStop(LineSegment stop, string currentCity, string[] takenCities)
        {
            bool HasCurrentCity = false;
            bool NoOtherTakenCities = true;

            foreach(string city in stop.Points)
            {
                if(city.Equals(currentCity))
                {
                    HasCurrentCity = true;
                }
                else if(takenCities.Contains(city))
                {
                    NoOtherTakenCities = false;
                }
            }
            
            return HasCurrentCity && NoOtherTakenCities;
        }
        #endregion IsValidNextStop

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
