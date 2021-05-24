using Klepach.Core.VHDV.Client.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klepach.Core.VHDV.Client
{
    class CommonFunctions
    {
        #region Settings
        /// <summary>
        /// Settings the window size save.
        /// </summary>
        /// <param name="WindowForm">The window form.</param>
        public static void SettingWindowSizeSave(Form WindowForm, bool directSave)
        {
            Settings.Default[WindowForm.Name + "_WindowSize"] = $"{WindowForm.Width},{WindowForm.Height},{WindowForm.Left},{WindowForm.Top}";
            Settings.Default.Save();
        }
        /// <summary>
        /// Settings the window size load.
        /// </summary>
        /// <param name="WindowForm">The window form.</param>
        public static void SettingWindowSizeLoad(Form WindowForm)
        {
            var windowSize = Settings.Default[WindowForm.Name + "_WindowSize"] as string;
            if (string.IsNullOrEmpty(windowSize))
                return;
            var windowSizeDims = windowSize.Split(",");
            WindowForm.Width = int.Parse(windowSizeDims[0]);
            WindowForm.Height = int.Parse(windowSizeDims[1]);
            WindowForm.Left = int.Parse(windowSizeDims[2]);
            WindowForm.Top = int.Parse(windowSizeDims[3]);
        }
        #endregion
    }
}
