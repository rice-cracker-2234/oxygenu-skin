   M S B A M L   `   `   `  ����B  >OxygenU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:OxygenUOxygenU  5   	       OxygenU.SyntaxEditor   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:OxygenU     5   	   ^ ZICSharpCode.AvalonEdit, Version=6.1.1.20, Culture=neutral, PublicKeyToken=9cc39be672370310;editor.http://icsharpcode.net/sharpdevelop/avalonedit  5   	   -   5          � Title$  
MainWindow��5	   	   $	��271��6   $	��428��6)    � AllowsTransparency .  � WindowStyle$
 None=�6e   $	��428��6�   $	��271��6�    � Topmost . ��65   Z� '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 1Q�5         CornerRadius$ 0��5         ResizeBorderThickness$ 10q�5         GlassFrameThickness$ 0q�5      5      .��5      �� -   5        	 ��Name$	 
windowBord��5      ���	#FF646464$��2q�69   $��Stretch=�6M   $��Stretch=�6k   $��5q�6�   ���	#FF404040
 �� CornerRadius$
 10��6�   ._�5   
   � $��-2q�6   .+�5      �� -   5        $		 top��5      
���   �$��Stretch=�66   $��24��6T   $	��Top=�6`   ���	#FF646464$
 	10,10,0,0��6�   ._�5      � .+�5      �� $��-1q�6   
�������$��12(�6;   $��Center=�6I   $��Center=�6k   .��6�   Syntax Editor6�   �� 5      -   5        $	����5      $��Right=�60   $��0,4,10,4q�6L   $��Center=�6^   $��30��6y   $��16��6�      	TopButton#��[  6�   $��8(�6�   $��Center=�6�   $��Center=�6�   $��Segoe MDL2 Assets)�6	  
�������6=  �� 5      -   5         ��Name$ minib��5      $��Segoe MDL2 Assets)�6,   $	����6K   $��Right=�6^   $��0,4,45,4q�6z   $��Center=�6�   $��31��6�   $��16��6�   #��[  6�   $��Center=�6�   $��Center=�6  
�������$��8(�6=  6J  �� 5      -   5        $��Segoe MDL2 Assets)�5      $	����6<   $��Right=�6O   $��0,4,81,4q�6k   $��Center=�6}   $��30��6�   $��16��6�   #��[  6�   $��Center=�6�   $��Center=�6�   
�������$��8(�6.  6;  �� 5      -   5        $
 save��5      $��Segoe MDL2 Assets)�6+   $	����6J   $
��Left=�6]   $��10,4,0,4q�6x   $��Center=�6�   $��16��6�   $��16��6�   #��[  6�   $��Center=�6�   $��Center=�6  ���	#FFE27FFF � ToolTip$
 Save��6?  6N  �� 5      -   5        $��Segoe MDL2 Assets)�5      $	����6<   $
��Left=�6O   $��31,4,0,4q�6j   $��Center=�6|   $��16��6�   $��16��6�   #��[  6�   $��Center=�6�   $��Center=�6�   ���	#FF1ED102$ Restore default��61  6K  5      5      � 5       $��Stretch=�6   $��0,24,0,0q�61   $��Stretch=�6C   .+�5!      �� $��10q�6   ���	#FF242424���	#FF646464$��1q�6T   $
 5��6h   ._�5"      '  !ICSharpCode.AvalonEdit.TextEditor  -	   5          Name$ Editor��5"   (      SyntaxHighlightingM  GICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter$	 XML 68   $��1q�6g   
�������$��Jetbrains Mono)�6�   $   HorizontalScrollBarVisibility$
 Auto=�6�      ShowLineNumbers . ��6Q   Z� 6   5#      5$      5%      5&      5'      