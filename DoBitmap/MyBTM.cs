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

        int largeur = 0;
        int hauteur = 0;
        int taille;

        Pixel[,] Pix;

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

            byte[] largeurBinaire = { headerInfo[4], headerInfo[5], headerInfo[6], headerInfo[7] };
            byte[] hauteurBinaire = { headerInfo[8], headerInfo[9], headerInfo[10], headerInfo[11] };

            largeur = BitConverter.ToInt32(largeurBinaire,0);
            hauteur = BitConverter.ToInt32(hauteurBinaire, 0);

            taille = (largeur * hauteur)*3;

            image = new byte[taille];
            for (int i = 0; i < taille; i++)
                image[i] = DataBitmap[i + 54];

            toPixel();
        }

        /// <summary>
        /// Fonction convertissant les données image en un tableau de pixels.
        /// </summary>
        private void toPixel()
        {
            Pix = new Pixel[hauteur, largeur];

            byte[] PrePix = new byte[3];
            int index = taille - 1;

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    Pix[hauteur - 1 - i, largeur - 1 - j] = new Pixel(new byte[] { DataBitmap[index - 2], DataBitmap[index - 1], DataBitmap[index] });
                }

                index -= 3;
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
            foreach (Pixel p in Pix)
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
            int index = taille - 1+54;


            for(int i = 0; i< hauteur; i++)
            {
                for(int j =0; j < largeur; j++)
                {
                    for(int o = 0; o < 3; o++)
                    {
                        DataBitmap[index--] = Pix[i, j].Exporter(o);
                    }
                }
            }

            File.WriteAllBytes(PathDestination, DataBitmap);
        }
    }
}
