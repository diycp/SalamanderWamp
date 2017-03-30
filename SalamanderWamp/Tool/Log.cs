﻿using SalamanderWamp.UI;
using SalamanderWnmp.UI;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SalamanderWamp.Tool
{
    /// <summary>
    /// Logs information and errors to a RichTextBox
    /// </summary>
    public static class Log
    {
        private static RichTextBox rtfLog;

        public enum LogSection
        {
            WNMP_MAIN = 0,
            WNMP_APACHE,
            WNMP_MARIADB,
        }
        public static string LogSectionToString(LogSection logSection)
        {
            switch (logSection) {
                case LogSection.WNMP_MAIN:
                    return "Wnmp Main";
                case LogSection.WNMP_APACHE:
                    return "Wnmp Apache";
                case LogSection.WNMP_MARIADB:
                    return "Wnmp Mysql";
                default:
                    return "";
            }
    }

        private static void wnmp_log(string message, SolidColorBrush brush, LogSection logSection)
        {
            DispatcherHelper.UIDispatcher.Invoke(new Action(() =>
            {
                var SectionName = LogSectionToString(logSection);
                var DateNow = DateTime.Now.ToString();
                Run run1 = new Run(DateNow + " [");
                Run run2 = new Run() { Text = SectionName, Foreground = brush };
                Run run3 = new Run("] - " + message);
                var p = new Paragraph();
                p.Inlines.AddRange(new Run[] { run1, run2, run3 });
                rtfLog.Document.Blocks.Add(p);
                rtfLog.ScrollToEnd();
            }));
        }
        /// <summary>
        /// Log error
        /// </summary>
        public static void wnmp_log_error(string message, LogSection logSection)
        {
            wnmp_log(message, Brushes.Red, logSection);
        }
        /// <summary>
        /// Log information
        /// </summary>
        public static void wnmp_log_notice(string message, LogSection logSection)
        {
            wnmp_log(message, Brushes.DarkBlue, logSection);
        }

        public static void setLogComponent(RichTextBox logRichTextBox)
        {
            rtfLog = logRichTextBox;            
            wnmp_log_notice("Initializing Control Panel", LogSection.WNMP_MAIN);
            wnmp_log_notice("Control Panel Version: " + Constants.CPVER, LogSection.WNMP_MAIN);
            wnmp_log_notice("Wnmp Version: " + Constants.APPVER, LogSection.WNMP_MAIN);
            wnmp_log_notice("Wnmp Directory: " + MainWin.StartupPath, LogSection.WNMP_MAIN);
        }
    }
}
