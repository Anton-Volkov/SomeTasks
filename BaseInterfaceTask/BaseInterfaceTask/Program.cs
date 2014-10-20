using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseInterfaceTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Student st1 = new Student(20, "Игумен", "Пафнутий"); // создаем студента
            st1.Rate(5); // проставляем оценки
            st1.ShowMarks(); // вывод оценок студента
            Console.WriteLine("Средняя оценка студента: " + st1.ArithmeticAverage()); // средняя оценка по всем предметам
            Group gr1 = new Group(12); // создаем группу из 12 студентов
            gr1.Add(st1); // добавляем в группу студента
            gr1.Add(new Student(20, "Иван", "Иванов"));
            gr1.Add(new Student(20, "Игумен", "Пафнутий"));
            gr1.Add(new Student(22, "Товарищ", "Вечность"));
            gr1.Add(new Student(25, "Никола", "Кривошеев"));
            gr1.Add(new Student(35, "Андрей", "Стоменов"));
            gr1.Add(new Student(19, "Прохор", "Коровьев"));
            gr1.Add(new Student(15, "Антон", "Кротов"));
            gr1.Add(new Student(22, "Венечка", "Ерофеев"));
            gr1.Add(new Student(33, "Юй", "Вэнь"));
            gr1.Add(new Student(33, "Барух", "Спиноза"));
            gr1.Add(new Student(44, "Степан", "Войнич"));
            gr1.Add(new Student(23, "Джек", "Рассел"));
            gr1.Add(new Student(24, "Марселло", "Стровачи"));
            gr1.Add(new Student(20, "Очередной", "Студент"));
            Console.WriteLine("\nГруппа отсортирована по возрасту: ");
            foreach (Student elem in gr1)
            {
                elem.Rate(5); // проставляем оценки каждому студенту
                Console.WriteLine(elem);
            }
            Array.Sort(gr1.group, new Student.SortByName());
            Console.WriteLine("\nГруппа отсортирована по имени: ");
            foreach (Student elem in gr1.group)
                Console.WriteLine(elem);
            Array.Sort(gr1.group, new Student.SortBySurname());
            Console.WriteLine("\nГруппа отсортирована по фамилии: ");
            foreach (Student elem in gr1.group)
                Console.WriteLine(elem);
            Array.Sort(gr1.group, new Student.SortByMarks());
            Console.WriteLine("\nГруппа отсортирована по успеваемости: ");
            foreach (Student elem in gr1.group)
                Console.WriteLine(elem);
            Console.ReadKey();
        }
    }
}
