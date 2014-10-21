using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t1 = new Test();
            Test t2 = new Test();
            Thread th1 = new Thread(t1.View); // создаем поток th1, работающий с методом View
            Thread th2 = new Thread(t2.View);
            th1.Start(); // запускаем поток
            Thread.Sleep(200);
            th2.Start();
            for (int x = 1; x < 30; x++ ) // цикл работающий в основном потоке
            {
                Console.WriteLine(Math.Sqrt(x));
                Thread.Sleep(60);
            }
            Console.WriteLine("Содержимое очереди: {0}", Test.ShowQueue());
            Console.ReadKey();
        }
    }
    class Test
    {
        static Queue<bool?> q = new Queue<bool?>(); // очередь
        static bool testFlag = true;
        bool? Method() // метод случайно возвращает логическую обнуляемую переменную
        {
            Random r = new Random();
            Thread.Sleep(r.Next(80, 150));
            bool? b;
            if (r.Next(0, 31) % 3 == 0)
                b = true;
            else if (r.Next(0, 31) % 3 == 1)
                b = false;
            else
                b = null;
            return b;
        }
        public void View()         // Метод выводит значение, получаемое от метода Method, добавляет в общую очередь для класса Test. 
        {                         //Метод завершается, если получает 'null' и общая для класса Test логическая переменная устанавливается в 'false'
            bool? flag = true;
            while (flag != null && Test.testFlag == true)
            {
                flag = Method();
                if(Test.testFlag != false)
                     Test.q.Enqueue(flag); // добавляем очередь, если общая для класса логическая переменная не успела установиться в false из другого потока
                if (flag == null)
                {
                    Console.WriteLine("Null    " + Thread.CurrentThread.GetHashCode());
                    Test.testFlag = false;
                }
                else
                    Console.WriteLine(flag + "    " + Thread.CurrentThread.GetHashCode());
                Thread.Sleep(50);
            }
        }
        static public string ShowQueue() // отображение того, что оказалось в очереди
        {
            string s = "";
            foreach (bool? elem in q)
            {
                if (elem == null)
                    s += "Null ";
                s += (Convert.ToString(elem) + " ");
            }
            return s;
        }
    }
}
