using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.Reflection;
using System.Threading;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        static string pcName = Environment.MachineName;
        dynamic settingsJSON;
        HubConnection connection;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            new Thread(doWork).Start();
        }

        private void doWork()
        {
            Start:
            try
            {
                PerformanceCounter performanceCounterSent = null;
                PerformanceCounter performanceCounterReceived = null;

                string settings = "";
                using (StreamReader sr = new StreamReader(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\settings.json"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        settings += line;
                    }
                }

                settingsJSON = JsonConvert.DeserializeObject(settings);

                connect();

                performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", (string)settingsJSON.networkinterface);
                performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", (string)settingsJSON.networkinterface);

                while (true)
                {
                    int send = (int)(performanceCounterSent.NextValue() / 1024);
                    int recieve = (int)(performanceCounterReceived.NextValue() / 1024);
                    sendUsage(send, recieve);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch(Exception ex){
                WriteToFile(ex.InnerException.ToString());
                WriteToFile(ex.Message.ToString());
                WriteToFile(ex.ToString());
                goto Start;
            };
        }

        private async void connect()
        {
            connection = new HubConnectionBuilder()
            .WithUrl((string)settingsJSON.host)
            .Build();
            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("SendMessage", pcName, JsonConvert.SerializeObject(new { Send = 0, Recieve = 0, State = "Connected", Interface = "-" }));
                //await connection.InvokeAsync("SendMessage",
                //  "Anonymoys", "استغفر الله");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void sendUsage(int send, int recieve)
        {
            await connection.InvokeAsync("SendMessage", pcName, JsonConvert.SerializeObject(new { Send = send, Recieve = recieve, State = "Connected", Interface = settingsJSON.networkinterface }));
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        public void WriteToFile(string Message)
        {
            string filepath = "C:\\output.txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
