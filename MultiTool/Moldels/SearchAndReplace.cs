using MultiTool.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;


namespace MultiTool.Moldels
{
    class SearchAndReplace
    {
        private bool RegEx { get; set; }
        private bool FileContent { get; set; }
        private bool FileName { get; set; }
        private bool FolderName { get; set; }
        private bool CaseSensitive { get; set; }
        private bool RegExMultiline { get; set; }



        //public SearchAndReplace()
        //{

        //}
        public SearchAndReplace(bool regEx, bool fileContent, bool fileName,
            bool folderName, bool caseSensitive, bool reMultiline)
        {

            this.RegEx = regEx;
            this.FileContent = fileContent;
            this.FileName = fileName;
            this.FolderName = folderName;
            this.CaseSensitive = caseSensitive;
            this.RegExMultiline = reMultiline;
            
        }

        public void Search(string filePath, string searchText, ResultInfo resultInfo)
        {
            if (FileContent)
            {
                SearchFileContent(filePath, searchText, resultInfo);
            }
            else if (FileName)
            {
                SearchFileName(filePath, searchText, resultInfo);
            }
            else if (FolderName)
            {
                SearchFolderName(filePath, searchText, resultInfo);
            }
        }

        private void SearchFolderName(string filePath, string searchText, ResultInfo resultInfo)
        {
            throw new NotImplementedException();
        }

        private void SearchFileName(string filePath, string searchText, ResultInfo resultInfo)
        {
            throw new NotImplementedException();
        }

        private void SearchFileContent(string filePath, string searchText, ResultInfo resultInfo)
        {
            StreamReader SR = new StreamReader(filePath);
            string workingFileContent = SR.ReadToEnd();
            SR.Close();

            if (this.RegEx)
            {
                //workingFileContent = SearchRegEx(workingFileContent, searchText,  resultInfo);
            }
            else
            {
                //workingFileContent = SearchAboslute(workingFileContent, searchText,  resultInfo);
            }

            StreamWriter SW = new StreamWriter(filePath);
            SW.Write(workingFileContent);
            SW.Close();
        }

        public void Replace(string filePath, string searchText, string replaceText, ResultInfo resultInfo)
        {
            if (FileContent)
            {
                ReplaceFileContent(filePath, searchText, replaceText, resultInfo);
            }
            else if (FileName)
            {
                ReplaceFileName(filePath, searchText, replaceText, resultInfo);
            }
            else if (FolderName)
            {
                ReplaceFolderName(filePath, searchText, replaceText, resultInfo);
            }
        }

        private void ReplaceFileName(string filePath, string searchText, string replaceText, ResultInfo resultInfo)
        {
            throw new NotImplementedException();
        }

        private void ReplaceFolderName(string filePath, string searchText, string replaceText, ResultInfo resultInfo)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// General method for S&R in file content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="searchText"></param>
        /// <param name="replaceText"></param>
        private void ReplaceFileContent(string filePath, string searchText, string replaceText, ResultInfo resultInfo)
        {
            StreamReader SR = new StreamReader(filePath);
            string workingFileContent = SR.ReadToEnd();
            SR.Close();

            if (this.RegEx)
            {
                workingFileContent = ReplaceRegEx(workingFileContent, searchText, replaceText,  resultInfo);
            }
            else
            {
                workingFileContent = ReplaceAboslute(workingFileContent, searchText, replaceText, resultInfo);
            }

            StreamWriter SW = new StreamWriter(filePath);
            SW.Write(workingFileContent);
            SW.Close();
        }

        //private string ReplaceWildcards(string fileContent, string searchText, string replaceText)
        //{
        //    throw new NotImplementedException();
        //}

        //private string ReplaceExtendedMode(string fileContent, string searchText, string replaceText)
        //{
        //    throw new NotImplementedException();
        //}

        private string ReplaceRegEx(string fileContent, string searchText, string replaceText, ResultInfo resultInfo)
        {
            RegexOptions regexOptions = GetRegExpOption();
            string workingString = fileContent;
            Regex.Replace(fileContent, searchText, replaceText, regexOptions);
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
        private string ReplaceAboslute(string fileContent, string searchText, string replaceText, ResultInfo resultInfo)
        {
            //string workingString = fileContent;
            //try
            //{
            //    if (!this.CaseSensitive)
            //        workingString = workingString.Replace(searchText, replaceText);

            //    else
            //        workingString = Regex.Replace(fileContent, searchText, replaceText, GetRegExpOption());

            //}
            //catch (Exception ex)
            //{
            //    resultInfo.AddError("Error during replacing :" + ex.Message);
            //}
           
            //List<int> FoundIndexes = new List<int>();
            int currentIndex = 0;
            StringComparison stringComparison = StringComparison.Ordinal;
            if (!CaseSensitive)
                stringComparison = StringComparison.OrdinalIgnoreCase;

            string workingString = fileContent;

            int foundIndex = currentIndex;
            
            while ((currentIndex < fileContent.Length) && (foundIndex > -1))
            {
                // znajdz indeks search text, zrob substring - index + 
                
                foundIndex = workingString.IndexOf(searchText, currentIndex, stringComparison);
                workingString = workingString.Remove(foundIndex, searchText.Length);
                workingString = workingString.Insert(foundIndex, replaceText);
                //FoundIndexes.Add(foundIndex);
                currentIndex = foundIndex+replaceText.Length;
                resultInfo.AddChange(String.Format("Replaced {} to {} in {} file position ", searchText, replaceText, foundIndex));
            }
            
            return workingString;
        }

        private List<int> SearchAbsoulute(string fileContent, string searchText, ResultInfo resultInfo)
        {
            List<int> FoundIndexes = new List<int>();
            int currentIndex = 0;
            StringComparison stringComparison = StringComparison.Ordinal;
            if (!CaseSensitive)
                stringComparison = StringComparison.OrdinalIgnoreCase;

            int foundIndex = currentIndex;

            while ((currentIndex < fileContent.Length) && (foundIndex >-1))
            {
                foundIndex = fileContent.IndexOf(searchText,currentIndex,stringComparison);
                FoundIndexes.Add(foundIndex);
                currentIndex = foundIndex;
            }

            return FoundIndexes;



        }


        /// <summary>
        /// returns collection of Matches
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="searchText"></param>
        /// <param name="replaceText"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="resultInfo"></param>
        /// <returns></returns>
        private MatchCollection SearchRegEx(string fileContent, string searchText)
        {
            return Regex.Matches(fileContent, searchText, GetRegExpOption());
        }
        /// <summary>
        /// Get option for Regular Expression Depending on class object bools depending on program checkbox selected
        /// </summary>
        /// <returns></returns>
        private RegexOptions GetRegExpOption()
        {
            RegexOptions regexOptions;
            
            if (RegExMultiline && CaseSensitive)
                regexOptions = RegexOptions.Multiline;

            else if (RegExMultiline && !CaseSensitive)
                regexOptions = RegexOptions.Multiline & RegexOptions.IgnoreCase;

            else if (!RegExMultiline && !CaseSensitive)
                regexOptions = RegexOptions.IgnoreCase;

            else  //!RegExMultiline && CaseSensitive
                regexOptions = RegexOptions.None;

            return regexOptions;

        }
    }
    
}
