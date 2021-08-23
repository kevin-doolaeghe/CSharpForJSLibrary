using System;
using System.Diagnostics;

namespace AppLauncherLibrary {

    public class AppLauncher {
        public string LaunchApp(string program, string args)
        {
            string result = "No error";

            try
            {
                Process.Start(program, args);
            }
            catch (Exception e)
            {
                result = "Error: " + e.ToString();
            }

            return result;
        }
    }

}
