using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertBinary
{
    class Class1
    {
        private static Random rnd = new Random();

        public int a;
        public string b;
        public List<string> c = new List<string>();
        public double d;
        public bool e;
        public byte[] f = new byte[rnd.Next(90) + 10];

        public byte[] ToStream()
        {
            // 1. 현재 객체의 총 크기 계산
            int size = 0;
            size += sizeof(int);
            // 스트링의 길이 계산
            /*
             * 1. 스트링에 abcde 가 있다.
             * 2. 먼저 문자의 갯수를 파악하고,
             * 3. 뒤에 문자의 길이를 저장한다.
             */
            size += (sizeof(int) + b.Length);
            size += sizeof(int);
            foreach (var con in c)
            {
                size += (sizeof(int) + con.Length);
            }
            size += sizeof(double);
            size += sizeof(bool);
            size += (sizeof(int) + f.Length);

            // 2. 결과물로 출력될 배열을 생성
            int offset = 0;
            byte[] stream = new byte[size];

            Concat(ref stream, ref offset, a);
            Concat(ref stream, ref offset, b);
            Concat(ref stream, ref offset, c);
            Concat(ref stream, ref offset, d);
            Concat(ref stream, ref offset, e);
            Concat(ref stream, ref offset, f);

            return stream;
        }

        public void FromStream(byte[] stream)
        {
            int offset = 0;
            a = ToInt32(stream, ref offset);
            b = ToString(stream, ref offset);
            c = ToList(stream, ref offset);
            d = ToDouble(stream, ref offset);
            e = ToBoolean(stream, ref offset);
            f = ToByteArray(stream, ref offset);
        }

        #region ToStream Methods

        /// <summary>
        /// int 타입의 값을 Stream으로 변환하여 합친다.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        private void Concat(ref byte[] destination, ref int offset, int value)
        {
            byte[] source = BitConverter.GetBytes(value);
            System.Array.Copy(source, 0, destination, offset, source.Length);
            offset += source.Length;
        }

        private void Concat(ref byte[] destination, ref int offset, double value)
        {
            byte[] source = BitConverter.GetBytes(value);
            System.Array.Copy(source, 0, destination, offset, source.Length);
            offset += source.Length;
        }

        private void Concat(ref byte[] destination, ref int offset, bool value)
        {
            byte[] source = BitConverter.GetBytes(value);
            System.Array.Copy(source, 0, destination, offset, source.Length);
            offset += source.Length;
        }

        private void Concat(ref byte[] destination, ref int offset, string value)
        {
            Concat(ref destination, ref offset, value.Length);

            byte[] source = System.Text.Encoding.Default.GetBytes(value);
            System.Array.Copy(source, 0, destination, offset, source.Length);
            offset += source.Length;
        }

        private void Concat(ref byte[] destination, ref int offset, List<string> value)
        {
            Concat(ref destination, ref offset, value.Count);

            foreach (var val in value)
            {
                Concat(ref destination, ref offset, val);
            }
        }

        private void Concat(ref byte[] destination, ref int offset, byte[] value)
        {
            Concat(ref destination, ref offset, value.Length);

            System.Array.Copy(value, 0, destination, offset, value.Length);
            offset += value.Length;
        }


        #endregion

        #region FromStream Methods

        /// <summary>
        /// 입력된 배열의 지정된 위치에서 int 크기만큼 값을 복원한다.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private int ToInt32(byte[] stream, ref int offset)
        {
            int value = BitConverter.ToInt32(stream, offset);
            offset += sizeof(int);
            return value;
        }

        private double ToDouble(byte[] stream, ref int offset)
        {
            double value = BitConverter.ToDouble(stream, offset);
            offset += sizeof(double);
            return value;
        }

        private bool ToBoolean(byte[] stream, ref int offset)
        {
            bool value = BitConverter.ToBoolean(stream, offset);
            offset += sizeof(bool);
            return value;
        }

        private string ToString(byte[] stream, ref int offset)
        {
            int len = ToInt32(stream, ref offset);
            string value = System.Text.Encoding.Default.GetString(stream, offset, len);
            offset += len;
            return value;
        }

        private List<string> ToList(byte[] stream, ref int offset)
        {
            int count = ToInt32(stream, ref offset);
            List<string> list = new List<string>();

            for (int i = 0; i < count; i++)
            {
                list.Add(ToString(stream, ref offset));
            }

            return list;
        }

        private byte[] ToByteArray(byte[] stream, ref int offset)
        {
            int count = ToInt32(stream, ref offset);
            byte[] list = new byte[count];

            for (int i = 0; i < count; i++)
            {
                list[i] = stream[offset + i];
            }

            return list;
        }

        #endregion
    }
}
