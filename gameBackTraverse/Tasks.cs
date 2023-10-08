using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace gameBackTraverse
{
    public class Tasks
    {
        public static (Table table, IntegerPoint[] deadPoints) Task2A()
        {
            return (new Table(new Dictionary<IntegerPoint, bool> { { new IntegerPoint(8, 8), true } }, new IntegerPoint(1, 1), new IntegerPoint(8, 8), (IntegerPoint start, IntegerPoint end) =>
            {
                var generalConidition = (start.X == end.X && (start.Y < end.Y) && (end.Y - start.Y <= 3)) || (start.Y == end.Y && (start.X < end.X));
                var passThroughDeadPoint = (start.X == 8 && end.X == 8) && ((start.Y < 4 && end.Y > 4) || end.Y == 4);

                return generalConidition && !passThroughDeadPoint;
            }), new IntegerPoint[] { new IntegerPoint(8, 4) });
        }
        public static (Table table, IntegerPoint[] deadPoints) Task2B()
        {
            return (new Table(new Dictionary<IntegerPoint, bool> { { new IntegerPoint(8, 8), true } }, new IntegerPoint(1, 1), new IntegerPoint(8, 8), (IntegerPoint start, IntegerPoint end) =>
            {
                var generalConidition = (start.X == end.X && (start.Y < end.Y) && (end.Y - start.Y <= 3)) || (start.Y == end.Y && (start.X < end.X));
                var passThroughDeadPoint = (start.X == 8 && end.X == 8) && ((start.Y < 4 && end.Y > 4) || end.Y == 4);
                var passThroughDeadPoint2 = (start.X == 3 && end.X == 3) && ((start.Y < 3 && end.Y > 3) || end.Y == 3) || (start.Y == 3 && end.Y == 3) && ((start.X < 3 && end.X > 3) || end.X == 3);

                return generalConidition && !passThroughDeadPoint && !passThroughDeadPoint2;
            }), new IntegerPoint[] { new IntegerPoint(8, 4), new IntegerPoint(3, 3) });
        }
        public static Table Task3()
        {
            return new Table(new Dictionary<IntegerPoint, bool> { { new IntegerPoint(10, 10), true } }, new IntegerPoint(0, 0), new IntegerPoint(10, 10), (IntegerPoint start, IntegerPoint end) =>
            {
                var condition = ((start.X == end.X) && (start.Y < end.Y)) || ((start.Y == end.Y) && (start.X < end.X)) && ((end.X - start.X + end.Y - start.Y == 4));

                return condition;
            });
        }
        public static Table Task4()
        {
            var winningOutcomes = new Dictionary<IntegerPoint, bool>();

            for (int x = 2; x <= 22; x++)
            {
                for (int y = 4; y <= 22; y++)
                {
                    if (x + y >= 22)
                    {
                        winningOutcomes.Add(new IntegerPoint(x, y), true);
                    }
                }
            }

            return new Table(winningOutcomes, new IntegerPoint(2, 4), new IntegerPoint(22, 22), (IntegerPoint start, IntegerPoint end) =>
            {
                var move1 = (end.X == start.X * 2 + 1) && (start.Y == end.Y);
                var move2 = (end.X == start.X + 3) && (start.Y == end.Y);
                var move3 = (end.Y == 2 * start.Y + 1) && (start.X == end.X);
                var move4 = (end.Y == start.Y + 3) && (start.X == end.X);
                var c = ((start.X == end.X) && (start.Y < end.Y)) || ((start.Y == end.Y) && (start.X < end.X));

                return move1 || move2 || move3 || move4;
            });
        }
    }
}
