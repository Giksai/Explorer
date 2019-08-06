using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Text.RegularExpressions;
using JustTag.Controls;
using JustTag.Tagging;

namespace JustTag.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           // Directory.SetCurrentDirectory(@"C:\");
        }

        // Event handlers

        private void fullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            TaggedFilePath[] browsableFiles = fileBrowser.VisibleFiles;
            Fullscreen fullscreen = new Fullscreen(filePreviewer, browsableFiles, fileBrowser.SelectedIndex);
            Hide();
            fullscreen.ShowDialog();
            Show();
        }

        private async void fileBrowser_SelectedFileChanged(object sender, SelectionChangedEventArgs e)
        {
            // Don't do anything if the last preview hasn't loaded yet
            if (filePreviewer.IsOpening)
                return;

            // Don't do anything if selection is null
            if (fileBrowser.SelectedItem == null)
                return;

            // If the selected item is a shortcut, resolve it.
            TaggedFilePath selectedItem = fileBrowser.SelectedItem;
            if (selectedItem.Extension.ToLower() == ".lnk")
                selectedItem = Utils.GetShortcutTarget(selectedItem);

            // Show the file preview
            await filePreviewer.OpenPreview(selectedItem); //crashes

            // Enable the tag box and update it with this file's tags
            // NOTE: This affects the shortcut itself, not its target.  This is intentional.
            StringBuilder builder = new StringBuilder();
            foreach (string t in selectedItem.Tags)
                builder.AppendLine(t);

            // Hide the save button
        }



        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the settings window
            SettingsWindow w = new SettingsWindow();
            w.ShowDialog();
            
        }

        private async void findReplaceTagsButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the currently open file in case it needs to be renamed
            await filePreviewer.ClosePreview();

            // Show a window for finding/replacing
            var findReplaceWindow = new FindReplaceTagsWindow(Directory.GetCurrentDirectory(), Utils.allKnownTags);
            findReplaceWindow.ShowDialog();

            // Refresh the UI
            fileBrowser.RefreshCurrentDirectory();
        }

        private async void deleteTagButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the currently open file in case it needs to be renamed
            await filePreviewer.ClosePreview();

            // Show the window
            var toolWindow = new DeleteTagWindow(Directory.GetCurrentDirectory(), Utils.allKnownTags);
            toolWindow.ShowDialog();

            // Refresh the UI
            fileBrowser.RefreshCurrentDirectory();
        }



        /// <summary>
        /// Function that writes text to console
        /// </summary>
        /// <param name="index"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static void WriteConsole(string txt, bool isError)
        {
            if (isError)
                (Application.Current.MainWindow as JustTag.Pages.MainWindow).ConsoleTXT.Text += "ERROR on " + DateTime.Now.ToString() + " : ";
            (Application.Current.MainWindow as JustTag.Pages.MainWindow).ConsoleTXT.Text += txt + "\n";
        }



    }
}
