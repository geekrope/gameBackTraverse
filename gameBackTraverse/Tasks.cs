using System;
using System.Collections.Generic;

namespace gameBackTraverse
{
    public class Tasks
    {
        public static (Table table, IntegerPoint[] deadPoints) Task2A()
        {
            return (new Table(new Dictionary<IntegerPoint, bool> { { new IntegerPoint(8, 8), true } }, new IntegerPoint(1, 1), new IntegerSize(8, 8), (IntegerPoint start, IntegerPoint end) =>
            {
                var generalConidition = (start.X == end.X && (start.Y < end.Y) && (end.Y - start.Y <= 3)) || (start.Y == end.Y && (start.X < end.X));
                var passThroughDeadPoint = (start.X == 8 && end.X == 8) && ((start.Y < 4 && end.Y > 4) || end.Y == 4);

                return generalConidition && !passThroughDeadPoint;
            }), new IntegerPoint[] { new IntegerPoint(8, 4) });
        }
        public static (Table table, IntegerPoint[] deadPoints) Task2B()
        {
            return (new Table(new Dictionary<IntegerPoint, bool> { { new IntegerPoint(8, 8), true } }, new IntegerPoint(1, 1), new IntegerSize(8, 8), (IntegerPoint start, IntegerPoint end) =>
            {
                var generalConidition = (start.X == end.X && (start.Y < end.Y) && (end.Y - start.Y <= 3)) || (start.Y == end.Y && (start.X < end.X));
                var passThroughDeadPoint = (start.X == 8 && end.X == 8) && ((start.Y < 4 && end.Y > 4) || end.Y == 4);
                var passThroughDeadPoint2 = (start.X == 3 && end.X == 3) && ((start.Y < 3 && end.Y > 3) || end.Y == 3) || (start.Y == 3 && end.Y == 3) && ((start.X < 3 && end.X > 3) || end.X == 3);

                return generalConidition && !passThroughDeadPoint && !passThroughDeadPoint2;
            }), new IntegerPoint[] { new IntegerPoint(8, 4), new IntegerPoint(3, 3) });
        }
        public static Table Task3()
        {
            return new Table(new Dictionary<IntegerPoint, bool> { { new IntegerPoint(10, 10), true } }, new IntegerPoint(0, 0), new IntegerSize(10, 10), (IntegerPoint start, IntegerPoint end) =>
            {
                var condition = ((start.X == end.X) && (start.Y < end.Y)) || ((start.Y == end.Y) && (start.X < end.X)) && ((end.X - start.X + end.Y - start.Y == 4));

                return condition;
            });
        }
    }
}
