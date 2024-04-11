using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PS3Lib
{
	// Token: 0x02000003 RID: 3
	public class CCAPI
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020E8 File Offset: 0x000002E8
		public Extension Extension
		{
			get
			{
				return new Extension(SelectAPI.ControlConsole);
			}
		}

		// Token: 0x06000007 RID: 7
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllName);

		// Token: 0x06000008 RID: 8
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x06000009 RID: 9 RVA: 0x00002100 File Offset: 0x00000300
		public CCAPI()
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\FrenchModdingTeam\\CCAPI\\InstallFolder");
			bool flag = registryKey != null;
			if (flag)
			{
				string text = registryKey.GetValue("path") as string;
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					string text2 = text + "\\CCAPI.dll";
					bool flag3 = File.Exists(text2);
					if (flag3)
					{
						bool flag4 = this.libModule == IntPtr.Zero;
						if (flag4)
						{
							this.libModule = CCAPI.LoadLibrary(text2);
						}
						bool flag5 = this.libModule != IntPtr.Zero;
						if (flag5)
						{
							this.CCAPIFunctionsList.Clear();
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIConnectConsole"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIDisconnectConsole"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetConnectionStatus"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetConsoleInfo"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetDllVersion"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetFirmwareInfo"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetNumberOfConsoles"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetProcessList"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetMemory"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetProcessName"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIGetTemperature"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIVshNotify"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIRingBuzzer"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPISetBootConsoleIds"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPISetConsoleIds"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPISetConsoleLed"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPISetMemory"));
							this.CCAPIFunctionsList.Add(CCAPI.GetProcAddress(this.libModule, "CCAPIShutdown"));
							bool flag6 = this.IsCCAPILoaded();
							if (flag6)
							{
								this.connectConsole = (CCAPI.connectConsoleDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.ConnectConsole), typeof(CCAPI.connectConsoleDelegate));
								this.disconnectConsole = (CCAPI.disconnectConsoleDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.DisconnectConsole), typeof(CCAPI.disconnectConsoleDelegate));
								this.getConnectionStatus = (CCAPI.getConnectionStatusDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetConnectionStatus), typeof(CCAPI.getConnectionStatusDelegate));
								this.getConsoleInfo = (CCAPI.getConsoleInfoDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetConsoleInfo), typeof(CCAPI.getConsoleInfoDelegate));
								this.getDllVersion = (CCAPI.getDllVersionDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetDllVersion), typeof(CCAPI.getDllVersionDelegate));
								this.getFirmwareInfo = (CCAPI.getFirmwareInfoDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetFirmwareInfo), typeof(CCAPI.getFirmwareInfoDelegate));
								this.getNumberOfConsoles = (CCAPI.getNumberOfConsolesDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetNumberOfConsoles), typeof(CCAPI.getNumberOfConsolesDelegate));
								this.getProcessList = (CCAPI.getProcessListDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetProcessList), typeof(CCAPI.getProcessListDelegate));
								this.getProcessMemory = (CCAPI.getProcessMemoryDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetMemory), typeof(CCAPI.getProcessMemoryDelegate));
								this.getProcessName = (CCAPI.getProcessNameDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetProcessName), typeof(CCAPI.getProcessNameDelegate));
								this.getTemperature = (CCAPI.getTemperatureDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.GetTemperature), typeof(CCAPI.getTemperatureDelegate));
								this.notify = (CCAPI.notifyDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.VshNotify), typeof(CCAPI.notifyDelegate));
								this.ringBuzzer = (CCAPI.ringBuzzerDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.RingBuzzer), typeof(CCAPI.ringBuzzerDelegate));
								this.setBootConsoleIds = (CCAPI.setBootConsoleIdsDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.SetBootConsoleIds), typeof(CCAPI.setBootConsoleIdsDelegate));
								this.setConsoleIds = (CCAPI.setConsoleIdsDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.SetConsoleIds), typeof(CCAPI.setConsoleIdsDelegate));
								this.setConsoleLed = (CCAPI.setConsoleLedDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.SetConsoleLed), typeof(CCAPI.setConsoleLedDelegate));
								this.setProcessMemory = (CCAPI.setProcessMemoryDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.SetMemory), typeof(CCAPI.setProcessMemoryDelegate));
								this.shutdown = (CCAPI.shutdownDelegate)Marshal.GetDelegateForFunctionPointer(this.GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions.ShutDown), typeof(CCAPI.shutdownDelegate));
							}
							else
							{
								MessageBox.Show("Impossible to load CCAPI 2.60+", "This CCAPI.dll is not compatible", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						else
						{
							MessageBox.Show("Impossible to load CCAPI 2.60+", "CCAPI.dll cannot be loaded", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else
					{
						MessageBox.Show("You need to install CCAPI 2.60+ to use this library.", "CCAPI.dll not found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("Invalid CCAPI folder, please re-install it.", "CCAPI not installed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("You need to install CCAPI 2.60+ to use this library.", "CCAPI not installed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026B8 File Offset: 0x000008B8
		private IntPtr ReadDataFromUnBufPtr<T>(IntPtr unBuf, ref T storage)
		{
			storage = (T)((object)Marshal.PtrToStructure(unBuf, typeof(T)));
			return new IntPtr(unBuf.ToInt64() + (long)Marshal.SizeOf(storage));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002704 File Offset: 0x00000904
		private IntPtr GetCCAPIFunctionPtr(CCAPI.CCAPIFunctions Function)
		{
			return this.CCAPIFunctionsList.ElementAt((int)Function);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002724 File Offset: 0x00000924
		private bool IsCCAPILoaded()
		{
			for (int i = 0; i < this.CCAPIFunctionsList.Count; i++)
			{
				bool flag = this.CCAPIFunctionsList.ElementAt(i) == IntPtr.Zero;
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000277F File Offset: 0x0000097F
		private void CompleteInfo(ref CCAPI.TargetInfo Info, int fw, int ccapi, ulong sysTable, int consoleType, int tempCELL, int tempRSX)
		{
			Info.Firmware = fw;
			Info.CCAPI = ccapi;
			Info.SysTable = sysTable;
			Info.ConsoleType = consoleType;
			Info.TempCell = tempCELL;
			Info.TempRSX = tempRSX;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000027B8 File Offset: 0x000009B8
		public bool SUCCESS(int Void)
		{
			return Void == 0;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000027D0 File Offset: 0x000009D0
		public bool ConnectTarget()
		{
			return new ConsoleList(new PS3API(SelectAPI.ControlConsole)).Show();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000027F4 File Offset: 0x000009F4
		public int ConnectTarget(string targetIP)
		{
			return this.connectConsole(targetIP);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002814 File Offset: 0x00000A14
		public int GetConnectionStatus()
		{
			int result = 0;
			this.getConnectionStatus(ref result);
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002838 File Offset: 0x00000A38
		public int DisconnectTarget()
		{
			return this.disconnectConsole();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002858 File Offset: 0x00000A58
		public int AttachProcess()
		{
			CCAPI.System.processID = 0U;
			int num = this.GetProcessList(out CCAPI.System.processIDs);
			bool flag = this.SUCCESS(num) && CCAPI.System.processIDs.Length != 0;
			if (flag)
			{
				for (int i = 0; i < CCAPI.System.processIDs.Length; i++)
				{
					string empty = string.Empty;
					num = this.GetProcessName(CCAPI.System.processIDs[i], out empty);
					bool flag2 = !this.SUCCESS(num);
					if (flag2)
					{
						break;
					}
					bool flag3 = !empty.Contains("flash");
					if (flag3)
					{
						CCAPI.System.processID = CCAPI.System.processIDs[i];
						break;
					}
					num = -1;
				}
				bool flag4 = CCAPI.System.processID == 0U;
				if (flag4)
				{
					CCAPI.System.processID = CCAPI.System.processIDs[CCAPI.System.processIDs.Length - 1];
				}
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002930 File Offset: 0x00000B30
		public int AttachProcess(CCAPI.ProcessType procType)
		{
			CCAPI.System.processID = 0U;
			int num = this.GetProcessList(out CCAPI.System.processIDs);
			bool flag = num >= 0 && CCAPI.System.processIDs.Length != 0;
			if (flag)
			{
				for (int i = 0; i < CCAPI.System.processIDs.Length; i++)
				{
					string empty = string.Empty;
					num = this.GetProcessName(CCAPI.System.processIDs[i], out empty);
					bool flag2 = num < 0;
					if (flag2)
					{
						break;
					}
					bool flag3 = procType == CCAPI.ProcessType.VSH && empty.Contains("vsh");
					if (flag3)
					{
						CCAPI.System.processID = CCAPI.System.processIDs[i];
						break;
					}
					bool flag4 = procType == CCAPI.ProcessType.SYS_AGENT && empty.Contains("agent");
					if (flag4)
					{
						CCAPI.System.processID = CCAPI.System.processIDs[i];
						break;
					}
					bool flag5 = procType == CCAPI.ProcessType.CURRENTGAME && !empty.Contains("flash");
					if (flag5)
					{
						CCAPI.System.processID = CCAPI.System.processIDs[i];
						break;
					}
				}
				bool flag6 = CCAPI.System.processID == 0U;
				if (flag6)
				{
					CCAPI.System.processID = CCAPI.System.processIDs[CCAPI.System.processIDs.Length - 1];
				}
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A58 File Offset: 0x00000C58
		public int AttachProcess(uint process)
		{
			uint[] array = new uint[64];
			int num = this.GetProcessList(out array);
			bool flag = this.SUCCESS(num);
			if (flag)
			{
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2 = array[i] == process;
					if (flag2)
					{
						num = 0;
						CCAPI.System.processID = process;
						break;
					}
					num = -1;
				}
			}
			array = null;
			return num;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002ABC File Offset: 0x00000CBC
		public int GetProcessList(out uint[] processIds)
		{
			uint num = 64U;
			IntPtr intPtr = Marshal.AllocHGlobal(256);
			int num2 = this.getProcessList(ref num, intPtr);
			processIds = new uint[num];
			bool flag = this.SUCCESS(num2);
			if (flag)
			{
				IntPtr unBuf = intPtr;
				for (uint num3 = 0U; num3 < num; num3 += 1U)
				{
					unBuf = this.ReadDataFromUnBufPtr<uint>(unBuf, ref processIds[(int)((uint)((UIntPtr)num3))]);
				}
			}
			Marshal.FreeHGlobal(intPtr);
			return num2;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B44 File Offset: 0x00000D44
		public int GetProcessName(uint processId, out string name)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(529);
			int num = this.getProcessName(processId, intPtr);
			name = string.Empty;
			bool flag = this.SUCCESS(num);
			if (flag)
			{
				name = Marshal.PtrToStringAnsi(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002B94 File Offset: 0x00000D94
		public uint GetAttachedProcess()
		{
			return CCAPI.System.processID;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BAC File Offset: 0x00000DAC
		public int SetMemory(uint offset, byte[] buffer)
		{
			return this.setProcessMemory(CCAPI.System.processID, (ulong)offset, (uint)buffer.Length, buffer);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public int SetMemory(ulong offset, byte[] buffer)
		{
			return this.setProcessMemory(CCAPI.System.processID, offset, (uint)buffer.Length, buffer);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002BFC File Offset: 0x00000DFC
		public int SetMemory(ulong offset, string hexadecimal, EndianType Type = EndianType.BigEndian)
		{
			byte[] array = CCAPI.StringToByteArray(hexadecimal);
			bool flag = Type == EndianType.LittleEndian;
			if (flag)
			{
				Array.Reverse(array);
			}
			return this.setProcessMemory(CCAPI.System.processID, offset, (uint)array.Length, array);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002C3C File Offset: 0x00000E3C
		public int GetMemory(uint offset, byte[] buffer)
		{
			return this.getProcessMemory(CCAPI.System.processID, (ulong)offset, (uint)buffer.Length, buffer);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C64 File Offset: 0x00000E64
		public int GetMemory(ulong offset, byte[] buffer)
		{
			return this.getProcessMemory(CCAPI.System.processID, offset, (uint)buffer.Length, buffer);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C8C File Offset: 0x00000E8C
		public byte[] GetBytes(uint offset, uint length)
		{
			byte[] array = new byte[length];
			this.GetMemory(offset, array);
			return array;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public byte[] GetBytes(ulong offset, uint length)
		{
			byte[] array = new byte[length];
			this.GetMemory(offset, array);
			return array;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public int Notify(CCAPI.NotifyIcon icon, string message)
		{
			return this.notify((int)icon, message);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public int Notify(int icon, string message)
		{
			return this.notify(icon, message);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D14 File Offset: 0x00000F14
		public int ShutDown(CCAPI.RebootFlags flag)
		{
			return this.shutdown((int)flag);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D34 File Offset: 0x00000F34
		public int RingBuzzer(CCAPI.BuzzerMode flag)
		{
			return this.ringBuzzer((int)flag);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D54 File Offset: 0x00000F54
		public int SetConsoleLed(CCAPI.LedColor color, CCAPI.LedMode mode)
		{
			return this.setConsoleLed((int)color, (int)mode);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002D74 File Offset: 0x00000F74
		private int GetTargetInfo()
		{
			int[] array = new int[2];
			int fw = 0;
			int ccapi = 0;
			int consoleType = 0;
			ulong sysTable = 0UL;
			int num = this.getFirmwareInfo(ref fw, ref ccapi, ref consoleType);
			bool flag = num >= 0;
			if (flag)
			{
				num = this.getTemperature(ref array[0], ref array[1]);
				bool flag2 = num >= 0;
				if (flag2)
				{
					this.CompleteInfo(ref this.pInfo, fw, ccapi, sysTable, consoleType, array[0], array[1]);
				}
			}
			return num;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002E04 File Offset: 0x00001004
		public int GetTargetInfo(out CCAPI.TargetInfo Info)
		{
			Info = new CCAPI.TargetInfo();
			int[] array = new int[2];
			int fw = 0;
			int ccapi = 0;
			int consoleType = 0;
			ulong sysTable = 0UL;
			int num = this.getFirmwareInfo(ref fw, ref ccapi, ref consoleType);
			bool flag = num >= 0;
			if (flag)
			{
				num = this.getTemperature(ref array[0], ref array[1]);
				bool flag2 = num >= 0;
				if (flag2)
				{
					this.CompleteInfo(ref Info, fw, ccapi, sysTable, consoleType, array[0], array[1]);
					this.CompleteInfo(ref this.pInfo, fw, ccapi, sysTable, consoleType, array[0], array[1]);
				}
			}
			return num;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002EAC File Offset: 0x000010AC
		public string GetFirmwareVersion()
		{
			bool flag = this.pInfo.Firmware == 0;
			if (flag)
			{
				this.GetTargetInfo();
			}
			string text = this.pInfo.Firmware.ToString("X8");
			string str = text.Substring(1, 1) + ".";
			string str2 = text.Substring(3, 1);
			string str3 = text.Substring(4, 1);
			return str + str2 + str3;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002F20 File Offset: 0x00001120
		public string GetTemperatureCELL()
		{
			bool flag = this.pInfo.TempCell == 0;
			if (flag)
			{
				this.GetTargetInfo(out this.pInfo);
			}
			return this.pInfo.TempCell.ToString() + " C";
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F70 File Offset: 0x00001170
		public string GetTemperatureRSX()
		{
			bool flag = this.pInfo.TempRSX == 0;
			if (flag)
			{
				this.GetTargetInfo(out this.pInfo);
			}
			return this.pInfo.TempRSX.ToString() + " C";
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002FC0 File Offset: 0x000011C0
		public string GetFirmwareType()
		{
			bool flag = this.pInfo.ConsoleType == 0;
			if (flag)
			{
				this.GetTargetInfo(out this.pInfo);
			}
			string result = "UNK";
			bool flag2 = this.pInfo.ConsoleType == 1;
			if (flag2)
			{
				result = "CEX";
			}
			else
			{
				bool flag3 = this.pInfo.ConsoleType == 2;
				if (flag3)
				{
					result = "DEX";
				}
				else
				{
					bool flag4 = this.pInfo.ConsoleType == 3;
					if (flag4)
					{
						result = "TOOL";
					}
				}
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003050 File Offset: 0x00001250
		public void ClearTargetInfo()
		{
			this.pInfo = new CCAPI.TargetInfo();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003060 File Offset: 0x00001260
		public int SetConsoleID(string consoleID)
		{
			bool flag = string.IsNullOrEmpty(consoleID);
			int result;
			if (flag)
			{
				MessageBox.Show("Cannot send an empty value", "Empty or null console id", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				result = -1;
			}
			else
			{
				string hex = string.Empty;
				bool flag2 = consoleID.Length >= 32;
				if (flag2)
				{
					hex = consoleID.Substring(0, 32);
				}
				result = this.SetConsoleID(CCAPI.StringToByteArray(hex));
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000030CC File Offset: 0x000012CC
		public int SetConsoleID(byte[] consoleID)
		{
			bool flag = consoleID.Length == 0;
			int result;
			if (flag)
			{
				MessageBox.Show("Cannot send an empty value", "Empty or null console id", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				result = -1;
			}
			else
			{
				result = this.setConsoleIds(0, consoleID);
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003114 File Offset: 0x00001314
		public int SetPSID(string PSID)
		{
			bool flag = string.IsNullOrEmpty(PSID);
			int result;
			if (flag)
			{
				MessageBox.Show("Cannot send an empty value", "Empty or null psid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				result = -1;
			}
			else
			{
				string hex = string.Empty;
				bool flag2 = PSID.Length >= 32;
				if (flag2)
				{
					hex = PSID.Substring(0, 32);
				}
				result = this.SetPSID(CCAPI.StringToByteArray(hex));
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003180 File Offset: 0x00001380
		public int SetPSID(byte[] consoleID)
		{
			bool flag = consoleID.Length == 0;
			int result;
			if (flag)
			{
				MessageBox.Show("Cannot send an empty value", "Empty or null psid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				result = -1;
			}
			else
			{
				result = this.setConsoleIds(1, consoleID);
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000031C8 File Offset: 0x000013C8
		public int SetBootConsoleID(string consoleID, CCAPI.IdType Type = CCAPI.IdType.IDPS)
		{
			string text = string.Empty;
			bool flag = consoleID.Length >= 32;
			if (flag)
			{
				text = consoleID.Substring(0, 32);
			}
			return this.SetBootConsoleID(CCAPI.StringToByteArray(consoleID), Type);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000320C File Offset: 0x0000140C
		public int SetBootConsoleID(byte[] consoleID, CCAPI.IdType Type = CCAPI.IdType.IDPS)
		{
			return this.setBootConsoleIds((int)Type, 1, consoleID);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000322C File Offset: 0x0000142C
		public int ResetBootConsoleID(CCAPI.IdType Type = CCAPI.IdType.IDPS)
		{
			return this.setBootConsoleIds((int)Type, 0, null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000324C File Offset: 0x0000144C
		public int GetDllVersion()
		{
			return this.getDllVersion();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000326C File Offset: 0x0000146C
		public List<CCAPI.ConsoleInfo> GetConsoleList()
		{
			List<CCAPI.ConsoleInfo> list = new List<CCAPI.ConsoleInfo>();
			int num = this.getNumberOfConsoles();
			IntPtr intPtr = Marshal.AllocHGlobal(512);
			IntPtr intPtr2 = Marshal.AllocHGlobal(512);
			for (int i = 0; i < num; i++)
			{
				CCAPI.ConsoleInfo consoleInfo = new CCAPI.ConsoleInfo();
				this.getConsoleInfo(i, intPtr, intPtr2);
				consoleInfo.Name = Marshal.PtrToStringAnsi(intPtr);
				consoleInfo.Ip = Marshal.PtrToStringAnsi(intPtr2);
				list.Add(consoleInfo);
			}
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr2);
			return list;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000330C File Offset: 0x0000150C
		internal static byte[] StringToByteArray(string hex)
		{
			byte[] result;
			try
			{
				string replace = hex.Replace("0x", "");
				string Stringz = replace.Insert(replace.Length - 1, "0");
				int length = replace.Length;
				bool flag = length % 2 == 0;
				bool flag2 = flag;
				if (flag2)
				{
					result = (from x in Enumerable.Range(0, replace.Length)
							  where x % 2 == 0
							  select Convert.ToByte(replace.Substring(x, 2), 16)).ToArray<byte>();
				}
				else
				{
					result = (from x in Enumerable.Range(0, replace.Length)
							  where x % 2 == 0
							  select Convert.ToByte(Stringz.Substring(x, 2), 16)).ToArray<byte>();
				}
			}
			catch
			{
				MessageBox.Show("Incorrect value (empty)", "StringToByteArray Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				result = new byte[1];
			}
			return result;
		}

		// Token: 0x04000003 RID: 3
		private CCAPI.connectConsoleDelegate connectConsole;

		// Token: 0x04000004 RID: 4
		private CCAPI.disconnectConsoleDelegate disconnectConsole;

		// Token: 0x04000005 RID: 5
		private CCAPI.getConnectionStatusDelegate getConnectionStatus;

		// Token: 0x04000006 RID: 6
		private CCAPI.getConsoleInfoDelegate getConsoleInfo;

		// Token: 0x04000007 RID: 7
		private CCAPI.getDllVersionDelegate getDllVersion;

		// Token: 0x04000008 RID: 8
		private CCAPI.getFirmwareInfoDelegate getFirmwareInfo;

		// Token: 0x04000009 RID: 9
		private CCAPI.getNumberOfConsolesDelegate getNumberOfConsoles;

		// Token: 0x0400000A RID: 10
		private CCAPI.getProcessListDelegate getProcessList;

		// Token: 0x0400000B RID: 11
		private CCAPI.getProcessMemoryDelegate getProcessMemory;

		// Token: 0x0400000C RID: 12
		private CCAPI.getProcessNameDelegate getProcessName;

		// Token: 0x0400000D RID: 13
		private CCAPI.getTemperatureDelegate getTemperature;

		// Token: 0x0400000E RID: 14
		private CCAPI.notifyDelegate notify;

		// Token: 0x0400000F RID: 15
		private CCAPI.ringBuzzerDelegate ringBuzzer;

		// Token: 0x04000010 RID: 16
		private CCAPI.setBootConsoleIdsDelegate setBootConsoleIds;

		// Token: 0x04000011 RID: 17
		private CCAPI.setConsoleIdsDelegate setConsoleIds;

		// Token: 0x04000012 RID: 18
		private CCAPI.setConsoleLedDelegate setConsoleLed;

		// Token: 0x04000013 RID: 19
		private CCAPI.setProcessMemoryDelegate setProcessMemory;

		// Token: 0x04000014 RID: 20
		private CCAPI.shutdownDelegate shutdown;

		// Token: 0x04000015 RID: 21
		private IntPtr libModule = IntPtr.Zero;

		// Token: 0x04000016 RID: 22
		private List<IntPtr> CCAPIFunctionsList = new List<IntPtr>();

		// Token: 0x04000017 RID: 23
		private CCAPI.TargetInfo pInfo = new CCAPI.TargetInfo();

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x0600013E RID: 318
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int connectConsoleDelegate(string targetIP);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000142 RID: 322
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int disconnectConsoleDelegate();

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x06000146 RID: 326
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getConnectionStatusDelegate(ref int status);

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x0600014A RID: 330
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getConsoleInfoDelegate(int index, IntPtr ptrN, IntPtr ptrI);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x0600014E RID: 334
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getDllVersionDelegate();

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000152 RID: 338
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getFirmwareInfoDelegate(ref int firmware, ref int ccapi, ref int consoleType);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x06000156 RID: 342
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getNumberOfConsolesDelegate();

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x0600015A RID: 346
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getProcessListDelegate(ref uint numberProcesses, IntPtr processIdPtr);

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x0600015E RID: 350
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getProcessMemoryDelegate(uint processID, ulong offset, uint size, byte[] buffOut);

		// Token: 0x0200001B RID: 27
		// (Invoke) Token: 0x06000162 RID: 354
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getProcessNameDelegate(uint processID, IntPtr strPtr);

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x06000166 RID: 358
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int getTemperatureDelegate(ref int cell, ref int rsx);

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x0600016A RID: 362
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int notifyDelegate(int mode, string msgWChar);

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x0600016E RID: 366
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int ringBuzzerDelegate(int type);

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x06000172 RID: 370
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int setBootConsoleIdsDelegate(int idType, int on, byte[] ID);

		// Token: 0x02000020 RID: 32
		// (Invoke) Token: 0x06000176 RID: 374
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int setConsoleIdsDelegate(int idType, byte[] consoleID);

		// Token: 0x02000021 RID: 33
		// (Invoke) Token: 0x0600017A RID: 378
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int setConsoleLedDelegate(int color, int status);

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x0600017E RID: 382
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int setProcessMemoryDelegate(uint processID, ulong offset, uint size, byte[] buffIn);

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x06000182 RID: 386
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int shutdownDelegate(int mode);

		// Token: 0x02000024 RID: 36
		private enum CCAPIFunctions
		{
			// Token: 0x040000DF RID: 223
			ConnectConsole,
			// Token: 0x040000E0 RID: 224
			DisconnectConsole,
			// Token: 0x040000E1 RID: 225
			GetConnectionStatus,
			// Token: 0x040000E2 RID: 226
			GetConsoleInfo,
			// Token: 0x040000E3 RID: 227
			GetDllVersion,
			// Token: 0x040000E4 RID: 228
			GetFirmwareInfo,
			// Token: 0x040000E5 RID: 229
			GetNumberOfConsoles,
			// Token: 0x040000E6 RID: 230
			GetProcessList,
			// Token: 0x040000E7 RID: 231
			GetMemory,
			// Token: 0x040000E8 RID: 232
			GetProcessName,
			// Token: 0x040000E9 RID: 233
			GetTemperature,
			// Token: 0x040000EA RID: 234
			VshNotify,
			// Token: 0x040000EB RID: 235
			RingBuzzer,
			// Token: 0x040000EC RID: 236
			SetBootConsoleIds,
			// Token: 0x040000ED RID: 237
			SetConsoleIds,
			// Token: 0x040000EE RID: 238
			SetConsoleLed,
			// Token: 0x040000EF RID: 239
			SetMemory,
			// Token: 0x040000F0 RID: 240
			ShutDown
		}

		// Token: 0x02000025 RID: 37
		public enum IdType
		{
			// Token: 0x040000F2 RID: 242
			IDPS,
			// Token: 0x040000F3 RID: 243
			PSID
		}

		// Token: 0x02000026 RID: 38
		public enum NotifyIcon
		{
			// Token: 0x040000F5 RID: 245
			INFO,
			// Token: 0x040000F6 RID: 246
			CAUTION,
			// Token: 0x040000F7 RID: 247
			FRIEND,
			// Token: 0x040000F8 RID: 248
			SLIDER,
			// Token: 0x040000F9 RID: 249
			WRONGWAY,
			// Token: 0x040000FA RID: 250
			DIALOG,
			// Token: 0x040000FB RID: 251
			DIALOGSHADOW,
			// Token: 0x040000FC RID: 252
			TEXT,
			// Token: 0x040000FD RID: 253
			POINTER,
			// Token: 0x040000FE RID: 254
			GRAB,
			// Token: 0x040000FF RID: 255
			HAND,
			// Token: 0x04000100 RID: 256
			PEN,
			// Token: 0x04000101 RID: 257
			FINGER,
			// Token: 0x04000102 RID: 258
			ARROW,
			// Token: 0x04000103 RID: 259
			ARROWRIGHT,
			// Token: 0x04000104 RID: 260
			PROGRESS,
			// Token: 0x04000105 RID: 261
			TROPHY1,
			// Token: 0x04000106 RID: 262
			TROPHY2,
			// Token: 0x04000107 RID: 263
			TROPHY3,
			// Token: 0x04000108 RID: 264
			TROPHY4
		}

		// Token: 0x02000027 RID: 39
		public enum ConsoleType
		{
			// Token: 0x0400010A RID: 266
			CEX = 1,
			// Token: 0x0400010B RID: 267
			DEX,
			// Token: 0x0400010C RID: 268
			TOOL
		}

		// Token: 0x02000028 RID: 40
		public enum ProcessType
		{
			// Token: 0x0400010E RID: 270
			VSH,
			// Token: 0x0400010F RID: 271
			SYS_AGENT,
			// Token: 0x04000110 RID: 272
			CURRENTGAME
		}

		// Token: 0x02000029 RID: 41
		public enum RebootFlags
		{
			// Token: 0x04000112 RID: 274
			ShutDown = 1,
			// Token: 0x04000113 RID: 275
			SoftReboot,
			// Token: 0x04000114 RID: 276
			HardReboot
		}

		// Token: 0x0200002A RID: 42
		public enum BuzzerMode
		{
			// Token: 0x04000116 RID: 278
			Continuous,
			// Token: 0x04000117 RID: 279
			Single,
			// Token: 0x04000118 RID: 280
			Double,
			// Token: 0x04000119 RID: 281
			Triple
		}

		// Token: 0x0200002B RID: 43
		public enum LedColor
		{
			// Token: 0x0400011B RID: 283
			Green = 1,
			// Token: 0x0400011C RID: 284
			Red
		}

		// Token: 0x0200002C RID: 44
		public enum LedMode
		{
			// Token: 0x0400011E RID: 286
			Off,
			// Token: 0x0400011F RID: 287
			On,
			// Token: 0x04000120 RID: 288
			Blink
		}

		// Token: 0x0200002D RID: 45
		private class System
		{
			// Token: 0x04000121 RID: 289
			public static int connectionID = -1;

			// Token: 0x04000122 RID: 290
			public static uint processID = 0U;

			// Token: 0x04000123 RID: 291
			public static uint[] processIDs;
		}

		// Token: 0x0200002E RID: 46
		public class TargetInfo
		{
			// Token: 0x04000124 RID: 292
			public int Firmware = 0;

			// Token: 0x04000125 RID: 293
			public int CCAPI = 0;

			// Token: 0x04000126 RID: 294
			public int ConsoleType = 0;

			// Token: 0x04000127 RID: 295
			public int TempCell = 0;

			// Token: 0x04000128 RID: 296
			public int TempRSX = 0;

			// Token: 0x04000129 RID: 297
			public ulong SysTable = 0UL;
		}

		// Token: 0x0200002F RID: 47
		public class ConsoleInfo
		{
			// Token: 0x0400012A RID: 298
			public string Name;

			// Token: 0x0400012B RID: 299
			public string Ip;
		}
	}
}
