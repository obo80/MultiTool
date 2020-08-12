using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MultiTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModels.MainWindowViewModel MainWindow_VM;



        public MainWindow()
        {
            InitializeComponent();
            MainWindow_VM = new ViewModels.MainWindowViewModel();
            DataContext = MainWindow_VM;
            UpdateAllStatus();


        }
        #region List View methods

        private void ClearList2Btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_VM.ClearList2();
            UpdateAllStatus();
        }

        private void ClearList1Btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_VM.ClearList1();
            UpdateAllStatus();
        }

        private void ListView1_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] AddedFilesList = (string[])e.Data.GetData(DataFormats.FileDrop);
                MainWindow_VM.AddFilesToList(AddedFilesList, MainWindow_VM.FilesList1);
                UpdateAllStatus();
            }

        }

        private void ListView2_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] AddedFilesList = (string[])e.Data.GetData(DataFormats.FileDrop);
                MainWindow_VM.AddFilesToList(AddedFilesList, MainWindow_VM.FilesList2);
                UpdateAllStatus();
            }

        }

        private void RemoveSelectedItemsList1Btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_VM.RemoveItemsFromFileList(GetSelectedItemsFromListView(ListView1), MainWindow_VM.FilesList1);
            UpdateAllStatus();
        }

        private void RemoveSelectedItemsList2Btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_VM.RemoveItemsFromFileList(GetSelectedItemsFromListView(ListView2), MainWindow_VM.FilesList2);
            UpdateAllStatus();
        }


        private List<string> GetSelectedItemsFromListView(ListView listView)
        {
            List<string> selectedItemsString = new List<string>();

            foreach (var selectedItem in listView.SelectedItems)
            {
                selectedItemsString.Add(selectedItem.ToString());

            }
            return selectedItemsString;

            #endregion List View methods



            #region Supporting methods


        }

        #endregion

        //private void OnlySelectedExtensionList1Btn_Click(object sender, RoutedEventArgs e)
        //{

        //}
        #region UserInterfaceBehavior
        private void RemoveOnlySelectedExtensionList2Btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedExt = ExtensionListCmbBox2.Text;
            MainWindow_VM.RemoveAllDependingExtension(selectedExt, MainWindow_VM.FilesList2, false);
            UpdateAllStatus();
        }

        private void RemoveAllExceptSelectedExtList2Btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedExt = ExtensionListCmbBox2.Text;
            MainWindow_VM.RemoveAllDependingExtension(selectedExt, MainWindow_VM.FilesList2, true);
            UpdateAllStatus();
        }

        private void RemoveAllExceptSelectedExtList1Btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedExt = ExtensionListCmbBox1.Text;
            MainWindow_VM.RemoveAllDependingExtension(selectedExt, MainWindow_VM.FilesList1, true);
            UpdateAllStatus();
        }

        private void RemoveOnlySelectedExtensionList1Btn_Click(object sender, RoutedEventArgs e)
        {
            string selectedExt = ExtensionListCmbBox1.Text;
            MainWindow_VM.RemoveAllDependingExtension(selectedExt, MainWindow_VM.FilesList1, false);
            UpdateAllStatus();
        }

        private void UpdateAllStatus()
        {
            CounterList1Tbx.Text = ListView1.Items.Count.ToString() + "file(s)";
            CounterList2Tbx.Text = ListView2.Items.Count.ToString() + "file(s)";
        }

        private void SR_Preview_cbox_Click(object sender, RoutedEventArgs e)
        {
            if (SR_Preview_cbox.IsChecked == true)
            {
                Preview_rtbox.Visibility = Visibility.Visible;
                ListView2.Visibility = Visibility.Hidden;
            }
            else
            {
                Preview_rtbox.Visibility = Visibility.Hidden;
                ListView2.Visibility = Visibility.Visible;
            }
        }

        #endregion



        #region Search And Replace Methods


        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            bool srAbsolute = (bool)this.SR_aboslute_rbox.IsChecked;
            bool srRegEx = (bool)this.SR_RegEx_rbox.IsChecked;
            //bool srWildcards = (bool)this.SR_wildcards_rbox.IsChecked;
            //bool srExtended = (bool)this.SR_extended_rbox.IsChecked;

            bool srFileContext = (bool)this.SR_FileContent_rbox.IsChecked;
            bool srFileName = (bool)this.SR_FileNames_rbox.IsChecked;
            bool srFolderName = (bool)this.SR_FolderNames_rbox.IsChecked;

            bool srReMultiline = (bool)this.SR_RegExMultiline_cbox.IsChecked;
            bool srCaseSensitive = (bool)this.CaseSensitive_cbox.IsChecked;
            //bool srWholeWords = (bool)this.WholeWordsOnly_cbox.IsChecked;

            string searchText = SearchTbx.Text;
            string replaceText = ReplaceTbx.Text;


            //MainWindow_VM.Search(searchText, replaceText, srRegEx, srFileContext, srFileName, srFolderName, srCaseSensitive, srReMultiline);
            string data = AppDomain.CurrentDomain.BaseDirectory;
            data = Environment.CurrentDirectory;
            MessageBox.Show(data);


        }

        private void ReplaceBtn_Click(object sender, RoutedEventArgs e)
        {
            bool srAbsolute = (bool)this.SR_aboslute_rbox.IsChecked;
            bool srRegEx = (bool)this.SR_RegEx_rbox.IsChecked;
            //bool srWildcards = (bool)this.SR_wildcards_rbox.IsChecked;
            //bool srExtended = (bool)this.SR_extended_rbox.IsChecked;

            bool srFileContext = (bool)this.SR_FileContent_rbox.IsChecked;
            bool srFileName = (bool)this.SR_FileNames_rbox.IsChecked;
            bool srFolderName = (bool)this.SR_FolderNames_rbox.IsChecked;

            bool srReMultiline = (bool)this.SR_RegExMultiline_cbox.IsChecked;
            bool srCaseSensitive = (bool)this.CaseSensitive_cbox.IsChecked;
            //bool srWholeWords = (bool)this.WholeWordsOnly_cbox.IsChecked;

            string searchText = SearchTbx.Text;
            string replaceText = ReplaceTbx.Text;

            MainWindow_VM.Replace(searchText, replaceText, srRegEx,
                srFileContext, srFileName, srFolderName, srCaseSensitive, srReMultiline);
        }

        #endregion
    }
}
