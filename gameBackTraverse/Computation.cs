using System;
using System.Collections.Generic;

namespace gameBackTraverse
{
    public record struct IntegerPoint(int X, int Y);
    public record struct IntegerSize(int Width, int Height);

    public class Table
    {
        private Dictionary<IntegerPoint, bool> _cells;
        private IntegerPoint _start;
        private IntegerSize _tableSize;
        private Func<IntegerPoint, IntegerPoint, bool> _ableToMove;

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
        public IntegerSize Size
        {
            get => _tableSize;
        }

        private bool EvaluateOutcomes(IntegerPoint point)
        {
            var result = true;

            ForEach((int x, int y) =>
            {
                var currentCellPosition = new IntegerPoint(x, y);

                if (currentCellPosition != point)
                {
                    var ableToMove = _ableToMove(point, currentCellPosition);

                    if (ableToMove)
                    {
                        var value = GetOutcome(currentCellPosition);

                        if (value)
                        {
                            result = false;
                        }
                    }
                }
            });

            _cells.Add(point, result);

            return result;
        }
        private bool GetOutcome(IntegerPoint point)
        {
            if (_cells.ContainsKey(point))
            {
                return _cells[point];
            }
            else
            {
                return EvaluateOutcomes(point);
            }
        }
        private void Evaluate()
        {
            ForEach((int x, int y) =>
            {
                var point = new IntegerPoint(x, y);

                if (!_cells.ContainsKey(point))
                {
                    EvaluateOutcomes(point);
                }
            });
        }
        private void ForEach(Action<int, int> action)
        {
            for (int x = _start.X; x < _tableSize.Width + _start.X; x++)
            {
                for (int y = _start.Y; y < _tableSize.Height + _start.Y; y++)
                {
                    action(x, y);
                }
            }
        }

        public void ForEach(Action<IntegerPoint, bool> action)
        {
            foreach (var cell in _cells)
            {
                action(cell.Key, cell.Value);
            }
        }

        public Table(Dictionary<IntegerPoint, bool> initial, IntegerPoint start, IntegerSize tableSize, Func<IntegerPoint, IntegerPoint, bool> ableToMove)
        {
            _cells = initial;
            _start = start;
            _tableSize = tableSize;
            _ableToMove = ableToMove;

            Evaluate();
        }
    }
}
