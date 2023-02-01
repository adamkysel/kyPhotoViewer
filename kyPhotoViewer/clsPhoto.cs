using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace kyPhotoViewer
{
    internal class clsPhoto
    {
        public string ActualPhotoPath  // property
        { get; set; }
        public string ActualSourceFolder  // property
        { get; set; }

        public enum enumPhotoPath
        {
            eNext,
            ePrevious
        }
        

        public void setParameters(string photoPath, string sourceFolder)
        {
            this.ActualPhotoPath = photoPath;
            this.ActualSourceFolder = sourceFolder;
        }

        public BitmapImage preparePhoto(string filePath)
        {
            string localPosition = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            BitmapImage bitmap = new BitmapImage();

            try
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath);
                bitmap.EndInit();
            }
            catch(Exception ex)
            {
                clsLogger.writeLog(ex.Message, localPosition, clsLogger.enumLogType.eError);
            }

            return bitmap;         
        }

        public bool savePhoto(string destinationFolder)
        {
            string localPosition = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            bool savePhoto = false;
            string savedPhotoPath = destinationFolder + "/" + System.IO.Path.GetFileName(this.ActualPhotoPath);
            try
            {
                if(System.IO.File.Exists(this.ActualPhotoPath))
                {
                    if (System.IO.Directory.Exists(destinationFolder))
                    {
                        System.IO.File.Copy(this.ActualPhotoPath, savedPhotoPath);
                        if (System.IO.File.Exists(savedPhotoPath))
                        {
                            savePhoto = true;
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Priecinok, kde chces fotku ulozit neexistuje.");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Fotka, ktoru chces ulozit neexistuje.");
                }
            }
            catch(Exception ex)
            {
                clsLogger.writeLog(ex.Message, localPosition, clsLogger.enumLogType.eError);
            }

            return savePhoto;

        }

        public bool deletePhoto()
        {
            string localPosition = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            bool deletePhoto = false;
            try
            {
                if (System.IO.File.Exists(this.ActualPhotoPath))
                {
                    System.IO.File.Delete(this.ActualPhotoPath);
                   
                    if (System.IO.File.Exists(this.ActualPhotoPath))
                    {
                        deletePhoto = true;
                    }
                   
                }
                else
                {
                    System.Windows.MessageBox.Show("Fotka, ktoru chces vymazat neexistuje.");
                }
            }
            catch (Exception ex)
            {
                clsLogger.writeLog(ex.Message, localPosition, clsLogger.enumLogType.eError);
            }

            return deletePhoto;

        }

        public string getNext_Previous_PhotoPath(enumPhotoPath ePhotoPath)
        {
            string localPosition = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string nextPath = "";
            string[] files;
            int actualIndex;

            try
            {
                if (Directory.Exists(this.ActualSourceFolder) && System.IO.File.Exists(this.ActualPhotoPath))
                {
                    files = Directory.GetFiles(this.ActualSourceFolder);
                    actualIndex = Array.IndexOf(files, this.ActualPhotoPath);
                    if (ePhotoPath == enumPhotoPath.ePrevious)
                    {
                        if (actualIndex == 0)
                        {
                            nextPath = files[files.Length - 1];
                        }
                        else
                        {
                            nextPath = files[actualIndex - 1];
                        }
                    }
                    if (ePhotoPath == enumPhotoPath.eNext)
                    {
                        if (actualIndex == files.Length - 1)
                        {
                            nextPath = files[0];
                        }
                        else
                        {
                            nextPath = files[actualIndex + 1];
                        }

                    }
                    this.ActualPhotoPath = nextPath;
                }
                else
                {
                    System.Windows.MessageBox.Show("Zdrojový priečinok alebo aktuálna fotka neexistuje. Vyber fotku, ktora sa ma zobrazit.");
                }

            }    
            catch (Exception ex)
            {                
                clsLogger.writeLog(ex.Message, localPosition, clsLogger.enumLogType.eError);
            }

            return nextPath;

        }
    }
}
