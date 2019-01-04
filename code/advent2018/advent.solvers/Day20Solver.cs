using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day20Solver : Solver
    {
        public string ProblemName => "A Regular Map";

        private readonly DataProvider<string> dataProvider;

        public Day20Solver(DataProvider<string> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var mapBuilder = new MapBuilder();
            var regexParser = new RegexParser(mapBuilder);
            regexParser.Parse(dataProvider.GetData());
            var distanceCalculator = new DistanceCalculator(mapBuilder.Rooms());
            return distanceCalculator.FarthestRoom().Distance.ToString();
        }

        public string SolveSecondPart()
        {
            var mapBuilder = new MapBuilder();
            var regexParser = new RegexParser(mapBuilder);
            regexParser.Parse(dataProvider.GetData());
            var distanceCalculator = new DistanceCalculator(mapBuilder.Rooms());
            return distanceCalculator.RoomsWithDistanceMoreThan(1000).ToString();
        }

        private class MapBuilder
        {
            private readonly Dictionary<(int X, int Y), RoomDoors> rooms;

            internal MapBuilder()
            {
                rooms = new Dictionary<(int X, int Y), RoomDoors>();
            }

            internal void AddRoom((int X, int Y) position)
            {
                rooms.Add(position, RoomDoors.None);
            }

            internal (int X, int Y) AddDoor((int X, int Y) position, RoomDoors direction)
            {
                (int X, int Y) newRoom;
                switch (direction)
                {
                    case RoomDoors.North:
                        newRoom = (position.X, position.Y - 1);
                        AddRoomAndDoor(position, RoomDoors.North);
                        AddRoomAndDoor(newRoom, RoomDoors.South);
                        return newRoom;
                    case RoomDoors.South:
                        newRoom = (position.X, position.Y + 1);
                        AddRoomAndDoor(position, RoomDoors.South);
                        AddRoomAndDoor(newRoom, RoomDoors.North);
                        return newRoom;
                    case RoomDoors.East:
                        newRoom = (position.X + 1, position.Y);
                        AddRoomAndDoor(position, RoomDoors.East);
                        AddRoomAndDoor(newRoom, RoomDoors.West);
                        return newRoom;
                    case RoomDoors.West:
                        newRoom = (position.X - 1, position.Y);
                        AddRoomAndDoor(position, RoomDoors.West);
                        AddRoomAndDoor(newRoom, RoomDoors.East);
                        return newRoom;
                }

                return position;
            }

            private void AddRoomAndDoor((int X, int Y) position, RoomDoors newDoor)
            {
                if (rooms.ContainsKey(position))
                    rooms[position] |= newDoor;
                else
                    rooms[position] = newDoor;
            }

            public IDictionary<(int X, int Y), RoomDoors> Rooms()
            {
                return rooms;
            }
        }

        private class DistanceCalculator
        {
            private readonly IDictionary<(int X, int Y), RoomDoors> rooms;
            private readonly IDictionary<(int X, int Y), int> distances;

            internal DistanceCalculator(IDictionary<(int X, int Y), RoomDoors> rooms)
            {
                this.rooms = rooms;
                distances = new Dictionary<(int X, int Y), int>();
                ComputeDistances();
            }

            private void ComputeDistances()
            {
                foreach (var (room, doors) in rooms)
                {
                    if (room == (0, 0))
                    {
                        distances.Add(room, 0);
                        continue;
                    }

                    var smallestDistance = int.MaxValue;
                    if (doors.HasFlag(RoomDoors.North))
                    {
                        var distance = GetDistance(room.X, room.Y - 1);
                        if (distance != null &&
                            distance.Value < smallestDistance)
                            smallestDistance = distance.Value;
                    }

                    if (doors.HasFlag(RoomDoors.South))
                    {
                        var distance = GetDistance(room.X, room.Y + 1);
                        if (distance != null &&
                            distance.Value < smallestDistance)
                            smallestDistance = distance.Value;
                    }

                    if (doors.HasFlag(RoomDoors.East))
                    {
                        var distance = GetDistance(room.X + 1, room.Y);
                        if (distance != null &&
                            distance.Value < smallestDistance)
                            smallestDistance = distance.Value;
                    }

                    if (doors.HasFlag(RoomDoors.West))
                    {
                        var distance = GetDistance(room.X - 1, room.Y);
                        if (distance != null &&
                            distance.Value < smallestDistance)
                            smallestDistance = distance.Value;
                    }
                    
                    distances.Add(room, smallestDistance + 1);
                }
            }

            internal (int X, int Y, int Distance) FarthestRoom()
            {
                var ((x, y), value) = distances.OrderByDescending(kv => kv.Value).First();
                return (x, y, value);
            }

            internal int RoomsWithDistanceMoreThan(int doors)
            {
                return distances.Count(kv => kv.Value >= doors);
            }

            private int? GetDistance(int x, int y)
            {
                if (distances.ContainsKey((x, y)))
                    return distances[(x, y)];
                return null;
            }
        }

        private class RegexParser
        {
            private readonly Stack<(int X, int Y)> stack;
            private readonly MapBuilder builder;

            internal RegexParser(MapBuilder builder)
            {
                stack = new Stack<(int X, int Y)>();
                this.builder = builder;
            }

            internal void Parse(string regex)
            {
                foreach (var character in regex)
                    Parse(character);
            }

            private void Parse(char character)
            {
                (int X, int Y) current;
                switch (character)
                {
                    case '^':
                        current = (0, 0);
                        stack.Push(current);
                        builder.AddRoom(current);
                        break;
                    case '$':
                        stack.Pop();
                        break;
                    case '(':
                        current = stack.Pop();
                        stack.Push(current);
                        stack.Push(current);
                        break;
                    case '|':
                        stack.Pop();
                        current = stack.Pop();
                        stack.Push(current);
                        stack.Push(current);
                        break;
                    case ')':
                        stack.Pop();
                        break;
                    case 'N':
                        current = stack.Pop();
                        current = builder.AddDoor(current, RoomDoors.North);
                        stack.Push(current);
                        break;
                    case 'E':
                        current = stack.Pop();
                        current = builder.AddDoor(current, RoomDoors.East);
                        stack.Push(current);
                        break;
                    case 'W':
                        current = stack.Pop();
                        current = builder.AddDoor(current, RoomDoors.West);
                        stack.Push(current);
                        break;
                    case 'S':
                        current = stack.Pop();
                        current = builder.AddDoor(current, RoomDoors.South);
                        stack.Push(current);
                        break;
                }
            }
        }

        [Flags]
        private enum RoomDoors
        {
            None = 0,
            North = 1,
            South = 2,
            East = 4,
            West = 8
        }
    }
}