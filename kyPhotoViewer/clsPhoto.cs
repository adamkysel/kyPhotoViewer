using System;
using System.Collections.Generic;
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
    }
}
