using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JustTag.Controls.FileBrowser
{
    /// <summary>
    /// Логика взаимодействия для ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window
    {

        public ProgressBar()
        {
            InitializeComponent();
        }

        public void UpdateValue(int value)
        {
            Pbar.Dispatcher.Invoke(() => Pbar.Value = value, DispatcherPriority.Background);
        }

        public void Start()
        {
            Thread thr1 = new Thread(UpdValueCycle);
            thr1.Start();
        }

        public void UpdValueCycle(object sender)
        {
            for (int i = 0; i < 100; i++)
            {
                // Thread.Sleep(10);
                UpdateValue(i);
            }
            Pbar.Dispatcher.Invoke(() => this.Hide(), DispatcherPriority.Background);
        }
    }
}
