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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        public string cur_text = "";
        public CaptchaWindow()
        {
            InitializeComponent();
            captchaTB.Text = GenerateCaptcha();
            
        }

        private string GenerateCaptcha()
        {
            var str = "";
            var chars = new string[] { "A", "B", "#", "^", "1", "!", "I", "-", "7", "0", "./", "~", "@" };
            var rnd = new Random(); 
            for(int i = 0; i < 4; i++)
            {
                str+=chars[rnd.Next(chars.Length-1)];
            }
            cur_text = str;
            return str;
        }

        private void acceptBTN_Click(object sender, RoutedEventArgs e)
        {
            if (captchaEntered_TB.Text == captchaTB.Text)
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                captchaTB.Text = GenerateCaptcha();
            }
        }

        private void updateBTN_Click(object sender, RoutedEventArgs e)
        {
            captchaTB.Text = GenerateCaptcha();
        }
    }
}
