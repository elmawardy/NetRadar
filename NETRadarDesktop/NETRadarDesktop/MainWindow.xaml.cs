using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETRadarDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;
        List<string[]> NetworkInterfaces = new List<string[]>();
        TaskbarIcon tbi = new TaskbarIcon();
        string settingspath = "Resources\\settings.json";

        public MainWindow()
        {
            InitializeComponent();
            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;

            //NetworkInterface[] interfaces
            //    = NetworkInterface.GetAllNetworkInterfaces();

            //foreach (NetworkInterface ni in interfaces)
            //{
            //    NetworkInterfaces.Add(new string[] { ni.Description,ni.Name });
            //}


            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");

            foreach (string ni in performanceCounterCategory.GetInstanceNames())
            {
                interfaceDropDown.Items.Add(ni);
            }
            Task.Run(() => displayUsage());

            connection = new HubConnectionBuilder()
            .WithUrl(serverURL.Text)
            .Build();
            connect();

            computerName.Content = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            if (!File.Exists(settingspath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(settingspath))
                {
                    sw.WriteLine("{'host':'netradar.azurewebsites.net','networkinterface':'-'}");
                }
            }

            //Thread th1 = new Thread(displayUsage);
            //th1.Start();
        }

        private async void connect()
        {
            connection = new HubConnectionBuilder()
            .WithUrl(serverURL.Text)
            .Build();
            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connection started");
                conState.Content = "Connected !";
                conState.Foreground = new SolidColorBrush(Colors.Green);
                conBtn.Content = "Disconnect";
                await connection.InvokeAsync("SendMessage", computerName.Content,JsonConvert.SerializeObject(new { Send=0,Recieve=0,State="Connected",Interface="-" }));
                //await connection.InvokeAsync("SendMessage",
                //  "Anonymoys", "استغفر الله");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void displayUsage()
        {
            PerformanceCounter performanceCounterSent = null;
            PerformanceCounter performanceCounterReceived = null;
            while (true)
            {
                int itemCount = 0;
                object selectedItem = null;
                this.Dispatcher.Invoke(() =>
                {
                    itemCount = interfaceDropDown.Items.Count;
                    selectedItem = interfaceDropDown.SelectedItem == null ? null : interfaceDropDown.SelectedItem;
                });
                if (itemCount > 0 && selectedItem != null)
                {
                    performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", selectedItem.ToString());
                    performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", selectedItem.ToString());
                    int selectedIndex = 0;
                    int postSelectedIndex = 0;
                    this.Dispatcher.Invoke(() =>
                    {
                        selectedIndex = interfaceDropDown.SelectedIndex;
                        postSelectedIndex = selectedIndex;
                    });
                    while (postSelectedIndex == selectedIndex)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            int send = (int)(performanceCounterSent.NextValue() / 1024);
                            int recieve = (int)(performanceCounterReceived.NextValue() / 1024);
                            sendLabel.Content = $"{ send } KB";
                            receiveLabel.Content = $"{ recieve } KB";
                            postSelectedIndex = interfaceDropDown.SelectedIndex;
                            using (StreamWriter sw = File.CreateText(settingspath))
                            {
                                sw.WriteLine($"{{'host':'{serverURL.Text}','networkinterface':'{interfaceDropDown.SelectedItem.ToString()}'}}");
                            }
                            sendUsage(send,recieve);
                        });
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private async void sendUsage(int send,int recieve)
        {
            await connection.InvokeAsync("SendMessage", computerName.Content, JsonConvert.SerializeObject(new { Send = send, Recieve = recieve, State = "Connected", Interface = interfaceDropDown.SelectedItem.ToString() }));
        }

        private void conBtn_Click(object sender, RoutedEventArgs e)
        {
            connect();
        }

        private void hideBtn_Click(object sender, RoutedEventArgs e)
        {
            tbi.Icon = new Icon(@"Resources/icon.ico");
            tbi.ToolTipText = "Show NetRadar";
            tbi.Visibility = Visibility.Visible;
            tbi.TrayMouseDoubleClick += Tbi_TrayMouseDoubleClick;
            this.Hide();
        }

        private void Tbi_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            tbi.Visibility = Visibility.Hidden;
            this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            if (isElevated)
            {
                System.Diagnostics.Process.Start("CMD.exe",$"/C cd c:\\windows\\microsoft.net\\framework\\v4.0.30319 && installUtil.exe {System.Environment.CurrentDirectory}\\Resources\\WindowsService1.exe");
            }
            else
                MessageBox.Show("You must have admin privilege!");  
            //ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
            //ServiceBase[] servicesToRun = new ServiceBase[]
            //              {
            //                  new WindowsService1.Service1()
            //              };
            //ServiceBase.Run(servicesToRun);
        }
    }
}
