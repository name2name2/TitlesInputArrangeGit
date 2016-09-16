using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArbitaryNumeralSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] EachPlaceSize = new[] {2, 3, 0, 4};


            PrintArr(EachPlaceSize);



            //Thread.Sleep(5000);
            Console.Read();
        }


        public static void PrintArr(int[] eachPlaceSize)
        {

            int positionCount = eachPlaceSize.Count();

            int[] currentNum = new int[positionCount]; //inital to all 0

            while (currentNum[0] < eachPlaceSize[0])
            {

                Console.WriteLine(currentNum.ToNumString());

                AddCarry(ref currentNum,eachPlaceSize);

            }
            
        }


        public static void AddCarry(ref int[] currentNum, int[] eachPlaceSize)
        {
            int lastPos = eachPlaceSize.Count() - 1;

            int workingPos = lastPos;

            int loopTimes = 0;

            bool continu = true;

            while (continu)
            {
                if (loopTimes < eachPlaceSize.Count() && ++currentNum[workingPos] < eachPlaceSize[workingPos] )
                {
                    for (int i = 0, j = lastPos; i < loopTimes; i++, j--)
                        currentNum[j] = 0;

                    continu = false;
                }
                else
                {
                    workingPos--;
                    loopTimes++;
                }
            }

        }


    }


    public static class Meth
    {
        public static string ToNumString(this int[] intArr)
        {
            string output = "";
            foreach (int i in intArr)
                output += i;
            return output;
        }

    }

}

