using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrestZeroProject
{
    class TicTacToe
    {
        public TicTacToe(Window win, List<Button> ListButton, string userS)
        {
            for (int x = 0; x < user.Length; x++)
            {
                user[x] = false;
                pc[x] = false;
            }
            this.win = win;
            this.ListButton = ListButton;
            if (userS == "X")
            {
                this.userSymbol = userS;
                this.PCSymbol = "O";
            }
            else if (userS == "O")
            {
                this.userSymbol = userS;
                this.PCSymbol = "X";
            }
            else
            {
                this.userSymbol = "X";
                this.PCSymbol = "O";
            }
            draw = 9;
            userCanWin = new List<bool[]>();
            for (int x = 0; x < 9; x++)
            {
                userCanWin.Add(new bool[] {false, false, false});
            }
            pcCanWin = new List<bool[]>();
            for (int x = 0; x < 9; x++)
            {
                pcCanWin.Add(new bool[] { false, false, false });
            }
        }
        Window win; // форма игры
        List<Button> ListButton; // список кнопок игры
        List<bool[]> userCanWin; // список с выигрышными комбинациями игрока
        List<bool[]> pcCanWin; // список с выигрышными комбинациями компьютера
        bool[] user = new bool[9]; // массив с ходами игрока
        bool[] pc = new bool[9]; // массив с ходами компьютера
        int draw; // ничья
        string userSymbol; // чем ходит игрок
        string PCSymbol; // чем ходит компьютер
        public void Stroke(string s) // ход игрока
        {
            if (ListButton[Convert.ToInt32(s)].Content == "")
            {
                ListButton[Convert.ToInt32(s)].Content = userSymbol;
                user[Convert.ToInt32(s)] = true;
                draw--;
                UserWin();
                Standoff();
            }
        }
        void UserWin() // победа игрока
        {
            if (TryToWin(user, userCanWin) == 10)
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
            else 
            {
                PCTurn();
            }
        }
        public void PCTurn() // ход компьютера
        {
            Random r = new Random();
            int metka = TryToWin(pc, pcCanWin); 
            if (((user[2] == true && user[6] == true) || (user[0] == true && user[8] == true)) && draw ==6) // предотвращаем классическую вилку, поставленную по уголкам поля
            {
                if (r.Next(0, 40) % 4 == 0)
                    metka = 1;
                else if (r.Next(0, 40) % 4 == 2)
                    metka = 3;
                else if (r.Next(0, 40) % 4 == 3)
                    metka = 5;
                else
                    metka = 7;
                if (ListButton[metka].Content == "") // ставим в одну из угловых клеток
                {
                    ListButton[metka].Content = PCSymbol;
                    pc[metka] = true;
                    draw--;
                    PCWin();
                    Standoff();
                    return;
                }
            }
            if (metka < 9) // проверяем может ли компьютер выиграть методом TryToWin
            {
                ListButton[metka].Content = PCSymbol;
                pc[metka] = true;
                draw--;
                PCWin();
                Standoff();
                return;
            }
            metka = TryToWin(user, userCanWin); // проверяем, может ли игрок выиграть методом TryToWin и мешаем ему
            if (metka < 9) // если выбрана метка для хода
            {
                ListButton[metka].Content = PCSymbol;
                pc[metka] = true;
                draw--;
                PCWin();
                Standoff();
                return;
            }
            if (ListButton[4].Content == "") // если центральная клетка свободна - ходим в нее
            {
                ListButton[4].Content = PCSymbol;
                pc[4] = true;
                draw--;
                PCWin();
                Standoff();
                return;
            }
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
                ListButton[metka].Content = PCSymbol;
                pc[metka] = true;
                draw--;
                PCWin();
                Standoff();
                return;
            }
            for (int x = 0; x < pc.Length; x++) // перебираем все клетки, ищем свободную
            {
                if (ListButton[x].Content == "")
                {
                    ListButton[x].Content = PCSymbol;
                    pc[x] = true;
                    draw--;
                    PCWin();
                    Standoff();
                    return;
                }
            }          
        }
        void PCWin() // победа компьютера
        {
            if (TryToWin(pc, pcCanWin) == 10)
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
        int TryToWin(bool[] bl, List<bool[]> ls) // метод возвращает 9, если нельзя победить в этот ход, 10 - если уже есть выигрышная комбинация, от 0 до 8 - клетка, куда нужно ходить для победы
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
            }
            return result;
        }
    }
}
