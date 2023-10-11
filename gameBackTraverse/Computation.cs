using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace gameBackTraverse
{
    public record struct IntegerPoint(int X, int Y);
    public record struct IntegerSize(int Width, int Height);

    public class Table
    {
        private Dictionary<IntegerPoint, bool> _cells;
        private HashSet<IntegerPoint> _deadPoints;
        private IntegerPoint _start;
        private IntegerPoint _end;
        private IntegerSize _size;
        private Func<IntegerPoint, IntegerPoint[]> _possibleMoves;
        private Func<IntegerPoint, bool> _winningCondition;

        public bool this[IntegerPoint point]
        {
            get
            {
                return _cells[point];
            }
        }
        public IntegerPoint Start
        {
            get => _start;
        }
        public IntegerPoint End
        {
            get => _end;
        }
        public IntegerSize Size
        {
            get => _size;
        }

        /// <summary>
        /// Gets weather point is "dead" or not
        /// </summary>
        private bool IsDeadPoint(IntegerPoint point)
        {
            return _deadPoints.Contains(point);
        }
        /// <summary>
        /// Gets weather point belongs to table or not
        /// </summary>
        private bool IsInTable(IntegerPoint point)
        {
            return point.X >= Start.X && point.X <= End.X && point.Y >= Start.Y && point.Y <= End.Y;
        }
        /// <summary>
        /// Evaluates outcome of concrete point
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Return value if point is not "dead". Returns null otherwise</returns>
        private bool? EvaluateOutcome(IntegerPoint point)
        {
            if (IsDeadPoint(point))
            {
                return null;
            }
            else
            {
                var winning = _winningCondition(point);

                if (winning)
                {
                    if (IsInTable(point))
                    {
                        _cells.Add(point, true);
                    }

                    return true;
                }
                else
                {
                    var result = true;
                    var possibleMoves = _possibleMoves(point);

                    foreach (var possibleMove in possibleMoves)
                    {
                        var value = GetOutcome(possibleMove);

                        if (value.HasValue && value.Value)
                        {
                            result = false;
                        }
                    }

                    _cells.Add(point, result);

                    return result;
                }
            }
        }
        /// <summary>
        /// Gets from dictionary or evaluate concrete point outcome
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Return value if point is not "dead". Returns null otherwise</returns>
        private bool? GetOutcome(IntegerPoint point)
        {
            if (IsDeadPoint(point))
            {
                return null;
            }
            else
            {
                if (_cells.ContainsKey(point))
                {
                    return _cells[point];
                }
                else
                {
                    return EvaluateOutcome(point);
                }
            }
        }
        /// <summary>
        /// Evaluates outcomes for all points in table
        /// </summary>
        private void Evaluate()
        {
            ForEach((int x, int y) =>
            {
                var point = new IntegerPoint(x, y);

                if (!_cells.ContainsKey(point))
                {
                    EvaluateOutcome(point);
                }
            });
        }
        /// <summary>
        /// Traverse all points of table except of "dead" points
        /// </summary>
        private void ForEach(Action<int, int> action)
        {
            for (int x = _start.X; x <= _end.X; x++)
            {
                for (int y = _start.Y; y <= _end.Y; y++)
                {
                    if (!IsDeadPoint(new IntegerPoint(x, y)))
                    {
                        action(x, y);
                    }
                }
            }
        }

        /// <summary>
        /// Traverse all points that belong to table
        /// </summary>
        public void ForEach(Action<IntegerPoint, bool> action)
        {
            foreach (var cell in _cells)
            {
                action(cell.Key, cell.Value);
            }
        }

        public Table(IntegerPoint start, IntegerPoint end, Func<IntegerPoint, IntegerPoint[]> possibleMoves, Func<IntegerPoint, bool> winningCondition)
        {
            _cells = new Dictionary<IntegerPoint, bool>();
            _start = start;
            _end = end;
            _size = new IntegerSize(_end.X - _start.X + 1, _end.Y - _start.Y + 1);
            _possibleMoves = possibleMoves;
            _deadPoints = new HashSet<IntegerPoint>();
            _winningCondition = winningCondition;

            Evaluate();
        }
        public Table(IntegerPoint start, IntegerPoint end, Func<IntegerPoint, IntegerPoint[]> possibleMoves, IntegerPoint[] deadPoints, Func<IntegerPoint, bool> winningCondition)
        {
            _cells = new Dictionary<IntegerPoint, bool>();
            _start = start;
            _end = end;
            _size = new IntegerSize(_end.X - _start.X + 1, _end.Y - _start.Y + 1);
            _possibleMoves = possibleMoves;
            _deadPoints = deadPoints.ToHashSet();
            _winningCondition = winningCondition;

            Evaluate();
        }
    }
}
