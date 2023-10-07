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

        private bool EvaluateCellValue(IntegerPoint point)
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
                        var value = CellValue(currentCellPosition);

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
        private bool CellValue(IntegerPoint point)
        {
            if (_cells.ContainsKey(point))
            {
                return _cells[point];
            }
            else
            {
                return EvaluateCellValue(point);
            }
        }
        private void Evaluate()
        {
            ForEach((int x, int y) =>
            {
                var point = new IntegerPoint(x, y);

                if (!_cells.ContainsKey(point))
                {
                    EvaluateCellValue(point);
                }
            });
        }
        private void ForEach(Action<int, int> action)
        {
            for (int x = _start.X; x <= _tableSize.Width; x++)
            {
                for (int y = _start.Y; y <= _tableSize.Height; y++)
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
