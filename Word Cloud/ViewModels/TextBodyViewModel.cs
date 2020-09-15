using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Word_Cloud.Commands;
using Word_Cloud.Models;
using Word_Cloud.Service;

namespace Word_Cloud.ViewModels
{
    public class TextBodyViewModel:ViewModelBase
    {
        //Models
        private WordMapModel _WordMapModel;
        private TextBodyModel _TextBodyModel;
        private List<DisplayWordModel> _DisplayWords;
        //Properties
        private string _TextBody = String.Empty;
        private SortedDictionary<string, int> _Map;
        //Commands
        public ICommand GenerateCloudCommand { get; set; }
        
        public TextBodyViewModel()
        {
            _TextBodyModel = new TextBodyModel();
            _WordMapModel = new WordMapModel();
            _Map = new SortedDictionary<string, int>();
            _DisplayWords = new List<DisplayWordModel>();
            GenerateCloudCommand = new GenerateCloudCommand("what");
            Debug.WriteLine("Here");
        }
        public string TextBody
        {
            get
            {
                return _TextBodyModel.TextBody;
            }
            set
            {
                _TextBodyModel.TextBody = value;
                Map = WordCloudUtility.GetMapFromString(value);
                Debug.WriteLine("Text Changed");

                NotifyPropertyChanged();
            }
        }

        public SortedDictionary<string,int> Map
        {
            get
            {
                return _WordMapModel.Map;
            }
            set
            {
                if (value !=_WordMapModel.Map)
                {
                    _WordMapModel.Map = value;
                    NotifyPropertyChanged();
                    DisplayWords = WordCloudUtility.FormatWordsForCanvas(Map);
                    Debug.WriteLine("Dictionary Changed");
                }
            }
        }
        public List<DisplayWordModel> DisplayWords
        {
            get
            {
                return _DisplayWords;
            }
            set
            {
                if (value != _DisplayWords)
                {
                    _DisplayWords = value;
                    NotifyPropertyChanged();
                    Debug.WriteLine("List Changed");
                }
            }
        }
    }
}
