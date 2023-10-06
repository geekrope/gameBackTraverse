using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace gameBackTraverse
{
    public partial class MainWindow : Window
    {
        private void InitializeGrid(Table table)
        {
            for (int column = table.Start.X; column <= table.Size.Width; column++)
            {
                var columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(columnDefinition);
            }
            for (int row = table.Start.Y; row <= table.Size.Height; row++)
            {
                var rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(rowDefinition);
            }
        }
        private void Draw(Table table)
        {
            Draw(table, new IntegerPoint[0]);
        }
        private void Draw(Table table, IntegerPoint[] deadPoints)
        {
            table.ForEach((IntegerPoint point, bool value) =>
            {
                var label = new Label();
                label.Content = value ? "+" : "-";
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;

                foreach (var deadPoint in deadPoints)
                {
                    if (point == deadPoint)
                    {
                        label.Content = "X";
                    }
                }

                var borderWidth = 1;

                Thickness thickness = new Thickness(0, 0, borderWidth, borderWidth);

                if (point.X == table.Start.X)
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

                label.SetValue(Grid.ColumnProperty, point.X - table.Start.X);
                label.SetValue(Grid.RowProperty, table.Size.Height - point.Y);

                grid.Children.Add(label);
            });
        }

        public MainWindow()
        {
            InitializeComponent();

            var task = Tasks.Task2B();
            InitializeGrid(task.table);
            Draw(task.table, task.deadPoints);
        }
    }
}
