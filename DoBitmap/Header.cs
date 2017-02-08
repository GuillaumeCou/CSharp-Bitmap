using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBitmap
{
    class Header
    {
        string signature = null;

        int tailleTotaleFichier;

        int offset;

        /// <summary>
        /// Initilise une nouvelle instance de Header
        /// </summary>
        /// <param name="Data">Tableau d'octet de taille 14</param>
        public Header(byte[] Data)
        {
            signature += (char)Data[0] + (char)Data[1];

            byte[] tailleBinaire = { Data[2], Data[3], Data[4], Data[5] };

            byte[] offsetBinaire = { Data[10], Data[11], Data[12], Data[13] };

            offset = BitConverter.ToInt32(offsetBinaire, 0);
            tailleTotaleFichier = BitConverter.ToInt32(tailleBinaire, 0);
        }

        public string Signature
        {
            get { return signature; }
        }

        public int TailleFichier
        {
            get { return tailleTotaleFichier; }
        }

        public int Offset
        {
            get { return offset; }
        }

        /// <summary>
        /// Fonction qui retourne un string contenant les informations du "header" de l'image.
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string mot = null;

            mot += "Header :\n\n";
            mot += "Signature = " + signature + "\n";
            mot += "Taille du fichier = " + tailleTotaleFichier + "\n";
            mot += "Offset =" + offset + "\n";

            return mot;
        }
    }
}
