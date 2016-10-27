using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbitaryNumeralSystem
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("若要跳出程式，請按Ctrl+C\n\n");
            Thread.Sleep(3000);

            Console.WriteLine(Properties.Settings.Default.help);//程式說明

            while (true)
            {
                int[] EachPlaceSize = PleaseInputNum();//使用這輸入進位樣式並讀取
                PrintArr(EachPlaceSize);//印出所有數字(排列組合)
            }
        }


        /// <summary>
        /// 印出所有數字(排列組合)
        /// </summary>
        /// <param name="eachPlaceSize">進位樣式</param>
        public static void PrintArr(int[] eachPlaceSize)
        {
            string print = "";
            Console.WriteLine();
            
            int[] currentNum = new int[eachPlaceSize.Count()]; //inital to all 0

            while (currentNum[0] < eachPlaceSize[0])
            {
                print += currentNum.ToNumString() + "\n";//紀錄印出的字串
                Console.WriteLine(currentNum.ToNumString());//印出字串
                AddCarry(ref currentNum, eachPlaceSize);//丟到加法器裡+1
            }

            Console.WriteLine("以上為輸出結果，已複製到剪貼簿");
            Clipboard.SetText(print);
        }



        /// <summary>
        /// 進位加法器
        /// </summary>
        /// <param name="currentNum">正在被進位累加的int陣列</param>
        /// <param name="eachPlaceSize">進位的樣式</param>
        public static void AddCarry(ref int[] currentNum, int[] eachPlaceSize)
        {
            int lastPos = eachPlaceSize.Count() - 1;//加法都是從最後一位數開始+1，這是取得最後一位數在陣列中的位置

            int workingPos = lastPos;//正在執行加法的位數位置

            int loopTimes = 0;//底下的while跑了幾次，以決定進位時是進哪一位，還有多少更小的位數要被重設為0

            while (true)
            {
                if (loopTimes == eachPlaceSize.Count() )//到最大值時跳出
                    break;
                if ( ++currentNum[workingPos] < eachPlaceSize[workingPos] )//working位數+1 並且若 < 進位樣式working位數的最大值就進入
                {
                    for (int i = 0, j = lastPos; i < loopTimes; i++, j--)//把後方位數設0
                        currentNum[j] = 0;

                    break;//結束這次的加法
                }
                else
                {
                    workingPos--;//對大一位的位數進行運算
                    loopTimes++;//累加迴圈數
                }
            }

        }


        public static int[] PleaseInputNum()//請輸入進位樣式
        {
            List<int> outLs;

            while (true)
            {
                try
                {
                    outLs = new List<int>();
                    Console.WriteLine("\n請輸入進位的樣式:");

                    //讀取輸入的進位樣式
                    string input = Console.ReadLine();
                    foreach (char c in input)
                        outLs.Add(Convert.ToInt32(c.ToString()));

                    return outLs.ToArray();
                }
                catch (Exception e){Console.WriteLine("讀取錯誤:::"+e.Message);}
            }
        }
        


        

    }


    public static class Meth
    {
        public static string ToNumString(this int[] intArr)//Array To String
        {
            string output = "";
            foreach (int i in intArr)
                output += i;
            return output;
        }

    }

}

