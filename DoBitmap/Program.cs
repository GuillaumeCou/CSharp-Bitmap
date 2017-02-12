using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoBitmap
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathLac = @"C:\Users\couzi\Documents\Cours\Projet Info\lac_en_montagne.bmp";
            string pathAmis = @"C:\Users\couzi\Documents\Cours\Projet Info\zh.bmp";
            string pathlena = @"C:\Users\couzi\Documents\Cours\Projet Info\lena.bmp";
            string patharrivee = @"C:\Users\couzi\Documents\Cours\Projet Info\Sortie.bmp";

            MyBTM lac = new MyBTM(pathLac);
            MyBTM amis = new MyBTM(pathAmis);
            MyBTM test = new MyBTM(pathAmis);


            //test.Attenuer(100, 'R');
            //test.Exporter(patharrivee);
            //test.toString();

            Outils.Superposition(lac, amis, patharrivee);

            Console.ReadKey();
            
        }
    }
}
