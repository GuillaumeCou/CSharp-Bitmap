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

        /// <summary>
        /// Fonction qui superpose deux images
        /// </summary>
        /// <param name="Image1">Image située devant</param>
        /// <param name="Image2">Image située derrière (elle donne sa dimension à l'image de sortie)</param>
        /// <param name="PathDestination">Chemin où sera créé le fichier de la nouvelle image.</param>
        public static void Superposition(MyBTM Image1, MyBTM Image2, string PathDestination)
        {

            Pixel[,] MatAvant = Image1.MatricePixel;
            Pixel[,] MatArriere = Image2.MatricePixel;

            int dimColonne = Minimum(MatAvant.GetLength(0), MatArriere.GetLength(0));
            int dimLigne = Minimum(MatAvant.GetLength(1), MatArriere.GetLength(1));

            
            
            Image2.Exporter(PathDestination);
        }

        static int Minimum(int a, int b)
        {
            if (a <= b)
                return a;
            else
                return b;
        }
    }
}
