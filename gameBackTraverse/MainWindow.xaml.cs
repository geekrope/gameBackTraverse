using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

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
        private void CreateTable(Table table, IntegerPoint[] deadPoints, double fontSize, double borderWidth)
        {
            table.ForEach((IntegerPoint point, bool value) =>
            {
                var content = value ? "+" : "-";

                foreach (var deadPoint in deadPoints)
                {
                    if (point == deadPoint)
                    {
                        content = "x";
                    }
                }

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

                grid.Children.Add(label);
            });
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
            CreateTable(table, deadPoints, tableFontSize, borderWidth);
        }

        public MainWindow()
        {
            InitializeComponent();

            var task = Tasks.Task4();
            InitializeGrid(task, grid);
            Draw(task, grid, new NaturalNumberIterator(2), new NaturalNumberIterator(4));
        }
    }
}
