using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBitmap
{
    class HeaderInfo
    {
        int largeur;
        int hauteur;
        int ajoutMultiple4;

        public HeaderInfo(byte[] Data)
        {
            byte[] largeurBinaire = { Data[4], Data[5], Data[6], Data[7] };
            byte[] hauteurBinaire = { Data[8], Data[9], Data[10], Data[11] };

            largeur = BitConverter.ToInt32(largeurBinaire, 0);
            hauteur = BitConverter.ToInt32(hauteurBinaire, 0);

            // Tant que la largeur (le nombre de pixel par ligne) n'est pas un multiple de 4
            while (!((largeur + ajoutMultiple4) * 3 % 4 == 0))
                ajoutMultiple4++;
        }

        public string toString()
        {
            string mot = null;

            mot += "Header Info : \n\n";
            mot += "Largeur = " + largeur + "\n";
            mot += "Hauteur = " + hauteur + "\n";
            mot += "Nombre d'ajout = " + ajoutMultiple4 + "\n";

            return mot;
        }

        public int Largeur
        {
            get { return largeur; }
        }

        public int Hauteur
        {
            get { return hauteur; }
        }

        public int Ajout
        {
            get { return ajoutMultiple4; }
        }

        public int LargeurAug
        {
            get { return (largeur + ajoutMultiple4); }
        }

        public int TailleImage
        {
            get { return (largeur * hauteur); }
        }
    }
}
