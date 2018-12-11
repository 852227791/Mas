using System;
using System.Threading;
using System.Threading.Tasks;

namespace Invoke
{
    class Program
    {

        public static void Main() {
            var data = new  double[1000];

            
        //    BubbleSort();
        }
        public void BubbleSort(double[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = data.Length - 1; j > i; j--)
                {
                    if (data[j] > data[j - 1])
                    {
                        data[j] = data[j] + data[j - 1];
                        data[j - 1] = data[j] - data[j - 1];
                        data[j] = data[j] - data[j - 1];
                        Console.WriteLine(data[j]);
                    }
                }
            }
        }


    }
}
