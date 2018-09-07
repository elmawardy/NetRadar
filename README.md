# .NET RADAR

*This project was made initially to know who makes 'LAAG!' in a LAN*

*so the idea was to install an agent on Desktops and Mobile phones to read network data and send it to a shared hub so everybody on the LAN knows others's network usage and hence know who makes lag 👮👌*


#### The project is totally implemented in C# ecosystem

##### Mobile = Xamarin (android only atm)
##### Desktop = WPF/C#
##### Web (Hub) = ASP.NET/C# (.netcore) & Vuejs

#

First of all you need to deploy your server (the hub) which is a website made with asp.net core and [signalr](https://docs.microsoft.com/en-us/aspnet/core/signalr/?view=aspnetcore-2.1) library , this hub is responsible for gathering network usage from all online agents in real time them display then on the "LAN Usage" page

Once you run the app (Desktop or Mobile) on the client device, you can write your hub URL then share users LAN usage.




