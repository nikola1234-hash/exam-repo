   M S B A M L   `   `   `  ����I  EExamManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ExamManagement.ModelsExamManagement.Models  5   @   %    ExamManagement.CreateExamWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6models#clr-namespace:ExamManagement.Models     6@   @d2http://schemas.microsoft.com/expression/blend/2008     6s     � Title$  Create Exam��5   	   $	��500��6   $	��600��6*    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�6.    � DataContext 5      ��  �� RelativeSource �� *Self+.��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Create Exam��6   $��20(�6'   $��0 0 0 10q�65   $��0��6G   $��0��6T   $��2��6d   �� 5   
    �� Orientation$ 
Horizontal=�6   $��1��6.   .+�5      �� $��Name:��6   $��0 0 10 0q�6%   $��1��67   $��0��6D   �� 5      -   5         ��Name$ NameTextBox��5      $	��300��6+   $��1��67   $��1��6D   5      �� 5   
   $ 
Horizontal=�6   $��2��6.   $��0 10 0 0q�6;   .+�5       �� $��Date:��6   (  "System.Windows.Controls.DatePicker  5!      -   5        	  Name$	 
DatePicker��5!   7   $��20 0 0 0q�6   $	��200��6+   67   5"      �� 5$   
   $ 
Horizontal=�6   $��3��6.   $��0 10 0 0q�6;   .+�5%      �� $��Starting hour:��6   �� 5&      -   5        $ StartingHourTextBox��5&      $��Yo��63   $	��150��6=   $��20��6I   $��Center=�6U   $	��Top=�6r   6�   5'      �� 5(   
   $ 
Horizontal=�6   $��4��6.   $��0 10 0 0q�6;   .+�5)      �� $��Total time:��6   �� 5*      -   5        $ totalTimeTextBox��5*      $�� ��60   $	��150��68   $��20��6D   $��Center=�6P   $	��Top=�6m   6�   5+      �� 5,   
   $ 
Horizontal=�6   $��6��6.   $��0 10 0 0q�6;   .+�5-      �� $��Randomiz Questions:��6   �� 5.      -   5        
 ��Name$
 rendomizeQuestions��5.      $	��150��63   $��20��6?   $��Center=�6K   $	��Top=�6h   6�   5/      �� 51   
   $��0 0 0 10q�6   $ 
Horizontal=�6'   $	��Top=�6@   $��7��6X   $��0��6e   $��2��6u   .+�52      �� $��
Questions:��6   $��16(�6*   �� 53      -   5         ��Name$ itemsControl��53      ��61   �� *	Questions+��54      �� 55      .R�56      A� $
��Auto��6&   57      ��59      �� 5:      .R�5;      �� $��20q�6!   $��2q�6-    �� CornerRadius$ 1��6A   
���   �._�5<      �� $��20q�6)   $ Vertical=�65   .+�5=   "   �� $	��200��6@   $��20��6O   $��0 0 10 0q�6[   ��6*   �� *Text+6[   �� 5>   "   .+�5?   &   ��  ��Name$ FileInformation��60   $�� ��6I   6S   �� 5@   &   -   5        $��Add file��5@   -   $	��200��6W   $��20��6f   $��0 0 10 0q�6r   5A   #   �� 5B   "   -   5        $��Add Answers��5B   )   ��6X   �� *Text+6|   �� 5C   "   -	   5        $��Delete��5C   )   $	��100��6]   $��	0 10 10 0q�6l   ��6   �� *Text+5D      5E      5G      5I      5J      �� 5L   
   $ 
Horizontal=�6   $��8��6.   $��1��6;   $��Right=�6K   $��Bottom=�6g   $	��400��6�   $	��100��6�   .+�5M      �� -
   5         ��Name$ AddQuestionButton��5M      $��Add Question��60   $��0 0 10 0q�6g   �� 5N      -   5        $ CreateExamButton��5N      $��Create Exam��6/   6E   5O      5R      5S      