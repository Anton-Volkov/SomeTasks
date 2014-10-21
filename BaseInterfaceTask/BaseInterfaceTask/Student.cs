using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseInterfaceTask
{
    class Student : IComparable // класс студент, реализует интерфейс IComparable
    {
        int age; // возраст
        string name; // имя
        string surname; // фамилия 
        List<int[]> marks; // список с массивами оценок по предметам
        public Student(int age, string name, string surname) 
        {
            if (age > 0)
                this.age = age;
            else
                this.age = 18;
            this.name = name;
            this.surname = surname;
            marks = new List<int[]>();
        }
        public int Age { get { return age; } }
        public string Name { get { return name; } }
        public string Surname { get { return surname; } }
        public void Rate(int number) // метод проставляет оценки, принимает количество предметов для оценивания
        {
            Random r = new Random();
            for (int z = 0; z < number; z++)
            {
                Thread.Sleep(r.Next(20, 60));
                int[] subject = new int[r.Next(1, 20)];
                for (int x = 0; x < subject.Length; x++)
                    subject[x] = r.Next(1, 13);
                marks.Add(subject);
            }
        }
        public void ShowMarks() // отображение оценок
        {
            for (int x = 0; x < marks.Count; x++)
            {
                Console.Write("Предмет {0}: ", x + 1);
                for (int y = 0; y < marks[x].Length; y++)
                {
                    Console.Write(marks[x][y] + " ");
                }
                Console.WriteLine();
            }
        }
        public double ArithmeticAverage() // средний бал по всем предметам
        {
            double d = 0;
            for (int x = 0; x < marks.Count; x++)
            {
                d += Enumerable.Average(marks[x]);
            }
            d /= marks.Count;
            d = Math.Round(d, 3); // округляем значение до 3-го знака после запятой
            return d;
        }
        public int CompareTo(object obj) // сравнение студентов
        {
            Student st = (Student)obj;
            if (this.Age > st.Age)
                return 1;
            if (this.Age < st.Age)
                return -1;
            if (this.Age == st.Age && this.Name == st.Name && this.Surname == st.Surname)
                return 0;
            return -1;
        }
        public override string ToString()
        {
            return string.Format("Student {0}\t{1}, {2}\t average mark - {3}.", this.Name, this.Surname, this.Age, this.ArithmeticAverage());
        }
        public class SortByName : IComparer //класс отвечает за сортировку студентов по имени
        {
            int IComparer.Compare(object obj1, object obj2)
            {
                Student stud1 = (Student)obj1;
                Student stud2 = (Student)obj2;
                return String.Compare(stud1.Name, stud2.Name);
            }
        }
        public class SortBySurname : IComparer //класс отвечает за сортировку студентов по фамилии
        {
            int IComparer.Compare(object obj1, object obj2)
            {
                Student stud1 = (Student)obj1;
                Student stud2 = (Student)obj2;
                return String.Compare(stud1.Surname, stud2.Surname);
            }
        }
        public class SortByAge : IComparer  //класс отвечает за сортировку студентов по возрасту
        {
            int IComparer.Compare(Object obj1, object obj2)
            {
                Student st1 = (Student)obj1;
                Student st2 = (Student)obj2;
                if (st1.Age > st2.Age)
                    return 1;
                if (st2.Age > st1.Age)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByMarks : IComparer //класс отвечает за сортировку студентов по средней оценке
        {
            int IComparer.Compare(Object obj1, object obj2)
            {
                Student st1 = (Student)obj1;
                Student st2 = (Student)obj2;
                if (st1.ArithmeticAverage() > st2.ArithmeticAverage())
                    return 1;
                if (st1.ArithmeticAverage() < st2.ArithmeticAverage())
                    return -1;
                else
                    return 0;
            }
        }
    }
}
