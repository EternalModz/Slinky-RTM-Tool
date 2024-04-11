// Decompiled with JetBrains decompiler
// Type: PS3ManagerAPI.PS3MAPI
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace PS3ManagerAPI
{
  public class PS3MAPI
  {
    public int PS3M_API_PC_LIB_VERSION = 288;
    public PS3MAPI.CORE_CMD Core = new PS3MAPI.CORE_CMD();
    public PS3MAPI.SERVER_CMD Server = new PS3MAPI.SERVER_CMD();
    public PS3MAPI.PS3_CMD PS3 = new PS3MAPI.PS3_CMD();
    public PS3MAPI.PROCESS_CMD Process = new PS3MAPI.PROCESS_CMD();
    public PS3MAPI.VSH_PLUGINS_CMD VSH_Plugin = new PS3MAPI.VSH_PLUGINS_CMD();
    private LogDialog LogDialog;

    public PS3MAPI()
    {
      this.Core = new PS3MAPI.CORE_CMD();
      this.Server = new PS3MAPI.SERVER_CMD();
      this.PS3 = new PS3MAPI.PS3_CMD();
      this.Process = new PS3MAPI.PROCESS_CMD();
    }

    public string GetLibVersion_Str()
    {
      string str = this.PS3M_API_PC_LIB_VERSION.ToString("X4");
      return str.Substring(1, 1) + "." + (str.Substring(2, 1) + ".") + str.Substring(3, 1);
    }

    public bool IsConnected
    {
      get
      {
        return PS3ManagerAPI.PS3MAPI.PS3MAPI_Client_Server.IsConnected;
      }
    }

    public bool IsAttached
    {
      get
      {
        return PS3MAPI.PS3MAPI_Client_Server.IsAttached;
      }
    }

    public bool ConnectTarget(string ip, int port = 7887)
    {
      bool flag;
      try
      {
        PS3MAPI.PS3MAPI_Client_Server.Connect(ip, port);
        flag = true;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message, ex);
      }
      return flag;
    }

    public bool ConnectTarget()
    {
      ConnectDialog connectDialog1 = new ConnectDialog();
      ConnectDialog connectDialog2;
      bool flag1;
      try
      {
        bool flag2 = false;
        if (connectDialog1.ShowDialog() == DialogResult.OK)
          flag2 = this.ConnectTarget(connectDialog1.txtIp.Text, int.Parse(connectDialog1.txtPort.Text));
        if (connectDialog1 != null)
        {
          connectDialog1.Dispose();
          connectDialog2 = (ConnectDialog) null;
        }
        flag1 = flag2;
      }
      catch (Exception ex)
      {
        if (connectDialog1 != null)
        {
          connectDialog1.Dispose();
          connectDialog2 = (ConnectDialog) null;
        }
        throw new Exception(ex.Message, ex);
      }
      return flag1;
    }

    public bool AttachProcess(uint pid)
    {
      bool flag;
      try
      {
        this.Process.Process_Pid = pid;
        flag = true;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message, ex);
      }
      return flag;
    }

    public bool AttachProcess()
    {
      AttachDialog attachDialog = (AttachDialog) null;
      bool flag1;
      try
      {
label_2:
        bool flag2 = false;
        if (attachDialog != null)
        {
          attachDialog.Dispose();
          attachDialog = (AttachDialog) null;
        }
        attachDialog = new AttachDialog(this);
        switch (attachDialog.ShowDialog())
        {
          case DialogResult.OK:
            flag2 = this.AttachProcess(Convert.ToUInt32(attachDialog.comboBox1.Text.Split('_')[0], 16));
            break;
          case DialogResult.Retry:
            goto label_2;
        }
        if (attachDialog != null)
          attachDialog.Dispose();
        flag1 = flag2;
      }
      catch (Exception ex)
      {
        if (attachDialog != null)
          attachDialog.Dispose();
        throw new Exception(ex.Message, ex);
      }
      return flag1;
    }

    public void DisconnectTarget()
    {
      try
      {
        PS3MAPI.PS3MAPI_Client_Server.Disconnect();
      }
      catch
      {
      }
    }

    public void ShowLog()
    {
      if (this.LogDialog == null)
        this.LogDialog = new LogDialog(this);
      this.LogDialog.Show();
    }

    public string Log
    {
      get
      {
        return PS3MAPI.PS3MAPI_Client_Server.Log;
      }
    }

    public class SERVER_CMD
    {
      public int Timeout
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.Timeout;
        }
        set
        {
          PS3MAPI.PS3MAPI_Client_Server.Timeout = value;
        }
      }

      public uint GetVersion()
      {
        uint version;
        try
        {
          version = PS3MAPI.PS3MAPI_Client_Server.Server_Get_Version();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return version;
      }

      public string GetVersion_Str()
      {
        string str = PS3MAPI.PS3MAPI_Client_Server.Server_Get_Version().ToString("X4");
        return str.Substring(1, 1) + "." + (str.Substring(2, 1) + ".") + str.Substring(3, 1);
      }
    }

    public class CORE_CMD
    {
      public uint GetVersion()
      {
        uint version;
        try
        {
          version = PS3MAPI.PS3MAPI_Client_Server.Core_Get_Version();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return version;
      }

      public string GetVersion_Str()
      {
        string str = PS3MAPI.PS3MAPI_Client_Server.Core_Get_Version().ToString("X4");
        return str.Substring(1, 1) + "." + (str.Substring(2, 1) + ".") + str.Substring(3, 1);
      }
    }

    public class PS3_CMD
    {
      public uint GetFirmwareVersion()
      {
        uint fwVersion;
        try
        {
          fwVersion = PS3MAPI.PS3MAPI_Client_Server.PS3_GetFwVersion();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return fwVersion;
      }

      public string GetFirmwareVersion_Str()
      {
        string str = PS3MAPI.PS3MAPI_Client_Server.PS3_GetFwVersion().ToString("X4");
        return str.Substring(1, 1) + "." + str.Substring(2, 1) + str.Substring(3, 1);
      }

      public string GetFirmwareType()
      {
        string firmwareType;
        try
        {
          firmwareType = PS3MAPI.PS3MAPI_Client_Server.PS3_GetFirmwareType();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return firmwareType;
      }

      public void Power(PS3MAPI.PS3_CMD.PowerFlags flag)
      {
        try
        {
          switch (flag)
          {
            case PS3MAPI.PS3_CMD.PowerFlags.ShutDown:
              PS3MAPI.PS3MAPI_Client_Server.PS3_Shutdown();
              break;
            case PS3MAPI.PS3_CMD.PowerFlags.QuickReboot:
              PS3MAPI.PS3MAPI_Client_Server.PS3_Reboot();
              break;
            case PS3MAPI.PS3_CMD.PowerFlags.SoftReboot:
              PS3MAPI.PS3MAPI_Client_Server.PS3_SoftReboot();
              break;
            case PS3MAPI.PS3_CMD.PowerFlags.HardReboot:
              PS3MAPI.PS3MAPI_Client_Server.PS3_HardReboot();
              break;
          }
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void Notify(string msg)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_Notify(msg);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void RingBuzzer(PS3MAPI.PS3_CMD.BuzzerMode mode)
      {
        try
        {
          switch (mode)
          {
            case PS3MAPI.PS3_CMD.BuzzerMode.Single:
              PS3MAPI.PS3MAPI_Client_Server.PS3_Buzzer(1);
              break;
            case PS3MAPI.PS3_CMD.BuzzerMode.Double:
              PS3MAPI.PS3MAPI_Client_Server.PS3_Buzzer(2);
              break;
            case PS3MAPI.PS3_CMD.BuzzerMode.Triple:
              PS3MAPI.PS3MAPI_Client_Server.PS3_Buzzer(3);
              break;
          }
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void Led(PS3MAPI.PS3_CMD.LedColor color, PS3MAPI.PS3_CMD.LedMode mode)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_Led(Convert.ToInt32((object) color), Convert.ToInt32((object) mode));
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void GetTemperature(out uint cpu, out uint rsx)
      {
        cpu = 0U;
        rsx = 0U;
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_GetTemp(out cpu, out rsx);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void DisableSyscall(int num)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_DisableSyscall(num);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public bool CheckSyscall(int num)
      {
        bool flag;
        try
        {
          flag = PS3MAPI.PS3MAPI_Client_Server.PS3_CheckSyscall(num);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return flag;
      }

      public void PartialDisableSyscall8(PS3MAPI.PS3_CMD.Syscall8Mode mode)
      {
        try
        {
          switch (mode)
          {
            case PS3MAPI.PS3_CMD.Syscall8Mode.Enabled:
              PS3MAPI.PS3MAPI_Client_Server.PS3_PartialDisableSyscall8(0);
              break;
            case PS3MAPI.PS3_CMD.Syscall8Mode.Only_CobraMambaAndPS3MAPI_Enabled:
              PS3MAPI.PS3MAPI_Client_Server.PS3_PartialDisableSyscall8(1);
              break;
            case PS3MAPI.PS3_CMD.Syscall8Mode.Only_PS3MAPI_Enabled:
              PS3MAPI.PS3MAPI_Client_Server.PS3_PartialDisableSyscall8(2);
              break;
            case PS3MAPI.PS3_CMD.Syscall8Mode.FakeDisabled:
              PS3MAPI.PS3MAPI_Client_Server.PS3_PartialDisableSyscall8(3);
              break;
          }
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public PS3MAPI.PS3_CMD.Syscall8Mode PartialCheckSyscall8()
      {
        PS3MAPI.PS3_CMD.Syscall8Mode syscall8Mode;
        try
        {
          syscall8Mode = PS3MAPI.PS3MAPI_Client_Server.PS3_PartialCheckSyscall8() != 0 ? (PS3MAPI.PS3MAPI_Client_Server.PS3_PartialCheckSyscall8() != 1 ? (PS3MAPI.PS3MAPI_Client_Server.PS3_PartialCheckSyscall8() != 2 ? PS3MAPI.PS3_CMD.Syscall8Mode.FakeDisabled : PS3MAPI.PS3_CMD.Syscall8Mode.Only_PS3MAPI_Enabled) : PS3MAPI.PS3_CMD.Syscall8Mode.Only_CobraMambaAndPS3MAPI_Enabled) : PS3MAPI.PS3_CMD.Syscall8Mode.Enabled;
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return syscall8Mode;
      }

      public void RemoveHook()
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_RemoveHook();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void ClearHistory(bool include_directory = true)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_ClearHistory(include_directory);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public string GetPSID()
      {
        string psid;
        try
        {
          psid = PS3MAPI.PS3MAPI_Client_Server.PS3_GetPSID();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return psid;
      }

      public void SetPSID(string PSID)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_SetPSID(PSID);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public string GetIDPS()
      {
        string idps;
        try
        {
          idps = PS3MAPI.PS3MAPI_Client_Server.PS3_GetIDPS();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return idps;
      }

      public void SetIDPS(string IDPS)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.PS3_SetIDPS(IDPS);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public enum PowerFlags
      {
        ShutDown,
        QuickReboot,
        SoftReboot,
        HardReboot,
      }

      public enum BuzzerMode
      {
        Single,
        Double,
        Triple,
      }

      public enum LedColor
      {
        Red,
        Green,
        Yellow,
      }

      public enum LedMode
      {
        Off,
        On,
        BlinkFast,
        BlinkSlow,
      }

      public enum Syscall8Mode
      {
        Enabled,
        Only_CobraMambaAndPS3MAPI_Enabled,
        Only_PS3MAPI_Enabled,
        FakeDisabled,
        Disabled,
      }
    }

    public class PROCESS_CMD
    {
      public PS3MAPI.PROCESS_CMD.MEMORY_CMD Memory = new PS3MAPI.PROCESS_CMD.MEMORY_CMD();
      public PS3MAPI.PROCESS_CMD.MODULES_CMD Modules = new PS3MAPI.PROCESS_CMD.MODULES_CMD();

      public PROCESS_CMD()
      {
        this.Memory = new PS3MAPI.PROCESS_CMD.MEMORY_CMD();
        this.Modules = new PS3MAPI.PROCESS_CMD.MODULES_CMD();
      }

      public uint[] Processes_Pid
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.Processes_Pid;
        }
      }

      public uint Process_Pid
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.Process_Pid;
        }
        set
        {
          PS3MAPI.PS3MAPI_Client_Server.Process_Pid = value;
        }
      }

      public uint[] GetPidProcesses()
      {
        uint[] pidList;
        try
        {
          pidList = PS3MAPI.PS3MAPI_Client_Server.Process_GetPidList();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return pidList;
      }

      public string GetName(uint pid)
      {
        string name;
        try
        {
          name = PS3MAPI.PS3MAPI_Client_Server.Process_GetName(pid);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        return name;
      }

      public class MEMORY_CMD
      {
        public void Set(uint Pid, ulong Address, byte[] Bytes)
        {
          try
          {
            PS3MAPI.PS3MAPI_Client_Server.Memory_Set(Pid, Address, Bytes);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
        }

        public void Get(uint Pid, ulong Address, byte[] Bytes)
        {
          try
          {
            PS3MAPI.PS3MAPI_Client_Server.Memory_Get(Pid, Address, Bytes);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
        }

        public byte[] Get(uint Pid, ulong Address, uint Length)
        {
          byte[] numArray;
          try
          {
            byte[] Bytes = new byte[(int) Length];
            PS3MAPI.PS3MAPI_Client_Server.Memory_Get(Pid, Address, Bytes);
            numArray = Bytes;
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
          return numArray;
        }
      }

      public class MODULES_CMD
      {
        public int[] Modules_Prx_Id
        {
          get
          {
            return PS3MAPI.PS3MAPI_Client_Server.Modules_Prx_Id;
          }
        }

        public int[] GetPrxIdModules(uint pid)
        {
          int[] prxIdList;
          try
          {
            prxIdList = PS3MAPI.PS3MAPI_Client_Server.Module_GetPrxIdList(pid);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
          return prxIdList;
        }

        public string GetName(uint pid, int prxid)
        {
          string name;
          try
          {
            name = PS3MAPI.PS3MAPI_Client_Server.Module_GetName(pid, prxid);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
          return name;
        }

        public string GetFilename(uint pid, int prxid)
        {
          string filename;
          try
          {
            filename = PS3MAPI.PS3MAPI_Client_Server.Module_GetFilename(pid, prxid);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
          return filename;
        }

        public void Load(uint pid, string path)
        {
          try
          {
            PS3MAPI.PS3MAPI_Client_Server.Module_Load(pid, path);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
        }

        public void Unload(uint pid, int prxid)
        {
          try
          {
            PS3MAPI.PS3MAPI_Client_Server.Module_Unload(pid, prxid);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message, ex);
          }
        }
      }
    }

    public class VSH_PLUGINS_CMD
    {
      public void Load(uint slot, string path)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.VSHPlugins_Load(slot, path);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void Unload(uint slot)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.VSHPlugins_Unload(slot);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }

      public void GetInfoBySlot(uint slot, out string name, out string path)
      {
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.VSHPlugins_GetInfoBySlot(slot, out name, out path);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
      }
    }

    internal class PS3MAPI_Client_Web
    {
    }

    internal class PS3MAPI_Client_Server
    {
      private static int ps3m_api_server_minversion = 288;
      private static string sMessages = "";
      private static string sServerIP = "";
      private static int iPort = 7887;
      private static string sBucket = "";
      private static int iTimeout = 5000;
      private static uint iPid = 0;
      private static uint[] iprocesses_pid = new uint[16];
      private static int[] imodules_prx_id = new int[64];
      private static string sLog = "";
      private static PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode;
      private static string sResponse;
      internal static Socket main_sock;
      internal static Socket listening_sock;
      internal static Socket data_sock;
      internal static IPEndPoint main_ipEndPoint;
      internal static IPEndPoint data_ipEndPoint;

      public static string Log
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.sLog;
        }
      }

      public static uint[] Processes_Pid
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.iprocesses_pid;
        }
      }

      public static uint Process_Pid
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.iPid;
        }
        set
        {
          PS3MAPI.PS3MAPI_Client_Server.iPid = value;
        }
      }

      public static int[] Modules_Prx_Id
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.imodules_prx_id;
        }
      }

      public static int Timeout
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.iTimeout;
        }
        set
        {
          PS3MAPI.PS3MAPI_Client_Server.iTimeout = value;
        }
      }

      public static bool IsConnected
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.main_sock != null && PS3MAPI.PS3MAPI_Client_Server.main_sock.Connected;
        }
      }

      public static bool IsAttached
      {
        get
        {
          return PS3MAPI.PS3MAPI_Client_Server.iPid > 0U;
        }
      }

      internal static void Connect()
      {
        PS3MAPI.PS3MAPI_Client_Server.Connect(PS3MAPI.PS3MAPI_Client_Server.sServerIP, PS3MAPI.PS3MAPI_Client_Server.iPort);
      }

      internal static void Connect(string sServer, int Port)
      {
        PS3MAPI.PS3MAPI_Client_Server.sServerIP = sServer;
        PS3MAPI.PS3MAPI_Client_Server.iPort = Port;
        if (Port.ToString().Length == 0)
          throw new Exception("Unable to Connect - No Port Specified.");
        if (PS3MAPI.PS3MAPI_Client_Server.sServerIP.Length == 0)
          throw new Exception("Unable to Connect - No Server Specified.");
        if (PS3MAPI.PS3MAPI_Client_Server.main_sock != null && PS3MAPI.PS3MAPI_Client_Server.main_sock.Connected)
          return;
        PS3MAPI.PS3MAPI_Client_Server.main_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        PS3MAPI.PS3MAPI_Client_Server.main_ipEndPoint = new IPEndPoint(Dns.GetHostByName(PS3MAPI.PS3MAPI_Client_Server.sServerIP).AddressList[0], Port);
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.main_sock.Connect((EndPoint) PS3MAPI.PS3MAPI_Client_Server.main_ipEndPoint);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message, ex);
        }
        PS3MAPI.PS3MAPI_Client_Server.ReadResponse();
        if (PS3MAPI.PS3MAPI_Client_Server.eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.PS3MAPIConnected)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        PS3MAPI.PS3MAPI_Client_Server.ReadResponse();
        if (PS3MAPI.PS3MAPI_Client_Server.eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.PS3MAPIConnectedOK)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        if ((ulong) PS3MAPI.PS3MAPI_Client_Server.Server_GetMinVersion() < (ulong) PS3MAPI.PS3MAPI_Client_Server.ps3m_api_server_minversion)
        {
          PS3MAPI.PS3MAPI_Client_Server.Disconnect();
          throw new Exception("PS3M_API SERVER (webMAN-MOD) OUTDATED! PLEASE UPDATE.");
        }
        if ((ulong) PS3MAPI.PS3MAPI_Client_Server.Server_GetMinVersion() > (ulong) PS3MAPI.PS3MAPI_Client_Server.ps3m_api_server_minversion)
        {
          PS3MAPI.PS3MAPI_Client_Server.Disconnect();
          throw new Exception("PS3M_API PC_LIB (PS3ManagerAPI.dll) OUTDATED! PLEASE UPDATE.");
        }
      }

      internal static void Disconnect()
      {
        PS3MAPI.PS3MAPI_Client_Server.CloseDataSocket();
        if (PS3MAPI.PS3MAPI_Client_Server.main_sock != null)
        {
          if (PS3MAPI.PS3MAPI_Client_Server.main_sock.Connected)
          {
            PS3MAPI.PS3MAPI_Client_Server.SendCommand("DISCONNECT");
            PS3MAPI.PS3MAPI_Client_Server.iPid = 0U;
            PS3MAPI.PS3MAPI_Client_Server.main_sock.Close();
          }
          PS3MAPI.PS3MAPI_Client_Server.main_sock = (Socket) null;
        }
        PS3MAPI.PS3MAPI_Client_Server.main_ipEndPoint = (IPEndPoint) null;
      }

      internal static uint Server_Get_Version()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("SERVER GETVERSION");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return Convert.ToUInt32(PS3MAPI.PS3MAPI_Client_Server.sResponse);
      }

      internal static uint Server_GetMinVersion()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("SERVER GETMINVERSION");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return Convert.ToUInt32(PS3MAPI.PS3MAPI_Client_Server.sResponse);
      }

      internal static uint Core_Get_Version()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("CORE GETVERSION");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return Convert.ToUInt32(PS3MAPI.PS3MAPI_Client_Server.sResponse);
      }

      internal static uint Core_GetMinVersion()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("CORE GETMINVERSION");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return Convert.ToUInt32(PS3MAPI.PS3MAPI_Client_Server.sResponse);
      }

      internal static uint PS3_GetFwVersion()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 GETFWVERSION");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return Convert.ToUInt32(PS3MAPI.PS3MAPI_Client_Server.sResponse);
      }

      internal static string PS3_GetFirmwareType()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 GETFWTYPE");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return PS3MAPI.PS3MAPI_Client_Server.sResponse;
      }

      internal static void PS3_Shutdown()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 SHUTDOWN");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Disconnect();
        else
          PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_Reboot()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 REBOOT");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Disconnect();
        else
          PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_SoftReboot()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 SOFTREBOOT");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Disconnect();
        else
          PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_HardReboot()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 HARDREBOOT");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Disconnect();
        else
          PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_Notify(string msg)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 NOTIFY  " + msg);
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_Buzzer(int mode)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 BUZZER" + mode.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_Led(int color, int mode)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 LED " + color.ToString() + " " + mode.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_GetTemp(out uint cpu, out uint rsx)
      {
        cpu = 0U;
        rsx = 0U;
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 GETTEMP");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        string[] strArray = PS3MAPI.PS3MAPI_Client_Server.sResponse.Split('|');
        cpu = Convert.ToUInt32(strArray[0], 10);
        rsx = Convert.ToUInt32(strArray[1], 10);
      }

      internal static void PS3_DisableSyscall(int num)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 DISABLESYSCALL " + num.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void PS3_ClearHistory(bool include_directory)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        if (include_directory)
          PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 DELHISTORY+D");
        else
          PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 DELHISTORY");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static bool PS3_CheckSyscall(int num)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 CHECKSYSCALL " + num.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return Convert.ToInt32(PS3MAPI.PS3MAPI_Client_Server.sResponse) == 0;
      }

      internal static void PS3_PartialDisableSyscall8(int mode)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 PDISABLESYSCALL8 " + mode.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static int PS3_PartialCheckSyscall8()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 PCHECKSYSCALL8");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return Convert.ToInt32(PS3MAPI.PS3MAPI_Client_Server.sResponse);
      }

      internal static void PS3_RemoveHook()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 REMOVEHOOK");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static string PS3_GetIDPS()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 GETIDPS");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return PS3MAPI.PS3MAPI_Client_Server.sResponse;
      }

      internal static void PS3_SetIDPS(string IDPS)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 SETIDPS " + IDPS.Substring(0, 16) + " " + IDPS.Substring(16, 16));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static string PS3_GetPSID()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 GETPSID");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return PS3MAPI.PS3MAPI_Client_Server.sResponse;
      }

      internal static void PS3_SetPSID(string PSID)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PS3 SETPSID " + PSID.Substring(0, 16) + " " + PSID.Substring(16, 16));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static string Process_GetName(uint pid)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PROCESS GETNAME " + string.Format("{0}", (object) pid));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return PS3MAPI.PS3MAPI_Client_Server.sResponse;
      }

      internal static uint[] Process_GetPidList()
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PROCESS GETALLPID");
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        int index = 0;
        PS3MAPI.PS3MAPI_Client_Server.iprocesses_pid = new uint[16];
        string sResponse = PS3MAPI.PS3MAPI_Client_Server.sResponse;
        char[] chArray = new char[1]{ '|' };
        foreach (string str in sResponse.Split(chArray))
        {
          if (str.Length != 0 && str != null && (str != "" && str != " ") && str != "0")
          {
            PS3MAPI.PS3MAPI_Client_Server.iprocesses_pid[index] = Convert.ToUInt32(str, 10);
            ++index;
          }
        }
        return PS3MAPI.PS3MAPI_Client_Server.iprocesses_pid;
      }

      internal static void Memory_Get(uint Pid, ulong Address, byte[] Bytes)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SetBinaryMode(true);
        int length = Bytes.Length;
        long num1 = 0;
        bool flag = false;
        PS3MAPI.PS3MAPI_Client_Server.OpenDataSocket();
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MEMORY GET " + string.Format("{0}", (object) Pid) + " " + string.Format("{0:X16}", (object) Address) + " " + string.Format("{0}", (object) Bytes.Length));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode1 = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode1 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.DataConnectionAlreadyOpen && eResponseCode1 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.MemoryStatusOK)
          throw new Exception(PS3MAPI.PS3MAPI_Client_Server.sResponse);
        PS3MAPI.PS3MAPI_Client_Server.ConnectDataSocket();
        byte[] buffer = new byte[Bytes.Length];
        while (!flag)
        {
          try
          {
            long num2 = (long) PS3MAPI.PS3MAPI_Client_Server.data_sock.Receive(buffer, length, SocketFlags.None);
            if (num2 > 0L)
            {
              Buffer.BlockCopy((Array) buffer, 0, (Array) Bytes, (int) num1, (int) num2);
              num1 += num2;
              if ((int) (num1 * 100L / (long) length) >= 100)
                flag = true;
            }
            else
              flag = true;
            if (flag)
            {
              PS3MAPI.PS3MAPI_Client_Server.CloseDataSocket();
              PS3MAPI.PS3MAPI_Client_Server.ReadResponse();
              PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode2 = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
              if (eResponseCode2 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful && eResponseCode2 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.MemoryActionCompleted)
                throw new Exception(PS3MAPI.PS3MAPI_Client_Server.sResponse);
              PS3MAPI.PS3MAPI_Client_Server.SetBinaryMode(false);
            }
          }
          catch (Exception ex)
          {
            PS3MAPI.PS3MAPI_Client_Server.CloseDataSocket();
            PS3MAPI.PS3MAPI_Client_Server.ReadResponse();
            PS3MAPI.PS3MAPI_Client_Server.SetBinaryMode(false);
            throw ex;
          }
        }
      }

      internal static void Memory_Set(uint Pid, ulong Address, byte[] Bytes)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SetBinaryMode(true);
        int length = Bytes.Length;
        long num1 = 0;
        long num2 = 0;
        bool flag = false;
        PS3MAPI.PS3MAPI_Client_Server.OpenDataSocket();
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MEMORY SET " + string.Format("{0}", (object) Pid) + " " + string.Format("{0:X16}", (object) Address));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode1 = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode1 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.DataConnectionAlreadyOpen && eResponseCode1 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.MemoryStatusOK)
          throw new Exception(PS3MAPI.PS3MAPI_Client_Server.sResponse);
        PS3MAPI.PS3MAPI_Client_Server.ConnectDataSocket();
        while (!flag)
        {
          try
          {
            byte[] buffer = new byte[length - (int) num1];
            Buffer.BlockCopy((Array) Bytes, (int) num2, (Array) buffer, 0, length - (int) num1);
            num2 = (long) PS3MAPI.PS3MAPI_Client_Server.data_sock.Send(buffer, Bytes.Length - (int) num1, SocketFlags.None);
            flag = false;
            if (num2 > 0L)
            {
              num1 += num2;
              if ((int) (num1 * 100L / (long) length) >= 100)
                flag = true;
            }
            else
              flag = true;
            if (flag)
            {
              PS3MAPI.PS3MAPI_Client_Server.CloseDataSocket();
              PS3MAPI.PS3MAPI_Client_Server.ReadResponse();
              PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode2 = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
              if (eResponseCode2 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful && eResponseCode2 != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.MemoryActionCompleted)
                throw new Exception(PS3MAPI.PS3MAPI_Client_Server.sResponse);
              PS3MAPI.PS3MAPI_Client_Server.SetBinaryMode(false);
            }
          }
          catch (Exception ex)
          {
            PS3MAPI.PS3MAPI_Client_Server.CloseDataSocket();
            PS3MAPI.PS3MAPI_Client_Server.ReadResponse();
            PS3MAPI.PS3MAPI_Client_Server.SetBinaryMode(false);
            throw ex;
          }
        }
      }

      internal static int[] Module_GetPrxIdList(uint pid)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE GETALLPRXID " + string.Format("{0}", (object) pid));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        int index = 0;
        PS3MAPI.PS3MAPI_Client_Server.imodules_prx_id = new int[128];
        string sResponse = PS3MAPI.PS3MAPI_Client_Server.sResponse;
        char[] chArray = new char[1]{ '|' };
        foreach (string str in sResponse.Split(chArray))
        {
          if (str.Length != 0 && str != null && (str != "" && str != " ") && str != "0")
          {
            PS3MAPI.PS3MAPI_Client_Server.imodules_prx_id[index] = Convert.ToInt32(str, 10);
            ++index;
          }
        }
        return PS3MAPI.PS3MAPI_Client_Server.imodules_prx_id;
      }

      internal static string Module_GetName(uint pid, int prxid)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE GETNAME " + string.Format("{0}", (object) pid) + " " + prxid.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return PS3MAPI.PS3MAPI_Client_Server.sResponse;
      }

      internal static string Module_GetFilename(uint pid, int prxid)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE GETFILENAME " + string.Format("{0}", (object) pid) + " " + prxid.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        return PS3MAPI.PS3MAPI_Client_Server.sResponse;
      }

      internal static void Module_Load(uint pid, string path)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE LOAD " + string.Format("{0}", (object) pid) + " " + path);
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void Module_Unload(uint pid, int prx_id)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE UNLOAD " + string.Format("{0}", (object) pid) + " " + prx_id.ToString());
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void VSHPlugins_GetInfoBySlot(uint slot, out string name, out string path)
      {
        name = "";
        path = "";
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE GETVSHPLUGINFO " + string.Format("{0}", (object) slot));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK && eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        string[] strArray = PS3MAPI.PS3MAPI_Client_Server.sResponse.Split('|');
        name = strArray[0];
        path = strArray[1];
      }

      internal static void VSHPlugins_Load(uint slot, string path)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE LOADVSHPLUG " + string.Format("{0}", (object) slot) + " " + path);
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void VSHPlugins_Unload(uint slot)
      {
        if (!PS3MAPI.PS3MAPI_Client_Server.IsConnected)
          throw new Exception("PS3MAPI not connected!");
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("MODULE UNLOADVSHPLUGS " + string.Format("{0}", (object) slot));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void Fail()
      {
        PS3MAPI.PS3MAPI_Client_Server.Fail(new Exception("[" + PS3MAPI.PS3MAPI_Client_Server.eResponseCode.ToString() + "] " + PS3MAPI.PS3MAPI_Client_Server.sResponse));
      }

      internal static void Fail(Exception e)
      {
        PS3MAPI.PS3MAPI_Client_Server.Disconnect();
        throw e;
      }

      internal static void SetBinaryMode(bool bMode)
      {
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("TYPE" + (bMode ? " I" : " A"));
        PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode eResponseCode = PS3MAPI.PS3MAPI_Client_Server.eResponseCode;
        if (eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.CommandOK || eResponseCode == PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.RequestSuccessful)
          return;
        PS3MAPI.PS3MAPI_Client_Server.Fail();
      }

      internal static void OpenDataSocket()
      {
        PS3MAPI.PS3MAPI_Client_Server.Connect();
        PS3MAPI.PS3MAPI_Client_Server.SendCommand("PASV");
        if (PS3MAPI.PS3MAPI_Client_Server.eResponseCode != PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode.EnteringPassiveMode)
          PS3MAPI.PS3MAPI_Client_Server.Fail();
        string[] strArray;
        try
        {
          int startIndex = PS3MAPI.PS3MAPI_Client_Server.sResponse.IndexOf('(') + 1;
          int length = PS3MAPI.PS3MAPI_Client_Server.sResponse.IndexOf(')') - startIndex;
          strArray = PS3MAPI.PS3MAPI_Client_Server.sResponse.Substring(startIndex, length).Split(',');
        }
        catch (Exception ex)
        {
          PS3MAPI.PS3MAPI_Client_Server.Fail(new Exception("Malformed PASV response: " + PS3MAPI.PS3MAPI_Client_Server.sResponse));
          throw new Exception("Malformed PASV response: " + PS3MAPI.PS3MAPI_Client_Server.sResponse);
        }
        if (strArray.Length < 6)
          PS3MAPI.PS3MAPI_Client_Server.Fail(new Exception("Malformed PASV response: " + PS3MAPI.PS3MAPI_Client_Server.sResponse));
        string.Format("{0}.{1}.{2}.{3}", (object) strArray[0], (object) strArray[1], (object) strArray[2], (object) strArray[3]);
        int port = (int.Parse(strArray[4]) << 8) + int.Parse(strArray[5]);
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.CloseDataSocket();
          PS3MAPI.PS3MAPI_Client_Server.data_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          PS3MAPI.PS3MAPI_Client_Server.data_ipEndPoint = new IPEndPoint(Dns.GetHostByName(PS3MAPI.PS3MAPI_Client_Server.sServerIP).AddressList[0], port);
          PS3MAPI.PS3MAPI_Client_Server.data_sock.Connect((EndPoint) PS3MAPI.PS3MAPI_Client_Server.data_ipEndPoint);
        }
        catch (Exception ex)
        {
          throw new Exception("Failed to connect for data transfer: " + ex.Message);
        }
      }

      internal static void ConnectDataSocket()
      {
        if (PS3MAPI.PS3MAPI_Client_Server.data_sock != null)
          return;
        try
        {
          PS3MAPI.PS3MAPI_Client_Server.data_sock = PS3MAPI.PS3MAPI_Client_Server.listening_sock.Accept();
          PS3MAPI.PS3MAPI_Client_Server.listening_sock.Close();
          PS3MAPI.PS3MAPI_Client_Server.listening_sock = (Socket) null;
          if (PS3MAPI.PS3MAPI_Client_Server.data_sock == null)
            throw new Exception("Winsock error: " + Convert.ToString(Marshal.GetLastWin32Error()));
        }
        catch (Exception ex)
        {
          throw new Exception("Failed to connect for data transfer: " + ex.Message);
        }
      }

      internal static void CloseDataSocket()
      {
        if (PS3MAPI.PS3MAPI_Client_Server.data_sock != null)
        {
          if (PS3MAPI.PS3MAPI_Client_Server.data_sock.Connected)
            PS3MAPI.PS3MAPI_Client_Server.data_sock.Close();
          PS3MAPI.PS3MAPI_Client_Server.data_sock = (Socket) null;
        }
        PS3MAPI.PS3MAPI_Client_Server.data_ipEndPoint = (IPEndPoint) null;
      }

      internal static void ReadResponse()
      {
        PS3MAPI.PS3MAPI_Client_Server.sMessages = "";
        string lineFromBucket;
        while (true)
        {
          lineFromBucket = PS3MAPI.PS3MAPI_Client_Server.GetLineFromBucket();
          if (!Regex.Match(lineFromBucket, "^[0-9]+ ").Success)
            PS3MAPI.PS3MAPI_Client_Server.sMessages = PS3MAPI.PS3MAPI_Client_Server.sMessages + Regex.Replace(lineFromBucket, "^[0-9]+-", "") + "\n";
          else
            break;
        }
        PS3MAPI.PS3MAPI_Client_Server.sResponse = lineFromBucket.Substring(4).Replace("\r", "").Replace("\n", "");
        PS3MAPI.PS3MAPI_Client_Server.eResponseCode = (PS3MAPI.PS3MAPI_Client_Server.PS3MAPI_ResponseCode) int.Parse(lineFromBucket.Substring(0, 3));
        PS3MAPI.PS3MAPI_Client_Server.sLog = PS3MAPI.PS3MAPI_Client_Server.sLog + "RESPONSE CODE: " + PS3MAPI.PS3MAPI_Client_Server.eResponseCode.ToString() + Environment.NewLine;
        PS3MAPI.PS3MAPI_Client_Server.sLog = PS3MAPI.PS3MAPI_Client_Server.sLog + "RESPONSE MSG: " + PS3MAPI.PS3MAPI_Client_Server.sResponse + Environment.NewLine + Environment.NewLine;
      }

      internal static void SendCommand(string sCommand)
      {
        PS3MAPI.PS3MAPI_Client_Server.sLog = PS3MAPI.PS3MAPI_Client_Server.sLog + "COMMAND: " + sCommand + Environment.NewLine;
        PS3MAPI.PS3MAPI_Client_Server.Connect();
        byte[] bytes = Encoding.ASCII.GetBytes((sCommand + "\r\n").ToCharArray());
        PS3MAPI.PS3MAPI_Client_Server.main_sock.Send(bytes, bytes.Length, SocketFlags.None);
        PS3MAPI.PS3MAPI_Client_Server.ReadResponse();
      }

      internal static void FillBucket()
      {
        byte[] numArray = new byte[512];
        int num1 = 0;
        while (PS3MAPI.PS3MAPI_Client_Server.main_sock.Available < 1)
        {
          Thread.Sleep(50);
          num1 += 50;
          if (num1 > PS3MAPI.PS3MAPI_Client_Server.Timeout)
            PS3MAPI.PS3MAPI_Client_Server.Fail(new Exception("Timed out waiting on server to respond."));
        }
        while (PS3MAPI.PS3MAPI_Client_Server.main_sock.Available > 0)
        {
          long num2 = (long) PS3MAPI.PS3MAPI_Client_Server.main_sock.Receive(numArray, 512, SocketFlags.None);
          PS3MAPI.PS3MAPI_Client_Server.sBucket += Encoding.ASCII.GetString(numArray, 0, (int) num2);
          Thread.Sleep(50);
        }
      }

      internal static string GetLineFromBucket()
      {
        int length;
        for (length = PS3MAPI.PS3MAPI_Client_Server.sBucket.IndexOf('\n'); length < 0; length = PS3MAPI.PS3MAPI_Client_Server.sBucket.IndexOf('\n'))
          PS3MAPI.PS3MAPI_Client_Server.FillBucket();
        string str = PS3MAPI.PS3MAPI_Client_Server.sBucket.Substring(0, length);
        PS3MAPI.PS3MAPI_Client_Server.sBucket = PS3MAPI.PS3MAPI_Client_Server.sBucket.Substring(length + 1);
        return str;
      }

      internal enum PS3MAPI_ResponseCode
      {
        DataConnectionAlreadyOpen = 125, // 0x0000007D
        MemoryStatusOK = 150, // 0x00000096
        CommandOK = 200, // 0x000000C8
        PS3MAPIConnected = 220, // 0x000000DC
        RequestSuccessful = 226, // 0x000000E2
        EnteringPassiveMode = 227, // 0x000000E3
        PS3MAPIConnectedOK = 230, // 0x000000E6
        MemoryActionCompleted = 250, // 0x000000FA
        MemoryActionPended = 350, // 0x0000015E
      }
    }
  }
}
