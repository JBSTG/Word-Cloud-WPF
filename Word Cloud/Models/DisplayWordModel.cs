using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Word_Cloud.Models
{
    public class DisplayWordModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _Text;
        private int _Count;
        private int _X;
        private int _Y;
        private int _FontSize;

        public DisplayWordModel()
        {

        }

        public DisplayWordModel(string t,int c,int x, int y, int f)
        {
            Text = t;
            Count = c;
            X = x;
            Y = y;
            FontSize = f;
        }



        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                if (value != _Count)
                {
                    _Count = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int X
        {
            get
            {
                return _X;
            }
            set
            {
                if (value != _X)
                {
                    _X = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Y
        {
            get
            {
                return _Y;
            }
            set
            {
                if (value != _Y)
                {
                    _Y = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                if (value != _FontSize)
                {
                    _FontSize = value;
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
