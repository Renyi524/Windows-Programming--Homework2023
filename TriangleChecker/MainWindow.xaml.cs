using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace TriangleChecker
{
    public partial class MainWindow : Window
    {
        private List<Triangle> triangles = new List<Triangle>();    //儲存所有三角形的List 變數名稱triangles
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CheckTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            double side1, side2, side3;
                                                                    //讀取使用者輸入的三邊長
            if (Double.TryParse(side1TextBox.Text, out side1) &&
                Double.TryParse(side2TextBox.Text, out side2) &&
                Double.TryParse(side3TextBox.Text, out side3))
            {
                if (side1 > 0 && side2 > 0 && side3 > 0)            //如果成功轉換為 double 類型 且邊長皆大於 0 則觸發
                {
                    Triangle triangle = new Triangle(side1, side2, side3);  //創建 Triangle 物件
                    triangles.Add(triangle);                                //加入到三角形List 變數名稱triangles

                    UpdateResultLabel(triangle);                    //呼叫方法 更新結果標籤
                    UpdateTestResults();                            //呼叫方法 更新測試結果
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
            Color backgroundColor = triangle.IsValid ? Color.FromArgb(200, 40, 210, 60) : Color.FromArgb(200, 240, 10, 30); //根據三角形是否正確來設定背景色

            resultLabel.Content = triangle.IsValid      //根據三角形是否合法更新結果標籤的內容
                ? $"邊長 {triangle.Side1}, {triangle.Side2}, {triangle.Side3} 可構成三角形"         //如果三角形合法
                : $"邊長 {triangle.Side1}, {triangle.Side2}, {triangle.Side3} 不可構成三角形";      //否則

            resultLabel.Background = new SolidColorBrush(backgroundColor);    //在resultLabel.Background屬性設定結果標籤的背景色 以創建的SolidColorBrush物件 填入backgroundColor顏色
        }
        private void UpdateTestResults()
        {
            testResultsTextBlock.Text = "測試結果：\n";

            foreach (var myTriangle in triangles)               //掃描所有三角形 將其字串形式存入myTriangle
            {
                testResultsTextBlock.Text += $"{myTriangle}\n"; //將 myTriangle 的字串累加到 testResultsTextBlock.Text中，字串插值 $"{myTriangle}\n" 會自動呼叫 myTriangle物件的 ToString()方法
            }
        }
    }
    public class Triangle
    {
        public double Side1 { get; }    //public公開屬性，{ get; }設定唯讀屬性
        public double Side2 { get; }
        public double Side3 { get; }
        public bool IsValid { get; }

        public Triangle(double side1, double side2, double side3)
        {                           //將參數side1 side2 side3 分別傳給 Triangle 類別的三個屬性 Side1 Side2  Side3
            Side1 = side1;
            Side2 = side2;
            Side3 = side3;
            IsValid = IsTriangleValid();    //呼叫自定義sTriangleValid()方法 將結果存入布林變數 IsValid中
        }

        private bool IsTriangleValid()
        {   //第一邊和第二邊的和大於第三邊 或 第一邊和第三邊的和大於第二邊 或 第二邊和第三邊的和大於第一邊 傳回true 否則傳false
            return Side1 + Side2 > Side3 && Side1 + Side3 > Side2 && Side2 + Side3 > Side1;
        }

        public override string ToString()
        {
            return $"{(IsValid ? "正確的" : "不正確的")}三角形：{Side1}, {Side2}, {Side3}";
        }
    }
}
