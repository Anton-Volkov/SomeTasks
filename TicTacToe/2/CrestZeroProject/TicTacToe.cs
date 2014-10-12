using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrestZeroProject
{
    class TicTacToe
    {
        public TicTacToe(Window win, List<Button> ListButton, int symbol, int type)
        {
            for (int x = 0; x < first.Length; x++)
            {
                first[x] = false;
                second[x] = false;
            }
            this.win = win;
            this.ListButton = ListButton;
            if (symbol == 1)
            {
                this.firstSymbol = "X";
                this.secondSymbol = "O";
            }
            else if (symbol == 0)
            {
                this.firstSymbol = "O";
                this.secondSymbol = "X";
            }
            else
            {
                this.firstSymbol = "X";
                this.secondSymbol = "O";
            }
            this.type = type;
            draw = 9;
            firstCanWin = new List<bool[]>();
            for (int x = 0; x < 9; x++)
            {
                firstCanWin.Add(new bool[] {false, false, false});
            }
            secondCanWin = new List<bool[]>();
            for (int x = 0; x < 9; x++)
            {
                secondCanWin.Add(new bool[] { false, false, false });
            }
        }
        Window win; // форма игры
        List<Button> ListButton; // список кнопок игры
        List<bool[]> firstCanWin; // список с выигрышными комбинациями игрока
        List<bool[]> secondCanWin; // список с выигрышными комбинациями компьютера
        bool[] first = new bool[9]; // массив с ходами первого игрока
        bool[] second = new bool[9]; // массив с ходами второго игрока
        int draw; // ничья
        string firstSymbol; // чем ходит игрок
        string secondSymbol; // чем ходит компьютер
        int type; // тип игры
        public void Stroke(string s) // ход игрока
        {
            if (draw % 2 == 1)
            {
                Turn(1, Convert.ToInt32(s));
            }
            else if (draw % 2 == 0)
            {
                Turn(2, Convert.ToInt32(s));
            }
            if (type == 0 && draw != 9)
            {
                PCTurn();
            }
        }
        void UserWin() // победа игрока
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
        public void PCTurn() // ход компьютера
        {
            Random r = new Random();
            int metka = TryToWin(second, secondCanWin, false); 
            if (((first[2] == true && first[6] == true) || (first[0] == true && first[8] == true)) && draw ==6) // предотвращаем классическую вилку, поставленную по уголкам поля
            {
                if (r.Next(0, 40) % 4 == 0)
                    metka = 1;
                else if (r.Next(0, 40) % 4 == 2)
                    metka = 3;
                else if (r.Next(0, 40) % 4 == 3)
                    metka = 5;
                else
                    metka = 7;
                if (ListButton[metka].Content == "") 
                {
                    Turn(2, metka);
                    return;
                }
            }
            if (metka < 9) // проверяем может ли компьютер выиграть методом TryToWin
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
            if (ListButton[4].Content == "") // если центральная клетка свободна - ходим в нее
            {
                Turn(2, 4);
                return;
            }
            Thread.Sleep(r.Next(40, 100));
            if (r.Next(0, 40) % 4 == 0)
                metka = 8;
            else if (r.Next(0, 40) % 4 == 2)
                metka = 6;
            else if (r.Next(0, 40) % 4 == 3)
                metka = 0;
            else
                metka = 2;
            if (ListButton[metka].Content == "") // ставим в одну из угловых клеток
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
                if (ListButton[x].Content == "")
                {
                    Turn(2, x);
                    return;
                }
            }          
        }
        void PCWin() // победа компьютера
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
                if ((temp == "00" || temp == "30" || temp == "60") && ListButton[0].Content == "")
                    result = 0;
                else if ((temp == "01" || temp == "40") && ListButton[1].Content == "")
                    result = 1;
                else if ((temp == "02" || temp == "50" || temp == "70") && ListButton[2].Content == "")
                    result = 2;
                else if ((temp == "10" || temp == "31") && ListButton[3].Content == "")
                    result = 3;
                else if ((temp == "11" || temp == "41" || temp == "61" || temp == "71") && ListButton[4].Content == "")
                    result = 4;
                else if ((temp == "12" || temp == "51") && ListButton[5].Content == "")
                    result = 5;
                else if ((temp == "20" || temp == "32" || temp == "72") && ListButton[6].Content == "")
                    result = 6;
                else if ((temp == "21" || temp == "42") && ListButton[7].Content == "")
                    result = 7;
                else if ((temp == "22" || temp == "52" || temp == "62") && ListButton[8].Content == "")
                    result = 8;
            }
            return result;
        }
        public void PCGame() 
        {
            while (TryToWin(second, secondCanWin, false) != 10)
            {
                PCTurn();
                Thread.Sleep(200);
            }
        }
        void Turn(int t, int metka) // метод заполнения клетки
        {
            if (t == 1) // походил первый игрок
            {
                if (ListButton[metka].Content == "")
                {
                    ListButton[metka].Content = firstSymbol;
                    ListButton[metka].IsEnabled = false;
                    first[metka] = true;
                    draw--;
                    UserWin();
                    Standoff();
                    return;
                }
            }
            else if (t == 2) // походил второй игрок
            {
                if (Convert.ToString(ListButton[metka].Content) == "")
                {
                    ListButton[metka].Content = secondSymbol;
                    ListButton[metka].IsEnabled = false;
                    second[metka] = true;
                    draw--;
                    PCWin();
                    Standoff();
                    return;
                }
            }
        }
    }
}
