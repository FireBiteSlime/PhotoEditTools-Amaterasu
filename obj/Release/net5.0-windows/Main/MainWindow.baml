   M S B A M L   `   `   `  ���� I  EPhotoEditTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PhotoEditToolsPhotoEditTools  a ]PresentationFramework.Aero, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35gJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroMicrosoft.Windows.Themes     PhotoEditTools.MainWindow   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:PhotoEditTools     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  WThemesJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero  -     J� 	AllowDrop$
  True�� � Title$ 	Amaterasu��$	��650��$
��1000��.��� ����  j� TileMode$
 Tile=� �� ImageSource$ /4.jpg�� j� ViewportUnits$ Absolute=� j� Viewport$ 0,0,400,400� � RowDefinitions �� -    ��Name$ TopPanel��$
��0.2* ��� -   $ TopControlPanel��$
��0.5* ��� -   $ 	QVertical���� -   $! VerticalCentralControlPanel��$
��0.5* ��� -   $ 	WVertical���� -   $ BottomControlPanel��$
��0.5* � � ColumnDefinitions �� -   	 ��Name$	 LeftControlPanel��$
��Auto ��� -	   $	 Q���� -
   $	 CenterControlPanel��$
��Auto ��� -   $	 W���� -   $	 RightControlPanel��$
��Auto �.+��� -   
 ��Name$
 	imageMain��$��6��$��5���� -    ��Name$	 BLP��$��2��$��6��$
��Left=�$��25��$��0,0,0,0q�
���   �$��Center=�$��25����!� �� -   $	 BCP��$��2��$��6��$��Center=�$��25��
���   �$��Center=�$��25����!� �� -   $	 BRP��$��2��$��6��$��Right=�$��25��$��0,0,0,0q�
���   �$��Center=�$��25����!� �� -   $	 BFP��$��2��$��6��$��Center=�$��23��$��0,55,0,0q�
���   �$��Center=�$��84����!� �� -   $	 TLP��$��2��$��1��$
��Left=�$��25��$��0,0,0,0q�
���   �$��Center=�$��25����!� �� -   $	 TCP��$��2��$��1��$��Center=�$��25��
���   �$��Center=�$��25����!� �� -   $	 TRP��$��2��$��1��$��Right=�$��25��$��0,0,0,0q�
���   �$��Center=�$��25����!� �� -   $	 TFP��$��2��$��1��$��Center=�$��23��$��	0,-55,0,0q�
���   �$��Center=�$��84����!� �� -   $	 RTP��$��4��$��3��$��Right=�$��25��$��0,0,55,0q�
���   �$	��Top=�$��25����!� �� -   $	 RCP��$��4��$��3��$��Right=�$��25��$��0,0,55,0q�
���   �$��Center=�$��25����!� �� -   $	 RBP��$��4��$��3��$��Right=�$��25��$��0,0,55,0q�
���   �$��Bottom=�$��25����!� �� -   $	 RFP��$��4��$��3��$��Center=�$��25��$��0,0,0,0q�
���   �$��Center=�$��84����!� �� -   $	 LTP��$��0��$��3��$��Right=�$��25��$��0,0,0,0q�
���   �$	��Top=�$��25����!� �� -   $	 LCP��$��0��$��3��$��Right=�$��25��$��55,0,0,0q�
���   �$��Center=�$��25����!� �� -   $	 LBP��$��0��$��3��$��Right=�$��25��$��0,0,0,0q�
���   �$��Bottom=�$��25����!� �� -   $	 LFP��$��0��$��3��$��Center=�$��25��$��0,0,0,0q�
���   �$��Center=�$��84������  �� 
TargetType" �� �� Triggers P�  P� Property �@J�IsMouseDirectlyOver P� Value$
 True��. ���  �� Property � �� �� Value$ 30����  � ��$ 89����  �@�Cursor$
 Hand��P�  �@J�IsMouseOver$ False��. ���  � ��$ 25����  � ��$ 84����!� o� $	��Top=�$��5��$��15(�$
��Auto��.@�m� $��Instruments��.@�m� $��Curves��.@�m� -    m�Name$ 
CubicInter��$��Cubic interpolation��${�False��m� -   $ LinearInter��$��Linear interpolation��${�False��m� $��	Filtering��.@�m� -    $ SpatialFiltering��$��Spatial filtering��${�False���� m� -!   $
��Exit��