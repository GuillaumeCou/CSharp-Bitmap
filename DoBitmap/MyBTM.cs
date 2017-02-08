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
        Header head;
        HeaderInfo headinfo;
        byte[] image;
        string path;

        Pixel[,] Pix;
        int ajoutMultiple4 = 0;

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

            byte[] header = new byte[14];

            for (int i = 0; i < 14; i++)
                header[i] = DataBitmap[i];

            head = new Header(header);

            byte[] headerInfo = new byte[50];
            for (int i = 0; i < 40; i++)
            {
                headerInfo[i] = DataBitmap[i + 14];
            }
            headinfo = new HeaderInfo(headerInfo);




            image = new byte[headinfo.TailleImage * 3];

            for (int i = 0; i < image.Length; i++)
                image[i] = DataBitmap[i + head.Offset];

            toPixel();
        }

        /// <summary>
        /// Fonction convertissant les données image en un tableau de pixels.
        /// </summary>
        private void toPixel()
        {
            int hauteur = headinfo.Hauteur;
            int largeur = headinfo.Largeur;
            Pix = new Pixel[hauteur, largeur];

            byte[] PrePix = new byte[3];
            int index = 0;

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (j < largeur)
                    {
                        byte[] tab = new byte[3];
                        for (int oct = 0; oct < 3; oct++)
                        {
                            tab[oct] = image[index + oct];
                        }

                        Pix[(hauteur - 1 - i), j] = new Pixel(tab);
                    }
                    index += 3;
                }
            }
        }

        /// <summary>
        /// Fonction qui affiche dans la console, les différentes données du fichier Bitmap
        /// </summary>
        public void toString()
        {
            string StringHeader = head.toString();

            string StringHeaderInfo = headinfo.toString();

            string StringImage = null;
            for (int i = 0; i < image.Length; i++)
            {
                if (i % (headinfo.LargeurAug * 3) == 0 && i != 0)
                    StringImage += "\n";

                StringImage += image[i] + "\t";
            }


            Console.WriteLine("HEADER : \n \n" + StringHeader + "\n");
            Console.WriteLine("HEADERINFO : \n \n" + StringHeaderInfo + "\n");
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
            int largeur = headinfo.Largeur;
            int hauteur = headinfo.Hauteur;
            int offset = head.Offset;

            byte[] DataExport = new byte[DataBitmap.Length];

            for (int i = 0; i < offset; i++)
                DataExport[i] = DataBitmap[i];

            int index = 0;

            // Indice de la ligne de la matrice
            for (int i = 0; i < hauteur; i++)
            {
                // Indice de la colonne de la matrice
                for (int j = 0; j < largeur; j++)
                {
                    // Indice de la couleur du (B,V ou R) du pixel
                    for (int oct = 0; oct < 3; oct++)
                    {
                        // Si j est inférieur à la largeur moins les valeurs complémentaire pour la multiplicité de 4
                        if (j < largeur)
                        {
                            DataExport[offset + index + oct] = Pix[(hauteur - 1 - i), j].Exporter(oct);
                        }
                        else
                            DataExport[offset + index + oct] = 0;
                    }
                    index = index + 3;
                }  
            }
            
            File.WriteAllBytes(PathDestination, DataExport);
        }
    }
}
