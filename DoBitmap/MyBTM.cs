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

        public MyBTM(string Path)
        {
            DataBitmap = File.ReadAllBytes(Path);

            for (int i = 0; i < 14; i++)
                header[i] = DataBitmap[i];

            for (int i = 0; i < 50; i++)
            {
                headerInfo[i] = DataBitmap[i + 14];
            }

            int taille = DataBitmap.Length-64;

            image = new byte[taille];
            for (int i = 0; i < taille ; i++)
                image[i] = DataBitmap[i + 64];


        }

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
    }
}
