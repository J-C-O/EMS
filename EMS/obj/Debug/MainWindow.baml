   M S B A M L   `   `   `  ����>  :EMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EMSEMS  5   	   %clr-namespace:EMS.Views	EMS.Views  5   	   /clr-namespace:EMS.ViewModelsEMS.ViewModels  5   	       EMS.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:EMS     5   	   )viewsclr-namespace:EMS.Views     5   	   3
viewmodelsclr-namespace:EMS.ViewModels     5   	     � Title$	  EMS��5
   	   $	��450��6   $��1100.773��6"    � 	Resources 5       �� DataType(   "EMS.ViewModels.ManageTreeViewModel   EMS.Views.ManageTreeView � DataContext$  EMS.ViewModels.EditorViewModel   EMS.Views.EditorView��%�   (��       5   3   *, +)(�� W     5   /   *, +)�� 5   
   ManageTreeTemplate��"  63   .R�5         6#   �� 5      �� 5   
   EditorTemplate��"  6/   .R�5         6   �� 5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $	��83* �6   �� 5      $
��166* �6   �� 5      $
��248* �6   �� 5      $
��248* �6   �� 5      $
��248* �6    � RowDefinitions 5!   
   �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      .+�5(   
   ]� ���	#FFB0BCB0$��0��6+   $��0��6;   $��5��6H   .+�5)      �� $
��Left=�6   .+�5*      �� -   5         ��Name$ button_ManageTree��5*   1   $	��100��6   $��50��6%   $��Manage Tree��6L   6b   �� 5+      -   5        $ button_EditTree��5+   1   $	��100��6   $��50��6%   $��	Edit Tree��6J   6^   �� 5,      -   5        $ button_Close��5,   1   $	��100��6   $��50��6%   $��Close��6G   $��Bottom=�6W   6r   5.      5/      �� 50   
   -   5         ��Name$ statusTextBox��50      $��0��6A   $��4��6Q   $��25��6^   $��Bottom=�6j   
���������6)   �� *Status+6�   �� 51   
   $��1��6-   $��0��6=   $��5��6J   $��5��6[   ��6   �� 6[   52      53      