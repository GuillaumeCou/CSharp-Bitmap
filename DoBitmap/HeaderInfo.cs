using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBitmap
{
    class HeaderInfo
    {
        byte[] data;
        int largeur;
        int hauteur;
        int ajoutMultiple4;
        int tailleAug;


        public HeaderInfo(byte[] Data)
        {
            data = new byte[Data.Length];
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = Data[i];
            }
            byte[] largeurBinaire = { data[4], data[5], data[6], data[7] };
            byte[] hauteurBinaire = { data[8], data[9], data[10], data[11] };

            largeur = BitConverter.ToInt32(largeurBinaire, 0);
            hauteur = BitConverter.ToInt32(hauteurBinaire, 0);

            // Tant que la largeur (le nombre de pixel par ligne) n'est pas un multiple de 4
            while (!((largeur + ajoutMultiple4) * 3 % 4 == 0))
                ajoutMultiple4++;

            tailleAug = (largeur + ajoutMultiple4) * hauteur;
        }

        /// <summary>
        /// Fonction qui renvoie une chaîne de caractères contenant les informations du HeaderInfo de l'image.
        /// </summary>
        /// <returns></returns>
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

        public int TailleImageAug
        {
            get { return tailleAug; }
        }

        public int TailleImage
        {
            get { return (largeur * hauteur); }
        }

        public int TailleHeaderInfo
        {
            get { return data.Length; }
        }

        public byte[] ValeursBinaires
        {
            get { return data; }
        }

        /// <summary>
        /// Fonction qui copie et retourne les valeurs binaire du HeaderInfo
        /// </summary>
        /// <returns></returns>
        public byte[] Clone()
        {
            byte[] tab = new byte[data.Length];

            for (int i = 0; i < tab.Length; i++)
                tab[i] = data[i];

            return tab;
        }
    }
}
