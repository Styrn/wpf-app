using Database;
using DiplomAnonima.Properties;
using System;
using System.Data.SQLite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DiplomAnonima
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWin.xaml
    /// </summary>
    public partial class AutorizationWin : Window
    {
        bool isLoginState = true;
        DbCreator DataBase;
        SQLiteCommand command;
        SQLiteConnection sqlcon;
        SQLiteDataReader reader;
        public AutorizationWin()
        {
            DataBase = new DbCreator();
            sqlcon = new SQLiteConnection(DataBase.createDbConnection());
            sqlcon.Open();

            command = sqlcon.CreateCommand();

            InitializeComponent();
        }

        public void AutorizationRegistrationBut_Click(object sender, RoutedEventArgs e)
        {
            errTxt.Visibility = Visibility.Hidden;
            if (isLoginState)
            {
                DoubleAnimation gridHeightAnim = new DoubleAnimation
                {
                    From = 0,
                    To = 57.96,
                    Duration = TimeSpan.FromMilliseconds(100)
                };

                autorizationNameGrid.BeginAnimation(Grid.HeightProperty, gridHeightAnim);
                autorizationSurnameGrid.BeginAnimation(Grid.HeightProperty, gridHeightAnim);
                isLoginState = false;
                autorizationRegistrationBut.Style = FindResource("autorizationLoginButton") as Style;
                autorizationLoginBut.Style = FindResource("autorizationRegistrationButton") as Style;
                autorizationPhoneHintText.Text = "Номер телефона*";
                Title = "Регистрация";
                autorizationTitle.Text = Title;
                requiredFieldsText.Visibility = Visibility.Visible;
            }
            else
            {
                if (!(autorizationPhone.Text == string.Empty || autorizationName.Text == string.Empty || autorizationSurname.Text == string.Empty))
                {
                    string _user_phone = autorizationPhone.Text;
                    _user_phone.Replace(" ", "");
                    if (_user_phone.IndexOf("8") == 0)
                    {
                        StringBuilder sb = new StringBuilder(_user_phone);
                        sb.Insert(0, "+");
                        sb[1] = '7';
                        _user_phone = sb.ToString();
                    }

                    command.CommandText = $"SELECT User_ID FROM Users WHERE(User_Phone = '{_user_phone}')";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        errTxt.Text = "Номер телефона зарегистрирован!";
                        errTxt.Visibility = Visibility.Visible;
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        command.CommandText = $"INSERT INTO Users(User_Phone, User_Name, User_Surname) VALUES('{_user_phone}', '{autorizationName.Text}', '{autorizationSurname.Text}')";
                        command.ExecuteNonQuery();

                        command.CommandText = $"SELECT User_ID FROM Users WHERE(User_Phone = '{_user_phone}')";
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Settings.Default.UserLoginId = reader.GetInt32(0);
                                Settings.Default.isUserLoggedBefore = true;
                            }
                            reader.Close();
                            Settings.Default.Save();
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        reader.Close();
                    }

                }
                else
                {
                    errTxt.Text = "Обязательные поля должны быть заполнены!";
                    errTxt.Visibility = Visibility.Visible;
                }
            }
        } 

        public void AutorizationLoginBut_Click(object sender, RoutedEventArgs e)
        {
            errTxt.Visibility = Visibility.Hidden;
            if (!isLoginState)
            {
                DoubleAnimation gridHeightAnim = new DoubleAnimation
                {
                    From = 57.96,
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(100)
                };

                autorizationNameGrid.BeginAnimation(Grid.HeightProperty, gridHeightAnim);
                autorizationSurnameGrid.BeginAnimation(Grid.HeightProperty, gridHeightAnim);
                isLoginState = true;
                autorizationRegistrationBut.Style = FindResource("autorizationRegistrationButton") as Style;
                autorizationLoginBut.Style = FindResource("autorizationLoginButton") as Style;
                autorizationPhoneHintText.Text = "Номер телефона";
                Title = "Вход";
                autorizationTitle.Text = Title;
                requiredFieldsText.Visibility = Visibility.Hidden;
            }
            else
            {
                string _user_phone = autorizationPhone.Text;
                _user_phone.Replace(" ", "");
                if (_user_phone.IndexOf("8") == 0)
                {
                    StringBuilder sb = new StringBuilder(_user_phone);
                    sb.Insert(0, "+");
                    sb[1] = '7';
                    _user_phone = sb.ToString();
                }

                command.CommandText = $"SELECT User_ID FROM Users WHERE(User_Phone = '{_user_phone}')";
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Settings.Default.UserLoginId = reader.GetInt32(0);
                        Settings.Default.isUserLoggedBefore = true;
                    }
                    reader.Close();
                    Settings.Default.Save();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    errTxt.Text = "Номер телефона не найден!";
                    errTxt.Visibility = Visibility.Visible;
                    reader.Close();
                }
            }
        }
    }
}
