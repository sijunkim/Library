using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library.Tools
{
    public class LogManager
    {
        private string path;

        #region Constructors
        public LogManager(string path)
        {
            this.path = path;
            SetLogPath();
        }

        public LogManager()
            : this(Path.Combine(Application.Root, "Log"))
        {
        }
        #endregion

        #region Methods
        private void SetLogPath()
        {
            string logFile = string.Empty;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            logFile = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            path = Path.Combine(path, logFile);
        }

        public void Write(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
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
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss\t") + data);
                }
            }
            catch { }
        }
        #endregion
    }
}
