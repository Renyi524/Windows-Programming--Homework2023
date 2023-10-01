using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace TriangleChecker
{
    public partial class MainWindow : Window
    {
        private List<Triangle> triangles = new List<Triangle>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CheckTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            double side1, side2, side3;

            if (Double.TryParse(side1TextBox.Text, out side1) &&
                Double.TryParse(side2TextBox.Text, out side2) &&
                Double.TryParse(side3TextBox.Text, out side3))
            {
                if (side1 > 0 && side2 > 0 && side3 > 0)
                {
                    Triangle triangle = new Triangle(side1, side2, side3);
                    triangles.Add(triangle);

                    UpdateResultLabel(triangle);
                    UpdateTestResults();
                }
                else
                {
                    MessageBox.Show("邊長必須大於0", "錯誤提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("請輸入有效的數值", "錯誤提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateResultLabel(Triangle triangle)
        {
            Color backgroundColor = triangle.IsValid ? Color.FromArgb(200, 40, 210, 60) : Color.FromArgb(200, 240, 10, 30);

            resultLabel.Content = triangle.IsValid
                ? $"邊長 {triangle.Side1}, {triangle.Side2}, {triangle.Side3} 可構成三角形"
                : $"邊長 {triangle.Side1}, {triangle.Side2}, {triangle.Side3} 不可構成三角形";

            resultLabel.Background = new SolidColorBrush(backgroundColor);
        }
        private void UpdateTestResults()
        {
            testResultsTextBlock.Text = "測試結果：\n";

            foreach (var myTriangle in triangles)
            {
                testResultsTextBlock.Text += $"{myTriangle}\n";
            }
        }
    }
    public class Triangle
    {
        public double Side1 { get; }
        public double Side2 { get; }
        public double Side3 { get; }
        public bool IsValid { get; }

        public Triangle(double side1, double side2, double side3)
        {
            Side1 = side1;
            Side2 = side2;
            Side3 = side3;
            IsValid = IsTriangleValid();
        }

        private bool IsTriangleValid()
        {
            return Side1 + Side2 > Side3 && Side1 + Side3 > Side2 && Side2 + Side3 > Side1;
        }

        public override string ToString()
        {
            return $"{(IsValid ? "正確的" : "不正確的")}三角形：{Side1}, {Side2}, {Side3}";
        }
    }
}
