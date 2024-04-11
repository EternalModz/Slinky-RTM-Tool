// Decompiled with JetBrains decompiler
// Type: PS3Lib.Extension
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe

using PS3ManagerAPI;
using System;
using System.Linq;
using System.Text;

namespace PS3Lib
{
    public class Extension
    {
        private SelectAPI CurrentAPI;

        public Extension(SelectAPI API)
        {
            this.CurrentAPI = API;
            switch (API)
            {
                case SelectAPI.ControlConsole:
                    if (Extension.Common.CcApi != null)
                        break;
                    Extension.Common.CcApi = new CCAPI();
                    break;
                case SelectAPI.TargetManager:
                    if (Extension.Common.TmApi != null)
                        break;
                    Extension.Common.TmApi = new TMAPI();
                    break;
                case SelectAPI.PS3Manager:
                    if (Extension.Common.Ps3mApi == null)
                        Extension.Common.Ps3mApi = new PS3MAPI();
                    break;
            }
        }

        public sbyte ReadSByte(uint offset)
        {
            byte[] buffer = new byte[1];
            this.GetMem(offset, buffer, this.CurrentAPI);
            return (sbyte)buffer[0];
        }

        public bool ReadBool(uint offset)
        {
            byte[] buffer = new byte[1];
            this.GetMem(offset, buffer, this.CurrentAPI);
            return buffer[0] > (byte)0;
        }

        public short ReadInt16(uint offset)
        {
            byte[] bytes = this.GetBytes(offset, 2U, this.CurrentAPI);
            Array.Reverse((Array)bytes, 0, 2);
            return BitConverter.ToInt16(bytes, 0);
        }

        public int ReadInt32(uint offset)
        {
            byte[] bytes = this.GetBytes(offset, 4U, this.CurrentAPI);
            Array.Reverse((Array)bytes, 0, 4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public long ReadInt64(uint offset)
        {
            byte[] bytes = this.GetBytes(offset, 8U, this.CurrentAPI);
            Array.Reverse((Array)bytes, 0, 8);
            return BitConverter.ToInt64(bytes, 0);
        }

        public byte ReadByte(uint offset)
        {
            return this.GetBytes(offset, 1U, this.CurrentAPI)[0];
        }

        public byte[] ReadBytes(uint offset, int length)
        {
            return this.GetBytes(offset, (uint)length, this.CurrentAPI);
        }

        public ushort ReadUInt16(uint offset)
        {
            byte[] bytes = this.GetBytes(offset, 2U, this.CurrentAPI);
            Array.Reverse((Array)bytes, 0, 2);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public uint ReadUInt32(uint offset)
        {
            byte[] bytes = this.GetBytes(offset, 4U, this.CurrentAPI);
            Array.Reverse((Array)bytes, 0, 4);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public ulong ReadUInt64(uint offset)
        {
            byte[] bytes = this.GetBytes(offset, 8U, this.CurrentAPI);
            Array.Reverse((Array)bytes, 0, 8);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public float ReadFloat(uint offset)
        {
            byte[] bytes = this.GetBytes(offset, 4U, this.CurrentAPI);
            Array.Reverse((Array)bytes, 0, 4);
            return BitConverter.ToSingle(bytes, 0);
        }

        public string ReadString(uint offset)
        {
            int length1 = 40;
            int num = 0;
            string source = "";
            do
            {
                byte[] bytes = this.ReadBytes(offset + (uint)num, length1);
                source += Encoding.UTF8.GetString(bytes);
                num += length1;
            }
            while (!source.Contains<char>(char.MinValue));
            int length2 = source.IndexOf(char.MinValue);
            string str = source.Substring(0, length2);
            string empty = string.Empty;
            return str;
        }

        public void WriteSByte(uint offset, sbyte input)
        {
            byte[] buffer = new byte[1] { (byte)input };
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteBool(uint offset, bool input)
        {
            byte[] buffer = new byte[1]
            {
        input ? (byte) 1 : (byte) 0
            };
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteInt16(uint offset, short input)
        {
            byte[] buffer = new byte[2];
            BitConverter.GetBytes(input).CopyTo((Array)buffer, 0);
            Array.Reverse((Array)buffer, 0, 2);
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteInt32(uint offset, int input)
        {
            byte[] buffer = new byte[4];
            BitConverter.GetBytes(input).CopyTo((Array)buffer, 0);
            Array.Reverse((Array)buffer, 0, 4);
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteInt64(uint offset, long input)
        {
            byte[] buffer = new byte[8];
            BitConverter.GetBytes(input).CopyTo((Array)buffer, 0);
            Array.Reverse((Array)buffer, 0, 8);
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteByte(uint offset, byte input)
        {
            byte[] buffer = new byte[1] { input };
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteBytes(uint offset, byte[] input)
        {
            byte[] buffer = input;
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteString(uint offset, string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            Array.Resize<byte>(ref bytes, bytes.Length + 1);
            this.SetMem(offset, bytes, this.CurrentAPI);
        }

        public void WriteUInt16(uint offset, ushort input)
        {
            byte[] buffer = new byte[2];
            BitConverter.GetBytes(input).CopyTo((Array)buffer, 0);
            Array.Reverse((Array)buffer, 0, 2);
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteUInt32(uint offset, uint input)
        {
            byte[] buffer = new byte[4];
            BitConverter.GetBytes(input).CopyTo((Array)buffer, 0);
            Array.Reverse((Array)buffer, 0, 4);
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteUInt64(uint offset, ulong input)
        {
            byte[] buffer = new byte[8];
            BitConverter.GetBytes(input).CopyTo((Array)buffer, 0);
            Array.Reverse((Array)buffer, 0, 8);
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        public void WriteFloat(uint offset, float input)
        {
            byte[] buffer = new byte[4];
            BitConverter.GetBytes(input).CopyTo((Array)buffer, 0);
            Array.Reverse((Array)buffer, 0, 4);
            this.SetMem(offset, buffer, this.CurrentAPI);
        }

        private void SetMem(uint offset, byte[] buffer, SelectAPI API)
        {
            switch (API)
            {
                case SelectAPI.ControlConsole:
                    Extension.Common.CcApi.SetMemory(offset, buffer);
                    break;
                case SelectAPI.TargetManager:
                    Extension.Common.TmApi.SetMemory(offset, buffer);
                    break;
                case SelectAPI.PS3Manager:
                    Extension.Common.Ps3mApi.SetMemory(offset, buffer);
                    break;
            }
        }




        private void GetMem(uint offset, byte[] buffer, SelectAPI API)
        {
            switch (API)
            {
                case SelectAPI.ControlConsole:
                    Extension.Common.CcApi.GetMemory(offset, buffer);
                    break;
                case SelectAPI.TargetManager:
                    Extension.Common.TmApi.GetMemory(offset, buffer);
                    break;
                case SelectAPI.PS3Manager:
                    Extension.Common.Ps3mApi.GetMemory(offset, buffer);
                    break;
            }
        }

        private byte[] GetBytes(uint offset, uint length, SelectAPI API)
        {
            byte[] numArray = new byte[(int)length];
            switch (API)
            {
                case SelectAPI.ControlConsole:
                    numArray = Extension.Common.CcApi.GetBytes(offset, length);
                    break;
                case SelectAPI.TargetManager:
                    numArray = Extension.Common.TmApi.GetBytes(offset, length);
                    break;
                case SelectAPI.PS3Manager:
                    numArray = Extension.Common.Ps3mApi.GetBytes(offset, length);
                    break;
            }
            return numArray;
        }

        private class Common
        {
            public static CCAPI CcApi;
            public static TMAPI TmApi;
            public static PS3MAPI Ps3mApi;
        }
    }
}
