using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrestZeroProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string symbol;
        TicTacToe t1;
        List<Button> ListButton;
        public MainWindow()
        {
            ChooseSymbolWindow win = new ChooseSymbolWindow();
            win.ShowDialog();
            this.symbol = win.Symbol;
            if (win.Symbol == null) // если не выбрали чем играем, то будет "Х"
                this.symbol = "X";
            InitializeComponent();
            
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            lbSymbol.Content += " " + this.symbol;
            ListButton = new List<Button>(); // создаем список кнопок
            foreach (var item in gridForBtn.Children) // заполняем его кнопками с грида
            {
                if (item is Button)
                    ListButton.Add(item as Button);
            }
            t1 = new TicTacToe(MainF1, ListButton, symbol); // инициализируем объект класса TicTacToe. Передаем ему основную форму, список с кнопками и символ, которым играем.

            if (this.symbol == "O") // если выбрали "О", то первый ходит компьютер
                t1.PCTurn();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                t1.Stroke(((sender as Button).Tag).ToString()); // при клике вызываем метод Stroke, передаем ему тег кнопки, по которой кликнули
        }
    }
}
