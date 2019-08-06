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
    /// Логика взаимодействия для FileCreater.xaml
    /// </summary>
    public partial class FileCreater : Window
    {
        public FileCreater()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Exit button
        {
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Create button
        {
            if(FileNameTXTBox.Text == "")
            {
                Utils.WriteConsole("Имя файла не указано!");
                return;
            }
            try
            {
                if (File.Exists(Directory.GetCurrentDirectory() + @"\" + FileNameTXTBox.Text))
                {
                    Utils.WriteConsole("Файл с таким именем уже существует!");
                    return;
                }
                File.Create(Directory.GetCurrentDirectory() + @"\" + FileNameTXTBox.Text).Close();
                (Application.Current.MainWindow as JustTag.Pages.MainWindow).fileBrowser.RefreshCurrentDirectory();
                Utils.WriteFileLog("create", Directory.GetCurrentDirectory() + @"\" + FileNameTXTBox.Text);
                
            }
            catch(Exception e1)
            {
                Utils.WriteConsole(e1.Message, true);
            }
            
        }
    }
}
