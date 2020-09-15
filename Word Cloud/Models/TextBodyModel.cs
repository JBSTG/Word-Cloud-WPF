using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Word_Cloud.Models
{
    class TextBodyModel: INotifyPropertyChanged
    {
        public TextBodyModel()
        {
            TextBody = String.Empty;
        }
    public TextBodyModel(string initialText)
        {
            TextBody = initialText;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _TextBody = String.Empty;
        public string TextBody
        {
            get
            {
                return _TextBody;
            }
            set
            {
                if (value!=_TextBody)
                {
                    _TextBody = value;
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
