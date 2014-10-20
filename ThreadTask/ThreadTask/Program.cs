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
            th2.Start();
            for (int x = 1; x < 50; x++ ) // цикл работающий в основном потоке
            {
                Console.WriteLine(Math.Sqrt(x));
                Thread.Sleep(60);
            }
            Console.WriteLine("Конец.");
            Console.ReadKey();
        }
    }
    class Test
    {
        Queue<bool?> q = new Queue<bool?>(); // очередь
        bool? Method() // метод случайно возвращает логическую обнуляемую переменную
        {
            Random r = new Random();
            Thread.Sleep(r.Next(30, 80));
            bool? b;
            if (r.Next(0, 31) % 3 == 0)
                b = true;
            else if (r.Next(0, 31) % 3 == 1)
                b = false;
            else
                b = null;
            return b;
        }
        public void View() // метод выводит значение, получаемое от метода Method
        {
            bool? flag = true;
            while (flag != null)
            {
                flag = Method();
                q.Enqueue(flag); // добавляем очередь
                if (flag == null)
                    Console.WriteLine("Метод вернул 'null'.");
                else
                    Console.WriteLine(flag);
                Thread.Sleep(50);
            }
        }
    }
}
