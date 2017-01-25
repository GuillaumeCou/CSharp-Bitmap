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
        byte type;
        byte 

        public MyBTM(string Path)
        {
            DataBitmap = File.ReadAllBytes(Path);
        }
    }
}
