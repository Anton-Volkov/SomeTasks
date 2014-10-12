using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        int symbol = 1; // символ, которым играем
        int type; // тип игры
        TicTacToe t1; // объект класса игры
        List<Button> ListButton; // лист кнопок на гриде
        public MainWindow()
        {
            ChooseSymbolWindow win = new ChooseSymbolWindow(); // окно выбора игры
            win.ShowDialog();
            this.symbol = win.Symbol;
            this.type = win.Type;
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            // устанавливаем фон грида в зависимости от типа игры
            ImageBrush imgNull = new ImageBrush();
            imgNull.ImageSource = new BitmapImage(new Uri("Image/grid2-null.jpg", UriKind.Relative));
            ImageBrush img1 = new ImageBrush();
            img1.ImageSource = new BitmapImage(new Uri("Image/grid2-1.jpg", UriKind.Relative));
            ImageBrush img0 = new ImageBrush();
            img0.ImageSource = new BitmapImage(new Uri("Image/grid2-0.jpg", UriKind.Relative));
            if (this.type == 0 && this.symbol == 1)
                gridForBtn.Background = img1;
            else if (this.type == 0 && this.symbol == 0)
                gridForBtn.Background = img0;
            else if (this.type == 1 || this.type == 2)
                gridForBtn.Background = imgNull;
            ListButton = new List<Button>(); // создаем список кнопок
            foreach (var item in gridForBtn.Children) // заполняем его кнопками с грида
            {
                if (item is Button)
                    ListButton.Add(item as Button);
            }
            t1 = new TicTacToe(MainF1, ListButton, symbol, type); // инициализируем объект класса TicTacToe. Передаем ему основную форму, список с кнопками,символ, которым играем и тип игры

            if (this.symbol == 0 && this.type == 0) // если выбрали "О", то первый ходит компьютер
                t1.PCTurn();
            if (this.type == 2) // если выбрали игру компьютера
                t1.PCGame();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(((Button)sender).Background == t1.ImgNull) // если кнопка пустая
                t1.Stroke(((sender as Button).Tag).ToString()); // при клике вызываем метод Stroke, передаем ему тег кнопки, по которой кликнули
        }
    }
}
