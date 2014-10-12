using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CrestZeroProject
{
    /// <summary>
    /// Данный класс представляет из себя игру "Крестики-Нолики". Реализует все три варианта игры- 
    /// с компьютером, на два игрока, компьютер с компьютером, в зависимости от параметров, которые принимает конструктор.
    /// </summary>
    class TicTacToe
    {
        #region Поля класса.
        Window win; // форма игры
        List<Button> ListButton; // список кнопок игры
        ImageBrush imgNull;// фон пустой кнопки
        ImageBrush imgFirst;// фон кнопки
        ImageBrush imgSecond; // фон кнопки
        List<bool[]> firstCanWin; // список с выигрышными комбинациями игрока
        List<bool[]> secondCanWin; // список с выигрышными комбинациями компьютера
        bool[] first = new bool[9]; // массив с ходами первого игрока
        bool[] second = new bool[9]; // массив с ходами второго игрока
        int draw; // количество пустых клеток
        int type; // тип игры
        int symbol; // чем играет игрок (0 - нолик, 1 - крестик)
        #endregion
        #region Конструктор
        public TicTacToe(Window win, List<Button> ListButton, int symbol, int type)
        {
            this.win = win;
            this.ListButton = ListButton;
            this.symbol = symbol;
            this.type = type;
            // инициализация фонов для кнопок
            imgNull = new ImageBrush();
            imgNull.ImageSource = new BitmapImage(new Uri("Image/null.jpg", UriKind.Relative));
            imgFirst = new ImageBrush();
            imgSecond = new ImageBrush();
            foreach (var x in ListButton)
            {
                x.Background = imgNull; // проставляется пустой фон во все кнопки
            }
            if (symbol == 1)
            {
                imgFirst.ImageSource = new BitmapImage(new Uri("Image/1.jpg", UriKind.Relative));
                imgSecond.ImageSource = new BitmapImage(new Uri("Image/0.jpg", UriKind.Relative));
            }
            else if (symbol == 0)
            {
                imgFirst.ImageSource = new BitmapImage(new Uri("Image/0.jpg", UriKind.Relative));
                imgSecond.ImageSource = new BitmapImage(new Uri("Image/1.jpg", UriKind.Relative));
            }
            else
            {
                imgFirst.ImageSource = new BitmapImage(new Uri("Image/1.jpg", UriKind.Relative));
                imgSecond.ImageSource = new BitmapImage(new Uri("Image/0.jpg", UriKind.Relative));
            }
            for (int x = 0; x < first.Length; x++) // установка значений в массивы ходов игроков
            {
                first[x] = false;
                second[x] = false;
            }
            draw = 9; // 9 пустых клеток
            // инициализация и установка значений в списках выигрышных комбинаций игроков
            firstCanWin = new List<bool[]>();
            secondCanWin = new List<bool[]>();
            for (int x = 0; x < 9; x++)
            {
                firstCanWin.Add(new bool[] { false, false, false });
            }
            for (int x = 0; x < 9; x++)
            {
                secondCanWin.Add(new bool[] { false, false, false });
            }
        }
        #endregion
        #region Свойства.
        public ImageBrush ImgNull // свойство для пустого фона для кнопки, только возвращает значение
        {
            get { return imgNull; }
        }
        #endregion
        #region Методы.
        /// <summary>
        /// Метод для хода игрока
        /// </summary>
        public void Stroke(string s) // ход игрока
        {

            if (draw % 2 == 1 || symbol == 0)
            {
                Turn(1, Convert.ToInt32(s));
            }
            else if (draw % 2 == 0 && type == 1)
            {
                Turn(2, Convert.ToInt32(s));
            }
            if (type == 0 && draw != 9 && TryToWin(first, firstCanWin, false) != 10)
            {
                PCTurn();
            }
        }
        /// <summary>
        /// Метод для хода компьютера.
        /// </summary>
        public void PCTurn() // ход компьютера
        {
            Random r = new Random();
            int metka = TryToWin(second, secondCanWin, false); // проверяем может ли компьютер выиграть методом TryToWin
            if (metka < 9) // если выбрана метка для хода
            {
                Turn(2, metka);
                return;
            }
            metka = TryToWin(first, firstCanWin, false); // проверяем, может ли игрок выиграть методом TryToWin и мешаем ему
            if (metka < 9) // если выбрана метка для хода
            {
                Turn(2, metka);
                return;
            }
            if (ListButton[4].Background == imgNull) // если центральная клетка свободна - ходим в нее
            {
                Turn(2, 4);
                return;
            }
            if ((ListButton[4].Background != imgNull && draw == 6 && symbol == 1) && ((first[0] == true && first[8] == true) || (first[2] == true && first[6] == true) || (first[2] == true && first[3] == true) || (first[0] == true && first[7] == true) || (first[6] == true && first[5] == true) || (first[8] == true && first[1] == true))) // если готовится вилка
            {
                if (r.Next(0, 40) % 4 == 0)
                    metka = 1;
                else if (r.Next(0, 40) % 4 == 2)
                    metka = 3;
                else if (r.Next(0, 40) % 4 == 3)
                    metka = 5;
                else
                    metka = 7;
                if (ListButton[metka].Background == imgNull)
                {
                    Turn(2, metka);
                    return;
                }
            }
            // ставим в одну из угловых клеток
            if (r.Next(0, 40) % 4 == 0)
                metka = 8;
            else if (r.Next(0, 40) % 4 == 2)
                metka = 6;
            else if (r.Next(0, 40) % 4 == 3)
                metka = 0;
            else
                metka = 2;
            if (ListButton[metka].Background == imgNull)
            {
                Turn(2, metka);
                return;
            }
            metka = TryToWin(first, firstCanWin, true); // проверяем, есть ли еще вероятные выигрышные комбинации
            if (metka < 9)
            {
                Turn(2, metka);
                return;
            }
            for (int x = 0; x < second.Length; x++) // перебираем все клетки, ищем свободную
            {
                if (ListButton[x].Background == imgNull)
                {
                    Turn(2, x);
                    return;
                }
            }          
        }
        /// <summary>
        /// Метод победы игрока или крестиков.
        /// </summary>
        void UserWin() // победа игрока или крестиков
        {
            if (TryToWin(first, firstCanWin, false) == 10 && type == 0)
            {
                draw = 9;
                MessageBoxResult gameEnd = MessageBox.Show("Вы победили! Хотите сыграть еще раз?", "Игра окончена. Еще раз?", MessageBoxButton.YesNo);
                if (gameEnd == MessageBoxResult.No)
                {
                    this.win.Close();
                }
                if (gameEnd == MessageBoxResult.Yes)
                {
                    this.win.Close();
                    System.Windows.Forms.Application.Restart();
                }
            }
            else if (TryToWin(first, firstCanWin, false) == 10)
            {
                draw = 9;
                MessageBoxResult gameEnd = MessageBox.Show("Крестики победили! Хотите сыграть еще раз?", "Игра окончена. Еще раз?", MessageBoxButton.YesNo);
                if (gameEnd == MessageBoxResult.No)
                {
                    this.win.Close();
                }
                if (gameEnd == MessageBoxResult.Yes)
                {
                    this.win.Close();
                    System.Windows.Forms.Application.Restart();
                }
            }
        }
        /// <summary>
        /// Метод победы компьютера или ноликов.
        /// </summary>
        void PCWin() // победа компьютера или ноликов
        {
            if (TryToWin(second, secondCanWin, false) == 10 && type ==0)
            {
                draw = 9;
                MessageBoxResult gameEnd = MessageBox.Show("Компьютер победил! Хотите сыграть еще раз?", "Игра окончена. Еще раз?", MessageBoxButton.YesNo);
                if (gameEnd == MessageBoxResult.No)
                {
                    this.win.Close();
                }
                if (gameEnd == MessageBoxResult.Yes)
                {
                    this.win.Close();
                    System.Windows.Forms.Application.Restart();
                }
            }
            else if (TryToWin(second, secondCanWin, false) == 10)
            {
                draw = 9;
                MessageBoxResult gameEnd = MessageBox.Show("Нолики победили! Хотите сыграть еще раз?", "Игра окончена. Еще раз?", MessageBoxButton.YesNo);
                if (gameEnd == MessageBoxResult.No)
                {
                    this.win.Close();
                }
                if (gameEnd == MessageBoxResult.Yes)
                {
                    this.win.Close();
                    System.Windows.Forms.Application.Restart();
                }
            }
        }
        /// <summary>
        /// Ничья в игре. Все клетки заняты - никто не победил.
        /// </summary>
        void Standoff() // ничья
        {
            if (draw == 0) // если все клетки заняты, и никто не победил
            {
                draw = 9;
                MessageBoxResult gameEnd = MessageBox.Show("Ничья! Хотите сыграть еще раз?", "Игра окончена. Еще раз?", MessageBoxButton.YesNo);
                if (gameEnd == MessageBoxResult.No)
                {
                    this.win.Close();
                }
                if (gameEnd == MessageBoxResult.Yes)
                {
                    this.win.Close();
                    System.Windows.Forms.Application.Restart();
                }
            }
        }
        /// <summary>
        /// Метод проверяет, есть ли победитель или подсказывает ход для компьютера.
        /// Возвращает значения тип integer.
        /// 0-8 - клетка, куда нужно ходить компьютеру.
        /// 9 - если нет победителя.
        /// 10 - если есть победитель.
        /// </summary>
        int TryToWin(bool[] bl, List<bool[]> ls, bool flag) // метод возвращает 9, если нельзя победить в этот ход, 10 - если уже есть выигрышная комбинация, от 0 до 8 - клетка, куда нужно ходить для победы
        {
            if (bl[0] == true)
            {
                ls.ElementAt(0)[0] = true;
                ls.ElementAt(3)[0] = true;
                ls.ElementAt(6)[0] = true;
            }
            if (bl[1] == true)
            {
                ls.ElementAt(0)[1] = true;
                ls.ElementAt(4)[0] = true;
            }
            if (bl[2] == true)
            {
                ls.ElementAt(0)[2] = true;
                ls.ElementAt(5)[0] = true;
                ls.ElementAt(7)[0] = true;
            }
            if (bl[3] == true)
            {
                ls.ElementAt(1)[0] = true;
                ls.ElementAt(3)[1] = true;
            }
            if (bl[4] == true)
            {
                ls.ElementAt(1)[1] = true;
                ls.ElementAt(4)[1] = true;
                ls.ElementAt(6)[1] = true;
                ls.ElementAt(7)[1] = true;
            }
            if (bl[5] == true)
            {
                ls.ElementAt(1)[2] = true;
                ls.ElementAt(5)[1] = true;
            }
            if (bl[6] == true)
            {
                ls.ElementAt(2)[0] = true;
                ls.ElementAt(3)[2] = true;
                ls.ElementAt(7)[2] = true;
            }
            if (bl[7] == true)
            {
                ls.ElementAt(2)[1] = true;
                ls.ElementAt(4)[2] = true;
            }
            if (bl[8] == true)
            {
                ls.ElementAt(2)[2] = true;
                ls.ElementAt(5)[2] = true;
                ls.ElementAt(6)[2] = true;
            }
            int result = 9;
            string temp = "";
            for (var x = 0; x < ls.Count; x++ )
            {
                int n = 0;
                for (var y = 0; y < ls[x].Length; y++ )
                {
                    if(ls.ElementAt(x)[y] == true)
                    {
                        n++;
                    }
                }
                if(n==3)
                {
                    result = 10;
                    return result;
                }
                if(n==2)
                {
                    for (var y = 0; y < ls[x].Length; y++)
                    {
                        if (ls.ElementAt(x)[y] == false)
                        {
                            temp = String.Format("{0}{1}", x, y);
                        }
                    }
                }
                if (n == 0 && flag == true)
                {
                    for (var y = 0; y < ls[x].Length; y++)
                    {
                        if (ls.ElementAt(x)[y] == false)
                        {
                            temp = String.Format("{0}{1}", x, y);
                        }
                    }
                }
                if ((temp == "00" || temp == "30" || temp == "60") && ListButton[0].Background == imgNull)
                    result = 0;
                else if ((temp == "01" || temp == "40") && ListButton[1].Background == imgNull)
                    result = 1;
                else if ((temp == "02" || temp == "50" || temp == "70") && ListButton[2].Background == imgNull)
                    result = 2;
                else if ((temp == "10" || temp == "31") && ListButton[3].Background == imgNull)
                    result = 3;
                else if ((temp == "11" || temp == "41" || temp == "61" || temp == "71") && ListButton[4].Background == imgNull)
                    result = 4;
                else if ((temp == "12" || temp == "51") && ListButton[5].Background == imgNull)
                    result = 5;
                else if ((temp == "20" || temp == "32" || temp == "72") && ListButton[6].Background == imgNull)
                    result = 6;
                else if ((temp == "21" || temp == "42") && ListButton[7].Background == imgNull)
                    result = 7;
                else if ((temp == "22" || temp == "52" || temp == "62") && ListButton[8].Background == imgNull)
                    result = 8;
            }
            return result;
        }
        /// <summary>
        /// Метод для заполнения клетки. Используется методами Stroke и PCTurn.
        /// </summary>
        void Turn(int t, int metka) // метод заполнения клетки
        {
            if (ListButton[metka].Background == imgNull)// если клетка пустая
            {
                if (t == 1)
                {
                    ListButton[metka].Background = imgFirst; // меняется картинка в кнопке
                    first[metka] = true; 
                    UserWin();// проверка, есть ли победиль
                }
                else if (t == 2)
                {
                    ListButton[metka].Background = imgSecond;
                    second[metka] = true;
                    PCWin();
                }
                draw--; // уменьшается количество пустых клеток.
                Standoff(); // ничья
            }
        }
        /// <summary>
        /// Метод игры "компьютер с компьютером".
        /// </summary>
        public void PCGame()
        {
            while (TryToWin(second, secondCanWin, false) != 10)
            {
                PCTurn();  // ход компьютера
                Thread.Sleep(200);  // пауза потока
            }
        }
        #endregion
    }
}
