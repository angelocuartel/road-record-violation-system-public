using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Helpers
{
    public static class BitmapHelper
    {
        public static byte[] BitMapToByteArray(this Bitmap qr)
        {
            using (var memStream = new MemoryStream())
            {
                qr.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
                return memStream.ToArray();
            }
        }
    }
}
