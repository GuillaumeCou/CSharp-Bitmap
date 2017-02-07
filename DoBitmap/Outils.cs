using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBitmap
{
    class Outils
    {
        public static void AttenuerCouleur(MyBTM Image, char RVB, int Attenuation)
        {
            Pixel[,] Mat = Image.MatricePixel;

            foreach(Pixel p in Mat)
            {
                p.Attenuer(Attenuation, RVB);
            }
        }
    }
}
