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
using System.Windows.Shapes;

namespace CrestZeroProject
{
    /// <summary>
    /// Логика взаимодействия для ChooseSymbolWindow.xaml
    /// </summary>
    public partial class ChooseSymbolWindow : Window
    {
        int type = 0; // тип игры, по умолчанию - игра с компьютером

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        int symbol = 1; // крестик или нолик, по умолчанию - крестик

        public int Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        public ChooseSymbolWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e) // выбираем игру с компьютером
        {
            this.Type = 0;
            btnX.IsEnabled = true;
            btnX.Visibility = System.Windows.Visibility.Visible;
            btnO.IsEnabled = true;
            btnO.Visibility = System.Windows.Visibility.Visible;
        }

        private void btn2_Click(object sender, RoutedEventArgs e) // выбираем игру вдвоем
        {
            this.Type = 1;
            this.Close();
        }

        private void btn3_Click(object sender, RoutedEventArgs e) // выбираем игру компьютера
        {
            this.Type = 2;
            this.Close();
        }

        private void btnX_Click(object sender, RoutedEventArgs e) // играем крестиком
        {
            this.Symbol = 1;
            this.Close();
        }
        private void btnO_Click(object sender, RoutedEventArgs e) // играем ноликом
        {
            this.Symbol = 0;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(this.Type == 1 || this.Type == 2)
                this.Symbol = 1;
        }
    }
}
