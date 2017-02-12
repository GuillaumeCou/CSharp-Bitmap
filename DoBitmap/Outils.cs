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

            foreach (Pixel p in Mat)
            {
                p.Attenuer(Attenuation, RVB);
            }
        }

        public static void Superposition(MyBTM Image1, MyBTM Image2, string PathDestination = @"C:\Users\couzi\Documents\Cours\Projet Info\Test001-1.bmp")
        {
            // La matrice1 est supposée plus petite que la matrice2
            Pixel[,] MatPP = Image1.MatricePixel;
            Pixel[,] MatPG = Image2.MatricePixel;
            bool AjoutPossible = false;

            if (MatPP.GetLength(0) < MatPG.GetLength(0) && MatPP.GetLength(1) < MatPG.GetLength(1))
                AjoutPossible = true;

            if (MatPP.GetLength(0) > MatPG.GetLength(0) && MatPP.GetLength(1) > MatPG.GetLength(1))
            {
                Pixel[,] Var = MatPP;
                MatPP = MatPG;
                MatPG = Var;
                AjoutPossible = true;
            }


            if (AjoutPossible)
            {
                for (int i = 0; i < MatPP.GetLength(0); i++)
                {
                    for (int j = 0; j < MatPP.GetLength(1); j++)
                    {
                        MatPG[i, j] = MatPP[i, j];
                    }
                }
            }

            Image2.Exporter(PathDestination);
        }
    }
}
