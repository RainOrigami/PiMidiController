using System;
using System.Net.Sockets;
using System.Reflection;
using Gtk;
using static System.Net.Mime.MediaTypeNames;
using Application = Gtk.Application;

namespace PiControllerClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2 || !int.TryParse(args[1], out int port))
            {
                Console.WriteLine("Usage: PiControllerClient.exe <host> <port>");
                return;
            }

            Application.Init();
            var win = new MainWindow(args[0], port);
            win.ShowAll();
            Application.Run();
        }
    }
}