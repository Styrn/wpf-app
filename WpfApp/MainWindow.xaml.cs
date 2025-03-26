using Database;
using DiplomAnonima.Properties;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace DiplomAnonima
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string resourceMenuBut = "menuButton";
        string resourceMenuButSelected = "menuButtonSelected";
        string resourceComboBox = "filterComboBoxBoy";
        bool isUserLoggedBefore;
        int userID;
        bool filterIsABoy = true;
        bool leftmenuisopened = false;
        DbCreator DataBase;
        SQLiteCommand command;
        SQLiteConnection sqlcon;
        SQLiteDataReader reader;

        public MainWindow()
        {
            DataBase = new DbCreator();
            DataBase.createDbFile();
            DataBase.createDbConnection();
            DataBase.createTables();

            isUserLoggedBefore = Settings.Default.isUserLoggedBefore;

            if (isUserLoggedBefore)
            {
                userID = Settings.Default.UserLoginId;
            }
            else
            {
                userID = 0;
            }

            sqlcon = new SQLiteConnection(DataBase.createDbConnection());
            sqlcon.Open();
            command = sqlcon.CreateCommand();

            InitializeComponent();
            FirstInitialization();
            MainFieldInitialization("None");
        }

        public void FirstInitialization()
        {
            if (isUserLoggedBefore)
            {
                command.CommandText = $"SELECT User_Name, User_Surname, User_Phone FROM Users WHERE(User_ID = '{userID}')";
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        leftMenuNameSurname.Text = reader.GetString(0) + " " + reader.GetString(1);
                        leftMenuPhone.Text = reader.GetString(2);
                    }
                }

                reader.Close();
                command.CommandText = $"SELECT * FROM Cart WHERE(Cart_User = '{userID}')";
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    leftMenuButEllipse.Visibility = Visibility.Visible;
                    cartEllipse.Visibility = Visibility.Visible;
                }
            }
            else
            {
                leftMenuNameSurname.FontSize = 18;
                leftMenuNameSurname.Text = "Вход не выполнен";
                leftMenuPhone.Text = string.Empty;
                accountExitButText.Text = "Войти";
                accountExitButIcon.Kind = PackIconMaterialKind.Login;
            }

            if (isUserLoggedBefore)
            {
                reader.Close();
                command.CommandText = "SELECT Product_Type FROM Products GROUP BY Product_Type";
                reader = command.ExecuteReader();
            }
            else
            {
                command.CommandText = "SELECT Product_Type FROM Products GROUP BY Product_Type";
                reader = command.ExecuteReader();
            }
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    productTypeComboBox.Items.Add(reader.GetValue(0));
                }
            }

            reader.Close();
            command.CommandText = "SELECT Product_Brand FROM Products GROUP BY Product_Brand";
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    productBrandComboBox.Items.Add(reader.GetValue(0));
                }
            }

            reader.Close();
            command.CommandText = "SELECT Product_Size FROM Products GROUP BY Product_Size";
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    productSizeComboBox.Items.Add(reader.GetValue(0));
                }
            }

            reader.Close();
            command.CommandText = "SELECT Product_Age FROM Products GROUP BY Product_Age";
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    productAgeComboBox.Items.Add(reader.GetValue(0));
                }
            }

            /*StackPanel buttonsPanel = new StackPanel();
            buttonsPanel.Orientation = Orientation.Horizontal;

            Border shadowborder = new Border();
            shadowborder.Background = new SolidColorBrush(Colors.White);
            shadowborder.BorderThickness = new Thickness(1);
            shadowborder.Margin = new Thickness(20);
            shadowborder.CornerRadius = new CornerRadius(0, 0, 8, 8);
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.Direction = 270;
            shadowEffect.Opacity = 1;
            shadowEffect.BlurRadius = 15;
            shadowEffect.RenderingBias = RenderingBias.Quality;
            shadowborder.Effect = shadowEffect;

            StackPanel contentPanel = new StackPanel();

            Image contentImage = new Image();
            contentImage.Width = 150;
            contentImage.Height = 150;
            contentImage.Stretch = Stretch.Fill;
            contentImage.Source = new BitmapImage(new Uri("/Images/1.png", UriKind.Relative));
            contentPanel.Children.Add(contentImage);

            StackPanel infoPanel = new StackPanel();
            infoPanel.Orientation = Orientation.Vertical;

            TextBlock productTitle = new TextBlock();
            productTitle.Text = "Product Text";
            productTitle.HorizontalAlignment = HorizontalAlignment.Left;
            productTitle.Margin = new Thickness(5, 10, 0, 0);
            productTitle.FontWeight = FontWeights.Bold;
            infoPanel.Children.Add(productTitle);

            TextBlock productSize = new TextBlock();
            productSize.Text = "SOme SIze";
            productSize.HorizontalAlignment = HorizontalAlignment.Left;
            productSize.Margin = new Thickness(5, 5, 0, 0);
            infoPanel.Children.Add(productSize);

            TextBlock productAge = new TextBlock();
            productAge.Text = "SOme age";
            productAge.HorizontalAlignment = HorizontalAlignment.Left;
            productAge.Margin = new Thickness(5, 0, 0, 0);
            infoPanel.Children.Add(productAge);

            Button buyButton = new Button();
            buyButton.Height = 30;
            buyButton.Style = FindResource("buyButton") as Style;
            buyButton.Cursor = Cursors.Hand;
            buyButton.Margin = new Thickness(0, 10, 0, 0);

            StackPanel buyButtonContent = new StackPanel();
            buyButtonContent.Orientation = Orientation.Horizontal;

            TextBlock buyButtonText = new TextBlock();
            buyButtonText.Text = "999";
            buyButtonText.FontWeight = FontWeights.Bold;
            buyButtonText.FontSize = 14;
            buyButtonContent.Children.Add(buyButtonText);
            
            PackIconMaterial iconMaterial = new PackIconMaterial();
            iconMaterial.Kind = PackIconMaterialKind.CurrencyRub;
            iconMaterial.Width = 11;
            iconMaterial.Height = 11;
            iconMaterial.VerticalAlignment = VerticalAlignment.Center;
            iconMaterial.Margin = new Thickness(4, 0, 0, 0);
            buyButtonContent.Children.Add(iconMaterial);
            buyButton.Content = buyButtonContent;
            infoPanel.Children.Add(buyButton);

            contentPanel.Children.Add(infoPanel);
            shadowborder.Child = contentPanel;
            buttonsPanel.Children.Add(shadowborder);
            Grid.SetColumn(buttonsPanel, 1);
            MainGrid.Children.Add(buttonsPanel);*/
        }

        public void MainFieldInitialization(string sqlcommand)
        {
            reader.Close();
            if (sqlcommand == "None")
            {
                command.CommandText = "SELECT * FROM Products WHERE(Product_Gender = 'М')";
            }
            else
            {
                command.CommandText = sqlcommand;
            }
            
            reader = command.ExecuteReader();
            int columncheck = 0;
            int rowcheck = 0;
            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            if (reader.HasRows)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
                int butId = 0;
                while (reader.Read())
                {
                    int id = butId + 1;
                    Border shadowborder = new Border
                    {
                        Background = new SolidColorBrush(Colors.White),
                        BorderThickness = new Thickness(1),
                        Margin = new Thickness(20),
                        CornerRadius = new CornerRadius(0, 0, 8, 8)
                    };
                    DropShadowEffect shadowEffect = new DropShadowEffect
                    {
                        Direction = 270,
                        Opacity = 1,
                        BlurRadius = 15,
                        RenderingBias = RenderingBias.Quality
                    };
                    shadowborder.Effect = shadowEffect;

                    StackPanel contentPanel = new StackPanel();

                    Image contentImage = new Image
                    {
                        Width = 150,
                        Height = 200,
                        Stretch = Stretch.Fill
                    };
                    string imagePath = $"/Images/{reader.GetValue(0)}.png";
                    contentImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    contentPanel.Children.Add(contentImage);

                    StackPanel infoPanel = new StackPanel
                    {
                        Orientation = Orientation.Vertical
                    };

                    TextBlock productTitle = new TextBlock
                    {
                        Text = reader.GetString(2) + " " + reader.GetString(1),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(5, 10, 0, 0),
                        FontWeight = FontWeights.Bold
                    };
                    infoPanel.Children.Add(productTitle);

                    TextBlock productSize = new TextBlock
                    {
                        Text = "Размер: " + reader.GetValue(3) + "см",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(5, 5, 0, 0)
                    };
                    infoPanel.Children.Add(productSize);

                    TextBlock productAge = new TextBlock
                    {
                        Text = "Возраст: " + reader.GetValue(4),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(5, 0, 0, 0)
                    };
                    infoPanel.Children.Add(productAge);

                    Button buyButton = new Button
                    {
                        Height = 30,
                        Style = filterIsABoy ? FindResource("buyButton") as Style : FindResource("buyButtonGirl") as Style,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 10, 0, 0),
                    };

                    StackPanel buyButtonContent = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    string buyButtonPriceText = reader.GetInt32(5).ToString();
                    TextBlock buyButtonText = new TextBlock
                    {
                        Text = buyButtonPriceText,
                        FontWeight = FontWeights.Bold,
                        FontSize = 14
                    };
                    buyButtonContent.Children.Add(buyButtonText);

                    PackIconMaterial iconMaterial = new PackIconMaterial
                    {
                        Kind = PackIconMaterialKind.CurrencyRub,
                        Width = 11,
                        Height = 11,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(4, 0, 0, 0)
                    };
                    buyButtonContent.Children.Add(iconMaterial);
                    buyButton.Content = buyButtonContent;
                    buyButton.Click += async (s, e) => 
                    {
                        if (isUserLoggedBefore)
                        {
                            BuyButton_Click(id);
                            buyButtonText.Text = "В корзине";
                            iconMaterial.Visibility = Visibility.Hidden;
                            buyButton.IsEnabled = false;
                            await Task.Delay(1000);
                            buyButtonText.Text = buyButtonPriceText;
                            iconMaterial.Visibility = Visibility.Visible;
                            buyButton.IsEnabled = true;
                        }
                        else
                        {
                            AutorizationWin autorizationWin = new AutorizationWin();
                            Close();
                            autorizationWin.Show();
                        }
                    };
                    infoPanel.Children.Add(buyButton);

                    contentPanel.Children.Add(infoPanel);
                    shadowborder.Child = contentPanel;
                    Grid.SetColumn(shadowborder, columncheck);
                    Grid.SetRow(shadowborder, rowcheck);
                    MainGrid.Children.Add(shadowborder);

                    columncheck++;
                    butId++;

                    if (columncheck == 4)
                    {
                        rowcheck++;
                        MainGrid.RowDefinitions.Add(new RowDefinition
                        {
                            Height = GridLength.Auto
                        });
                        columncheck = 0;
                    }
                }
            }
            else
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = "По вашему запросу ничего не найдено"
                };
                textPanel.Children.Add(textBlock);
                MainGrid.Children.Add(textPanel);
            }
        }

        private void BuyButton_Click(int butId)
        {
            reader.Close();
            command.CommandText = $"INSERT INTO Cart(Cart_User, Cart_Product) VALUES('{userID}', '{butId}')";
            command.ExecuteNonQuery();
            if (cartEllipse.Visibility == Visibility.Hidden)
            {
                cartEllipse.Visibility = Visibility.Visible;
                leftMenuButEllipse.Visibility = Visibility.Visible;
            }
        }

        public void ProfileLayout()
        {
            if (isUserLoggedBefore)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto

                });
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel topprofilepanel = new StackPanel();
                Grid.SetRow(topprofilepanel, 0);
                topprofilepanel.Orientation = Orientation.Horizontal;

                PackIconMaterial profileUserIcon = new PackIconMaterial
                {
                    Kind = PackIconMaterialKind.AccountCircleOutline,
                    Width = 120,
                    Height = 120
                };
                topprofilepanel.Children.Add(profileUserIcon);

                StackPanel topprofileNamepanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(20, 0, 0, 0)
                };

                Grid profileUserNameGrid = new Grid();

                TextBox nameTextBox = new TextBox
                {
                    Width = 150,
                    Style = FindResource("profileTextBox") as Style,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#595959"))
                };

                TextBlock profileUserNameTxt = new TextBlock
                {
                    Margin = new Thickness(20, 0, 20, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    IsHitTestVisible = false,
                    Text = "Ваше имя*"
                };
                Panel.SetZIndex(profileUserNameTxt, 1);

                nameTextBox.TextChanged += (s, e) =>
                {
                    if (nameTextBox.Text != string.Empty)
                    {
                        profileUserNameTxt.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        profileUserNameTxt.Visibility = Visibility.Visible;
                    }
                };
                Color color_foreground_name = (Color)ColorConverter.ConvertFromString("#a6a6a6");
                profileUserNameTxt.Foreground = new SolidColorBrush(color_foreground_name);

                profileUserNameGrid.Children.Add(nameTextBox);
                profileUserNameGrid.Children.Add(profileUserNameTxt);

                Grid profileUserSurameGrid = new Grid();

                TextBox surnameTextBox = new TextBox
                {
                    Width = 150,
                    Style = FindResource("profileTextBox") as Style,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#595959"))
                };

                TextBlock profileUserSurnameTxt = new TextBlock
                {
                    Margin = new Thickness(20, 0, 20, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    IsHitTestVisible = false,
                    Text = "Ваше фамилия*"
                };
                Panel.SetZIndex(profileUserSurnameTxt, 1);

                surnameTextBox.TextChanged += (s, e) =>
                {
                    if (surnameTextBox.Text != string.Empty)
                    {
                        profileUserSurnameTxt.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        profileUserSurnameTxt.Visibility = Visibility.Visible;
                    }
                };
                Color color_foreground_surname = (Color)ColorConverter.ConvertFromString("#a6a6a6");
                profileUserSurnameTxt.Foreground = new SolidColorBrush(color_foreground_surname);

                profileUserSurameGrid.Children.Add(profileUserSurnameTxt);
                profileUserSurameGrid.Children.Add(surnameTextBox);
                topprofileNamepanel.Children.Add(profileUserNameGrid);
                topprofileNamepanel.Children.Add(profileUserSurameGrid);
                topprofilepanel.Children.Add(topprofileNamepanel);
                MainGrid.Children.Add(topprofilepanel);

                Grid profileUserPhoneGrid = new Grid();
                Grid.SetRow(profileUserPhoneGrid, 1);

                TextBox phoneTextBox = new TextBox
                {
                    Width = 150,
                    Style = FindResource("profileTextBox") as Style,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#595959")),
                    MaxLength = 12
                };

                TextBlock profileUserPhoneTxt = new TextBlock
                {
                    Margin = new Thickness(20, 0, 20, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    IsHitTestVisible = false,
                    Text = "Ваш телефон*"
                };
                Panel.SetZIndex(profileUserPhoneTxt, 1);

                phoneTextBox.TextChanged += (s, e) =>
                {
                    if (phoneTextBox.Text != string.Empty)
                    {
                        profileUserPhoneTxt.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        profileUserPhoneTxt.Visibility = Visibility.Visible;
                    }
                };
                phoneTextBox.IsKeyboardFocusedChanged += (s, e) =>
                {
                    string _user_phone = phoneTextBox.Text;
                    _user_phone.Replace(" ", "");
                    if (_user_phone.IndexOf("8") == 0)
                    {
                        StringBuilder sb = new StringBuilder(_user_phone);
                        sb.Insert(0, "+");
                        sb[1] = '7';
                        phoneTextBox.Text = sb.ToString();
                    }
                };
                Color color_foreground_phone = (Color)ColorConverter.ConvertFromString("#a6a6a6");
                profileUserPhoneTxt.Foreground = new SolidColorBrush(color_foreground_phone);

                profileUserPhoneGrid.Children.Add(profileUserPhoneTxt);
                profileUserPhoneGrid.Children.Add(phoneTextBox);
                MainGrid.Children.Add(profileUserPhoneGrid);

                StackPanel profileSavePanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                Grid.SetRow(profileSavePanel, 2);

                Button profileSaveBut = new Button
                {
                    Width = 150,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = FindResource("autorizationLoginButton") as Style
                };
                Grid.SetRow(profileSaveBut, 2);

                TextBlock saveButTxt = new TextBlock
                {
                    Text = "Сохранить"
                };
                profileSaveBut.Content = saveButTxt;
                profileSavePanel.Children.Add(profileSaveBut);

                Border saveNotification = new Border
                {
                    Width = 200,
                    Height = 0,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(10, 0, 0, 0),
                    CornerRadius = new CornerRadius(7),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50eb98"))
                };
                TextBlock saveNotificationTxt = new TextBlock {
                    Text = "Успешно сохранено!",
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#257d4e")),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment= HorizontalAlignment.Center
                };
                saveNotification.Child = saveNotificationTxt;
                profileSavePanel.Children.Add(saveNotification);

                profileSaveBut.Click += async (s, e) =>
                {
                    string _user_phone = phoneTextBox.Text;
                    _user_phone.Replace(" ", "");
                    if (_user_phone.IndexOf("8") == 0)
                    {
                        StringBuilder sb = new StringBuilder(_user_phone);
                        sb.Insert(0, "+");
                        sb[1] = '7';
                        _user_phone = sb.ToString();
                    }
                    reader.Close();
                    command.CommandText = $"UPDATE Users SET User_Phone = '{_user_phone}', User_Name = '{nameTextBox.Text}', User_Surname = '{surnameTextBox .Text}' WHERE(User_ID = '{userID}')";
                    command.ExecuteNonQuery();

                    DoubleAnimation borderHeigthAnimIn = new DoubleAnimation
                    {
                        From = 0,
                        To = 35,
                        Duration = TimeSpan.FromMilliseconds(200)
                    };
                    DoubleAnimation borderHeigthAnimOut = new DoubleAnimation
                    {
                        From = 35,
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(200)
                    };

                    saveNotification.BeginAnimation(Border.HeightProperty, borderHeigthAnimIn);
                    await Task.Delay(2000);
                    saveNotification.BeginAnimation(Border.HeightProperty, borderHeigthAnimOut);
                };

                MainGrid.Children.Add(profileSavePanel);

                reader.Close();
                command.CommandText = $"SELECT * FROM Users WHERE(User_ID = '{userID}')";
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        nameTextBox.Text = reader.GetString(2);
                        surnameTextBox.Text = reader.GetString(3);
                        phoneTextBox.Text = reader.GetString(1);
                    }
                }
            }
            else
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = "Для просмотра и редактирования профиля необходимо выполнить вход в аккаунт. Чтобы это сделать, нажмите",
                };
                textPanel.Children.Add(textBlock);

                Button hyperText = new Button
                {
                    Background = new SolidColorBrush(Colors.White),
                    Width = 35,
                    Height = 17,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    Margin = new Thickness(3, 0, 0, 0)
                };
                TextBlock hyptext = new TextBlock
                {
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "сюда."
                };
                hyperText.Content = hyptext;
                hyperText.Click += (s, rea) =>
                {
                    AutorizationWin autorizationWin = new AutorizationWin();
                    Close();
                    autorizationWin.Show();
                };
                textPanel.Children.Add(hyperText);

                Grid.SetRow(textPanel, 0);
                MainGrid.Children.Add(textPanel);
            }
        }

        public void CartLayout()
        {
            reader.Close();
            command.CommandText = $"SELECT Cart_Product FROM Cart WHERE(Cart_User = '{userID}')";
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                List<int> cart_products = new List<int>();
                while (reader.Read())
                {
                    cart_products.Add(reader.GetInt32(0));
                }

                int columncheck = 0;
                int rowcheck = 0;
                string _user_query;
                foreach (int prodid in cart_products)
                {
                    _user_query = $"SELECT * FROM Products WHERE(Product_ID = '{prodid}')";
                    reader.Close();
                    command.CommandText = _user_query;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MainGrid.RowDefinitions.Add(new RowDefinition
                        {
                            Height = GridLength.Auto
                        });
                        while (reader.Read())
                        {
                            Border shadowborder = new Border
                            {
                                Background = new SolidColorBrush(Colors.White),
                                BorderThickness = new Thickness(1),
                                Margin = new Thickness(20),
                                CornerRadius = new CornerRadius(0, 0, 8, 8)
                            };
                            DropShadowEffect shadowEffect = new DropShadowEffect
                            {
                                Direction = 270,
                                Opacity = 1,
                                BlurRadius = 15,
                                RenderingBias = RenderingBias.Quality
                            };
                            shadowborder.Effect = shadowEffect;

                            StackPanel contentPanel = new StackPanel();

                            Image contentImage = new Image
                            {
                                Width = 150,
                                Height = 200,
                                Stretch = Stretch.Fill
                            };
                            string imagePath = $"/Images/{reader.GetValue(0)}.png";
                            contentImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                            contentPanel.Children.Add(contentImage);

                            StackPanel infoPanel = new StackPanel
                            {
                                Orientation = Orientation.Vertical
                            };

                            TextBlock productTitle = new TextBlock
                            {
                                Text = reader.GetString(2) + " " + reader.GetString(1),
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(5, 10, 0, 0),
                                FontWeight = FontWeights.Bold
                            };
                            infoPanel.Children.Add(productTitle);

                            TextBlock productSize = new TextBlock
                            {
                                Text = "Размер: " + reader.GetValue(3) + "см",
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(5, 5, 0, 0)
                            };
                            infoPanel.Children.Add(productSize);

                            TextBlock productAge = new TextBlock
                            {
                                Text = "Возраст: " + reader.GetValue(4),
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(5, 0, 0, 0)
                            };
                            infoPanel.Children.Add(productAge);

                            Button buyButton = new Button
                            {
                                Height = 30,
                                Style = FindResource("buyButton") as Style,
                                Cursor = Cursors.Hand,
                                Margin = new Thickness(0, 10, 0, 0),
                            };

                            StackPanel buyButtonContent = new StackPanel
                            {
                                Orientation = Orientation.Horizontal
                            };

                            string buyButtonPriceText = reader.GetInt32(5).ToString();
                            TextBlock buyButtonText = new TextBlock
                            {
                                Text = buyButtonPriceText,
                                FontWeight = FontWeights.Bold,
                                FontSize = 14
                            };
                            buyButtonContent.Children.Add(buyButtonText);

                            PackIconMaterial iconMaterial = new PackIconMaterial
                            {
                                Kind = PackIconMaterialKind.CurrencyRub,
                                Width = 11,
                                Height = 11,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(4, 0, 0, 0)
                            };
                            buyButtonContent.Children.Add(iconMaterial);
                            buyButton.Content = buyButtonContent;
                            buyButton.IsEnabled = false;

                            infoPanel.Children.Add(buyButton);

                            contentPanel.Children.Add(infoPanel);
                            shadowborder.Child = contentPanel;
                            Grid.SetColumn(shadowborder, columncheck);
                            Grid.SetRow(shadowborder, rowcheck);
                            MainGrid.Children.Add(shadowborder);

                            columncheck++;

                            if (columncheck == 4)
                            {
                                rowcheck++;
                                MainGrid.RowDefinitions.Add(new RowDefinition
                                {
                                    Height = GridLength.Auto
                                });
                                columncheck = 0;
                            }
                        }
                    }
                }
                rowcheck++;
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                Button cartDeleteBut = new Button
                {
                    Width = 150,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = FindResource("cartDeleteButton") as Style,
                    Margin = new Thickness(20, 0, 0, 0)
                };
                TextBlock deleteButTxt = new TextBlock
                {
                    Text = "Очистить корзину"
                };
                cartDeleteBut.Content = deleteButTxt;

                Button cartAcceptBut = new Button
                {
                    Width = 150,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = FindResource("autorizationLoginButton") as Style,
                    Margin = new Thickness(20, 5, 0, 0)
                };
                TextBlock acceptButTxt = new TextBlock
                {
                    Text = "Купить"
                };
                cartAcceptBut.Content = acceptButTxt;

                cartAcceptBut.Click += (s, e) =>
                {
                    reader.Close();
                    foreach (int prodid in cart_products)
                    {
                        command.CommandText = $"INSERT INTO Orders(Order_User, Order_Product, Order_Date) VALUES('{userID}', '{prodid}', '{DateTime.Today:yyyy-dd-MM}')";
                        command.ExecuteNonQuery();
                    }
                    command.CommandText = $"DELETE FROM Cart WHERE(Cart_User = '{userID}')";
                    command.ExecuteNonQuery();

                    MainGrid.RowDefinitions.Clear();
                    MainGrid.Children.Clear();

                    leftMenuButEllipse.Visibility = Visibility.Hidden;
                    cartEllipse.Visibility = Visibility.Hidden;

                    MainGrid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });

                    StackPanel textPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    TextBlock textBlock = new TextBlock
                    {
                        Text = "Спасибо за покупку"
                    };
                    textPanel.Children.Add(textBlock);
                    MainGrid.Children.Add(textPanel);
                };
                cartDeleteBut.Click += (s, e) =>
                {
                    reader.Close();
                    command.CommandText = $"DELETE FROM Cart WHERE(Cart_User = '{userID}')";
                    command.ExecuteNonQuery();

                    MainGrid.RowDefinitions.Clear();
                    MainGrid.Children.Clear();

                    leftMenuButEllipse.Visibility = Visibility.Hidden;
                    cartEllipse.Visibility = Visibility.Hidden;

                    MainGrid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });

                    StackPanel textPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    TextBlock textBlock = new TextBlock
                    {
                        Text = "Корзина очищена"
                    };
                    textPanel.Children.Add(textBlock);
                    MainGrid.Children.Add(textPanel);
                };

                Grid.SetRow(cartDeleteBut, rowcheck);
                Grid.SetColumn(cartDeleteBut, 0);
                MainGrid.Children.Add(cartDeleteBut);

                Grid.SetRow(cartAcceptBut, rowcheck + 1);
                Grid.SetColumn(cartAcceptBut, 0);
                MainGrid.Children.Add(cartAcceptBut);
            }
            else
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = "Ваша корзина пуста"
                };
                textPanel.Children.Add(textBlock);
                MainGrid.Children.Add(textPanel);
            }
        }

        public void OrdersLayout()
        {
            reader.Close();
            command.CommandText = $"SELECT Order_Date FROM Orders WHERE(Order_User = '{userID}') GROUP BY Order_Date";
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                List<int> order_products = new List<int>();
                List<string> order_dates = new List<string>();
                while (reader.Read())
                {
                    order_dates.Add(reader.GetString(0));
                }

                int rowcheck = 0;
                foreach (string order_date in order_dates)
                {
                    reader.Close();
                    command.CommandText = $"SELECT Order_Product FROM Orders WHERE(Order_User = '{userID}' and Order_Date = '{order_date}')";
                    reader = command.ExecuteReader();

                    DateTime date = DateTime.ParseExact(order_date, "yyyy-dd-MM", null);
                    if (reader.HasRows)
                    {
                        order_products.Clear();
                        while (reader.Read())
                        {
                            order_products.Add(reader.GetInt32(0));
                        }

                        MainGrid.RowDefinitions.Add(new RowDefinition
                        {
                            Height = GridLength.Auto
                        });

                        StackPanel textPanel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal
                        };

                        TextBlock textBlock = new TextBlock
                        {
                            Text = date.ToString("dd MMMM yyyy"),
                            FontWeight = FontWeights.Bold
                        };
                        textPanel.Children.Add(textBlock);
                        Grid.SetRow(textPanel, rowcheck);
                        MainGrid.Children.Add(textPanel);
                        rowcheck++;
                    }

                    int columncheck = 0;
                    string _user_query;
                    foreach (int prodid in order_products)
                    {
                        _user_query = $"SELECT * FROM Products WHERE(Product_ID = '{prodid}')";
                        reader.Close();
                        command.CommandText = _user_query;
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            MainGrid.RowDefinitions.Add(new RowDefinition
                            {
                                Height = GridLength.Auto
                            });
                            while (reader.Read())
                            {
                                Border shadowborder = new Border
                                {
                                    Background = new SolidColorBrush(Colors.White),
                                    BorderThickness = new Thickness(1),
                                    Margin = new Thickness(20),
                                    CornerRadius = new CornerRadius(0, 0, 8, 8)
                                };
                                DropShadowEffect shadowEffect = new DropShadowEffect
                                {
                                    Direction = 270,
                                    Opacity = 1,
                                    BlurRadius = 15,
                                    RenderingBias = RenderingBias.Quality
                                };
                                shadowborder.Effect = shadowEffect;

                                StackPanel contentPanel = new StackPanel();

                                Image contentImage = new Image
                                {
                                    Width = 150,
                                    Height = 200,
                                    Stretch = Stretch.Fill
                                };
                                string imagePath = $"/Images/{reader.GetValue(0)}.png";
                                contentImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                                contentPanel.Children.Add(contentImage);

                                StackPanel infoPanel = new StackPanel
                                {
                                    Orientation = Orientation.Vertical
                                };

                                TextBlock productTitle = new TextBlock
                                {
                                    Text = reader.GetString(2) + " " + reader.GetString(1),
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    Margin = new Thickness(5, 10, 0, 0),
                                    FontWeight = FontWeights.Bold
                                };
                                infoPanel.Children.Add(productTitle);

                                TextBlock productSize = new TextBlock
                                {
                                    Text = "Размер: " + reader.GetValue(3) + "см",
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    Margin = new Thickness(5, 5, 0, 0)
                                };
                                infoPanel.Children.Add(productSize);

                                TextBlock productAge = new TextBlock
                                {
                                    Text = "Возраст: " + reader.GetValue(4),
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    Margin = new Thickness(5, 0, 0, 0)
                                };
                                infoPanel.Children.Add(productAge);

                                Button buyButton = new Button
                                {
                                    Height = 30,
                                    Style = FindResource("buyButton") as Style,
                                    Cursor = Cursors.Hand,
                                    Margin = new Thickness(0, 10, 0, 0),
                                };

                                StackPanel buyButtonContent = new StackPanel
                                {
                                    Orientation = Orientation.Horizontal
                                };

                                string buyButtonPriceText = reader.GetInt32(5).ToString();
                                TextBlock buyButtonText = new TextBlock
                                {
                                    Text = buyButtonPriceText,
                                    FontWeight = FontWeights.Bold,
                                    FontSize = 14
                                };
                                buyButtonContent.Children.Add(buyButtonText);

                                PackIconMaterial iconMaterial = new PackIconMaterial
                                {
                                    Kind = PackIconMaterialKind.CurrencyRub,
                                    Width = 11,
                                    Height = 11,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Margin = new Thickness(4, 0, 0, 0)
                                };
                                buyButtonContent.Children.Add(iconMaterial);
                                buyButton.Content = buyButtonContent;
                                buyButton.IsEnabled = false;

                                infoPanel.Children.Add(buyButton);

                                contentPanel.Children.Add(infoPanel);
                                shadowborder.Child = contentPanel;
                                Grid.SetColumn(shadowborder, columncheck);
                                Grid.SetRow(shadowborder, rowcheck);
                                MainGrid.Children.Add(shadowborder);

                                columncheck++;

                                if (columncheck == 4)
                                {
                                    rowcheck++;
                                    MainGrid.RowDefinitions.Add(new RowDefinition
                                    {
                                        Height = GridLength.Auto
                                    });
                                    columncheck = 0;
                                }
                            }
                        }
                    }
                    rowcheck++;
                }
            }
            else
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = "Ранее у вас не было заказов"
                };
                textPanel.Children.Add(textBlock);
                MainGrid.Children.Add(textPanel);
            }
        }

        public void Apply_Filters(object sender, RoutedEventArgs e)
        {
            if (filterIsABoy)
            {
                resourceComboBox = "filterComboBoxBoy";
            }
            else
            {
                resourceComboBox = "filterComboBoxGirl";
            }

            productTypeComboBox.Style = FindResource(resourceComboBox) as Style;
            productBrandComboBox.Style = FindResource(resourceComboBox) as Style;
            productSizeComboBox.Style = FindResource(resourceComboBox) as Style;
            productAgeComboBox.Style = FindResource(resourceComboBox) as Style;

            string product_gender = filterIsABoy ? "М" : "Ж";
            string _user_query = "";
            int pop = 0, count = -1;
            List<int> indexes = new List<int>();
            List<int> count_spis = new List<int>();

            if (productTypeComboBox.SelectedIndex != 0)
            {
                indexes.Add(productTypeComboBox.SelectedIndex);
            }
            else
            {
                indexes.Add(0);
            }

            if (productBrandComboBox.SelectedIndex != 0)
            {
                indexes.Add(productBrandComboBox.SelectedIndex);
            }
            else
            {
                indexes.Add(0);
            }

            if (productSizeComboBox.SelectedIndex != 0)
            {
                indexes.Add(productSizeComboBox.SelectedIndex);
            }
            else
            {
                indexes.Add(0);
            }

            if (productAgeComboBox.SelectedIndex != 0)
            {
                indexes.Add(productAgeComboBox.SelectedIndex);
            }
            else
            {
                indexes.Add(0);
            }

            foreach (int i in indexes)
            {
                count++;
                if (i != 0)
                {
                    count_spis.Add(count);
                }
                if (i == 0)
                {
                    pop++;
                }
            }

            if (pop == 4)
            {
                if (product_gender == "М")
                {
                    MainFieldInitialization("None");
                }
                else
                {
                    MainFieldInitialization("SELECT * FROM Products WHERE(Product_Gender = 'Ж')");
                }
            }
            else
            {
                foreach (int i in count_spis)
                {
                    if (i == 0)
                    {
                        _user_query += $"Product_Type = '{productTypeComboBox.Text}'";
                    }
                    if (i == 1)
                    {
                        if (_user_query == "")
                        {
                            _user_query += $"Product_Brand = '{productBrandComboBox.Text}'";
                        }
                        else
                        {
                            _user_query += $"and Product_Brand = '{productBrandComboBox.Text}'";
                        }
                    }
                    if (i == 2)
                    {
                        if (_user_query == "")
                        {
                            _user_query += $"Product_Size = '{productSizeComboBox.Text}'";
                        }
                        else
                        {
                            _user_query += $"and Product_Size = '{productSizeComboBox.Text}'";
                        }
                    }
                    if (i == 3)
                    {
                        if (_user_query == "")
                        {
                            _user_query += $"Product_Age = '{productAgeComboBox.Text}'";
                        }
                        else
                        {
                            _user_query += $"and Product_Age = '{productAgeComboBox.Text}'";
                        }
                    }
                }

                string sqlcmnd = $"SELECT * FROM Products WHERE({_user_query} and Product_Gender = '{product_gender}')";
                MainFieldInitialization(sqlcmnd);
            }
        }

        public void Clear_Filters(object sender, RoutedEventArgs e)
        {
            productTypeComboBox.SelectedIndex = 0;
            productBrandComboBox.SelectedIndex = 0;
            productSizeComboBox.SelectedIndex = 0;
            productAgeComboBox.SelectedIndex = 0;

            if (filterIsABoy)
            {
                MainFieldInitialization("None");
            }
            else
            {
                MainFieldInitialization("SELECT * FROM Products WHERE(Product_Gender = 'Ж')");
            }
        }

        public void ChangeLeftMenuState(object sender, RoutedEventArgs e)
        {
            if (leftmenuisopened)
            {
                DoubleAnimation gridWidthAnim = new DoubleAnimation
                {
                    From = LeftMenuGrid.ActualWidth,
                    To = LeftMenuGrid.ActualWidth - 200,
                    Duration = TimeSpan.FromMilliseconds(100)
                };

                ColorAnimation foreground_color = new ColorAnimation();
                Color color_foreground_from = (Color)ColorConverter.ConvertFromString("#add8ff");
                Color color_foreground_to = (Color)ColorConverter.ConvertFromString("#707070");
                LeftMenuBut.Foreground = new SolidColorBrush(color_foreground_from);
                foreground_color.From = color_foreground_from;
                foreground_color.To = color_foreground_to;
                foreground_color.Duration = TimeSpan.FromMilliseconds(100);

                LeftMenuBut.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, foreground_color);
                LeftMenuGrid.BeginAnimation(Grid.WidthProperty, gridWidthAnim);
                leftmenuisopened = false;
                LeftMenuBut.Style = FindResource("topButtonClose") as Style;
            }
            else
            {
                DoubleAnimation gridWidthAnim = new DoubleAnimation
                {
                    From = LeftMenuGrid.ActualWidth,
                    To = LeftMenuGrid.ActualWidth + 200,
                    Duration = TimeSpan.FromMilliseconds(100)
                };

                ColorAnimation foreground_color = new ColorAnimation();
                Color color_foreground_from = (Color)ColorConverter.ConvertFromString("#707070");
                Color color_foreground_to = (Color)ColorConverter.ConvertFromString("#add8ff");
                LeftMenuBut.Foreground = new SolidColorBrush(color_foreground_from);
                foreground_color.From = color_foreground_from;
                foreground_color.To = color_foreground_to;
                foreground_color.Duration = TimeSpan.FromMilliseconds(100);

                LeftMenuBut.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, foreground_color);
                LeftMenuGrid.BeginAnimation(Grid.WidthProperty, gridWidthAnim);
                leftmenuisopened = true;
                LeftMenuBut.Style = FindResource("topButtonOpen") as Style;
            }
        }

        public void CatalogBut_Click(object sender, RoutedEventArgs e)
        {
            catalogBut.Style = FindResource(resourceMenuButSelected) as Style;

            cartBut.Style = FindResource(resourceMenuBut) as Style;
            profileBut.Style = FindResource(resourceMenuBut) as Style;
            ordersBut.Style = FindResource(resourceMenuBut) as Style;
            //favoriteBut.Style = FindResource(resourceMenuBut) as Style;

            filterStackPanel.Visibility = Visibility.Visible;
            LeftMenuBut.IsEnabled = true;
            LeftMenuBut.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            if (filterIsABoy)
            {
                MainFieldInitialization("None");
            }
            else
            {
                MainFieldInitialization("SELECT * FROM Products WHERE(Product_Gender = 'Ж')");
            }

        }

        public void FavoriteBut_Click(object sender, RoutedEventArgs e)
        {
            //favoriteBut.Style = FindResource(resourceMenuButSelected) as Style;

            profileBut.Style = FindResource(resourceMenuBut) as Style;
            cartBut.Style = FindResource(resourceMenuBut) as Style;
            catalogBut.Style = FindResource(resourceMenuBut) as Style;
            ordersBut.Style = FindResource(resourceMenuBut) as Style;

            filterStackPanel.Visibility = Visibility.Visible;
            LeftMenuBut.IsEnabled = false;

            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();

            if (isUserLoggedBefore)
            {

            }
            else
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = "Для просмотра избранных товаров необходимо выполнить вход в аккаунт. Чтобы это сделать, нажмите",
                };
                textPanel.Children.Add(textBlock);

                Button hyperText = new Button
                {
                    Background = new SolidColorBrush(Colors.White),
                    Width = 35,
                    Height = 17,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    Margin = new Thickness(3, 0, 0, 0)
                };
                TextBlock hyptext = new TextBlock
                {
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "сюда."
                };
                hyperText.Content = hyptext;
                hyperText.Click += (s, rea) =>
                {
                    AutorizationWin autorizationWin = new AutorizationWin();
                    Close();
                    autorizationWin.Show();
                };
                textPanel.Children.Add(hyperText);

                Grid.SetRow(textPanel, 0);
                MainGrid.Children.Add(textPanel);
            }
        }

        public void ProfileBut_Click(object sender, RoutedEventArgs e)
        {
            profileBut.Style = FindResource(resourceMenuButSelected) as Style;

            cartBut.Style = FindResource(resourceMenuBut) as Style;
            catalogBut.Style = FindResource(resourceMenuBut) as Style;
            ordersBut.Style = FindResource(resourceMenuBut) as Style;
            //favoriteBut.Style = FindResource(resourceMenuBut) as Style;

            filterStackPanel.Visibility = Visibility.Hidden;
            LeftMenuBut.IsEnabled = false;

            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            ProfileLayout();
        }

        public void OrdersBut_Click(object sender, RoutedEventArgs e)
        {
            ordersBut.Style = FindResource(resourceMenuButSelected) as Style;

            cartBut.Style = FindResource(resourceMenuBut) as Style;
            catalogBut.Style = FindResource(resourceMenuBut) as Style;
            profileBut.Style = FindResource(resourceMenuBut) as Style;
            //favoriteBut.Style = FindResource(resourceMenuBut) as Style;

            filterStackPanel.Visibility = Visibility.Hidden;
            LeftMenuBut.IsEnabled = false;

            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();

            if (isUserLoggedBefore)
            {
                OrdersLayout();
            }
            else
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = "Для просмотра списка ваших заказов необходимо выполнить вход в аккаунт. Чтобы это сделать, нажмите",
                };
                textPanel.Children.Add(textBlock);

                Button hyperText = new Button
                {
                    Background = new SolidColorBrush(Colors.White),
                    Width = 35,
                    Height = 17,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    Margin = new Thickness(3, 0, 0, 0)
                };
                TextBlock hyptext = new TextBlock
                {
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "сюда."
                };
                hyperText.Content = hyptext;
                hyperText.Click += (s, rea) =>
                {
                    AutorizationWin autorizationWin = new AutorizationWin();
                    Close();
                    autorizationWin.Show();
                };
                textPanel.Children.Add(hyperText);

                Grid.SetRow(textPanel, 0);
                MainGrid.Children.Add(textPanel);
            }
        }

        public void CartBut_Click(object sender, RoutedEventArgs e)
        {
            cartBut.Style = FindResource(resourceMenuButSelected) as Style;

            ordersBut.Style = FindResource(resourceMenuBut) as Style;
            catalogBut.Style = FindResource(resourceMenuBut) as Style;
            profileBut.Style = FindResource(resourceMenuBut) as Style;
            //favoriteBut.Style = FindResource(resourceMenuBut) as Style;

            filterStackPanel.Visibility = Visibility.Hidden;
            LeftMenuBut.IsEnabled = false;

            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            if (isUserLoggedBefore)
            {
                CartLayout();
            }
            else
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = "Для просмотра корзины необходимо выполнить вход в аккаунт. Чтобы это сделать, нажмите",
                };
                textPanel.Children.Add(textBlock);

                Button hyperText = new Button
                {
                    Background = new SolidColorBrush(Colors.White),
                    Width = 35,
                    Height = 17,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    Margin = new Thickness(3, 0, 0, 0)
                };
                TextBlock hyptext = new TextBlock
                {
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003682")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "сюда."
                };
                hyperText.Content = hyptext;
                hyperText.Click += (s, rea) =>
                {
                    AutorizationWin autorizationWin = new AutorizationWin();
                    Close();
                    autorizationWin.Show();
                };
                textPanel.Children.Add(hyperText);

                Grid.SetRow(textPanel, 0);
                MainGrid.Children.Add(textPanel);
            }
        }

        public void TabButBoy_Click(object sender, RoutedEventArgs e)
        {
            tabButBoy.Style = FindResource("tabButtonBoyPressed") as Style;
            tabButGirl.Style = FindResource("tabButtonGirl") as Style;
            filterIsABoy = true;
            filterAcceptBut.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            /*ColorAnimation background_color = new ColorAnimation();
            Color color_background_from = (Color)ColorConverter.ConvertFromString("#db53d7");
            Color color_background_to = (Color)ColorConverter.ConvertFromString("#0084ff");
            filterAcceptBut.Background = new SolidColorBrush(color_background_from);
            background_color.From = color_background_from;
            background_color.To = color_background_to;
            background_color.Duration = TimeSpan.FromMilliseconds(200);

            filterAcceptBut.Background.BeginAnimation(SolidColorBrush.ColorProperty, background_color);*/
            filterAcceptBut.Style = FindResource("filterAcceptButtonBoy") as Style;
            filterClearBut.Style = FindResource("filterAcceptButtonBoy") as Style;
        }

        public void TabButGirl_Click(object sender, RoutedEventArgs e)
        {
            tabButBoy.Style = FindResource("tabButtonBoy") as Style;
            tabButGirl.Style = FindResource("tabButtonGirlPressed") as Style;
            filterIsABoy = false;
            filterAcceptBut.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            /*ColorAnimation background_color = new ColorAnimation();
            Color color_background_from = (Color)ColorConverter.ConvertFromString("#0084ff");
            Color color_background_to = (Color)ColorConverter.ConvertFromString("#db53d7");
            filterAcceptBut.Background = new SolidColorBrush(color_background_from);
            background_color.From = color_background_from;
            background_color.To = color_background_to;
            background_color.Duration = TimeSpan.FromMilliseconds(200);

            filterAcceptBut.Background.BeginAnimation(SolidColorBrush.ColorProperty, background_color);*/
            filterAcceptBut.Style = FindResource("filterAcceptButtonGirl") as Style;
            filterClearBut.Style = FindResource("filterAcceptButtonGirl") as Style;
        }

        public void Account_Autorization(object sender, RoutedEventArgs e)
        {
            if (isUserLoggedBefore)
            {
                AutorizationWin autorizationWin = new AutorizationWin();
                Settings.Default.isUserLoggedBefore = false;
                Settings.Default.UserLoginId = 0;
                Settings.Default.Save();
                Close();
                autorizationWin.Show();
            }
            else
            {
                AutorizationWin autorizationWin = new AutorizationWin();
                Close();
                autorizationWin.Show();
            }
        }

        private void MainWin_Closed(object sender, EventArgs e)
        {
            Settings.Default.Save();
            reader.Close();
        }
    }
}