   M S B A M L   `   `   `  ���� I  EPhotoEditTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PhotoEditToolsPhotoEditTools  &     PhotoEditTools.ImageReviewerPage   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:PhotoEditTools     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     J� 	AllowDrop$
  True�� � Title$ ImageReviewerPage��$	��450��$	��350�� � WindowStyle$
 None=� � 
ResizeMode$ 	CanResize=�.���  � ColumnDefinitions �� $	��38* ��� $
��137* �.+��� -    ��Name$ buttonUpload��$��1��$��	Add image��$	��Top=�$��
10,45,10,0q�$��2���� -   $ 
buttonSave��$��1��$��Save result��$	��Top=�$��
10,10,10,0q�${�False��$��2���� -    ��Name$ Scroll��$��2��$��2��.���� -    ��Name$ itemsControl��$��2�� J� RenderTransformOrigin$ 0.501,0.502&����� Data��.R�� 	 � RowDefinitions	 �� 
 ��Name$
 FirstRow��$
��Auto ��� $
 	SecondRow��$
��Auto ��� $
 ThirdRow��$
��Auto ��� $
 	FourthRow��$
��Auto ��� $
 FifthRow��$
��Auto ��� $
 SixthRow��$
��Auto ��� $
 
SeventhRow��$
��Auto ��� $
 EigthRow��$
��Auto ��� $
 NinthRow��$
��Auto ��� $
 TenthRow��$
��Auto �.+��� $��0��$	��180��$	��172��$��5, 10, 0, 0q����� *ImageSource+��  ��Name$ sliderOpacity��-   $��1�� � Minimum$
 -100Q� � Maximum$	 100Q�$��Center=�$��5, 10, 0, 0q�$	��Top=�$	��172�� �� IsSelectionRangeEnabled$
 True�� � Value �� *	opacity+�� $ sliderBrightness��-   $��2��$ 0Q�$ 2Q�$��Center=�${�False��$��5, 10, 0, 0q�$	��Top=�$	��172��$
 True�� �� *
brightness+�� $��3��$ 0Q�$	 100Q�$��Center=�$��5, 10, 0, 0q�$	��Top=�$	��172��${�False��$
 True���� $��4��$��Center=�$��5, 10, 0, 0q�$	��Top=�$	��172�� �� SelectedIndex �� *method_view+.@��� -   .��None�� -	   .��Normal�� -
   .��Addition (Linear Dodge)�� -   .��	Substract�� -   .��
Multiply�� -   .��Divide�� -   .��Screen�� -   .��
Difference�� -   .��	Overlay�� -   .��	Exclusion�� -   .��
Soft Light�� -   .��
Hard Light�� -   .��Vivid Light�� -   .��Linear Light�� -   .��	Pin Light�� -   .��
Hard Mix�� -   .��Darken Only�� -   .��Lighten Only�� -   .��Color Dodge�� -   .��
Color Burn�� -   .��Linear Burn�� -   .��Experimental1�� -   .��Experimental2�� -   .��Experimental3#  MaterialDesignThemes.Wpf.Card  $��5��$	��172��$��4,7,0,0q��� $ 
HideButton��-    $��0,7,69,0q�$��5��$��50��$��25��$��Right=�$	��▼���� $��Binarization��$��5��$��70��$��0,7,33,0q���  ��Name$ comboBox��-!   $y�Visible=�$��0��$��Center=�$��6��$��5,10,0,0q�$	��Top=�$	��172�� ��  �� Mode$ OneWay=� �� Path$ binarizationData.typeView�.@��� -"   .��None�� -#   .��
Gavrilov�� -$   .��Otsu�� -%   .��	Niblack�� -&   .��	Sauvola�� -'   .��Wolf�� -(   .��	Bradley��  ��Name$ stackPanelInputs��$y�Hidden=�$��5,10,0,0q�$��0��$��7��.+���  ��Name$ label1��$��Window's size:��$��Center=���  ��Name$ textBoxSize�� �� TextWrapping$
 Wrap=�$	��120��$��Center=����� $ OneWay=�$" binarizationData.windowsSize��� $ label2��$��
Parameter:��$��Center=��� $ textBoxParam��$
 Wrap=�$	��120��$��Center=����� $ OneWay=�$  binarizationData.parametrs��� $ 	SetButton��-)   $��8��$��70��$��0��$��Center=�$��0,7,0,0q�$	��Set���� $ Upbutton��-*   $	��▲��$��9��$��Center=�$
��Left=�$��
75,10,0,10q��� $ 
Downbutton��-+   $	��▼��$��9��$��Center=�$��Right=�$��
0,10,70,10q��� $ Removebutton��-,   $	��✖��$��9��$��Center=�$��Center=�$��	5,10,0,10q��� --    ��Name$ GB��$	��Top=�$��Image reviewer��$��0��$��2��$��40��	 �� -.   $
 TopControlPanel�� �� 	AllowDrop$
 True��$��30 ��� -/   $
 CentralPanel��$
��Auto ��� -0   $
 BottomControlPanel��