
using QRCoder;
using RoadRecordViolationSystem.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public static class QrUtil 
    {
        public static string Generate(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrData);

            var image = qrCode.GetGraphic(60);
            byte[] bitmapArray = image.BitMapToByteArray();

            return $"data:image/png;base64,{Convert.ToBase64String(bitmapArray)}";
        }
    }
}
