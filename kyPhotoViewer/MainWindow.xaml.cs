using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        public MainWindow()
        {
            InitializeComponent();
            clsPhoto photo = new clsPhoto();

        }

        private void btnOpenImage_Click(object sender, RoutedEventArgs e)
        {
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
                    actualSourceFolder = System.IO.Path.GetDirectoryName(selectedFileName);
                    BitmapImage bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedFileName);
                    bitmap.EndInit();

                    imMain.Source = bitmap;
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSelectfolder_Click(object sender, RoutedEventArgs e)
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
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void btnCopyToFolder_Click(object sender, RoutedEventArgs e)
        {
            System.IO.File.Copy(actualPhotoPath, actualDestinationFolder + "/" + System.IO.Path.GetFileName(actualPhotoPath));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            System.IO.File.Delete(actualPhotoPath);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(actualSourceFolder);
            int actualIndex = Array.IndexOf(files, actualSourceFolder);


        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
