using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using MultiTool.Moldels;

namespace MultiTool.ViewModels
{
    class MainWindowViewModel
    {
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

        private ObservableCollection<string> Extension_list { get; set; }
        

        public MainWindowViewModel()
        {
            FilesList1 = new ObservableCollection<string>();
            FilesList2 = new ObservableCollection<string>();
            ExtensionList1 = new ObservableCollection<string>();
            ExtensionList2 = new ObservableCollection<string>();
            //Test_adding_items();
            
        }

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
                MessageBox.Show(se.Message,"Rremoving files by extension error");
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


        public void RemoveItemsFromFileList(List<string> ItemsToRemove, ObservableCollection<string> FileListX)
        {
            foreach (var item in ItemsToRemove)
            {
                FileListX.Remove(item);
            }
            RefreshExtensionLists();
        }


        //public void AddFilesToList_1(string[] AddedListOfFiles)
        //{
        //    AddFilesToList(AddedListOfFiles, FilesList1);
        //}
        //public void AddFilesToList_2(string[] AddedListOfFiles)
        //{
        //    AddFilesToList(AddedListOfFiles, FilesList2);
        //}


        #region SearchAndReplace

        public void Search(string searchText, string replaceText, bool srRegEx, bool srWildcards, bool srExtended,
                bool srFileContext, bool srFileName, bool srFolderName, bool srCaseSensitive, bool srReMultiline, bool srWholeWords)
        {
            SearchAndReplace SR = new SearchAndReplace(srRegEx, srWildcards, srExtended, srFileContext, srFileName,
               srFolderName, srCaseSensitive, srReMultiline, srWholeWords);
            
            foreach (string filePath in _FilesList1)
            {
                SR.Search(filePath, searchText);
            }

        }

        public void Replace(string searchText, string replaceText, bool srRegEx, bool srWildcards, bool srExtended,
                bool srFileContext, bool srFileName, bool srFolderName, bool srCaseSensitive, bool srReMultiline, bool srWholeWords)
        {
            SearchAndReplace SR = new SearchAndReplace(srRegEx, srWildcards, srExtended,srFileContext, srFileName, 
                srFolderName, srCaseSensitive, srReMultiline, srWholeWords);
            
            foreach (string filePath in _FilesList1)
            {
                SR.Replace(filePath, searchText, replaceText);
            }

        }


        #endregion




        private void Test_adding_items()
        {

            FilesList1.Add("file1 path bla bla bla");
            FilesList1.Add("file2 path bla bla bla");
            FilesList1.Add("file3 path bla bla bla");
            FilesList1.Add("file4 path bla bla bla");
            FilesList1.Add("file5 path bla bla bla");
            FilesList1.Add("file6 path bla bla bla");
            FilesList2.Add("file1 path kra kra kra");
            FilesList2.Add("file2 path kra kra kra");
            FilesList2.Add("file3 path kra kra kra");
            FilesList2.Add("file4 path kra kra kra");
            FilesList2.Add("file5 path kra kra kra");
            FilesList2.Add("file6 path kra kra kra");
            FilesList2.Add("file1 path kra kra kra");
            FilesList2.Add("file2 path kra kra kra");
            FilesList2.Add("file3 path kra kra kra");
            FilesList2.Add("file4 path kra kra kra");
            FilesList2.Add("file5 path kra kra kra");
            FilesList2.Add("file6 path kra kra kra");
            FilesList2.Add("file1 path kra kra kra");
            FilesList2.Add("file2 path kra kra kra");
            FilesList2.Add("file3 path kra kra kra");
            FilesList2.Add("file4 path kra kra kra");
            FilesList2.Add("file5 path kra kra kra");
            FilesList2.Add("file6 path kra kra kra");
            FilesList2.Add("file1 path kra kra kra");
            FilesList2.Add("file2 path kra kra kra");
            FilesList2.Add("file3 path kra kra kra");
            FilesList2.Add("file4 path kra kra kra");
            FilesList2.Add("file5 path kra kra kra");
            FilesList2.Add("file6 path kra kra kra");
            FilesList2.Add("file1 path kra kra kra");
            FilesList2.Add("file2 path kra kra kra");
            FilesList2.Add("file3 path kra kra kra");
            FilesList2.Add("file4 path kra kra kra");
            FilesList2.Add("file5 path kra kra kra");
            FilesList2.Add("file6 path kra kra kra");
        }
    }

}
