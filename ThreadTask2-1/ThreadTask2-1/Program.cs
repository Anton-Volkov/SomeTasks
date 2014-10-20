using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTask2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t1 = new Test("Хорошо");
            Test t2 = new Test("Отлично");
            Thread th1 = new Thread(t1.ReturnHash); // создаем дочерний поток с методом ReturnHash для объекта t1
            Thread th2 = new Thread(t2.ReturnHash);
            th1.Priority = ThreadPriority.Lowest; // низший приоритет для дочернего потока
            th2.Priority = ThreadPriority.Lowest;
            Thread.CurrentThread.Priority = ThreadPriority.Normal; // средний приоритет для текущего потока
            th1.Start(); // запустили поток
            th2.Start();
            for (int x = 0; x < 20; x++) // работа в текущем потоке, метод выводит строки
            {
                string s = new string('$', x);
                Console.WriteLine(s);
                Console.WriteLine("Хэшкод потока - " + Thread.CurrentThread.GetHashCode()); // хэшкод потока
                Thread.Sleep(0);
            }
            Console.ReadKey();
        }
    }
    class Test
    {
        public Test(string word)
        {
            this.Word = word;
        }
        string Word { get; set; }
        public void ReturnHash() // метод изменяем значение свойства Word и выводит на экран хэшкод для  этого свойства + хэшкод потока
        {
            for (int x = 0; x < 50; x++)
            {
                Word = String.Format("{0} + {1}", Word, Convert.ToChar(x));
                Console.WriteLine(Word.GetHashCode());
                Console.WriteLine("Хэшкод потока - " + Thread.CurrentThread.GetHashCode());
            }
            Thread.Sleep(0);
        }

    }
}
