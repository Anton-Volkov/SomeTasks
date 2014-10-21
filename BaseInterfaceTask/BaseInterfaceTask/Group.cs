using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInterfaceTask
{

    class Group : IEnumerable // класс группа, реализует интерфейс IEnumerable
    {
        public Group(int number)
        {
            Number = number;
            group = new Student[number];
            n = 0;
        }
        string Name { get; set; } // имя группы
        int Number { get; set; } // максимальное число студентов в группе
        int n; // текущее число студентов в группе
        public Student[] group;
        public void Add(Student newStudent) // метод добавления студента в группу
        {
            if (n >= Number) // если группа уже заполнена
            {
                Console.WriteLine("Больше добавить нельзя.");
                return;
            }
            foreach (Student item in group)
            {
                if (item == null)
                    break;
                else if (item.CompareTo(newStudent) == 0) // если такой студент уже есть.
                {
                    Console.WriteLine("Такой студент уже есть в группе. У нас все оригинальные студенты.");
                    return;
                }
            }
            group[n] = newStudent;
            n++;
        }
        public IEnumerator GetEnumerator() // метод GetEnumerator для перебора группы foreach
        {
            for (int i = 0; i < n; i++)
            {
                Student elem = group[i];
                int index = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (elem.CompareTo(group[j]) == 1)
                    {
                        elem = group[j];
                        index = j;
                    }
                }
                group[index] = group[i];
                group[i] = elem;
                yield return group[i];
            }
        }
    }
}
