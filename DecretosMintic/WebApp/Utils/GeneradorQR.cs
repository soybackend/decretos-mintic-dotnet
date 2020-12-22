using IronBarCode;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace WebApp.Utils
{

    public class PathQR {
        public string FullPath { get; set; }
        public string FileName { get; set; }
    }


    public class GeneradorQR
    {
        private readonly Random _random = new Random();


        public PathQR Generar(string texto) {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            var filename = RandomString(4, true) + ".png";

            var filePath = "Assets/" + filename;

            var fullPath = Path.GetFullPath(filePath);

            qrCodeImage.Save(filePath);

            return new PathQR {
                FileName = filename,
                FullPath = fullPath
            };

                
        }

        public void BorrarArchivo(string path) {
            if (File.Exists(@path))
            {
                File.Delete(@path);
            }
        }

        private string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
