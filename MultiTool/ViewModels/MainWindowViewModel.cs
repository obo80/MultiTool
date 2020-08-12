using MultiTool.Moldels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace MultiTool.ViewModels
{
    class MainWindowViewModel
    {

        #region List declaration
        private static ObservableCollection<string> _FilesList1 { get; set; }
        private static ObservableCollection<string> _FilesList2 { get; set; }

        public ObservableCollection<string> FilesList1
        {
            get { return _FilesList1; }
            set
            {
                _FilesList1 = value;
            }
        }
        public ObservableCollection<string> FilesList2
        {
            get { return _FilesList2; }
            set
            {
                _FilesList2 = value;
            }
        }

        public ObservableCollection<string> ExtensionList1 { get; set; }
        public ObservableCollection<string> ExtensionList2 { get; set; }

        //private ObservableCollection<string> Extension_list { get; set; }

        public ObservableCollection<string> LogList { get; set; }
        #endregion


        #region Constructor
        public MainWindowViewModel()
        {
            FilesList1 = new ObservableCollection<string>();
            FilesList2 = new ObservableCollection<string>();

            ExtensionList1 = new ObservableCollection<string>();
            ExtensionList2 = new ObservableCollection<string>();

            LogList = new ObservableCollection<string>();
            RefreshLogList();

        }
        #endregion

        #region FileList operations

        public void ClearList1()
        {
            FilesList1.Clear();
            RefreshExtensionLists();
        }
        public void ClearList2()
        {
            FilesList2.Clear();
            RefreshExtensionLists();
        }
        public void AddFilesToList(string[] AddedItemToList, ObservableCollection<string> FileListX)
        {
            foreach (string ItemToAdd in AddedItemToList)
            {
                if (!FileListX.Contains(ItemToAdd))
                {
                    if (File.Exists(ItemToAdd))
                    {
                        FileListX.Add(ItemToAdd);
                    }
                    else
                    {
                        if (Directory.Exists(ItemToAdd))
                        {
                            string[] FileList = Directory.GetFiles(ItemToAdd, "*.*", SearchOption.AllDirectories);
                            foreach (string filePath in FileList)
                            {
                                FileListX.Add(filePath);
                            }
                        }
                    }
                }
            }
            RefreshExtensionLists();
        }

        public void RemoveItemsFromFileList(List<string> ItemsToRemove, ObservableCollection<string> FileListX)
        {
            foreach (var item in ItemsToRemove)
            {
                FileListX.Remove(item);
            }
            RefreshExtensionLists();
        }



        #endregion

        #region Extension filtering

        public void RemoveAllDependingExtension(string selectedExt, ObservableCollection<string> FileListX, bool isSelected)
        {
            try
            {
                string[] files = FileListX.ToArray();
                foreach (string currentFile in files)
                {
                    FileInfo Fi = new FileInfo(currentFile);
                    string currentExt = Fi.Extension.ToLower();
                    if ((selectedExt == currentExt && !isSelected) || (selectedExt != currentExt && isSelected))
                    {
                        FileListX.Remove(currentFile);
                    }
                }
            }
            catch (System.Exception se)
            {
                MessageBox.Show(se.Message, "Rremoving files by extension error");
            }
            RefreshExtensionLists();
        }
        public void RefreshExtensionLists()
        {
            ExtensionList1.Clear(); ExtensionList2.Clear();
            try
            {
                foreach (string currentFile in FilesList1)
                {
                    FileInfo Fi = new FileInfo(currentFile);
                    string currentExt = Fi.Extension.ToLower();
                    if (!ExtensionList1.Contains(currentExt))
                        ExtensionList1.Add(currentExt);
                }
                foreach (string currentFile in FilesList2)
                {
                    FileInfo Fi = new FileInfo(currentFile);
                    string currentExt = Fi.Extension.ToLower();
                    if (!ExtensionList2.Contains(currentExt))
                        ExtensionList2.Add(currentExt);
                }
            }
            catch (System.Exception se)
            {
                MessageBox.Show(se.Message, "Rrefresing extension error");
            }
        }
        #endregion

        #region Log operations

        public void RefreshLogList()
        {
            LogList.Clear();
            try
            {
                //string Logs_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                string Logs_path = Path.Combine(Environment.CurrentDirectory, "logs");
                if (!Directory.Exists(Logs_path))
                    Directory.CreateDirectory(Logs_path);
                var LogPathsList = Directory.GetFiles(Logs_path);

                foreach (var logFile in LogPathsList)
                    LogList.Add(Path.GetFileName(logFile));



            }
            catch (System.Exception se)
            {
                MessageBox.Show(se.Message, "Rrefresing log list error");
            }
        }

        #endregion


        //}


        #region SearchAndReplace

        public void Search(string searchText, string replaceText, bool srRegEx,
                    bool srFileContext, bool srFileName, bool srFolderName, bool srCaseSensitive, bool srReMultiline)
        {
            SearchAndReplace SR = new SearchAndReplace(srRegEx, srFileContext, srFileName,
               srFolderName, srCaseSensitive, srReMultiline);

            foreach (string filePath in _FilesList1)
            {
                SR.Search(filePath, searchText);
            }

        }

        public void Replace(string searchText, string replaceText, bool srRegEx,
                bool srFileContext, bool srFileName, bool srFolderName, bool srCaseSensitive, bool srReMultiline)
        {
            SearchAndReplace SR = new SearchAndReplace(srRegEx, srFileContext, srFileName,
                srFolderName, srCaseSensitive, srReMultiline);

            foreach (string filePath in _FilesList1)
            {
                SR.Replace(filePath, searchText, replaceText);
            }

        }


        #endregion




        //private void Test_adding_items()
        //{

        //    FilesList1.Add("file1 path bla bla bla");
        //    FilesList1.Add("file2 path bla bla bla");
        //    FilesList1.Add("file3 path bla bla bla");
        //    FilesList1.Add("file4 path bla bla bla");
        //    FilesList1.Add("file5 path bla bla bla");
        //    FilesList1.Add("file6 path bla bla bla");
        //    FilesList2.Add("file1 path kra kra kra");
        //    FilesList2.Add("file2 path kra kra kra");
        //    FilesList2.Add("file3 path kra kra kra");
        //    FilesList2.Add("file4 path kra kra kra");
        //    FilesList2.Add("file5 path kra kra kra");
        //    FilesList2.Add("file6 path kra kra kra");
        //    FilesList2.Add("file1 path kra kra kra");
        //    FilesList2.Add("file2 path kra kra kra");
        //    FilesList2.Add("file3 path kra kra kra");
        //    FilesList2.Add("file4 path kra kra kra");
        //    FilesList2.Add("file5 path kra kra kra");
        //    FilesList2.Add("file6 path kra kra kra");
        //    FilesList2.Add("file1 path kra kra kra");
        //    FilesList2.Add("file2 path kra kra kra");
        //    FilesList2.Add("file3 path kra kra kra");
        //    FilesList2.Add("file4 path kra kra kra");
        //    FilesList2.Add("file5 path kra kra kra");
        //    FilesList2.Add("file6 path kra kra kra");
        //    FilesList2.Add("file1 path kra kra kra");
        //    FilesList2.Add("file2 path kra kra kra");
        //    FilesList2.Add("file3 path kra kra kra");
        //    FilesList2.Add("file4 path kra kra kra");
        //    FilesList2.Add("file5 path kra kra kra");
        //    FilesList2.Add("file6 path kra kra kra");
        //    FilesList2.Add("file1 path kra kra kra");
        //    FilesList2.Add("file2 path kra kra kra");
        //    FilesList2.Add("file3 path kra kra kra");
        //    FilesList2.Add("file4 path kra kra kra");
        //    FilesList2.Add("file5 path kra kra kra");
        //    FilesList2.Add("file6 path kra kra kra");
        //}
    }

}
