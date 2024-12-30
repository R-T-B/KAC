using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KAC
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class KAC : MonoBehaviour
    {
        public static bool patched;
        private void Awake()
        {
            String dllPath = "";
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                dllPath = KSPUtil.ApplicationRootPath + "KSP_x64_Data/Managed/Assembly-CSharp.dll";
            }
            else if (Application.platform == RuntimePlatform.OSXPlayer)
            {
                dllPath = KSPUtil.ApplicationRootPath + "KSP.app/Contents/Resources/Data/Managed/Assembly-CSharp.dll";
            }
            else if (Application.platform == RuntimePlatform.LinuxPlayer)
            {
                dllPath = KSPUtil.ApplicationRootPath + "KSP_Data/Managed/Assembly-CSharp.dll";
            }
            if (File.Exists(dllPath))
            {
                Byte[] data = File.ReadAllBytes(dllPath);
                if (data.Length >= 10485760)
                {
                    if ((!KAC.patched) && (Application.platform == RuntimePlatform.WindowsPlayer))
                    {
                        new Process
                        {
                            StartInfo =
                    {
                        UseShellExecute = true,
                        FileName = "autoexec_win.cmd",
                        CreateNoWindow = true,
                        WorkingDirectory = KSPUtil.ApplicationRootPath + "/GameData/KAC/"
                    }
                        }.Start();
                        KAC.patched = true;
                    }
                    else if ((!KAC.patched) && (Application.platform == RuntimePlatform.OSXPlayer))
                    {
                        new Process
                        {
                            StartInfo =
                    {
                        UseShellExecute = true,
                        FileName = "autoexec_mac.sh",
                        CreateNoWindow = true,
                        WorkingDirectory = KSPUtil.ApplicationRootPath + "/GameData/KAC/"
                    }
                        }.Start();
                        KAC.patched = true;
                    }
                    else if ((!KAC.patched) && (Application.platform == RuntimePlatform.LinuxPlayer))
                    {
                        new Process
                        {
                            StartInfo =
                    {
                        UseShellExecute = true,
                        FileName = "autoexec_lin.sh",
                        CreateNoWindow = true,
                        WorkingDirectory = KSPUtil.ApplicationRootPath + "/GameData/KAC/"
                    }
                        }.Start();
                        KAC.patched = true;
                    }
                    PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), "KAC Patcher", "KAC Patcher", "Assembly-CSharp.dll patched!  We also made a .bak backup of your original.  Please quit and relaunch for it to work!", "OK", true, UISkinManager.defaultSkin);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
