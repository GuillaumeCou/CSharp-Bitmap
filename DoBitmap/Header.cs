using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBitmap
{
    class Header
    {
        byte[] data;

        string signature = null;

        int tailleTotaleFichier;

        int offset;

        /// <summary>
        /// Initilise une nouvelle instance de Header
        /// </summary>
        /// <param name="Data">Tableau d'octet de taille 14</param>
        public Header(byte[] Data)
        {
            data = new byte[Data.Length];
            for(int i = 0; i <  data.Length; i++)
            {
                data[i] = Data[i];
            }

            signature += (char)data[0] + (char)data[1];

            byte[] tailleBinaire = { data[2], data[3], data[4], data[5] };

            byte[] offsetBinaire = { data[10], data[11], data[12], data[13] };

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

        public int TailleHeader
        {
            get { return data.Length; }
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

        public byte[] ValeursBinaires
        {
            get { return data; }
        }

        
    }
}
