   M S B A M L   `   `   `  ����I  EExamManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ExamManagementExamManagement  5   	   %    ExamManagement.AddAnswersWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ExamManagement     5   	     � Title$  AddAnswersWindow��5   	   $	��450��6"   $	��800��6/    � WindowStartupLocation$ CenterOwner=�6;    � DataContext 5	      ��  �� RelativeSource �� *Self+.��5
      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   5      �� 5      $
��Auto �6   5       � RowDefinitions 5   
   �� 5      $
��Auto �6   5      �� 5      $
��Auto �6   5      �� 5      $��* �6   5      .+�5   
   �� $��0 0 0 10q�6   $	��500��6'   $	��300��66    �� Orientation$ 
Horizontal=�6F   $	��Top=�6_   $��0��6w   $��0��6�   $��2��6�   .+�5      �� $��Answers:��6   $��16(�6(   �� 5       -   5         ��Name$ itemsControl��5       ��61   �� *	Answers+��5!      �� 5"      .R�5#      A� $
��Auto��6&   5$      ��5&      �� 5'      .R�5(      �� $��20q�6!   $��2q�6-    �� CornerRadius$ 1��6A   
���   �._�5)      �� $��20q�6)   $ Vertical=�65   .+�5*   "   �� $	��200��6@   $��20��6O   $��0 0 10 0q�6[   ��6*   �� *Text+6[   �� 5+   "   .+�5,   &   �� $��Is Correct?��6/   	 d� 	IsChecked	 6E   �� *	IsCorrect+6g   5-   #   5/      50      52      54      55      �� 56   
   $��1��6   $��50q�6"   $��Bottom=�6/   $��Right=�6J   .+�57      �� -   5        $��
Add Answer��57   *   6A   �� 59      -   5        $��Submit��59      6B   5:      5=      5>      