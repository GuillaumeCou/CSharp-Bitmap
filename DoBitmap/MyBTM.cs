using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DoBitmap
{
    class MyBTM
    {
        byte[] DataBitmap;
        //string type = null;
        byte[] header = new byte[14];
        byte[] headerInfo = new byte[50];
        byte[] image;
        string path;

        Pixel[] Pix;

        public string Path
        {
            get
            {
                return path;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe "MyBTM"
        /// </summary>
        /// <param name="Path">Chemin absolu menant au fichier bitmap</param>
        public MyBTM(string Path)
        {
            path = Path;
            DataBitmap = File.ReadAllBytes(Path);

            for (int i = 0; i < 14; i++)
                header[i] = DataBitmap[i];

            for (int i = 0; i < 40; i++)
            {
                headerInfo[i] = DataBitmap[i + 14];
            }

            int taille = DataBitmap.Length-54;

            image = new byte[taille];
            for (int i = 0; i < taille ; i++)
                image[i] = DataBitmap[i + 54];

            toPixel();
        }

        /// <summary>
        /// Fonction convertissant les données image en un tableau de pixels.
        /// </summary>
        private void toPixel()
        {
            Pix = new Pixel[image.Length / 3];

            byte[] PrePix = new byte[3];
            for(int i = 0; i < image.Length/3; i++)
            {
                for(int j= 0; j < 3; j++)
                {
                    PrePix[j] = image[i*3 + j];
                }
                Pix[i] = new Pixel(PrePix);
            }
        }

        /// <summary>
        /// Fonction qui affiche dans la console, les différentes données du fichier Bitmap
        /// </summary>
        public void toString()
        {
            string StringHeader = null;
            for (int i = 0; i < header.Length; i++)
                StringHeader += header[i] + " ";

            string StringHeaderInfo = null;
            for (int i = 0; i < headerInfo.Length; i++)
                StringHeaderInfo += headerInfo[i] + " ";

            string StringImage = null;
            for (int i = 0; i < image.Length; i++)
                StringImage += image[i] + "\t";

            Console.WriteLine("HEADER : \n \n" + StringHeader);
            Console.WriteLine("HEADERINFO : \n \n" + StringHeaderInfo);
            Console.WriteLine("IMAGE : \n \n" + StringImage);


        }

        /// <summary>
        /// Fonction qui annule les valeurs bleues des pixels
        /// </summary>
        public void SupprimerBleu()
        {
            foreach(Pixel p in Pix)
            {
                p.AttenuerBleu(255);
            }
        }
        /// <summary>
        /// Fonction qui annule les valeurs rouges des pixels
        /// </summary>
        public void SupprimerRouge()
        {
            foreach (Pixel p in Pix)
            {
                p.AttenuerRouge(255);
            }
        }
        /// <summary>
        /// Fonction qui annule les valeurs vertes des pixels
        /// </summary>
        public void SupprimerVert()
        {
            foreach (Pixel p in Pix)
            {
                p.AttenuerVert(255);
            }
        }


        /// <summary>
        /// Fonction qui exporte et convertit les données binaires en fichier bitmap
        /// </summary>
        /// <param name="Path">Chemin de destination</param>
        public void Exporter(string PathDestination)
        {
            for(int i = 0; i < DataBitmap.Length-54; i+=3)
            {
                for(int j = 0; j < 3; j++)
                {
                    DataBitmap[(i + 54 + j)] = Pix[(i/3)].Exporter(j);
                }
            }

            File.WriteAllBytes(PathDestination, DataBitmap);
        }
    }
}
