// ************************************************* //
//                 PS3MAPI By zFxbixn                //
//                                                   //
// ************************************************* //

using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS3Lib
{
    public class PS3MAPI
    {
        private static PS3ManagerAPI.PS3MAPI PS3M_API = new PS3ManagerAPI.PS3MAPI();

        public PS3MAPI()
        {
            PS3ManagerAPI.PS3MAPI PS3M_API = new PS3ManagerAPI.PS3MAPI();
        }

        public Extension Extension
        {
            get { return new Extension(SelectAPI.PS3Manager); }
        }

        public bool ConnectTarget()
        {
            return PS3M_API.ConnectTarget();
        }

        public bool ConnectTarget(string targetIP)
        {
            return PS3M_API.ConnectTarget(targetIP);
        }

        public bool ConnectTarget(string targetIP, int port)
        {
            return PS3M_API.ConnectTarget(targetIP, port);
        }

        public bool IsConnected()
        {
            return PS3M_API.IsConnected;
        }

        public bool DisconnectTarget()
        {
            try { PS3M_API.DisconnectTarget(); }
            catch { }
            return true;
        }

        public bool AttachProcess()
        {
            return PS3M_API.AttachProcess();
        }

        public bool AttachProcess(uint pid)
        {
            return PS3M_API.AttachProcess(pid);
        }

        public bool GetProcessList(out uint[] pids)
        {
            pids = new uint[16];
            try
            {
                pids = PS3M_API.Process.GetPidProcesses();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool GetProcessName(uint pid, out string name)
        {
            name = "";
            try
            {
                name = PS3M_API.Process.GetName(pid);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public uint GetAttachedProcess()
        {
            return PS3M_API.Process.Process_Pid;
        }

        public bool SetMemory(uint offset, byte[] buffer)
        {
            try
            {
                PS3M_API.Process.Memory.Set(PS3M_API.Process.Process_Pid, (ulong)offset, buffer);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SetMemory(ulong offset, byte[] buffer)
        {
            try
            {
                PS3M_API.Process.Memory.Set(PS3M_API.Process.Process_Pid, offset, buffer);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SetMemory(ulong offset, string hexadecimal, EndianType Type = EndianType.BigEndian)
        {
            byte[] Entry = StringToByteArray(hexadecimal);
            if (Type == EndianType.LittleEndian)
                Array.Reverse(Entry);
            try
            {
                PS3M_API.Process.Memory.Set(PS3M_API.Process.Process_Pid, offset, Entry);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool GetMemory(uint offset, byte[] buffer)
        {
            try
            {
                PS3M_API.Process.Memory.Get(PS3M_API.Process.Process_Pid, (ulong)offset, buffer);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool GetMemory(ulong offset, byte[] buffer)
        {
            try
            {
                PS3M_API.Process.Memory.Get(PS3M_API.Process.Process_Pid, offset, buffer);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public byte[] GetBytes(uint offset, uint length)
        {
            byte[] buffer = new byte[length];
            PS3M_API.Process.Memory.Get(PS3M_API.Process.Process_Pid, (ulong)offset, buffer);
            return buffer;
        }

        public byte[] GetBytes(ulong offset, uint length)
        {
            byte[] buffer = new byte[length];
            PS3M_API.Process.Memory.Get(PS3M_API.Process.Process_Pid, offset, buffer);
            return buffer;
        }

        public bool Notify(string message)
        {
            try
            {
                PS3M_API.PS3.Notify(message);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags flag)
        {
            try
            {
                PS3M_API.PS3.Power(flag);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool RingBuzzer(PS3ManagerAPI.PS3MAPI.PS3_CMD.BuzzerMode mode)
        {
            try
            {
                PS3M_API.PS3.RingBuzzer(mode);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor color, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode mode)
        {
            try
            {
                PS3M_API.PS3.Led(color, mode);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public string GetFirmwareVersion()
        {
            return PS3M_API.PS3.GetFirmwareVersion_Str();
        }

        public string GetFirmwareType()
        {
            return PS3M_API.PS3.GetFirmwareType();
        }

        public string GetTemperatureCELL()
        {
            try
            {
                uint cpu;
                uint rsx;
                PS3M_API.PS3.GetTemperature(out cpu, out rsx);
                return cpu.ToString() + "°C / " + (((9.0 / 5.0) * cpu) + 32).ToString() + "°F";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ERROR";
            }
        }

        public string GetTemperatureRSX()
        {
            try
            {
                uint cpu;
                uint rsx;
                PS3M_API.PS3.GetTemperature(out cpu, out rsx);
                return rsx.ToString() + "°C / " + (((9.0 / 5.0) * rsx) + 32).ToString() + "°F";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ERROR";
            }

        }

        public bool GetTemperatureCelcius(out uint cpu, out uint rsx)
        {
            cpu = 0; rsx = 0;
            try
            {
                PS3M_API.PS3.GetTemperature(out cpu, out rsx);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public bool GetTemperatureFahrenheit(out uint cpu, out uint rsx)
        {
            cpu = 0; rsx = 0;
            try
            {
                PS3M_API.PS3.GetTemperature(out cpu, out rsx);
                cpu = Convert.ToUInt32(((9.0 / 5.0) * cpu) + 32);
                rsx = Convert.ToUInt32(((9.0 / 5.0) * rsx) + 32);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public string GetLibVersion()
        {
            return PS3M_API.GetLibVersion_Str();
        }

        public string GetServerVersion()
        {
            return PS3M_API.Server.GetVersion_Str();
        }

        public string GetCoreVersion()
        {
            return PS3M_API.Core.GetVersion_Str();
        }

        public bool Syscall_IsEnabled(int num)
        {
            try
            {
                PS3M_API.PS3.CheckSyscall(num);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Syscall_Disable(int num)
        {
            try
            {
                PS3M_API.PS3.DisableSyscall(num);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public PS3ManagerAPI.PS3MAPI.PS3_CMD.Syscall8Mode Syscall8_CurrentMode()
        {
            try
            {
                PS3M_API.PS3.PartialCheckSyscall8();
                return PS3M_API.PS3.PartialCheckSyscall8();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return PS3ManagerAPI.PS3MAPI.PS3_CMD.Syscall8Mode.Disabled;
            }
        }

        public bool Syscall8_SetMode(PS3ManagerAPI.PS3MAPI.PS3_CMD.Syscall8Mode mode)
        {
            try
            {
                PS3M_API.PS3.PartialDisableSyscall8(mode);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SetConsoleID(string consoleID)
        {
            if (consoleID.Length < 32)
            {
                MessageBox.Show("Invalid ConsoleID", "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                string newCID = "";
                if (consoleID.Length > 32) newCID = consoleID.Substring(0, 32);
                PS3M_API.PS3.SetIDPS(newCID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
        }

        public bool SetConsoleID(byte[] consoleID)
        {
            string newCID = ByteArrayToString(consoleID);
            if (newCID.Length < 32)
            {
                MessageBox.Show("Invalid ConsoleID", "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                if (newCID.Length > 32) newCID = newCID.Substring(0, 32);
                PS3M_API.PS3.SetIDPS(newCID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
        }

        public bool SetPSID(string PSID)
        {
            if (PSID.Length < 32)
            {
                MessageBox.Show("Invalid ConsoleID", "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                string newPSID = "";
                if (PSID.Length > 32) newPSID = PSID.Substring(0, 32);
                PS3M_API.PS3.SetPSID(newPSID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
        }

        public bool SetPSID(byte[] PSID)
        {
            string newPSID = ByteArrayToString(PSID);
            if (newPSID.Length < 32)
            {
                MessageBox.Show("Invalid ConsoleID", "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                if (newPSID.Length > 32) newPSID = newPSID.Substring(0, 32);
                PS3M_API.PS3.SetPSID(newPSID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error PS3M_API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
        }

        internal static string ByteArrayToString(byte[] bytes)
        {
            try
            {
                StringBuilder hex = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                    hex.AppendFormat("{0:x2}", b);
                return hex.ToString();
            }
            catch { throw new ArgumentException("Value not possible.", "HEX String"); }
        }

        internal static byte[] StringToByteArray(string hex)
        {
            string replace = hex.Replace("0x", "");
            string Stringz = replace.Insert(replace.Length - 1, "0");

            int Odd = replace.Length;
            bool Nombre;
            if (Odd % 2 == 0)
                Nombre = true;
            else
                Nombre = false;
            try
            {
                if (Nombre == true)
                {
                    return Enumerable.Range(0, replace.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(replace.Substring(x, 2), 16))
                 .ToArray();
                }
                else
                {
                    return Enumerable.Range(0, replace.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(Stringz.Substring(x, 2), 16))
                 .ToArray();
                }
            }
            catch { throw new ArgumentException("Value not possible.", "Byte Array"); }
        }

    }
}
