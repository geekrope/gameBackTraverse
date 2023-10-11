using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace gameBackTraverse
{
    public class Tasks
    {
        public static Table Task2Prototype(IntegerPoint[] deadPoints)
        {
            return new Table(new IntegerPoint(1, 1), new IntegerPoint(8, 8), (IntegerPoint point) =>
            {
                var possibleMoves = new List<IntegerPoint>();

                for (int x = point.X + 1; x <= 8; x++)
                {
                    var able = true;

                    foreach (var deadPoint in deadPoints)
                    {
                        if (point.X < deadPoint.X && x >= deadPoint.X && point.Y == deadPoint.Y)
                        {
                            able = false;
                        }
                    }

                    if (able)
                    {
                        possibleMoves.Add(new IntegerPoint(x, point.Y));
                    }
                }

                for (int y = point.Y + 1; (y - point.Y <= 3) && y <= 8; y++)
                {
                    var able = true;

                    foreach (var deadPoint in deadPoints)
                    {
                        if (point.Y < deadPoint.Y && y >= deadPoint.Y && point.X == deadPoint.X)
                        {
                            able = false;
                        }
                    }

                    if (able)
                    {
                        possibleMoves.Add(new IntegerPoint(point.X, y));
                    }
                }

                return possibleMoves.ToArray();
            }, deadPoints, (IntegerPoint point) => { return point.X == 8 && point.Y == 8; });
        }
        public static (Table table, IntegerPoint[] deadPoints) Task2A()
        {
            var deadPoints = new IntegerPoint[] { new IntegerPoint(8, 4) };

            return (Task2Prototype(deadPoints), deadPoints);
        }
        public static (Table table, IntegerPoint[] deadPoints) Task2B()
        {
            var deadPoints = new IntegerPoint[] { new IntegerPoint(8, 4), new IntegerPoint(3, 3) };

            return (Task2Prototype(deadPoints), deadPoints);
        }
        public static Table Task3()
        {
            return new Table(new IntegerPoint(0, 0), new IntegerPoint(10, 10), (IntegerPoint point) =>
            {
                var possibleMoves = new List<IntegerPoint>();

                if (point.X + 1 <= 10 && point.Y + 3 <= 10)
                {
                    possibleMoves.Add(new IntegerPoint(point.X + 1, point.Y + 3));
                }
                if (point.Y + 2 <= 10 && point.X + 2 <= 10)
                {
                    possibleMoves.Add(new IntegerPoint(point.X + 2, point.Y + 2));
                }
                if (point.X + 3 <= 10 && point.Y + 1 <= 10)
                {
                    possibleMoves.Add(new IntegerPoint(point.X + 3, point.Y + 1));
                }

                for (int x = point.X + 1; x <= 10; x++)
                {
                    possibleMoves.Add(new IntegerPoint(x, point.Y));
                }
                for (int y = point.Y + 1; y <= 10; y++)
                {
                    possibleMoves.Add(new IntegerPoint(point.X, y));
                }

                return possibleMoves.ToArray();
            }, (IntegerPoint point) => { return point.X == 10 && point.Y == 10; });
        }
        public static Table Task4()
        {
            return new Table(new IntegerPoint(2, 4), new IntegerPoint(22, 22), (IntegerPoint point) =>
            {
                return new IntegerPoint[] { new IntegerPoint(2 * point.X + 1, point.Y), new IntegerPoint(point.X, 2 * point.Y + 1), new IntegerPoint(point.X + 3, point.Y), new IntegerPoint(point.X, point.Y + 3) };
            }, (IntegerPoint point) => { return point.X + point.Y >= 22; });
        }

        public static Table Test()
        {
            return new Table(new IntegerPoint(0, 0), new IntegerPoint(16, 16), (IntegerPoint point) =>
            {
                var possibleMoves = new List<IntegerPoint> { new IntegerPoint(point.X + 1, point.Y), new IntegerPoint(point.X, point.Y + 1) };

                if (point.X != 0)
                {
                    possibleMoves.Add(new IntegerPoint(point.X * 3, point.Y));
                }
                if (point.Y != 0)
                {
                    possibleMoves.Add(new IntegerPoint(point.X, point.Y * 3));
                }

                return possibleMoves.ToArray();
            }, (IntegerPoint point) => { return point.X + point.Y >= 16; });
        }
    }
}
