using System;
using System.Text;

namespace PS3Lib
{
    // Token: 0x0200010D RID: 269
    public class ArrayBuilder
    {
        private byte[] buffer;
        private int size;

        public ArrayBuilder(byte[] BytesArray)
        {
            this.buffer = BytesArray;
            this.size = this.buffer.Length;
        }

        public ArrayBuilder.ArrayReader Read
        {
            get
            {
                return new ArrayBuilder.ArrayReader(this.buffer);
            }
        }

        public ArrayBuilder.ArrayWriter Write
        {
            get
            {
                return new ArrayBuilder.ArrayWriter(this.buffer);
            }
        }

        public class ArrayReader
        {
            private byte[] buffer;
            private int size;

            public ArrayReader(byte[] BytesArray)
            {
                this.buffer = BytesArray;
                this.size = this.buffer.Length;
            }

            private sbyte GetSByte(int pos)
            {
                return (sbyte)this.buffer[pos];
            }

            public byte GetByte(int pos)
            {
                return this.buffer[pos];
            }

            public char GetChar(int pos)
            {
                return this.buffer[pos].ToString()[0];
            }

            public bool GetBool(int pos)
            {
                return this.buffer[pos] > (byte)0;
            }

            public short GetInt16(int pos, EndianType Type = EndianType.BigEndian)
            {
                byte[] numArray = new byte[2];
                for (int index = 0; index < 2; ++index)
                    numArray[index] = this.buffer[pos + index];
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)numArray, 0, 2);
                return BitConverter.ToInt16(numArray, 0);
            }

            public int GetInt32(int pos, EndianType Type = EndianType.BigEndian)
            {
                byte[] numArray = new byte[4];
                for (int index = 0; index < 4; ++index)
                    numArray[index] = this.buffer[pos + index];
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)numArray, 0, 4);
                return BitConverter.ToInt32(numArray, 0);
            }

            public long GetInt64(int pos, EndianType Type = EndianType.BigEndian)
            {
                byte[] numArray = new byte[8];
                for (int index = 0; index < 8; ++index)
                    numArray[index] = this.buffer[pos + index];
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)numArray, 0, 8);
                return BitConverter.ToInt64(numArray, 0);
            }

            public ushort GetUInt16(int pos, EndianType Type = EndianType.BigEndian)
            {
                byte[] numArray = new byte[2];
                for (int index = 0; index < 2; ++index)
                    numArray[index] = this.buffer[pos + index];
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)numArray, 0, 2);
                return BitConverter.ToUInt16(numArray, 0);
            }

            public uint GetUInt32(int pos, EndianType Type = EndianType.BigEndian)
            {
                byte[] numArray = new byte[4];
                for (int index = 0; index < 4; ++index)
                    numArray[index] = this.buffer[pos + index];
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)numArray, 0, 4);
                return BitConverter.ToUInt32(numArray, 0);
            }

            public ulong GetUInt64(int pos, EndianType Type = EndianType.BigEndian)
            {
                byte[] numArray = new byte[8];
                for (int index = 0; index < 8; ++index)
                    numArray[index] = this.buffer[pos + index];
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)numArray, 0, 8);
                return BitConverter.ToUInt64(numArray, 0);
            }

            public byte[] GetBytes(int pos, int length)
            {
                byte[] numArray = new byte[length];
                for (int index = 0; index < length; ++index)
                    numArray[index] = this.buffer[pos + index];
                return numArray;
            }

            public string GetString(int pos)
            {
                int length = 0;
                while (true)
                {
                    if (this.buffer[pos + length] > (byte)0)
                        ++length;
                    else
                        break;
                }
                byte[] bytes = new byte[length];
                for (int index = 0; index < length; ++index)
                    bytes[index] = this.buffer[pos + index];
                return Encoding.UTF8.GetString(bytes);
            }

            public float GetFloat(int pos)
            {
                byte[] numArray = new byte[4];
                for (int index = 0; index < 4; ++index)
                    numArray[index] = this.buffer[pos + index];
                Array.Reverse((Array)numArray, 0, 4);
                return BitConverter.ToSingle(numArray, 0);
            }
        }

        public class ArrayWriter
        {
            private byte[] buffer;
            private int size;

            public ArrayWriter(byte[] BytesArray)
            {
                this.buffer = BytesArray;
                this.size = this.buffer.Length;
            }

            public void SetSByte(int pos, sbyte value)
            {
                this.buffer[pos] = (byte)value;
            }

            public void SetByte(int pos, byte value)
            {
                this.buffer[pos] = value;
            }

            public void SetChar(int pos, char value)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value.ToString());
                this.buffer[pos] = bytes[0];
            }

            public void SetBool(int pos, bool value)
            {
                byte[] numArray = new byte[1]
                {
          value ? (byte) 1 : (byte) 0
                };
                this.buffer[pos] = numArray[0];
            }

            public void SetInt16(int pos, short value, EndianType Type = EndianType.BigEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)bytes, 0, 2);
                for (int index = 0; index < 2; ++index)
                    this.buffer[index + pos] = bytes[index];
            }

            public void SetInt32(int pos, int value, EndianType Type = EndianType.BigEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)bytes, 0, 4);
                for (int index = 0; index < 4; ++index)
                    this.buffer[index + pos] = bytes[index];
            }

            public void SetInt64(int pos, long value, EndianType Type = EndianType.BigEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)bytes, 0, 8);
                for (int index = 0; index < 8; ++index)
                    this.buffer[index + pos] = bytes[index];
            }

            public void SetUInt16(int pos, ushort value, EndianType Type = EndianType.BigEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)bytes, 0, 2);
                for (int index = 0; index < 2; ++index)
                    this.buffer[index + pos] = bytes[index];
            }

            public void SetUInt32(int pos, uint value, EndianType Type = EndianType.BigEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)bytes, 0, 4);
                for (int index = 0; index < 4; ++index)
                    this.buffer[index + pos] = bytes[index];
            }

            public void SetUInt64(int pos, ulong value, EndianType Type = EndianType.BigEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                if (Type == EndianType.BigEndian)
                    Array.Reverse((Array)bytes, 0, 8);
                for (int index = 0; index < 8; ++index)
                    this.buffer[index + pos] = bytes[index];
            }

            public void SetBytes(int pos, byte[] value)
            {
                int length = value.Length;
                for (int index = 0; index < length; ++index)
                    this.buffer[index + pos] = value[index];
            }

            public void SetString(int pos, string value)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value + "\0");
                for (int index = 0; index < bytes.Length; ++index)
                    this.buffer[index + pos] = bytes[index];
            }

            public void SetFloat(int pos, float value)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                Array.Reverse((Array)bytes, 0, 4);
                for (int index = 0; index < 4; ++index)
                    this.buffer[index + pos] = bytes[index];
            }
        }
    }
}
