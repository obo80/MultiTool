using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTool.ViewModels
{
    class LogSave
    {
        #region Properties
        public string LogFolder { get; set; }
        #endregion

        #region Constructors

        #endregion

        #region Methods
        //public void SaveLogToFile(string FilePath)
        //{
        //    try
        //    {
        //        string LogPath = Path.Combine(LogFolder, FilePath);
        //        StreamWriter SW = new StreamWriter(LogPath);
        //        SW.Write(PrepareLogContent());
        //        SW.Close();
        //    }
        //    catch (IOException ioe)
        //    {
        //        MessageBox.Show(ioe.Message, "Log file saving error");
        //    }
        //}

        public string SummarySearch(int filesNumber)
        {
            string summary = "";


            return summary;
        }

        #endregion

    }
}
