// Decompiled with JetBrains decompiler
// Type: PS3Lib.PS3API
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe

using System;
using System.Collections.Generic;
using System.Reflection;

namespace PS3Lib
{
    public class PS3API
    {
        private static string targetName = string.Empty;
        private static string targetIp = string.Empty;

        public PS3API(SelectAPI API = SelectAPI.TargetManager)
        {
            PS3API.SetAPI.API = API;
            this.MakeInstanceAPI(API);
        }

        public void setTargetName(string value)
        {
            PS3API.targetName = value;
        }

        private void MakeInstanceAPI(SelectAPI API)
        {
            switch (API)
            {
                case SelectAPI.ControlConsole:
                    if (PS3API.Common.CcApi != null)
                        break;
                    PS3API.Common.CcApi = new CCAPI();
                    break;
                case SelectAPI.TargetManager:
                    if (PS3API.Common.TmApi != null)
                        break;
                    PS3API.Common.TmApi = new TMAPI();
                    break;
                case SelectAPI.PS3Manager:
                    if (PS3API.Common.Ps3mApi == null)
                        PS3API.Common.Ps3mApi = new PS3MAPI();
                    break;
            }
        }

        public void InitTarget()
        {
            if (PS3API.SetAPI.API != SelectAPI.TargetManager)
                return;
            PS3API.Common.TmApi.InitComms();
        }

        public bool ConnectTarget(int target = 0)
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool flag = false;
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    flag = new ConsoleList(this).Show();
                    break;
                case SelectAPI.TargetManager:
                    flag = PS3API.Common.TmApi.ConnectTarget(target);
                    break;
                case SelectAPI.PS3Manager:
                    flag = PS3API.Common.Ps3mApi.ConnectTarget();
                    break;
            }
            return flag;
        }

        public bool ConnectTarget(string ip)
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool flag = false;
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    if (PS3API.Common.CcApi.SUCCESS(PS3API.Common.CcApi.ConnectTarget(ip)))
                    {
                        PS3API.targetIp = ip;
                        flag = true;
                        break;
                    }
                    flag = false;
                    break;
                case SelectAPI.PS3Manager:
                    flag = PS3API.Common.Ps3mApi.ConnectTarget(ip);
                    if (flag)
                        PS3API.targetIp = ip;
                    break;
            }
            return flag;
        }

        public bool ConnectTarget(string ip, int port)
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool flag = false;
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                flag = PS3API.Common.Ps3mApi.ConnectTarget(ip, port);
                if (flag)
                    PS3API.targetIp = ip;
            }
            return flag;
        }

        public void DisconnectTarget()
        {
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    PS3API.Common.CcApi.DisconnectTarget();
                    break;
                case SelectAPI.TargetManager:
                    PS3API.Common.TmApi.DisconnectTarget();
                    break;
                case SelectAPI.PS3Manager:
                    PS3API.Common.Ps3mApi.DisconnectTarget();
                    break;
            }
        }

        public bool AttachProcess()
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool flag = false;
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    flag = PS3API.Common.CcApi.SUCCESS(PS3API.Common.CcApi.AttachProcess());
                    break;
                case SelectAPI.TargetManager:
                    flag = PS3API.Common.TmApi.AttachProcess();
                    break;
                case SelectAPI.PS3Manager:
                    flag = PS3API.Common.Ps3mApi.AttachProcess();
                    break;
            }
            return flag;
        }

        public string GetConsoleName()
        {
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    if (PS3API.targetName != string.Empty)
                        return PS3API.targetName;
                    if (PS3API.targetIp != string.Empty)
                    {
                        List<CCAPI.ConsoleInfo> consoleInfoList = new List<CCAPI.ConsoleInfo>();
                        List<CCAPI.ConsoleInfo> consoleList = PS3API.Common.CcApi.GetConsoleList();
                        if (consoleList.Count > 0)
                        {
                            for (int index = 0; index < consoleList.Count; ++index)
                            {
                                if (consoleList[index].Ip == PS3API.targetIp)
                                    return consoleList[index].Name;
                            }
                        }
                    }
                    return PS3API.targetIp;
                case SelectAPI.TargetManager:
                    return PS3API.Common.TmApi.SCE.GetTargetName();
                case SelectAPI.PS3Manager:
                    return "PS3 Manager API";
                default:
                    return "none";
            }
        }

        public void SetMemory(uint offset, byte[] buffer)
        {
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    PS3API.Common.CcApi.SetMemory(offset, buffer);
                    break;
                case SelectAPI.TargetManager:
                    PS3API.Common.TmApi.SetMemory(offset, buffer);
                    break;
                case SelectAPI.PS3Manager:
                    PS3API.Common.Ps3mApi.SetMemory(offset, buffer);
                    break;
            }
        }

        public void GetMemory(uint offset, byte[] buffer)
        {
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    PS3API.Common.CcApi.GetMemory(offset, buffer);
                    break;
                case SelectAPI.TargetManager:
                    PS3API.Common.TmApi.GetMemory(offset, buffer);
                    break;
                case SelectAPI.PS3Manager:
                    PS3API.Common.Ps3mApi.GetMemory(offset, buffer);
                    break;
            }
        }

        public byte[] GetBytes(uint offset, int length)
        {
            byte[] numArray = new byte[length];
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    PS3API.Common.CcApi.GetMemory(offset, numArray);
                    break;
                case SelectAPI.TargetManager:
                    PS3API.Common.TmApi.GetMemory(offset, numArray);
                    break;
                case SelectAPI.PS3Manager:
                    PS3API.Common.Ps3mApi.GetMemory(offset, numArray);
                    break;
            }
            return numArray;
        }

        public void Notify(string msg, CCAPI.NotifyIcon icon = CCAPI.NotifyIcon.INFO)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.Notify(icon, msg);
            }
            else
            {
                if (PS3API.SetAPI.API != SelectAPI.PS3Manager)
                    return;
                PS3API.Common.Ps3mApi.Notify(msg);
            }
        }

        public void Power(PS3API.PowerFlags flag)
        {
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    switch (flag)
                    {
                        case PS3API.PowerFlags.ShutDown:
                            PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.ShutDown);
                            return;
                        case PS3API.PowerFlags.QuickReboot:
                            PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.SoftReboot);
                            return;
                        case PS3API.PowerFlags.SoftReboot:
                            PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.SoftReboot);
                            return;
                        case PS3API.PowerFlags.HardReboot:
                            PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.HardReboot);
                            return;
                        default:
                            return;
                    }
                case SelectAPI.PS3Manager:
                    switch (flag)
                    {
                        case PS3API.PowerFlags.ShutDown:
                            PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.ShutDown);
                            break;
                        case PS3API.PowerFlags.QuickReboot:
                            PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.QuickReboot);
                            break;
                        case PS3API.PowerFlags.SoftReboot:
                            PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.SoftReboot);
                            break;
                        case PS3API.PowerFlags.HardReboot:
                            PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.ShutDown);
                            break;
                    }
                    break;
            }
        }

        public void SetConsoleID(string consoleID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetConsoleID(consoleID);
            }
            else
            {
                if (PS3API.SetAPI.API != SelectAPI.PS3Manager)
                    return;
                PS3API.Common.Ps3mApi.SetConsoleID(consoleID);
            }
        }

        public void SetConsoleID(byte[] consoleID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetConsoleID(consoleID);
            }
            else
            {
                if (PS3API.SetAPI.API != SelectAPI.PS3Manager)
                    return;
                PS3API.Common.Ps3mApi.SetConsoleID(consoleID);
            }
        }

        public void SetPSID(string PSID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetPSID(PSID);
            }
            else
            {
                if (PS3API.SetAPI.API != SelectAPI.PS3Manager)
                    return;
                PS3API.Common.Ps3mApi.SetPSID(PSID);
            }
        }

        public void SetPSID(byte[] PSID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetPSID(PSID);
            }
            else
            {
                if (PS3API.SetAPI.API != SelectAPI.PS3Manager)
                    return;
                PS3API.Common.Ps3mApi.SetPSID(PSID);
            }
        }

        public void Buzzer(PS3API.BuzzerMode flag)
        {
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    switch (flag)
                    {
                        case PS3API.BuzzerMode.Single:
                            PS3API.Common.CcApi.RingBuzzer(CCAPI.BuzzerMode.Single);
                            return;
                        case PS3API.BuzzerMode.Double:
                            PS3API.Common.CcApi.RingBuzzer(CCAPI.BuzzerMode.Double);
                            return;
                        case PS3API.BuzzerMode.Triple:
                            PS3API.Common.CcApi.RingBuzzer(CCAPI.BuzzerMode.Continuous);
                            return;
                        default:
                            return;
                    }
                case SelectAPI.PS3Manager:
                    switch (flag)
                    {
                        case PS3API.BuzzerMode.Single:
                            PS3API.Common.Ps3mApi.RingBuzzer(PS3ManagerAPI.PS3MAPI.PS3_CMD.BuzzerMode.Single);
                            break;
                        case PS3API.BuzzerMode.Double:
                            PS3API.Common.Ps3mApi.RingBuzzer(PS3ManagerAPI.PS3MAPI.PS3_CMD.BuzzerMode.Double);
                            break;
                        case PS3API.BuzzerMode.Triple:
                            PS3API.Common.Ps3mApi.RingBuzzer(PS3ManagerAPI.PS3MAPI.PS3_CMD.BuzzerMode.Triple);
                            break;
                    }
                    break;
            }
        }

        public void Led(PS3API.LedColor color, PS3API.LedMode mode)
        {
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.Off)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Off);
                        break;
                    }
                    if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.On)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.On);
                        break;
                    }
                    if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkFast)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                        break;
                    }
                    if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkSlow)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                        break;
                    }
                    if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.Off)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.Off);
                        break;
                    }
                    if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.On)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.On);
                        break;
                    }
                    if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkFast)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.Blink);
                        break;
                    }
                    if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkSlow)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.Blink);
                        break;
                    }
                    if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.Off)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Off);
                        break;
                    }
                    if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.On)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.On);
                        break;
                    }
                    if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.BlinkFast)
                    {
                        PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                        break;
                    }
                    if (color != PS3API.LedColor.Yellow || mode != PS3API.LedMode.BlinkSlow)
                        break;
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                    break;
                case SelectAPI.PS3Manager:
                    if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.Off)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.Off);
                    else if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.On)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.On);
                    else if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkFast)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkFast);
                    else if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkSlow)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkSlow);
                    else if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.Off)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.Off);
                    else if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.On)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.On);
                    else if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkFast)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkFast);
                    else if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkSlow)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkSlow);
                    else if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.Off)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.Off);
                    else if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.On)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.On);
                    else if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.BlinkFast)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkFast);
                    else if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.BlinkSlow)
                        PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkSlow);
                    break;
            }
        }

        public void ChangeAPI(SelectAPI API)
        {
            PS3API.SetAPI.API = API;
            this.MakeInstanceAPI(this.GetCurrentAPI());
        }

        public SelectAPI GetCurrentAPI()
        {
            return PS3API.SetAPI.API;
        }

        public string GetCurrentAPIName()
        {
            string str = string.Empty;
            switch (PS3API.SetAPI.API)
            {
                case SelectAPI.ControlConsole:
                    str = Enum.GetName(typeof(SelectAPI), (object)SelectAPI.ControlConsole).Replace("Console", " Console");
                    break;
                case SelectAPI.TargetManager:
                    str = Enum.GetName(typeof(SelectAPI), (object)SelectAPI.TargetManager).Replace("Manager", " Manager");
                    break;
                case SelectAPI.PS3Manager:
                    str = Enum.GetName(typeof(SelectAPI), (object)SelectAPI.PS3Manager).Replace("Manager", " Manager");
                    break;
            }
            return str;
        }

        public Assembly PS3TMAPI_NET()
        {
            return PS3API.Common.TmApi.PS3TMAPI_NET();
        }

        public Extension Extension
        {
            get
            {
                return new Extension(PS3API.SetAPI.API);
            }
        }

        public TMAPI TMAPI
        {
            get
            {
                return new TMAPI();
            }
        }

        public CCAPI CCAPI
        {
            get
            {
                return new CCAPI();
            }
        }

        public PS3MAPI PS3MAPI
        {
            get
            {
                return new PS3MAPI();
            }
        }

        private class SetAPI
        {
            public static SelectAPI API;
        }

        private class Common
        {
            public static CCAPI CcApi;
            public static TMAPI TmApi;
            public static PS3MAPI Ps3mApi;
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
            BlinkSlow,
            BlinkFast,
        }
    }
}
