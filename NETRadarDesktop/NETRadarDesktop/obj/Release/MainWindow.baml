   M S B A M L   `   `   `  ���� J  FNETRadarDesktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:NETRadarDesktopNETRadarDesktop       NETRadarDesktop.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:NETRadarDesktop       � Title$  Net-Radar (Client)��$	��450��$	��800�� � Icon$ Resources/icon.bmp��.��� .+��� -    ��Name$ conState��$��Disconnected!��$
��Left=�$��	39,10,0,0q�$	��Top=����	#FF970000�� -    ��Name$ conBtn��$��Connect��$
��Left=�$��
139,13,0,0q�$	��Top=�$��75���� $.��(Connect Automatically                   ��$
��Left=�$��	39,41,0,0q�$	��Top=� d� 	IsChecked$
 TrueY��� $��Send : ��$
��Left=�$��
495,10,0,0q�$	��Top=��� -   $ 	sendLabel��$��0 KB/s��
��� � �$
��Left=�$��
542,10,0,0q�$	��Top=��� $
��Left=�$��23��$��
554,53,0,0q� �� TextWrapping$
 Wrap=�$��1��$	��Top=�$��41���� $��Throttle��$
��Left=�$��
474,58,0,0q�$	��Top=��� $
��KB/s��$
��Left=�$��
609,50,0,0q�$	��Top=��� $��People on Network��$
��Left=�$��
34,140,0,0q�$	��Top=�&   System.Windows.Controls.DataGrid  $	��221��$��
53,174,0,0q�$	��Top=�$
��Left=�$	��692��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 4*    Header$
 User��  $ 2* $ 	Send MB/s��  $ 2* $ Receive MB/s��  $ 2* $ State��  $ 2* $ 	Interface���� $��
Receive : ��$
��Left=�$��
495,30,0,0q�$	��Top=��� -   $ receiveLabel��$��0 KB/s��
��� � �$
��Left=�$��
555,30,0,0q�$	��Top=��� -   	 ��Name$	 interfaceDropDown��$
��Left=�$��
547,80,0,0q�$	��Top=�$	��212���� $��Network Interface :��$
��Left=�$��
431,76,0,0q�$	��Top=��� $��Server URL :��$
��Left=�$��	10,61,0,0q�$	��Top=��� -   
 ��Name$
 	serverURL��$
��Left=�$��23��$��	86,62,0,0q�$
 Wrap=�$$��http://localhost:5051/RadarHub��$	��Top=�$	��328���� $��User :��$
��Left=�$��502,107,0,0q�$	��Top=��� -   $ computerName��$��-��$
��Left=�$��542,107,0,0q�$	��Top=�