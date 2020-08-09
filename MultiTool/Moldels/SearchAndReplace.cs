using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;


namespace MultiTool.Moldels
{
    class SearchAndReplace
    {
        //private string SRfilePath { get; set; }
        private bool RegEx { get; set; }
        //private bool Wildcards { get; set; }     //for MS Word
        //private bool ExtendedMode { get; set; }  // /r/n/t etc...
        private bool FileContent { get; set; }
        private bool FileName { get; set; }
        private bool FolderName { get; set; }
        private bool CaseSensitive { get; set; }
        private bool RegExMultiline { get; set; }
        //private bool WholeWordsOnly { get; set; }


        public SearchAndReplace()
        {

        }
        public SearchAndReplace(bool regEx, bool fileContent, bool fileName, 
            bool folderName, bool caseSensitive, bool reMultiline)
        {
            
            this.RegEx = regEx;
            //this.Wildcards = wildcard;
            //this.ExtendedMode = extMode;
            this.FileContent = fileContent;
            this.FileName = fileName;
            this.FolderName = folderName;
            this.CaseSensitive = caseSensitive;
            this.RegExMultiline = reMultiline;
            //this.WholeWordsOnly = wholeWordsOnly;
        }

        public void Search(string filePath, string searchText) 
        {

        }

        public void Replace(string filePath, string searchText, string replaceText)
        {
            if (FileContent)
            {
                SearchReplaceFileContent(filePath, searchText, replaceText);
            }
            else if (FileName)
            {
                SearchReplaceFileName(filePath, searchText, replaceText);
            }
            else if (FolderName)
            {
                SearchReplaceFolderName(filePath, searchText, replaceText);
            }
        }

        private void SearchReplaceFileName(string filePath, string searchText, string replaceText)
        {
            throw new NotImplementedException();
        }

        private void SearchReplaceFolderName(string filePath, string searchText, string replaceText)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// General method for S&R in file content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="searchText"></param>
        /// <param name="replaceText"></param>
        private void SearchReplaceFileContent(string filePath, string searchText, string replaceText)
        {
            StreamReader SR = new StreamReader(filePath);
            string workingFileContent = SR.ReadToEnd();
            SR.Close();

            if (this.RegEx)
            {
                workingFileContent = ReplaceRegEx(workingFileContent, searchText, replaceText, this.CaseSensitive, this.RegExMultiline);
            }
            else
            {
                workingFileContent = ReplaceAboslute(workingFileContent, searchText, replaceText, this.CaseSensitive);
            }

            StreamWriter SW = new StreamWriter(filePath);
            SW.Write(workingFileContent);
            SW.Close();
        }

        private string ReplaceWildcards(string fileContent, string searchText, string replaceText)
        {
            throw new NotImplementedException();
        }

        private string ReplaceExtendedMode(string fileContent, string searchText, string replaceText)
        {
            throw new NotImplementedException();
        }

        private string ReplaceRegEx(string fileContent, string searchText, string replaceText, bool caseSensitive, bool regExMultiline)
        {
            string workingString = fileContent;
            if (regExMultiline && caseSensitive)
                workingString = Regex.Replace(fileContent, searchText, replaceText, RegexOptions.Multiline);
            
            else if (regExMultiline && !caseSensitive)
                workingString = Regex.Replace(fileContent, searchText, replaceText, RegexOptions.Multiline & RegexOptions.IgnoreCase);

            else if (!regExMultiline && !caseSensitive)
                workingString = Regex.Replace(fileContent, searchText, replaceText, RegexOptions.IgnoreCase);

            else  //!regExMultiline && caseSensitive
                workingString = Regex.Replace(fileContent, searchText, replaceText, RegexOptions.None);

            return workingString;
        }

        /// <summary>
        /// Method return file after simple Search and Replace, caseSentsitive optional
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="searchText"></param>
        /// <param name="replaceText"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        private string ReplaceAboslute(string fileContent, string searchText, string replaceText, bool caseSensitive)
        {
            string workingString = fileContent;
            if (!caseSensitive)
                workingString = workingString.Replace(searchText, replaceText);
            
            else
                workingString = Regex.Replace(fileContent, searchText, replaceText, RegexOptions.IgnoreCase);

            return workingString;
        }
    }
}
