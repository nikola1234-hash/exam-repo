   M S B A M L   `   `   `  ����I  EExamManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ExamManagement.ViewsExamManagement.Views  5      0    *ExamManagement.Views.LecturerDashboardPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      4local"clr-namespace:ExamManagement.Views     5        N� Title$  Lecturer Dashboard��5      .��5      � .+�5   
    � -   5          �Name$ 	MainFrame��5        � NavigationUIVisibility$ Hidden=�6#   �� 5   
   $��Welcome, {{LecturerName}}��6   �� 5	   
   -   5         ��Name$ WelcomeTextBlock��5	      ��6.   �� *WelcomeMessage+�� 5
   
   -   5         ��Name$ CreateExamButton��5
      $��Create Exam��6+   6A   �� 5   
   -   5         ��Name$ ExamsListView��5      $��0 20 0 0q�6*   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��0 0 20 0q�6:   ��6$   �� *Name+6:   �� 5      -   5        $
��Edit��5   !   60   �� 5      -   5        $��Delete��5   !   62   5      5      5      5      5      