using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTask2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t1 = new Test(12);
            Test t2 = new Test(18);
            Thread th1 = new Thread(t1.SomeMath); // создали дочерний поток th1 с методом SomeMath для объекта t1
            Thread th2 = new Thread(t2.SomeMath);
            th1.Start(); // запустили поток
            th2.Start();
            th1.Join(); // текущий поток ждет, пока завершиться поток th1
            th2.Join();
            Console.WriteLine("Основной поток после завершения вычислений в дочерних потоках: {0} + {1} = {2}", t1.Sum, t2.Sum, (t1.Sum+t2.Sum)); // работа основного потока после завершения дочерних потоков
            Console.ReadKey();
        }
    }
    class Test
    {
        public Test(int num)
        {
            this.Num = num;
        }
        int Num{get; set;}
        public double Sum { get; set; }
        public void SomeMath()
        {
            for (int x = 0; x < Num; x++)
            {
                Sum += Math.Sqrt(x);
                Console.WriteLine(Sum);
            }
        }

    }
}
