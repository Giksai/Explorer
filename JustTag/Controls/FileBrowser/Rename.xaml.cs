using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JustTag.Controls.FileBrowser
{
    /// <summary>
    /// Логика взаимодействия для Rename.xaml
    /// </summary>
    public partial class Rename : Window
    {
        public Rename()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (FileNameTXTBox.Text == "")
            {
                Utils.WriteConsole("Имя файла не указано!");
                return;
            }
            if((Application.Current.MainWindow as JustTag.Pages.MainWindow).fileBrowser.folderContentsBox.SelectedItem == null)
            {
                Utils.WriteConsole("Файл не выбран!");
                return;
            }
            try
            {               
                File.Move((Application.Current.MainWindow as JustTag.Pages.MainWindow).fileBrowser.folderContentsBox.SelectedItem.FullPath,
                   Directory.GetCurrentDirectory() + @"\" + FileNameTXTBox.Text);
                (Application.Current.MainWindow as JustTag.Pages.MainWindow).fileBrowser.RefreshCurrentDirectory();
                Utils.WriteFileLog("rename", (Application.Current.MainWindow as JustTag.Pages.MainWindow).fileBrowser.folderContentsBox.SelectedItem.FullPath);
            }
            catch (Exception e1)
            {
                Utils.WriteConsole(e1.Message, true);
            }
        }
    }
}
