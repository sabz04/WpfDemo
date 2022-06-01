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
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static User user;
        public int counter = 0;
        int captcha_counter = 0;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new DemoEntity())
            {

                var user =
                    db.User.FirstOrDefault(x => x.Login == logTB.Text && x.Password == passTB.Text);
                if (captcha_counter > 1)
                {
                    CaptchaWindow captchaWindow = new CaptchaWindow();
                    captchaWindow.Show();
                    this.Hide();
                    captcha_counter = 0;
                }
                if (user == null)
                {
                    captcha_counter++;
                    return;
                }
                if(user.Role == "admin")
                {
                    NewWindow win = new NewWindow();
                    win.Show();
                    this.Hide();
                }
            }
        }

        private void regBTN_Click(object sender, RoutedEventArgs e)
        {
            var regForm = new RegisterForm();
            regForm.Show();
            this.Hide();
        }
    }
}
