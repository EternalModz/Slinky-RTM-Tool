// Decompiled with JetBrains decompiler
// Type: PS3Lib.TMAPI
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe

using PS3Lib.NET;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PS3Lib
{
    public class TMAPI
    {
        public static int Target = (int)byte.MaxValue;
        public static bool AssemblyLoaded = true;
        public static PS3TMAPI.ResetParameter resetParameter;
        internal static Assembly LoadApi;

        public Extension Extension
        {
            get
            {
                return new Extension(SelectAPI.TargetManager);
            }
        }

        public TMAPI.SCECMD SCE
        {
            get
            {
                return new TMAPI.SCECMD();
            }
        }

        public void InitComms()
        {
            int num = (int)PS3TMAPI.InitTargetComms();
        }

        public bool ConnectTarget(int TargetIndex = 0)
        {
            bool flag = false;
            if (TMAPI.AssemblyLoaded)
                this.PS3TMAPI_NET();
            TMAPI.AssemblyLoaded = false;
            TMAPI.Target = TargetIndex;
            flag = PS3TMAPI.SUCCEEDED(PS3TMAPI.InitTargetComms());
            return PS3TMAPI.SUCCEEDED(PS3TMAPI.Connect(TargetIndex, (string)null));
        }

        public bool ConnectTarget(string TargetName)
        {
            bool flag1 = false;
            if (TMAPI.AssemblyLoaded)
                this.PS3TMAPI_NET();
            TMAPI.AssemblyLoaded = false;
            bool flag2 = PS3TMAPI.SUCCEEDED(PS3TMAPI.InitTargetComms());
            if (flag2)
            {
                flag1 = PS3TMAPI.SUCCEEDED(PS3TMAPI.GetTargetFromName(TargetName, out TMAPI.Target));
                flag2 = PS3TMAPI.SUCCEEDED(PS3TMAPI.Connect(TMAPI.Target, (string)null));
            }
            return flag2;
        }

        public void DisconnectTarget()
        {
            int num = (int)PS3TMAPI.Disconnect(TMAPI.Target);
        }

        public void PowerOn(int numTarget = 0)
        {
            if (TMAPI.Target != (int)byte.MaxValue)
                numTarget = TMAPI.Target;
            int num = (int)PS3TMAPI.PowerOn(numTarget);
        }

        public void PowerOff(bool Force)
        {
            int num = (int)PS3TMAPI.PowerOff(TMAPI.Target, Force);
        }

        public bool AttachProcess()
        {
            int processList = (int)PS3TMAPI.GetProcessList(TMAPI.Target, out TMAPI.Parameters.processIDs);
            bool flag = (uint)TMAPI.Parameters.processIDs.Length > 0U;
            if (flag)
            {
                TMAPI.Parameters.ProcessID = Convert.ToUInt32((ulong)TMAPI.Parameters.processIDs[0]);
                int num1 = (int)PS3TMAPI.ProcessAttach(TMAPI.Target, PS3TMAPI.UnitType.PPU, TMAPI.Parameters.ProcessID);
                int num2 = (int)PS3TMAPI.ProcessContinue(TMAPI.Target, TMAPI.Parameters.ProcessID);
                TMAPI.Parameters.info = "The Process 0x" + TMAPI.Parameters.ProcessID.ToString("X8") + " Has Been Attached !";
            }
            return flag;
        }

        public void SetMemory(uint Address, byte[] Bytes)
        {
            int num = (int)PS3TMAPI.ProcessSetMemory(TMAPI.Target, PS3TMAPI.UnitType.PPU, TMAPI.Parameters.ProcessID, 0UL, (ulong)Address, Bytes);
        }

        public void SetMemory(uint Address, ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse((Array)bytes);
            int num = (int)PS3TMAPI.ProcessSetMemory(TMAPI.Target, PS3TMAPI.UnitType.PPU, TMAPI.Parameters.ProcessID, 0UL, (ulong)Address, bytes);
        }

        public void SetMemory(uint Address, string hexadecimal, EndianType Type = EndianType.BigEndian)
        {
            byte[] byteArray = TMAPI.StringToByteArray(hexadecimal);
            if (Type == EndianType.LittleEndian)
                Array.Reverse((Array)byteArray);
            int num = (int)PS3TMAPI.ProcessSetMemory(TMAPI.Target, PS3TMAPI.UnitType.PPU, TMAPI.Parameters.ProcessID, 0UL, (ulong)Address, byteArray);
        }

        public void GetMemory(uint Address, byte[] Bytes)
        {
            int memory = (int)PS3TMAPI.ProcessGetMemory(TMAPI.Target, PS3TMAPI.UnitType.PPU, TMAPI.Parameters.ProcessID, 0UL, (ulong)Address, ref Bytes);
        }

        public byte[] GetBytes(uint Address, uint lengthByte)
        {
            byte[] buffer = new byte[(int)lengthByte];
            int memory = (int)PS3TMAPI.ProcessGetMemory(TMAPI.Target, PS3TMAPI.UnitType.PPU, TMAPI.Parameters.ProcessID, 0UL, (ulong)Address, ref buffer);
            return buffer;
        }

        public string GetString(uint Address, uint lengthString)
        {
            byte[] buffer = new byte[(int)lengthString];
            int memory = (int)PS3TMAPI.ProcessGetMemory(TMAPI.Target, PS3TMAPI.UnitType.PPU, TMAPI.Parameters.ProcessID, 0UL, (ulong)Address, ref buffer);
            return TMAPI.Hex2Ascii(TMAPI.ReplaceString(buffer));
        }

        internal static string Hex2Ascii(string iMCSxString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int startIndex = 0; startIndex <= iMCSxString.Length - 2; startIndex += 2)
                stringBuilder.Append(Convert.ToString(Convert.ToChar(int.Parse(iMCSxString.Substring(startIndex, 2), NumberStyles.HexNumber))));
            return stringBuilder.ToString();
        }

        internal static byte[] StringToByteArray(string hex)
        {
            string replace = hex.Replace("0x", "");
            string Stringz = replace.Insert(replace.Length - 1, "0");
            bool flag = replace.Length % 2 == 0;
            try
            {
                if (flag)
                    return Enumerable.Range(0, replace.Length).Where<int>((Func<int, bool>)(x => x % 2 == 0)).Select<int, byte>((Func<int, byte>)(x => Convert.ToByte(replace.Substring(x, 2), 16))).ToArray<byte>();
                return Enumerable.Range(0, replace.Length).Where<int>((Func<int, bool>)(x => x % 2 == 0)).Select<int, byte>((Func<int, byte>)(x => Convert.ToByte(Stringz.Substring(x, 2), 16))).ToArray<byte>();
            }
            catch
            {
                throw new ArgumentException("Value not possible.", "Byte Array");
            }
        }

        internal static string ReplaceString(byte[] bytes)
        {
            string str = BitConverter.ToString(bytes).Replace("00", string.Empty).Replace("-", string.Empty);
            for (int index = 0; index < 10; ++index)
                str = str.Replace("^" + index.ToString(), string.Empty);
            return str;
        }

        public void ResetToXMB(TMAPI.ResetTarget flag)
        {
            switch (flag)
            {
                case TMAPI.ResetTarget.Hard:
                    TMAPI.resetParameter = PS3TMAPI.ResetParameter.Hard;
                    break;
                case TMAPI.ResetTarget.Quick:
                    TMAPI.resetParameter = PS3TMAPI.ResetParameter.Quick;
                    break;
                case TMAPI.ResetTarget.ResetEx:
                    TMAPI.resetParameter = PS3TMAPI.ResetParameter.ResetEx;
                    break;
                case TMAPI.ResetTarget.Soft:
                    TMAPI.resetParameter = PS3TMAPI.ResetParameter.Soft;
                    break;
            }
            int num = (int)PS3TMAPI.Reset(TMAPI.Target, TMAPI.resetParameter);
        }

        public Assembly PS3TMAPI_NET()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (ResolveEventHandler)((s, e) =>
            {
                string name = new AssemblyName(e.Name).Name;
                string path1 = string.Format("C:\\Program Files\\SN Systems\\PS3\\bin\\ps3tmapi_net.dll", (object)name);
                string path2 = string.Format("C:\\Program Files (x64)\\SN Systems\\PS3\\bin\\ps3tmapi_net.dll", (object)name);
                string path3 = string.Format("C:\\Program Files (x86)\\SN Systems\\PS3\\bin\\ps3tmapi_net.dll", (object)name);
                if (File.Exists(path1))
                    TMAPI.LoadApi = Assembly.LoadFile(path1);
                else if (File.Exists(path2))
                    TMAPI.LoadApi = Assembly.LoadFile(path2);
                else if (File.Exists(path3))
                {
                    TMAPI.LoadApi = Assembly.LoadFile(path3);
                }
                else
                {
                    int num = (int)MessageBox.Show("Target Manager API cannot be founded to:\r\n\r\n" + path3, "Error with PS3 API!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                return TMAPI.LoadApi;
            });
            return TMAPI.LoadApi;
        }

        public class SCECMD
        {
            public string SNRESULT()
            {
                return TMAPI.Parameters.snresult;
            }

            public string GetTargetName()
            {
                if (TMAPI.Parameters.ConsoleName == null || TMAPI.Parameters.ConsoleName == string.Empty)
                {
                    int num = (int)PS3TMAPI.InitTargetComms();
                    PS3TMAPI.TargetInfo targetInfo1 = new PS3TMAPI.TargetInfo();
                    targetInfo1.Flags = PS3TMAPI.TargetInfoFlag.TargetID;
                    targetInfo1.Target = TMAPI.Target;
                    int targetInfo2 = (int)PS3TMAPI.GetTargetInfo(ref targetInfo1);
                    TMAPI.Parameters.ConsoleName = targetInfo1.Name;
                }
                return TMAPI.Parameters.ConsoleName;
            }

            public string GetStatus()
            {
                if (TMAPI.AssemblyLoaded)
                    return "NotConnected";
                TMAPI.Parameters.connectStatus = PS3TMAPI.ConnectStatus.Connected;
                int connectStatus = (int)PS3TMAPI.GetConnectStatus(TMAPI.Target, out TMAPI.Parameters.connectStatus, out TMAPI.Parameters.usage);
                TMAPI.Parameters.Status = TMAPI.Parameters.connectStatus.ToString();
                return TMAPI.Parameters.Status;
            }

            public uint ProcessID()
            {
                return TMAPI.Parameters.ProcessID;
            }

            public uint[] ProcessIDs()
            {
                return TMAPI.Parameters.processIDs;
            }

            public PS3TMAPI.ConnectStatus DetailStatus()
            {
                return TMAPI.Parameters.connectStatus;
            }
        }

        public class Parameters
        {
            public static string usage;
            public static string info;
            public static string snresult;
            public static string Status;
            public static string MemStatus;
            public static string ConsoleName;
            public static uint ProcessID;
            public static uint[] processIDs;
            public static byte[] Retour;
            public static PS3TMAPI.ConnectStatus connectStatus;
        }

        public enum ResetTarget
        {
            Hard,
            Quick,
            ResetEx,
            Soft,
        }
    }
}
