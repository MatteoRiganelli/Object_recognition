   M S B A M L   `   `   `  ����  WpfFaceDetectionTest%    WpfFaceDetectionTest.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
MainWindow��5   	   $	��739��6   $��965.5��6)   .��5      � .+�5   
   ]� .+�5      o� $	��Top=�6   $��6,0,-6,0q�6(   .@�5	      m� $��_File��6   .@�5
      m� $��_Exit��6   5      m� 5      $��_Attiva/Disattiva Obj��6   .@�5      m� -   5        $ ��_Bicchiere Plastica Bianco��5       m� IsCheckable .  m� 	IsChecked . 6g   m� 5      -   5        $��_Scatola Latte��5       .  . 6[   m� 5      -   5        $��_Tazza��5       .  . 6S   m� 5      -   5        $��_Banana��5       .  . 6T   m� 5      -   5        $��_TazzaGialla��5       .  . 6Y   5      5      5      �� 5   
   -   5         ��Name$ textBox1��5      $
��Left=�6$    �� TextWrapping$
 Wrap=�5      $��TextBox��5      $��66��6    J� RenderTransformOrigin$ -2.392,12.875&�5      $��135,356,0,331q�64   �� 5   
   -   5         ��Name$ 	textBlock��5      $
��Wrap=�6'   $��Frame Number��6;   $��48,357,1056,0q�6O   $��26��6f   $	��Top=�6r   �� 5   
   -	   5        $��image1��5      $
��Fill=�6   $	��640��6-   $	��480��69   $��Right=�6F   $��0,26,22,202q�6b   $ 0.495,0.636&�6w   �� 5   
   -
   5        $ textBox��5      $
��Left=�6#   $	��284��6>   $��	10,26,0,0q�6K   $
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��274��6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   �� 5    
   -   5        $ textBox1_nameObj��5       $
��Left=�6,   $
 Wrap=�5!   
   $	��217��6   $ -2.392,12.875&�5"   
   $��48,420,0,202q�60   $��29.333(�6F   $��Corbel)�6s    �� TextDecorations 6X   Z� 6s   �� 5$   
   -   5        $ textBox1_occurrencesBicc��5$      $
��Left=�64   $
 Wrap=�5%   
   $��59��6   $ -2.392,12.875&�5&   
   $��44,627,0,38q�60   $��29.333(�6E   $��Corbel)�6r    6W   Z� 6r   �� 5(   
   -   5        $ textBlock_Copy��5(      $
��Wrap=�6,   $��	Bicchiere��6@   $��26��6Q   $	��Top=�6]   $��48,556,852,0q�6u   �� 5*   
   -   5        	 ��Name$	 slider��5*      $��25,587,852,96q�6!   �� 5+   
   -   5        $ textBox_List��5+      $
��Left=�6(   $	��150��6C   $��492,535,0,0q�6P   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $	��444��6�   $
 Auto=�6�   �� 5,   
   -   5        $ textBlock_Copy1��5,      $
��Wrap=�6-   $��26��6A   $	��Top=�6M   $��135,556,765,0q�6e   �� 6}   $�Latte��6�   �� 6�   �� 6�   6�   �� 5-   
   -   5        $ textBox1_occurrencesLatte��5-      $
��Left=�65   $
 Wrap=�5.   
   $��60��6   $ -2.392,12.875&�5/   
   $��120,627,0,38q�60   $��29.333(�6F   $��Corbel)�6s    6X   Z� 6s   �� 51   
   -   5        $ textBlock_Copy2��51      $
��Wrap=�6-   $��26��6A   $	��Top=�6M   $��217,556,704,0q�6e   $��Tazza��6|   �� 52   
   -   5        $ textBox1_occurrencesTazza��52      $
��Left=�65   $
 Wrap=�53   
   $��60��6   $ -2.392,12.875&�54   
   $��205,627,0,38q�60   $��29.333(�6F   $��Corbel)�6s    6X   Z� 6s   �� 56   
   -   5        $ textBlock_Copy3��56      $
��Wrap=�6-   $��26��6A   $	��Top=�6M   $��296,556,614,0q�6e   $��Banana��6|   �� 57   
   -   5        $  textBox1_occurrencesBanana��57      $
��Left=�66   $
 Wrap=�58   
   $��60��6   $ -2.392,12.875&�59   
   $��284,627,0,38q�60   $��29.333(�6F   $��Corbel)�6s    6X   Z� 6s   �� 5;   
   -   5        $ textBlock_Copy4��5;      $
��Wrap=�6-   $��26��6A   $	��Top=�6M   $��380,556,530,0q�6e   $��Tazza2��6|   �� 5<   
   -   5        $% textBox1_occurrencesTazzaGialla��5<      $
��Left=�6;   $
 Wrap=�5=   
   $��60��6   $ -2.392,12.875&�5>   
   $��368,627,0,38q�60   $��29.333(�6F   $��Corbel)�6s    6X   Z� 6s   5C      5D      