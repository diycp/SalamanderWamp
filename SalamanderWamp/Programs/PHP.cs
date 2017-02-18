﻿using System;

using SalamanderWamp.UI;

namespace SalamanderWamp.Programs
{
    class PHPProgram : WampProgram
    {
        public PHPProgram()
        {
            ps.StartInfo.EnvironmentVariables.Add("PHP_FCGI_MAX_REQUESTS", "0"); // Disable auto killing PHP
        }

        private string GetPHPIniPath()
        {
            return MainWindow.StartupPath + "/" + Settings.PHPDirName.Value + "/php.ini";
        }


        public override void Start()
        {
            if(this.IsRunning())
            {
                return;
            }
            uint ProcessCount = Settings.PHP_Processes.Value;
            short port = Settings.PHP_Port.Value;
            string phpini = GetPHPIniPath();

            try {
                for (var i = 1; i <= ProcessCount; i++) {
                    StartProcess(exeName, String.Format("-b localhost:{0} -c {1}", port, phpini));
                    Log.wnmp_log_notice("Starting PHP " + i + "/" + ProcessCount + " on port: " + port, progLogSection);
                    port++;
                }
                if(String.IsNullOrEmpty(errOutput))
                {
                    Log.wnmp_log_notice("Started", progLogSection);
                }
                else
                {
                    Log.wnmp_log_error("Start(): " + errOutput, progLogSection);
                }
            } catch (Exception ex) {
                Log.wnmp_log_error("Start(): " + ex.Message, progLogSection);
            }
        }

    }
}
