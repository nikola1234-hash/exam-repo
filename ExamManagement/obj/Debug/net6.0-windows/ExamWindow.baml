   M S B A M L   `   `   `  ����I  EExamManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ExamManagement.ModelsExamManagement.Models  5   @       ExamManagement.ExamWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6models#clr-namespace:ExamManagement.Models     6@   @d2http://schemas.microsoft.com/expression/blend/2008     6s   Edxe5http://schemas.devexpress.com/winfx/2008/xaml/editors     5        � Title$  
ExamWindow��5      $	��800��6   $
��1000��6%    � DataContext 5      ��  �� RelativeSource �� *Self+.��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   6.   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   .+�5   
   �� $��0 10 0 0q�6   $��0��6'   $	��200��64   .+�5      �� $��Question��6   $��16(�6(   68   �� 5      -   5         ��Name$ question��5      $��16(�6*   6:   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��1��6;   .+�5      �� $��10q�6   $ Vertical=�6%   .+�5      �� $��Questions Total��6   65   �� 5      ��6   �� *NumberOfQuestions+6A   5      �� 5      $��10q�6   $ Vertical=�6%   .+�5      �� $��Solved Questions��6   66   �� 5      ��6   �� *SolvedQuestions+6?   5       �� 5!      $��10q�6   $ Vertical=�6%   .+�5"      �� $��Unsolved Questions��6   68   �� 5#      ��6   �� *UnsolvedQuestions+6A   5$      5&      �� 5'   
   -   5        $��radioListBoxEdit��5'      $	��300��6   $��10q�67   $��3��6C   $��2��6P    �� SelectedItem 6`   �� *SelectedAnswer+��5(      �� 5)      .R�5*      	� ��6"   ��  �� Path$
 Text�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 d� 	IsChecked
 6e   �� $ 
IsSelected� �� Mode$ TwoWay=�$	 PropertyChanged=� ��  �� AncestorType" ��6  5+      5-      �� 5/   
   -   5        $��20q�5/      $��2��6   $	��200��6*   $��20��66   $��Submit Question��6B   6{   �� 50   
   -   5         ��Name$ 
submitExam��50   B   $��20q�6   $��2��6   $	��200��6*   $��40��66   $��Submit Exam��6V   6�   51      52      