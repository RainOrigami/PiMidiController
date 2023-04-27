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
            Application.Init();
            var win = new MainWindow();
            win.ShowAll();
            Application.Run();
        }
    }
}