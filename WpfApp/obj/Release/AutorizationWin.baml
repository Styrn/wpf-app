   M S B A M L   `   `   `  ���� H  DDiplomAnonima, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DiplomAnonimaDiplomAnonima  #    DiplomAnonima.AutorizationWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:DiplomAnonima       � Title$  Вход��$	��500��$	��400��$	��500��$	��400�� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $��60 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� -    ��Name$ autorizationTitle��$��1��$��Вход��$��Center=�$
��Bold"�$��22(�� $��2��$	��250��.+��� -   $ autorizationPhoneHintText��$
��20 0q�$��Center=����#b0b9c6 J� IsHitTestVisible .   K� ZIndex$ 1��$!��Номер телефона��y���  �� ElementName$ autorizationPhone�� �� Path$ Text.IsEmpty�   	BoolToVis �� 	Converter# [  �� -   	 ��Name$	 autorizationPhone��  textboxSearch#��[ 
 � 
InputScope$
 TelephoneNumber�� �� 	MaxLength$ 12��� -    �Name$ autorizationNameGrid��$��3��$	��250��$��0��.+��� $
��20 0q�$��Center=����#b0b9c6 .  $ 1��$��Ваше имя*��y��� $ autorizationName��$ Text.IsEmpty�# [  �� -   $	 autorizationName��#��[ � -   $ autorizationSurnameGrid��$��4��$	��250��$��0��.+��� $
��20 0q�$��Center=����#b0b9c6 .  $ 1��$��Ваша фамилия*��y��� $ autorizationSurname��$ Text.IsEmpty�# [  �� -   $	 autorizationSurname��#��[ �� -   $ requiredFieldsText��$��5��$+��%* - Обязательные поля��$��Center=�$��	0 0 100 5q����#a1a1a1$y�Hidden=�� $��6��$	��250��.+���  �� Orientation$ 
Horizontal=�$��Center=�.+��� -	    ��Name$ autorizationLoginBut��  autorizationLoginButton#��[  � Cursor$
 Hand��$��0 0 10 0q�$��86��.���� $��Вход���� -
   $! autorizationRegistrationBut�� " autorizationRegistrationButton#��[ $
 Hand��$��10 0 0 0q�$	��136��.���� $��Регистрация���� -   $ errTxt��$��7��$4��.Номер телефона не найден!��$��Center=�$��0 5 80 0q�
���  ��$y�Hidden=�