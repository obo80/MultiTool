using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace MultiTool.ViewModels
{
    class ResultInfo
    {
        #region Properties
        public List<string> Errors { get; set; }
        public List<string> Changes { get; set; }
        public string FilePath { get; set; }

        public ResultInfo(string filePath)
        {
            FilePath = filePath;
        }


        #endregion

        //#region Constructors

        //#endregion


        #region Methods
        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }

        public void AddChange(string changeMessage)
        {
            Changes.Add(changeMessage);
        }



        /// <summary>
        /// zastanawiam sie czy nie przenieść tego do klasy Log Save - to musi być jakoś inaczej zbierane
        /// W taki sposób powstaje jakby osobny Log dla każdego przemielonego pliku
        /// Trzeba by zbierać to jako podsumowanie
        /// </summary>
        /// <returns></returns>
        public string PrepareLogContent()
        {
            string content = String.Format("There are {0} errors and {1} changes in file: {2}\nSee list below:\n"
                , Errors.Count.ToString(), Changes.Count.ToString(), FilePath);

            if (Errors.Count>0)
            {
                content = content + "Errors:\n";
                foreach (var item in Errors)
                {
                    content= content + item;
                }
            }
            content = content + "\n--------------------------------------------------------------------------\n";
            if (Changes.Count > 0)
            {
                content = content + "Changes:\n";
                foreach (var item in Changes)
                {
                    content = content + item;
                }
            }


            return content;
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
