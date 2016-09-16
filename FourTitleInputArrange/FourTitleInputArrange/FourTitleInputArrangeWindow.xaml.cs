using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FourTitleInputArrange
{
    /// <summary>
    /// Interaction logic for FourTitleInputArrangeWindow.xaml
    /// </summary>
    public partial class FourTitleInputArrangeWindow : Window
    {
        public FourTitleInputArrangeWindow()
        {
            InitializeComponent();

#if DEBUG
            T1.Text = 
@"qw
asdf
xc
";
#endif
        }

        private void Arrange_OnClick(object sender, RoutedEventArgs e)
        {
            string aLine = "";
            List<string> titles = new List<string>();
            StringReader strReader = new StringReader(T1.Text);
            while (true)
            {
                aLine = strReader.ReadLine();
                if (aLine != null)
                {
                    if (String.IsNullOrWhiteSpace(aLine))
                        continue;
                    titles.Add(aLine);
                }
                else
                    break;
            }

            int[] eachPlaceSize = ListStringToEachPlaceSize(titles);

            string result = ArrangeOutput(eachPlaceSize, titles);
            Clipboard.SetText(result);
            TO.Inlines.Add(new Run(result));

        }


        private int[] ListStringToEachPlaceSize(List<string> strLs)
        {
            List<int> eachPlaceSize = new List<int>();

            foreach (string s in strLs)
                eachPlaceSize.Add(s.Count());

            return eachPlaceSize.ToArray();
        }

        private string ArrangeOutput(int[] eachPlaceSize, List<string> titles)
        {
            string output = "下方排列結果已經複製到剪貼簿，可在其他地方按Ctrl+V貼上\r\n----------\r\n";

            int[] currentNum = new int[eachPlaceSize.Count()]; //inital to all 0

            while (currentNum[0] < eachPlaceSize[0])
            {
                output += TitlesToArrageString(currentNum, titles)+"\r\n";
                AddCarry(ref currentNum, eachPlaceSize);
            }

            return output;

        }

        private string TitlesToArrageString(int[] currentNum, List<string> titles)
        {
            string tostring = "";
            int cursor = 0;
            foreach (string title in titles)
                tostring += title[currentNum[cursor++]];
            return tostring;
        }





        public static void AddCarry(ref int[] currentNum, int[] eachPlaceSize)
        {
            int lastPos = eachPlaceSize.Count() - 1;//加法都是從最後一位數開始+1，這是取得最後一位數在陣列中的位置

            int workingPos = lastPos;//正在執行加法的位數位置

            int loopTimes = 0;//底下的while跑了幾次，以決定進位時是進哪一位，還有多少更小的位數要被重設為0

            bool continu = true;//是否繼續迴圈
            while (continu)
            {
                if (loopTimes == eachPlaceSize.Count())//到最大值時跳出
                    break;
                if (++currentNum[workingPos] < eachPlaceSize[workingPos])//working位數+1 並且若 < 進位樣式working位數的最大值就進入
                {
                    for (int i = 0, j = lastPos; i < loopTimes; i++, j--)//把後方位數設0
                        currentNum[j] = 0;

                    continu = false;//結束這次的加法
                }
                else
                {
                    workingPos--;//對大一位的位數進行運算
                    loopTimes++;//累加迴圈數
                }
            }

        }






        private void Label_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(
@"先在上方文字輸入方塊內依行打上所有標題，若有空白行程式會自動忽略，
例如輸入:
qw

asdf
xc
則程式只會讀取三行有文字的標題
-------
接著按下開始排列，排列結果就會顯示在下方方塊，
以上例的結果就是
qax
qac
...略
wfc
--------
此外，淺藍色的線可調整方塊高度，
及輸出結果可用Ctrl+滾輪 或 Ctr+ +/-調整文字大小"
);
        }
    }
}
