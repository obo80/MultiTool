using System;
using System.Collections.Generic;

namespace MultiTool.ViewModels
{
    class ResultInfo
    {
        #region Properties
        /// <summary>
        /// number of changed file
        /// </summary>
        public int FileChangedCount { get; set; }

        /// <summary>
        /// List of all paths to files where any change appears
        /// </summary>
        public List<string> FileChangedList { get; set; }

        /// <summary>
        /// global number of changes in all files
        /// </summary>
        public int ChangesCount { get; set; }

        /// <summary>
        /// Message for Status Bar
        /// </summary>
        public string StatusInfo { get; }

        /// <summary>
        /// String containing any error found durig files processing
        /// key = file path
        /// value = combined message for this file
        /// Item is added only if any error happens for any file
        /// </summary>
        public Dictionary<string, string> ErrorMessageList { get; set; }


        #endregion

        #region Constructors
        public ResultInfo()
        {
            ErrorMessageList = new Dictionary<string, string>();
            FileChangedList = new List<string>();
        }


        #endregion


        #region Methods

        /// <summary>
        /// Getting string for Status bar after operation is done
        /// </summary>
        /// <returns></returns>
        public string GetSummaryStatusBar()
        {
            string summaryText = "";
            if (ErrorMessageList.Count > 0)
                summaryText = String.Format("{0} results in {1} of {2} files", this.ChangesCount, this.FileChangedCount, this.FileChangedList.Count);

            else if (ErrorMessageList.Count == 0)
                summaryText = String.Format("{0} results in {1} of {2} files, but some errors happens in {3} files, please see log", this.ChangesCount, this.FileChangedCount, this.FileChangedList.Count, this.ErrorMessageList.Keys.Count);

            return summaryText;
        }

        private void SaveLogFile(string ActivityName, string logFileContent)
        {
            string dataTimeString = DateTime.Now.ToShortDateString() + "_" +
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
            //string logsFolder = 
            //StreamWriter SR = new StreamWriter(dataTimeString + "_" + ActivityName + ".txt");
            //SR.WriteAsync(logFileContent);
        }

        #endregion

        //public ResultInfo GetLogInfo
    }
}
/*
 * potrzebuje takie informacje:
 * w ilu plikach sie zmienilo
 * ile zmian było
 * lista zmienionych plików
 * text raportu z bledami
 * 
 * */
