using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using plujesyfem987123;

namespace ObrazkoRzucacz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IP:");
            string IP = Console.ReadLine();
            Console.WriteLine("PORT:");
            int port = int.Parse(Console.ReadLine());
            Console.WriteLine("Mission Name:");
            string missionName = Console.ReadLine();
            double time = 0;

            while (true)
            {
                List<Image> images = new List<Image>();
                images.Add(Image.FromFile(@"andrzej1.jpg"));
                images.Add(Image.FromFile(@"andrzej2.jpg"));
                images.Add(Image.FromFile(@"andrzej3.jpg"));
                images.Add(Image.FromFile(@"andrzej4.jpg"));
                images.Add(Image.FromFile(@"andrzej5.jpg"));

                foreach (var image in images)
                {
                    TCP.Connect(IP, "SendUpdate " + missionName + " Images " + time + " " + Base64.ImageToBase64(image, ImageFormat.Jpeg), port);
                    time+=0.2;
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
