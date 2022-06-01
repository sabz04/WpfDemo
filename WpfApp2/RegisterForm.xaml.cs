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
    /// Логика взаимодействия для RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
            
        }

        private void timerTB_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void logBTN_Click(object sender, RoutedEventArgs e)
        {
            var logForm = new MainWindow();
            logForm.Show();
            this.Hide();
        }

        private void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DemoEntity())
            {
                //foreach
                //foreach (var item in db.User)
                //{
                //    if (item.Login == logTB.Text && item.Password == passTB.Text)
                //    {
                //        MessageBox.Show("Такой пользователь уже существует!");
                //        return;
                //    }
                //}
                //Linq
                var user =
                    db.User.FirstOrDefault(x => x.Login == logTB.Text && x.Password == passTB.Text);

                if (user != null)
                {
                    MessageBox.Show("Такой пользователь уже существует!");
                    return;
                }

                db.User.Add(
                    new User
                    {
                        Login = logTB.Text,
                        Password = passTB.Text,
                        Role = "user"
                    });
                db.SaveChanges();
            }
        }
    }
}
