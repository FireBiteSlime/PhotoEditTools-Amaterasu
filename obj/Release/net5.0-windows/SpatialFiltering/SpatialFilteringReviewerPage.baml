   M S B A M L   `   `   `  ���� I  EPhotoEditTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PhotoEditToolsPhotoEditTools  1    +PhotoEditTools.SpatialFilteringReviewerPage   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:PhotoEditTools     -     � Title$   Spatial filtering reviewer��$	��450��$��453.5��.���  � ColumnDefinitions ��  � RowDefinitions �� .+��� -    ��Name$ stackPanelMedian��$��10,63,295,212q�.+��� -    ��Name$ label6��$��Window's radius:���� -    ��Name$ textBoxMedianRadius�� �� TextWrapping$
 Wrap=�$	��120��$
��Left=�����  �� Mode$ OneWay=� �� Path$' !spatialFilteringData.medianRadius�� -   	 �Name$	 gridLinearInput��$��0,58,0,0q�$��Stretch=�$��Stretch=�$y�Hidden=�.+��� -   $ label2��$��Kernel:��$
��Left=�$��10,4,0,0q�$	��Top=��� -   $ label5��$��Kernel sum:��$
��Left=�$��	10,0,0,64q�$��Bottom=��� -   $ labelKernelSum��$��0��$
��Left=�$��	81,0,0,64q�$��Bottom=��� -	   $ textBoxKernel��
 �� AcceptsReturn$

 True��$��Stretch=�$��Stretch=�" �� VerticalScrollBarVisibility$
 Auto=�$ �� HorizontalScrollBarVisibility$
 Auto=�$��10,30,10,90q� � Cursor$ IBeam������ $ OneWay=�$% spatialFilteringData.kernelView��� -
   $ label3��$#��Gaussian matrix coefficients:��$
��Left=�$��	10,0,0,36q�$��Bottom=��� -   $ textBoxSigmaGauss��$
��Left=�$��	60,0,0,14q�$��3��$
 Wrap=�$��25��$��30��$��Bottom=�$ IBeam���� -   $ label1��$��Sigma:��$
��Left=�$��	10,0,0,10q�$��Bottom=��� -   $ label4��$��Radius:��$
��Left=�$��	95,0,0,10q�$��Bottom=��� -   $ textBoxRadiusGauss��$
��Left=�$��
143,0,0,14q�$��6��$
 Wrap=�$��Bottom=�$��30���� -    ��Name$ buttonGenerateGauss��$��Generate��$
��Left=�$��
178,0,0,13q�$��Bottom=��� -    ��Name$ comboBox��$
��Left=�$��	10,36,0,0q�$	��Top=�$	��120�� �� SelectedIndex �� $ OneWay=�$# spatialFilteringData.typeView�.@��� .��None�� .��Linear�� .��Median�� -   $ label��$��Mode:��$
��Left=�$��	10,10,0,0q�$	��Top=��� -   $ 	buttonSet��$	��Set��$��30��$��71��$
��Left=�$��
135,36,0,0q�$	��Top=�