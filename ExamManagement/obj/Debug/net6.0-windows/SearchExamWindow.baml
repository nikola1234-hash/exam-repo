   M S B A M L   `   `   `  ����I  EExamManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ExamManagementExamManagement  5   	   %    ExamManagement.SearchExamWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ExamManagement     5   	     � Title$  SearchExamWindow��5   	   $	��450��6"   $	��800��6/    � DataContext 5	      ��  �� RelativeSource �� *Self+.��5
      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ Vertical=�6"   $	��200��69   $
��Left=�6E   $��10q�6`   .+�5      �� -   5         ��Name$ 	SearchBox��5      6+   �� 5      -   5        $��Search��5   -   6@   5      �� 5   
   -   5         ��Name$ 
resultList��5   X   $
��Left=�6   $	��127��6-   $��1��6l   $��	10,22,0,0q�5      $	��Top=�6!   $	��197��69   ��5   :   �� *Exams+��5      �� 5      .R�5      �� .+�5      �� ��6$   �� *Name+5      5      5      �� 5   
   -   5        $��	Open Exam��5   (   $��0��6<   $��1��6I   $	��200��6Y   6g   5       5!      