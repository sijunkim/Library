using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library.Tools
{
    public enum LogType { Daily, Monthly }

    public class LogManager
    {
        private string entirePath;

        #region Constructors
        public LogManager(string path, LogType logType, string prefix, string postfix)
        {
            this.entirePath = path;
            SetLogPath(logType, prefix, postfix);
        }

        public LogManager(string prefix, string postfix)
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, prefix, postfix)
        {

        }

        public LogManager()
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, null, null)
        {
        }
        #endregion

        #region Methods
        private void SetLogPath(LogType logType, string prefix, string postfix)
        {
            string path = string.Empty;
            string name = string.Empty;

            switch (logType)
            {
                case LogType.Daily:
                    path = string.Format(@"{0}\{1}\", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                    name = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    break;
                case LogType.Monthly:
                    path = string.Format(@"{0}\", DateTime.Now.Year);
                    name = DateTime.Now.ToString("yyyyMM") + ".txt";
                    break;
            }

            entirePath = Path.Combine(entirePath, path);

            if (!Directory.Exists(entirePath))
            {
                Directory.CreateDirectory(entirePath);
            }

            if (!String.IsNullOrEmpty(prefix))
            {
                name = prefix + name;
            }

            if (!String.IsNullOrEmpty(postfix))
            {
                name = name + postfix;
            }

            entirePath = Path.Combine(entirePath, name);
        }

        public void Write(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(entirePath, true))
                {
                    writer.Write(data);
                }
            }
            catch { }
        }

        public void WriteLine(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(entirePath, true))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss\t") + data);
                }
            }
            catch { }
        }
        #endregion
    }
}
