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

        int offset; // Taille des informations sur le fichier ( header + headerinfo)
        int largeur;
        int hauteur;
        int taille;

        Pixel[,] Pix;
        int ajoutMultiple4 = 0; // Correspond au octets rajouter en fin de ligne binaire pour assurer la divisibilité par 4

        public string Path
        {
            get
            {
                return path;
            }
        }
        public Pixel[,] MatricePixel
        {
            get { return Pix; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe "MyBTM"
        /// </summary>
        /// <param name="Path">Chemin absolu menant au fichier bitmap</param>
        public MyBTM(string Path)
        {
            // On extrait le code binaire du fichier
            path = Path;
            DataBitmap = File.ReadAllBytes(Path);

            // On sépare les données (header , headerinfo, image)
            for (int i = 0; i < 14; i++)
                header[i] = DataBitmap[i];

            for (int i = 0; i < 40; i++)
            {
                headerInfo[i] = DataBitmap[i + 14];
            }

            // On récupère les dimension de l'image. (4 octets).
            // On récupère aussi la taille du offset afin de savoir où démarre l'image.
            byte[] largeurBinaire = { headerInfo[4], headerInfo[5], headerInfo[6], headerInfo[7] };
            byte[] hauteurBinaire = { headerInfo[8], headerInfo[9], headerInfo[10], headerInfo[11] };
            byte[] offsetBinaire = { header[10], header[11], header[12], header[13] };

            largeur = BitConverter.ToInt32(largeurBinaire, 0);
            hauteur = BitConverter.ToInt32(hauteurBinaire, 0);
            offset = BitConverter.ToInt32(offsetBinaire, 0);

            // On calcule la variable assurant la divisibilité par 4 du nombre d'octets par ligne.
            // Cela nous sera utile pour créer la matrice de pixels sans prendre c'est octets "inutiles" pour nous.
            while (!((largeur + ajoutMultiple4) * 3 % 4 == 0))
                ajoutMultiple4++;

            largeur += ajoutMultiple4;

            // Il y a trois octets par pixel, donc la taille du tableau binaire est le triple des dimension de l'image.
            taille = (largeur * hauteur) * 3;
            
            // On extrait les données binaires de l'image
            image = new byte[largeur * hauteur * 3];
            for (int i = 0; i < image.Length; i++)
                image[i] = DataBitmap[i + offset];

            // Puis on place les pixel dans une matrice
            toPixel();
        }

        /// <summary>
        /// Fonction convertissant les données image en un tableau de pixels.
        /// </summary>
        private void toPixel()
        {
            Pix = new Pixel[hauteur, largeur - ajoutMultiple4];

            byte[] PrePix = new byte[3];
            int index = 0;

            // Les 2 premières boucles permettent de se déplacer dans la matrice.
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    // Nous ne voulons pas des octets rajoutés pour la multiplicité de 4.
                    if (j < largeur - ajoutMultiple4)
                    {
                        // On récupère les 3 octets de chaque pixel
                        byte[] tab = new byte[3];
                        for (int oct = 0; oct < 3; oct++)
                        {
                            tab[oct] = image[index + oct];
                        }

                        Pix[(hauteur - 1 - i), j] = new Pixel(tab);
                    }
                    // On avance de 3 dans le tableau "image" car pour un pixel, il y a trois valeurs binaires.
                    index += 3;
                }
            }
        }

        /// <summary>
        /// Fonction qui affiche dans la console, les différentes données du fichier Bitmap
        /// </summary>
        public void toString()
        {
            // On copie toute les donnes du header et du headerinfo qui ne changent pas
            string StringHeader = null;
            for (int i = 0; i < header.Length; i++)
                StringHeader += header[i] + " ";

            string StringHeaderInfo = null;
            for (int i = 0; i < headerInfo.Length; i++)
                StringHeaderInfo += headerInfo[i] + " ";


            // Puis on copie les valeur binaire de l'image de base
            string StringImage = null;
            for (int i = 0; i < image.Length; i++)
            {
                if (i % (largeur * 3) == 0 && i != 0)
                    StringImage += "\n";

                StringImage += image[i] + "\t";


            }


            Console.WriteLine("HEADER : \n \n" + StringHeader + "\n");
            Console.WriteLine("HEADERINFO : \n \n" + StringHeaderInfo + "\n");
            Console.WriteLine("IMAGE : \n \n" + StringImage);


        }

        #region TEST SUPRESSION COULEURS
        /*
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
        */
        #endregion


        /// <summary>
        /// Fonction qui exporte et convertit les données binaires en fichier bitmap
        /// </summary>
        /// <param name="Path">Chemin de destination</param>
        public void Exporter(string PathDestination)
        {
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
                        if (j < largeur - ajoutMultiple4)
                        {
                            // Les valeurs binaires commance en bas pour le pixel en haut et se lisent de gauche à droite.
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
