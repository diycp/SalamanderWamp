﻿using System;
using System.ServiceProcess;

namespace SalamanderWamp.Programs
{
    class MysqlProgram : WampProgram
    {
        private readonly ServiceController mysqlController = new ServiceController();
        public const string ServiceName = "mysql-salamander-wamp";

        public MysqlProgram()
        {
            mysqlController.MachineName = Environment.MachineName;
            mysqlController.ServiceName = ServiceName;
        }

        public void RemoveService()
        {
            StartProcess("cmd.exe", stopArgs, true);
        }

        public void InstallService()
        {
            StartProcess(exeName, startArgs, true);
        }

        public bool ServiceExists()
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (var service in services) {
                if (service.ServiceName == ServiceName)
                    return true;
            }
            return false;
        }

        public override void Start()
        {
            try
            {
                if (IsRunning())
                    return;
            }
            catch (Exception ex)
            {
                Log.wnmp_log_notice("You need to be the administrator to Start Mysql Service", progLogSection);
            }
            try {
                mysqlController.Start();
                mysqlController.WaitForStatus(ServiceControllerStatus.Running);
                Log.wnmp_log_notice("Started " + progName, progLogSection);
            } catch (Exception ex) {
                Log.wnmp_log_error("Start(): " + ex.Message, progLogSection);
            }
        }

        public override void Stop()
        {
            if(isStopped())
            {
                return;
            }
            try {
                mysqlController.Stop();
                mysqlController.WaitForStatus(ServiceControllerStatus.Stopped);
                Log.wnmp_log_notice("Stopped " + progName, progLogSection);
            } catch (Exception ex) {
                Log.wnmp_log_notice("Stop(): " + ex.Message, progLogSection);
            }
        }

         /// <summary>
        /// 通过ServiceController判断服务是否在运行
        /// </summary>
        /// <returns></returns>
        public override bool IsRunning()
        {
            return mysqlController.Status == ServiceControllerStatus.Running;
        }

        /// <summary>
        /// 通过ServiceController判断服务是否停止
        /// </summary>
        /// <returns></returns>
        private bool isStopped()
        {
            return mysqlController.Status == ServiceControllerStatus.Stopped;
        }

    }
}
