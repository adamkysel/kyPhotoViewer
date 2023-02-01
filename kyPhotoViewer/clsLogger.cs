using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace kyPhotoViewer
{
    internal class clsLogger
    {
        public enum enumLogType
        {
            eDebug,
            eError
        }
     
        public static void writeLog(string content, string localPosition, enumLogType logType)
        {
            try
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                string time = DateTime.Now.ToString("h:mm:ss.fff");
                string actualDirectoryName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string logFileName = dateTime.ToString("dd/MM/yyyy") + ".txt";
                string logPath = actualDirectoryName + @"\LOG\" + logFileName;
                if (System.IO.File.Exists(logPath) == false)
                {
                    System.IO.File.Create(logPath).Close();
                }
                // Appending the given texts
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    if (logType == enumLogType.eDebug)
                    {
                        sw.WriteLine(time + "   " + localPosition + "   " + content);
                    }
                    if (logType == enumLogType.eError)
                    {
                        sw.WriteLine(time + "   ERROR:   " + localPosition + "   " + content);
                        System.Windows.MessageBox.Show(localPosition + content);
                    }
                }
            }
            catch(Exception ex)
            {
                string localPositionS = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;
                System.Windows.MessageBox.Show(localPositionS + ex.Message);
            }


        }
     }
 }

