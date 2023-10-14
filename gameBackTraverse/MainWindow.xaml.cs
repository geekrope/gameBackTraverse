using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;

namespace gameBackTraverse
{
    public partial class MainWindow : Window
    {
        private void InitializeGrid(Table table, Grid grid)
        {
            for (int column = 0; column < table.Size.Width + 1; column++)
            {
                var columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(columnDefinition);
            }
            for (int row = 0; row < table.Size.Height + 1; row++)
            {
                var rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(rowDefinition);
            }
        }
        private Label CreateLabel(object content, double fontSize, int column, int row)
        {
            var label = new Label();

            label.Content = content;

            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;

            label.SetValue(FontSizeProperty, fontSize);

            label.SetValue(Grid.ColumnProperty, column);
            label.SetValue(Grid.RowProperty, row);
            label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            var squareSide = Math.Max(label.DesiredSize.Width, label.DesiredSize.Height);
            label.Width = squareSide;
            label.Height = squareSide;

            return label;
        }
        private void CreateColumnDefinitions(NamesIterator source, Table table, Grid grid, double fontSize)
        {
            var definition = source.First();

            for (int column = 1; column <= table.Size.Width; column++)
            {
                var label = CreateLabel(definition, fontSize, column, 0);

                grid.Children.Add(label);

                definition = source.Next();
            }
        }
        private void CreateRowDefinitions(NamesIterator source, Table table, Grid grid, double fontSize)
        {
            var definition = source.First();

            for (int row = 1; row <= table.Size.Height; row++)
            {
                var label = CreateLabel(definition, fontSize, 0, row);

                grid.Children.Add(label);

                definition = source.Next();
            }
        }
        private Label DrawPoint(object content, Table table, IntegerPoint point, double fontSize, double borderWidth, Color? background = null)
        {
            Thickness thickness = new Thickness(0, 0, borderWidth, borderWidth);

            if (point.X == table.Start.X)
            {
                thickness.Left = borderWidth;
            }
            if (point.Y == table.Start.Y)
            {
                thickness.Top = borderWidth;
            }
            var label = CreateLabel(content, fontSize, point.X - table.Start.X + 1, point.Y - table.Start.Y + 1);

            label.SetValue(BorderThicknessProperty, thickness);
            label.SetValue(BorderBrushProperty, new SolidColorBrush(Colors.Black));

            if (background.HasValue)
            {
                label.Background = new SolidColorBrush(background.Value);
            }

            return label;
        }
        private void CreateTable(Table table, IntegerPoint[] deadPoints, Func<IntegerPoint, Color?> highlightPoint, double fontSize, double borderWidth)
        {
            table.ForEach((IntegerPoint point, bool value) =>
            {
                var content = value ? "+" : "-";

                grid.Children.Add(DrawPoint(content, table, point, fontSize, borderWidth, highlightPoint(point)));
            });

            foreach (var deadPoint in deadPoints)
            {
                var content = "x";

                grid.Children.Add(DrawPoint(content, table, deadPoint, fontSize, borderWidth));
            }
        }
        private void Draw(Table table, Grid grid, NamesIterator columnIterator, NamesIterator rowIterator)
        {
            Draw(table, new IntegerPoint[0], grid, columnIterator, rowIterator);
        }
        private void Draw(Table table, IntegerPoint[] deadPoints, Grid grid, NamesIterator columnIterator, NamesIterator rowIterator)
        {
            var borderWidth = 1;
            var tableFontSize = 24;
            var definitionsFontSize = 12;
            CreateColumnDefinitions(columnIterator, table, grid, definitionsFontSize);
            CreateRowDefinitions(rowIterator, table, grid, definitionsFontSize);
            CreateTable(table, deadPoints, (IntegerPoint point) => { return null; }, tableFontSize, borderWidth);
        }
        private void Draw(Table table, IntegerPoint[] deadPoints, Func<IntegerPoint, Color?> highlightPoint, Grid grid, NamesIterator columnIterator, NamesIterator rowIterator)
        {
            var borderWidth = 1;
            var tableFontSize = 24;
            var definitionsFontSize = 12;
            CreateColumnDefinitions(columnIterator, table, grid, definitionsFontSize);
            CreateRowDefinitions(rowIterator, table, grid, definitionsFontSize);
            CreateTable(table, deadPoints, highlightPoint, tableFontSize, borderWidth);
        }

        private void DrawQuestion6()
        {
            var task = Tasks.Question6();

            var quest6Set1 = new HashSet<IntegerPoint>() {
                new IntegerPoint(5, 14),
                new IntegerPoint(5, 16),
                new IntegerPoint(5, 18),
                new IntegerPoint(5, 20),
                new IntegerPoint(5, 22),
                new IntegerPoint(5, 24),
                new IntegerPoint(5, 26),
                new IntegerPoint(5, 28),
                new IntegerPoint(5, 30),
                new IntegerPoint(5, 32),
                new IntegerPoint(5, 33) };
            var quest6Set2 = new HashSet<IntegerPoint>();
            foreach (var point in quest6Set1)
            {
                var moves = task.GetPossibleMoves(point);

                foreach (var move in moves)
                {
                    quest6Set2.Add(move);
                }
            }
            Func<IntegerPoint, Color?> highlightQuestion6 = (IntegerPoint point) =>
            {
                if (quest6Set1.Contains(point))
                {
                    return Colors.OrangeRed;
                }
                else if (quest6Set2.Contains(point))
                {
                    return Colors.DodgerBlue;
                }
                else
                {
                    return null;
                }
            };

            InitializeGrid(task, grid);
            Draw(task, new IntegerPoint[0], highlightQuestion6, grid, new NaturalNumberIterator(5), new NaturalNumberIterator(0));
        }
        private void DrawTask2A()
        {
            var task = Tasks.Task2A();

            InitializeGrid(task.table, grid);
            Draw(task.table, task.deadPoints, grid, AlphabetIterator.Instance, new NaturalNumberIterator(1));
        }
        private void DrawTask2B()
        {
            var task = Tasks.Task2B();

            InitializeGrid(task.table, grid);
            Draw(task.table, task.deadPoints, grid, AlphabetIterator.Instance, new NaturalNumberIterator(1));
        }
        private void DrawTask3()
        {
            var task = Tasks.Task3();

            InitializeGrid(task, grid);
            Draw(task, grid, new NaturalNumberIterator(0), new NaturalNumberIterator(0));
        }
        private void DrawTask4()
        {
            var task = Tasks.Task4();

            InitializeGrid(task, grid);
            Draw(task, grid, new NaturalNumberIterator(2), new NaturalNumberIterator(4));
        }
        private void DrawTask8()
        {
            var task = Tasks.Task8();

            InitializeGrid(task, grid);
            Draw(task, grid, new NaturalNumberIterator(15), new NaturalNumberIterator(0));
        }

        public MainWindow()
        {
            InitializeComponent();
            DrawQuestion6();
        }
    }
}
