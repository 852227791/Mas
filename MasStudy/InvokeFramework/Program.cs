using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvokeFramework
{
    class Program
    {
        //private delegate void Dowork();//定义一个委托
        //static void Main(string[] args)
        //{
        //    Dowork d = new Dowork(WorkPro);
        //    AsyncCallback callBack = new AsyncCallback(CallBack);
        //    d.BeginInvoke(callBack, "异步调用参数");
        //    for (int i = 0; i < 100; i++)
        //    {
        //        Thread.Sleep(1000);
        //        Console.WriteLine($"================================================我是主线程，执行第【 {i} 】================================================");
        //    }
        //    Console.WriteLine("================================================主线程调用结束================================================");
        //    Console.ReadKey();
        //}



        //public static void WorkPro()
        //{

        //    Thread.Sleep(5000);
        //    Console.WriteLine("================================================异步调用结束================================================");
        //}

        //public static void CallBack(IAsyncResult ar)
        //{
        //    Console.WriteLine("================================================异步调用回调================================================");

        //}

        static void Main(string[] args)
        {
            Task<int> t = new Task<int>((c) => Sum((int)c), 100);
            t.Start();
            t.ContinueWith(task => Console.WriteLine("任务完成的结果{0}", task.Result));//当任务执行完之后执行
            t.ContinueWith(task => Console.WriteLine(""), TaskContinuationOptions.OnlyOnFaulted);//当任务出现异常时才执行
            for (int i = 0; i < 200; i++)
            {
                Thread.Sleep(10);
            }
            Console.WriteLine("done");
            Console.ReadKey();
        }
        static int Sum(int count)
        {
            int sum = 0;
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(1000);
                if (i==50)
                {
                    throw new  Exception("异常");
                }
                sum += i;
            }
            Console.WriteLine("任务处理完成");
            return sum;
        }

    }
}
