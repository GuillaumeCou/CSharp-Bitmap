﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoBitmap
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\couzi\Documents\Cours\Projet Info\Test001.bmp";

            MyBTM test = new MyBTM(path);

            Console.ReadKey();
            
        }
    }
}
