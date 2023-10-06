using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace gameBackTraverse
{
    public record struct IntegerPoint(int X, int Y);
    public record struct IntegerSize(int Width, int Height);

    public class Table
    {
        private Dictionary<IntegerPoint, bool> _cells;
        private IntegerSize _tableSize;
        private Func<IntegerPoint, IntegerPoint, bool> _ableToMove;

        public bool this[IntegerPoint point]
        {
            get
            {
                return _cells[point];
            }
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
            for (int x = 1; x <= _tableSize.Width; x++)
            {
                for (int y = 1; y <= _tableSize.Height; y++)
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

        public Table(Dictionary<IntegerPoint, bool> initial, IntegerSize tableSize, Func<IntegerPoint, IntegerPoint, bool> ableToMove)
        {
            _cells = initial;
            _tableSize = tableSize;
            _ableToMove = ableToMove;

            Evaluate();
        }
    }
    public partial class MainWindow : Window
    {
        private void Draw(Table table)
        {
            table.ForEach((IntegerPoint point, bool value) =>
            {
                var label = new Label();
                label.Content = value ? "+" : "-";
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;

                var borderWidth = 1;

                Thickness thickness = new Thickness(0, 0, borderWidth, borderWidth);

                if (point.X == 1)
                {
                    thickness.Left = borderWidth;
                }
                if (point.Y == table.Size.Height)
                {
                    thickness.Top = borderWidth;
                }

                label.SetValue(BorderThicknessProperty, thickness);
                label.SetValue(BorderBrushProperty, new SolidColorBrush(Colors.Black));
                label.SetValue(FontSizeProperty, 24.0);

                label.SetValue(Grid.ColumnProperty, point.X - 1);
                label.SetValue(Grid.RowProperty, table.Size.Height - point.Y);

                grid.Children.Add(label);
            });
        }

        public MainWindow()
        {
            InitializeComponent();

            var table = new Table(new Dictionary<IntegerPoint, bool> { { new IntegerPoint(8, 8), true } }, new IntegerSize(8, 8), (IntegerPoint start, IntegerPoint end) =>
            {
                return (start.X == end.X && (start.Y < end.Y)) || (start.Y == end.Y && (start.X < end.X));
            });

            Draw(table);
        }
    }
}
