using Android.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace NetRadarMobile
{
    public partial class MainPage : ContentPage, System.ComponentModel.INotifyPropertyChanged
    {
        HubConnection connection;
        string userName = "";
        public string Rx { get; set; } = "0";
        public string Tx { get; set; } = "0";

        public MainPage()
        {       
            InitializeComponent();
            BindingContext = this;
            Task.Run(() => readNetworkUsage());
            userLabel.Text = userName = DeviceInfo.Name;

        }

        private void readNetworkUsage()
        {
            while (true)
            {
                long startRx = TrafficStats.TotalRxBytes;
                long startTx = TrafficStats.TotalTxBytes;
                System.Threading.Thread.Sleep(1000);
                Rx = ((TrafficStats.TotalRxBytes - startRx) / 1024).ToString();
                Tx = ((TrafficStats.TotalTxBytes - startTx) / 1024).ToString();
                OnPropertyChanged(nameof(Rx));
                OnPropertyChanged(nameof(Tx));
            }
        }

        private async void connectBtn_Clicked(object sender, EventArgs e)
        {
            connection = new HubConnectionBuilder()
            .WithUrl(serverURL.Text)
            .Build();
            await connection.StartAsync();
            conState.Text = "Connected !";
            conState.TextColor = Color.Green;
            await Task.Run(() => sendLiveUsage());
            //sendMessage("AllahuAkbar");
        }

        private void sendLiveUsage()
        {
            while (true)
            {
                sendMessage(JsonConvert.SerializeObject(new { Send = Tx, Recieve = Rx, State = "Connected", Interface = NetworkInterface.GetAllNetworkInterfaces()[0].Name.ToString() }));
                System.Threading.Thread.Sleep(1000);
            }
        }

        private async void sendMessage(string msg)
        {
            await connection.InvokeAsync("SendMessage", userName, msg);
        }
    }
}
