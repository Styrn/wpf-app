   M S B A M L   `   `   `  ����C  ?BabyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DiplomAnonimaDiplomAnonima  5   	   #    DiplomAnonima.AutorizationWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DiplomAnonima     5   	     � Title$  Вход��5   	   $	��500��6   $	��400��6#   $	��500��6/   $	��400��6?    � WindowStartupLocation$ CenterScreen=�6N   .��5	      �  � RowDefinitions 5
   
   �� 5      $��60 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� -   5         ��Name$ autorizationTitle��5      $��1��6/   $��Вход��6<   $��Center=�6H   $
��Bold"�6e   $��22(�6w   � 5   
   $��2��6   $	��250��6   .+�5      �� -   5        $ autorizationPhoneHintText��5      $
��20 0q�6;   $��Center=�6I   ���#b0b9c6 J� IsHitTestVisible .   K� ZIndex$ 1��5   $   $!��Номер телефона��65   y�5   $   ��  �� ElementName$ autorizationPhone�� �� Path$ Text.IsEmpty�   	BoolToVis �� 	Converter# [  �� 5      -   5        	 ��Name$	 autorizationPhone��5        textboxSearch#��[ 61   
 � 
InputScope$
 TelephoneNumber��6X    �� 	MaxLength$ 12��6u   5      � 5   
   -   5         �Name$ autorizationNameGrid��5   (   $��3��6   $	��250��6   $��0��6F   .+�5      �� $
��20 0q�6   $��Center=�6&   ���#b0b9c6 .  $ 1��5   $   $��Ваше имя*��65   y�5    $   �� $ autorizationName��$ Text.IsEmpty�# [  �� 5!      -   5        $	 autorizationName��5!      #��[ 60   5"      � 5$   
   -   5        $ autorizationSurnameGrid��5$   (   $��4��6   $	��250��6   $��0��6I   .+�5%      �� $
��20 0q�6   $��Center=�6&   ���#b0b9c6 .  $ 1��5&   $   $��Ваша фамилия*��65   y�5'   $   �� $ autorizationSurname��$ Text.IsEmpty�# [  �� 5(      -   5        $	 autorizationSurname��5(      #��[ 63   5)      �� 5+   
   -   5        $ requiredFieldsText��5+      $��5��60   $+��%* - Обязательные поля��6=   $��Center=�6Z   $��	0 0 100 5q�6w   ���#a1a1a1$y�Hidden=�6�   � 5,   
   $��6��6   $	��250��6   .+�5-      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5.      �� -	   5         ��Name$ autorizationLoginBut��5.        autorizationLoginButton#��[ 67    � Cursor$
 Hand��6h   $��0 0 10 0q�6v   $��86��6�   .��50      �� $��Вход��6    51      �� 53      -
   5        $! autorizationRegistrationBut��53       " autorizationRegistrationButton#��[ 6>   $
 Hand��6v   $��10 0 0 0q�6�   $	��136��6�   .��55      �� $��Регистрация��6    56      57      58      �� 5:   
   -   5        $ errTxt��5:      $��7��6$   $4��.Номер телефона не найден!��61   $��Center=�6R   $��0 5 80 0q�6o   
���  ��$y�Hidden=�6�   5<      5=      