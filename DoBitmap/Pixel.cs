using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBitmap
{
    class Pixel
    {
        int bleu;
        int vert;
        int rouge;

        public int Bleu
        {
            get
            {
                return bleu;
            }

            set
            {
                bleu = value;
            }
        }

        public int Vert
        {
            get
            {
                return vert;
            }

            set
            {
                vert = value;
            }
        }

        public int Rouge
        {
            get
            {
                return rouge;
            }

            set
            {
                rouge = value;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe "Pixel".
        /// </summary>
        /// <param name="Octets">Tableau de taille 3 contenant des octets représentant les couleurs BVR.</param>
        public Pixel(byte[] Octets)
        {
            if(Octets != null && Octets.Length == 3)
            {
                bleu = Octets[0];
                vert = Octets[1];
                rouge = Octets[2];
            }
        }


        /// <summary>
        /// Fonction qui exporte les données d'un pixel.
        /// </summary>
        /// <param name="a">Index pour sortir la couleur (0 = bleu, 1 = vert, 2 = rouge)</param>
        /// <returns></returns>
        public byte Exporter(int a)
        {
            switch (a)
            {
                default:
                    return 0;
                case 0:
                    return (byte)bleu;
                case 1:
                    return (byte)vert;
                case 2:
                    return (byte)rouge;
            }
        }

        public void Attenuer(int Attenuation, char RVG)
        {
            switch (RVG)
            {
                case 'R':
                    AttenuerRouge(Attenuation);
                    break;

                case 'V':
                    AttenuerVert(Attenuation);
                    break;
                case 'B':
                    AttenuerBleu(Attenuation);
                    break;
            }
        }

        /// <summary>
        /// Fonction qui atténue la couleur.
        /// </summary>
        /// <param name="attenuation">Nombre compris entre 0 et 255</param>
        void AttenuerBleu(int attenuation)
        {
            if (attenuation >= 100)
                bleu = 0;
            else
                bleu = (bleu * (100 - attenuation)) / 100; ;
        }

        /// <summary>
        /// Fonction qui atténue la couleur.
        /// </summary>
        /// <param name="attenuation">Nombre compris entre 0 et 255</param>
        void AttenuerVert(int attenuation)
        {
            if (attenuation >= 100)
                vert = 0;
            else
                vert = (vert * (100 - attenuation)) / 100; ;
        }

        /// <summary>
        /// Fonction qui atténue la couleur.
        /// </summary>
        /// <param name="attenuation">Nombre compris entre 0 et 255</param>
        void AttenuerRouge(int attenuation)
        {
            if (attenuation >= 100)
                rouge = 0;
            else
                rouge = (rouge * (100 - attenuation))/100;
        }
    }
}
