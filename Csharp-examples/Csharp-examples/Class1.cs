using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Csharp_examples
{
    class Class1
    {
        private static Random rnd = new Random();

        public BitmapSource a;

        public byte[] ToStream()
        {
            // 1. 결과물로 출력될 배열을 생성
            byte[] stream = ImageToByte(a);
            
            return stream;
        }

        public void FromStream(byte[] stream)
        {
            int offset = 0;
            a = ToBitmapSource(stream, ref offset);
        }

        #region ToStream Methods
        
        private byte[] ImageToByte(BitmapSource value)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            byte[] bytes = null;

            encoder.Frames.Add(BitmapFrame.Create(value));
            encoder.Save(memStream);
            bytes = memStream.ToArray();

            return bytes;
        }

        #endregion

        #region FromStream Methods

        private BitmapSource ToBitmapSource(byte[] stream, ref int offset)
        {
            var memStream = new MemoryStream(stream);
            memStream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = memStream;
            image.EndInit();
            return image;
        }
        #endregion
    }
}
