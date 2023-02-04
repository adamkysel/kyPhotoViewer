using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;




namespace kyPhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string actualPhotoPath = "";
        string actualDestinationFolder = "";
        string actualSourceFolder = "";
        clsPhoto photo = new clsPhoto();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenImage_Click(object sender, RoutedEventArgs e)
        {
            string localPosition = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = "c:\\";
                dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFileName = dlg.FileName;

                    actualPhotoPath = selectedFileName;
                    lblFileName.Content = selectedFileName;
                    lblFileName.Background = new SolidColorBrush(Colors.LightGreen);

                    actualSourceFolder = System.IO.Path.GetDirectoryName(selectedFileName);

                    photo.setParameters(actualPhotoPath, actualSourceFolder);

                    imMain.Source = photo.preparePhoto(selectedFileName);
                }
            }
            catch(Exception ex)
            {
                clsLogger.writeLog(ex.Message, localPosition, clsLogger.enumLogType.eError);
            }

        }

        private void btnSelectfolder_Click(object sender, RoutedEventArgs e)
        {
            string localPosition = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "---" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            try 
            {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;
                // Show the FolderBrowserDialog.  
                DialogResult result = folderDlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFolder = folderDlg.SelectedPath;
                    actualDestinationFolder = selectedFolder;
                    lblSelectedDestfolder.Content = selectedFolder;
                    lblSelectedDestfolder.Background = new SolidColorBrush(Colors.LightGreen);

                    Environment.SpecialFolder root = folderDlg.RootFolder;
                }
            }
            catch (Exception ex)
            {
                clsLogger.writeLog(ex.Message, localPosition, clsLogger.enumLogType.eError);
            }

        }

        private void btnCopyToFolder_Click(object sender, RoutedEventArgs e)
        {
            photo.savePhoto(actualDestinationFolder);
            //System.IO.File.Copy(actualPhotoPath, actualDestinationFolder + "/" + System.IO.Path.GetFileName(actualPhotoPath));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            photo.deletePhoto();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {                
            imMain.Source = photo.preparePhoto(photo.getNext_Previous_PhotoPath(clsPhoto.enumPhotoPath.eNext));            
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            imMain.Source = photo.preparePhoto(photo.getNext_Previous_PhotoPath(clsPhoto.enumPhotoPath.ePrevious));
        }

        private void btnRotate_Click(object sender, RoutedEventArgs e)
        {
            imMain.Source = photo.rotatePhoto();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                imMain.Source = photo.preparePhoto(photo.getNext_Previous_PhotoPath(clsPhoto.enumPhotoPath.ePrevious));
            }
            if (e.Key == Key.Right)
            {
                imMain.Source = photo.preparePhoto(photo.getNext_Previous_PhotoPath(clsPhoto.enumPhotoPath.eNext));
            }
            if (e.Key == Key.Tab)
            {
            }
        }

    }
}
