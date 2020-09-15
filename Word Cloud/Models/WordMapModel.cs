using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Word_Cloud.Service;

namespace Word_Cloud.Models
{
    class WordMapModel:INotifyPropertyChanged
    {

        public WordMapModel()
        {
            _Map = new SortedDictionary<string, int>();
        }
        public WordMapModel(string textBody)
        {
            _Map = WordCloudUtility.GetMapFromString(textBody);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private SortedDictionary<string, int> _Map;
        public SortedDictionary<string, int> Map
        {
            get
            {
                return _Map;
            }
            set
            {
                if (value != _Map)
                {
                    _Map = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
