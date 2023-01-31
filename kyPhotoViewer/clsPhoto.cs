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
            BitmapImage bitmap = new BitmapImage();

            try
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath);
                bitmap.EndInit();
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            return bitmap;         
        }

        public bool savePhoto(string filePath)
        {
            bool savePhoto = false;
            System.IO.File.Copy(this.ActualPhotoPath, this.ActualSourceFolder + "/" + System.IO.Path.GetFileName(this.ActualPhotoPath));

            return savePhoto;

        }

        public string getNext_Previous_PhotoPath(enumPhotoPath ePhotoPath)
        {
            string localPosition = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string nextPath = "";
            string[] files;
            int actualIndex;

            try
            {
                if (Directory.Exists(this.ActualSourceFolder) && Directory.Exists(this.ActualPhotoPath))
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
                    System.Windows.MessageBox.Show("Zdrojový priečinok alebo aktuálna fotka neexistuje.");
                }

            }    
            catch (Exception ex)
            {
                
                System.Windows.MessageBox.Show(ex.Message);
            }

            return nextPath;

        }
    }
}
