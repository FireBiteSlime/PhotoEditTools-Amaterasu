   M S B A M L   `   `   `  ����I  EPhotoEditTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PhotoEditToolsPhotoEditTools  5   	   1    +PhotoEditTools.SpatialFilteringReviewerPage   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:PhotoEditTools     5   	   -   5          � Title$   Spatial filtering reviewer��5   	   $	��450��6,   $��453.5��69   .��5
      �  � ColumnDefinitions 5   
   �� 5      6!    � RowDefinitions 5   
   �� 5      6   .+�5   
   �� -   5         ��Name$ stackPanelMedian��5      $��10,63,295,212q�6/   .+�5      �� -   5         ��Name$ label6��5      $��Window's radius:��6$   �� 5      -   5         ��Name$ textBoxMedianRadius��5       �� TextWrapping$
 Wrap=�6x   $	��120��6�   $
��Left=�6�   ��63   ��  �� Mode$ OneWay=� �� Path$' !spatialFilteringData.medianRadius�6�   5      � 5   
   -   5        	 �Name$	 gridLinearInput��5      $��0,58,0,0q�6(   $��Stretch=�6:   $��Stretch=�6X   $y�Hidden=�6t   .+�5      �� -   5        $ label2��5      $��Kernel:��6$   $
��Left=�66   $��10,4,0,0q�6Q   $	��Top=�6c   �� 5      -   5        $ label5��5      $��Kernel sum:��6$   $
��Left=�6:   $��	10,0,0,64q�6U   $��Bottom=�6h   �� 5      -   5        $ labelKernelSum��5      $��0��6,   $
��Left=�68   $��	81,0,0,64q�6S   $��Bottom=�6f   �� 5      -	   5        $ textBoxKernel��5      
 �� AcceptsReturn$

 True��6-   $��Stretch=�6B   $��Stretch=�6^   " �� VerticalScrollBarVisibility$
 Auto=�5      $ �� HorizontalScrollBarVisibility$
 Auto=�69   $��10,30,10,90q�6^    � Cursor$ IBeam��6�   ��5   |   �� $ OneWay=�$% spatialFilteringData.kernelView�5   �   �� 5       -
   5        $ label3��5       $#��Gaussian matrix coefficients:��6$   $
��Left=�6L   $��	10,0,0,36q�6g   $��Bottom=�6z   �� 5!      -   5        $ textBoxSigmaGauss��5!      $
��Left=�61   $��	60,0,0,14q�6L   $��3��6_   $
 Wrap=�6h   $��25��6|   $��30��6�   $��Bottom=�6�   $ IBeam��6�   �� 5"      -   5        $ label1��5"      $��Sigma:��6$   $
��Left=�65   $��	10,0,0,10q�6P   $��Bottom=�6d   �� 5#      -   5        $ label4��5#      $��Radius:��6$   $
��Left=�66   $��	95,0,0,10q�6Q   $��Bottom=�6d   �� 5$      -   5        $ textBoxRadiusGauss��5$      $
��Left=�62   $��
143,0,0,14q�6M   $��6��6a   $
 Wrap=�6j   $��Bottom=�6~   $��30��6�   �� 5%      -   5         ��Name$ buttonGenerateGauss��5%      $��Generate��62   $
��Left=�6E   $��
178,0,0,13q�6`   $��Bottom=�6t   6�   5&      �� 5(   
   -   5         ��Name$ comboBox��5(      $
��Left=�6%   $��	10,36,0,0q�6@   $	��Top=�5)      $	��120��6+    �� SelectedIndex 67   �� $ OneWay=�$# spatialFilteringData.typeView�.@�5+      �� .��6   None6!   �� 5,      .��6   Linear6#   �� 5-      .��6   Median6#   5.      �� 5/   
   -   5        $ label��5/      $��Mode:��6   $
��Left=�6/   $��	10,10,0,0q�6J   $	��Top=�6]   �� 50   
   -   5        $ 	buttonSet��50      $	��Set��6$   $��30��62   $��71��6>   $
��Left=�6I   $��
135,36,0,0q�6d   $	��Top=�6x   6�   51      52      