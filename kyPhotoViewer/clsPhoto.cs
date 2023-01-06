using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace kyPhotoViewer
{
    internal class clsPhoto
    {
        string actualPhotoPath;
        string actualSourceFolder;

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
