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
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для NewWindow.xaml
    /// </summary>
    public partial class NewWindow : Window
    {
        DispatcherTimer timer;
        User current_user;
        int main_counter = 300;
        int counter = 60;
        int minute;

        public NewWindow()
        {
            InitializeComponent();

            Refresh();
            
            //timer
            timer = new DispatcherTimer();

            minute = (main_counter / counter);
            var item = main_counter % counter;
            counter = item;
            

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            timerInfo.Text = $"Сеанс: {minute} : {counter}";

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (counter == 0 && main_counter > 0)
            {
                minute--;
                counter = 60;

            }
            if (main_counter == 0)
            {
                App.Current.Shutdown();
            }
            main_counter--;
            counter--;
            timerInfo.Text = $"Сеанс: {minute} : {counter}";
            
        }

        private void usersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
           
        }

        private void Refresh()
        {
            usersGrid.ItemsSource = null;
            usersGrid.Items.Clear();
            
            using (var db = new DemoEntity())
            {
                usersGrid.ItemsSource = db.User.ToList();
            }
        }

        private void usersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersGrid.SelectedItem == null) return;
            var selected_user = (User)usersGrid.SelectedItem;

            logTB.Text = selected_user.Login;
            passTB.Text = selected_user.Password;
            roleTB.Text = selected_user.Role;
            current_user = selected_user;
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {

            using(var db = new DemoEntity())
            {
                var user =
                    db.User.FirstOrDefault(x => x.Login == logTB.Text);
                if (user != null) {
                    MessageBox.Show("юзер с таким именем уже существует!");
                    return;
                };
                db.User.Add(
                    new User()
                    {
                        Login = logTB.Text,
                        Password = passTB.Text,
                        Role = roleTB.Text,
                    }
                    );
                db.SaveChanges();
            }
            Refresh();
        }

        private void deleteBTN_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new DemoEntity())
            {
                if (current_user == null) return;
                var user =
                    db.User.FirstOrDefault(x => x.Id == current_user.Id);
                db.User.Remove(user);
                db.SaveChanges();
            }
            Refresh();
        }

        private void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DemoEntity())
            {
                if (current_user == null) return;
                var user =
                    db.User.FirstOrDefault(x => x.Id == current_user.Id);
                user.Login = logTB.Text;
                user.Password = passTB.Text;
                user.Role = roleTB.Text;
                db.SaveChanges();
            }
            Refresh();
        }
    }
}
