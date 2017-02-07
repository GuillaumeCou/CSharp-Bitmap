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
            string path = @"C:\Users\couzi\Documents\Cours\Projet Info\lac_en_montagne.bmp";
            string patharrivee = @"C:\Users\couzi\Documents\Cours\Projet Info\Test001-1.bmp";

            MyBTM test = new MyBTM(path);

            //Outils.AttenuerCouleur(test, 'B', 50);
            Outils.AttenuerCouleur(test, 'V', 100);
            Outils.AttenuerCouleur(test, 'R', 100);

            test.Exporter(patharrivee);

            //test.toString();

            Console.ReadKey();
            
        }
    }
}
