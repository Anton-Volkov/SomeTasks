using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Данный класс представляет из себя игру "Змейка". 
    /// При создании объекта класса, задается размер квадратного поля для игры (количество точек на поле) - int length.
    /// Для запуска игры служит метод Game, который вызывается у объекта класса.
    /// </summary>
    class Snake // класс с игрой Змейка
    {
        delegate void MyDel();
        /// <summary>
        /// Класс с координатами и отображением для одного сегмента змеи.
        /// </summary>
        class SnakePath // класс с координатами и изображением каждого звена змеи
        {
            #region Поля класса.
            public int x;
            public int y;
            public char look;
            #endregion
            #region Конструктор.
            public SnakePath(int x, int y, char look)
            {
                this.x = x;
                this.y = y;
                this.look = look;
            }
            #endregion
        }
        #region События.
        event MyDel eat;
        event MyDel move;
        event MyDel knot;
        event MyDel wall;
        #endregion
        #region Поля класса.
        ConsoleKey key;
        int length; // размер поля
        int direct; // направление движения змеи
        int speed; // скорость игры
        bool flag; // идет или завершена игра
        bool appleFlag; // присутствие яблока на поле
        char[,] pole; // поле игры
        List<SnakePath> s = new List<SnakePath>(); // список с координатами змеи
        #endregion
        #region Конструктор.
        public Snake(int length) // конструктор 
        {
            this.length = length;
            pole = new char[this.length + 2, this.length + 2];
            for (int x = 0; x < pole.GetLength(0); x++)
                for (int y = 0; y < pole.GetLength(1); y++)
                {
                    if (x == 0 || y == 0 || x == pole.GetLength(0) - 1 || y == pole.GetLength(1) - 1)
                        pole[x, y] = (char)183;
                    else
                        pole[x, y] = ' ';
                }
            s.Add(new SnakePath(1, 1, 'x'));
            s.Add(new SnakePath(1, 2, (char)16));
            direct = 3; // изначально ползет вправо
            speed = 280; // скорость в начале игры
            appleFlag = false;
            move += Go; // подписываемся на события
            eat += EatApple;
            knot += Knot;
            wall += CrashWall;
        }
        #endregion
        #region Свойства класса.
        ConsoleKey Key
        {
            get { return key; }
            set
            {                   //если приходит кнопка, функционал которой нам не нужен, новое значение не заносится
                if (value == ConsoleKey.UpArrow || value == ConsoleKey.RightArrow || value == ConsoleKey.DownArrow || value == ConsoleKey.LeftArrow)
                    key = value;
            }
        }
        int Direct
        {
            get { return direct; }
            set
            {
                if ((direct - value) == 2 || (direct - value) == (-2))//проверяем, если направление противоположное, то оставляем то же, что было
                    return;
                else
                    direct = value;
            }
        }
        #endregion
        #region Методы класса.
        /// <summary>
        /// Основной метод класса. Запускает игру "Змейка".
        /// </summary>
        public void Game() // запустить игру
        {
            flag = true;
            do
            {
                if (Console.KeyAvailable == true)
                {
                    Click();
                }
                Thread.Sleep(speed);
                Console.Clear();
                move();
                Apple();
                View();
            }
            while (flag);
            if (!flag)
                TryAgain();// запустить игру снова
        }
        /// <summary>
        /// Метод выводит на экран позицию змеи.
        /// </summary>
        void View() // вывод на экран 
        {
            foreach (SnakePath sp in s)
            {
                pole[sp.x, sp.y] = sp.look;
            }
            pole[s.ElementAt(s.Count - 1).x, s.ElementAt(s.Count - 1).y] = s.ElementAt(s.Count - 1).look;
            for (int x = 0; x < pole.GetLength(0); x++)
            {
                for (int y = 0; y < pole.GetLength(1); y++)
                {
                    if (pole[x, y] == (char)183)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(pole[x, y]);
                        Console.ResetColor();
                    }
                    else if (pole[x, y] == (char)2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(pole[x, y]);
                        Console.ResetColor();
                    }
                    else if (pole[x, y] == 'x' || pole[x, y] == (char)16 || pole[x, y] == (char)17 || pole[x, y] == (char)30 || pole[x, y] == (char)31)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(pole[x, y]);
                        Console.ResetColor();
                    }
                    else
                        Console.Write(pole[x, y]);
                }
                Console.WriteLine();
            }
            foreach (SnakePath sp in s)
            {
                pole[sp.x, sp.y] = ' ';
            }
        }
        /// <summary>
        /// Метод осуществляет движение змеи.
        /// </summary>
        void Go() // движение
        {
            if ((s.ElementAt(s.Count - 1).y + 1) == pole.GetLength(0) || (s.ElementAt(s.Count - 1).x + 1) == pole.GetLength(1) || (s.ElementAt(s.Count - 1).y) == 0 || (s.ElementAt(s.Count - 1).x) == 0) // если доползла до края
            {
                wall();
            }
            else
            {
                Step();
                s.RemoveAt(0);
            }
            if (pole[s.ElementAt(s.Count - 1).x, s.ElementAt(s.Count - 1).y] == (char)2) // если достигли позиции яблока, вызывается метод EatApple
                eat();

            for (int n = 0; n < s.Count - 1; n++)
            {
                if (s[s.Count - 1].x == s[n].x && s[s.Count - 1].y == s[n].y) // если запутались в хвосте
                    knot();
            }
        }
        /// <summary>
        /// Метод вызывается при врезании змеи в стену.
        /// </summary>
        void CrashWall() // врезались в стену
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Врезались в стену, застряли и умерли с голода!  Игра окончена."); // сообщение
            Console.ResetColor();
            flag = false; // завершили игру
        }
        /// <summary>
        /// Метод поедания яблока змеей.
        /// </summary>
        void EatApple() // съедает яблоко, увеличивается на одно звено
        {
            Step();
            if (s.Count == 12) // если в змее уже 12 сегментов - победа
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Поздравляем! Вы победили!"); // сообщение
                Console.ResetColor();
                flag = false; // завершили игру
            }
            else
            {
                speed -= 20;
                appleFlag = false;
            }
        }
        /// <summary>
        /// Метод вызывается, если змея врезалась сама в себя.
        /// </summary>
        void Knot() // если змея запуталась
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Вы запутались в себе! Меньше самокопаний.  Игра окончена."); // сообщение
            Console.ResetColor();
            flag = false; // завершили игру
        }
        /// <summary>
        /// Метод разбрасывает яблоки по полю. На поле одновременно присутствует только одно яблоко. Новое появляется, в момент съедения прошлого.
        /// </summary>
        void Apple() // метод расбрасывает яблоки по полю
        {
            Random r = new Random();
            if (!appleFlag)
            {
                int tempX = r.Next(1, this.length - 1);
                Thread.Sleep(33);
                int tempY = r.Next(1, this.length - 1);
                if (pole[tempX, tempY] == ' ')  // проверяем, чтобы яблоко не попадало на змею
                {
                    pole[tempX, tempY] = (char)2;
                    appleFlag = true;
                }
            }
            else
                return;
        }
        /// <summary>
        /// Метод запускает игру заново.
        /// </summary>
        void TryAgain() // метод запускает игру заново
        {
            bool tempFlag = true;
            do
            {
                Console.WriteLine(@"Хотите попробовать еще раз? (Y\N)");
                string choice = Convert.ToString(Console.ReadLine());
                if (choice == "Y" || choice == "y" || choice == "Н" || choice == "н") //если Y
                {
                    for (int x = 0; x < pole.GetLength(0); x++)
                        for (int y = 0; y < pole.GetLength(1); y++)
                        {
                            if (x == 0 || y == 0 || x == pole.GetLength(0) - 1 || y == pole.GetLength(1) - 1)
                                pole[x, y] = (char)183;
                            else
                                pole[x, y] = ' ';
                        }
                    s.RemoveRange(0, s.Count);
                    s.Add(new SnakePath(1, 1, 'x'));
                    s.Add(new SnakePath(1, 2, (char)16));
                    direct = 3;
                    speed = 280;
                    appleFlag = false;
                    tempFlag = false;
                    Game();
                }
                else if (choice == "N" || choice == "n" || choice == "Т" || choice == "т") // если N
                {
                    Console.Clear();
                    tempFlag = false;
                }
            }
            while (tempFlag);
        }
        /// <summary>
        /// Метод осуществляет перемещение змеи по полю на один шаг. Нужен для метода движения и поедания яблок.
        /// </summary>
        void Step() // метод перемещает змею на шаг. Нужен для методов Go и EatApple
        {
            switch (direct)
            {
                case 1: // влево
                    {
                        s.Add(new SnakePath(s.ElementAt(s.Count - 1).x, s.ElementAt(s.Count - 1).y - 1, (char)17));
                        s.ElementAt(s.Count - 2).look = 'x';
                    }
                    break;
                case 2: // вверх
                    {
                        s.Add(new SnakePath(s.ElementAt(s.Count - 1).x - 1, s.ElementAt(s.Count - 1).y, (char)30));
                        s.ElementAt(s.Count - 2).look = 'x';
                    }
                    break;
                case 3: // вправо
                    {
                        s.Add(new SnakePath(s.ElementAt(s.Count - 1).x, s.ElementAt(s.Count - 1).y + 1, (char)16));
                        s.ElementAt(s.Count - 2).look = 'x';
                    }
                    break;
                case 4: // вниз
                    {
                        s.Add(new SnakePath(s.ElementAt(s.Count - 1).x + 1, s.ElementAt(s.Count - 1).y, (char)31));
                        s.ElementAt(s.Count - 2).look = 'x';
                    }
                    break;
            }
        }
        /// <summary>
        /// Метод отслеживает срабатывание кнопок клавиатуры, осуществляя изменение направления движения змеи.
        /// </summary>
        void Click()  //отслеживание срабатывания кнопок управления, изменяет направление движения
        {
            Key = Console.ReadKey().Key;
            if (Key == ConsoleKey.UpArrow)
                Direct = 2;
            if (Key == ConsoleKey.RightArrow)
                Direct = 3;
            if (Key == ConsoleKey.DownArrow)
                Direct = 4;
            if (Key == ConsoleKey.LeftArrow)
                Direct = 1;
            else
                return;
        }
        #endregion
    }
}
