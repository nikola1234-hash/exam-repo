   M S B A M L   `   `   `  ����I  EExamManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ExamManagementExamManagement  5   	   &     ExamManagement.AddQuestionWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ExamManagement     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	     � WindowStartupLocation$  CenterScreen=�5
   	    � WindowState$ 	Maximized=�6.    � Title$ AddQuestionWindow��5   	   $	��450��6#   $	��800��60    � DataContext 5	   	   ��  �� RelativeSource �� *Self+��5      �� 5   
    �� Color$ White��6   6*    � 	Resources 5         	BoolToVis�� 5   
   %   &	        �� 5      6+   5      .��5      � $��Center=�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5       $��* �6   6+   .+�5"   
   �� $��50q�6   $��0��6   $	��600��6,   $��	Question?��5%      ��5&      � 5'       5*      �� 5+      $
��Auto �6+   5-      �� 5.      $
��Auto �6+   50      �� 51      $��* �6+   53       56      �� 57      $
��Auto �6(   �� 59      $��* �6(   .+�5=      �� -   5        	 ��Name$	 questionText��5=   X   $��0��6"   $��0��6/   $	��300��6?   $	��600��6K   y�6n   �� 
 �� Path$
 IsTextQuestion� �� 	Converter# [  6�   �� 5>      -   5         ��Name$ 	imageArea��5>      $��0��60   $��0��6=   $	��300��6�   $	��300��6�   y�6M   �� $
 IsImageQuestion�# [  6�   �� 5?      $��0��6!   $��1��6.   $��10q�6>    �� Orientation$ Vertical=�6J   .+�5@      �� $"��Is Question in image format?��6J    d� 	IsChecked 6$   �� *IsImageQuestion+6s   �� 5A      -   5        $��0 20 0 0q�5A   !   $��	Add Image��6H   y�6\   �� $
 IsImageQuestion�# [  6�   5C      5E      5H      �� 5J   
   $��50q�6   $��0��6   $��1��6,   $��Answer��6<   .��5L      � $	��200��6   $
��Auto��6#    5M      �� 5N      $
��Auto �6$   �� 5O      $
��Auto �6$   �� 5P      $��* �6$   .+�5R      �� -   5        $	 asnwerTextBox��5R   =   $	��300��6   $	��500��6-   5T      �� 5U      $��1��6   $��0 20 0 0q�6(   $��Right=�6:   $��Is Correct?��6v    6V   �� *	IsCorrect+6�   �� 5V      -   5        $��2��5V      $��Right=�6&   $��Bottom=�6B   $��
Add Answer��6t   6�   5W      5X      �� 5Z   
   $��50q�6   $��0��6   $��2��6,   $��List of Answers ��6<   .��5[      �  5\      �� 5]      $
��Auto �6$   64   �� 5^      $
��Auto �6$   64   �� 5_      $��* �6$   61   .+�5a      �� $��0��6   $	��400��6'   $	��600��63    �� DisplayMemberPath$
 Text��5b      ��5a   @   �� *	Answers+5d      '  !HandyControl.Controls.ButtonGroup  5e      $��10q�6!   $��1��6-   $��1��6:      Orientation$ 
Horizontal=�6J   $��Bottom=�6c   $��Right=�6~   .@�5f      �� -   5        $	��150��5f   4   $��Add Another Question��6@   6a   �� 5g      -   5        $��Right=�5g      $��1��69   $	��100��6]   $��Save Question��6i   6�   5i      5k      5m      5n      5o      