using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Word_Cloud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int wordCount = 0;
        Dictionary<string, int> words = new Dictionary<string, int>();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void getWordCount(Object sender, KeyEventArgs e) {
            //Prepare string
            TextBox t = sender as TextBox;
            string text = t.Text;
            text = text.Trim();
            text = Regex.Replace(text, "[\n]", " ");
            text = Regex.Replace(text, "[!\",\\.)(\n]", "");

            //Get unique Characters
            string[] rawWords = text.Split(" ");
            words = new Dictionary<string, int>();

            for (int i = 0; i < rawWords.Length; i++) {
                if (words.ContainsKey(rawWords[i].Trim()))
                {
                    words[rawWords[i].Trim()]++;
                }
                else {
                    words[rawWords[i].Trim()] = 1;
                }
            }

            TextBlock tw = this.FindName("totalWords") as TextBlock;
            tw.Text = "Total Words: " + rawWords.Length;

            TextBlock uw = this.FindName("uniqueWords") as TextBlock;
            uw.Text = "Unique Words: " + words.Keys.Count;

            if (rawWords.Length == 1 && rawWords[0]=="") {
                tw.Text = "Total Words: " + 0;
                uw.Text = "Unique Words: " + 0;
            }
            buildButton.IsEnabled = true;
            //MessageBox.Show(text);
        }
        private void viewCloud(Object sender, RoutedEventArgs e) {

            List<KeyValuePair<string,int>> sortedList = words.ToList().OrderByDescending(o => o.Value).ToList();
            //MessageBox.Show(words.Keys.Count.ToString()+" "+sortedList.Count.ToString());
            int maxCount = 0;
            int direction = 0;
            maxCount = sortedList[0].Value;

            //Randomize the words
            //sortedList = sortedList.OrderBy(a => Guid.NewGuid()).ToList();

            modalBG.Visibility = Visibility.Visible;
            //StackPanel modal = this.FindName("modalBG") as Panel;

            sortedList = centerFrequentWords(sortedList);

            for (int i = 0;i<sortedList.Count;i++) {
                Label l = new Label();
                l.Content = sortedList[i].Key+" ";
                int fSize = (int)(40.0 * ((double)sortedList[i].Value / (double)maxCount));
                if (fSize < 10){
                    fSize = 10;
                }else if (fSize < 15) {
                    fSize = 15;
                }else if(fSize<20){
                    fSize = 20;
                }else if (fSize < 25)
                {
                    fSize = 25;
                }
                l.FontSize = fSize;
                l.Padding = new Thickness(0,0,0,0);
                //Assign dimentions to our new word.
                l.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                l.Arrange(new Rect(l.DesiredSize));
                Canvas.SetLeft(l, (cloudCanvas.Width - l.ActualWidth) / 2);
                Canvas.SetTop(l, (cloudCanvas.Height - l.ActualHeight) / 2);

                //Check for collisions.
                for (int j = 0;j<cloudCanvas.Children.Count;j++) {
                    Label labelToCheck = cloudCanvas.Children[j] as Label;
                    while (checkForCollision(l,labelToCheck)) {
                        reposition(l,direction);
                    }
                }
                cloudCanvas.Children.Add(l);

                direction += 1;
                if (direction == 10) {
                    direction = 1;
                }
            }

            Debug.WriteLine(cloudCanvas.ActualWidth);

        }

        private bool checkForCollision(Label la,Label lb) {
            if (Canvas.GetLeft(la)>=Canvas.GetLeft(lb)&&((Canvas.GetLeft(la))<=(Canvas.GetLeft(lb)+lb.ActualWidth))) {
                if (Canvas.GetTop(la) >= Canvas.GetTop(lb) && ((Canvas.GetTop(la)) <= (Canvas.GetTop(lb) + lb.ActualHeight)))
                {
                    return true;
                }
            }
            if (Canvas.GetLeft(la) <= Canvas.GetLeft(lb) && ((Canvas.GetLeft(la)) >= (Canvas.GetLeft(lb) + lb.ActualWidth)))
            {
                if (Canvas.GetTop(la) <= Canvas.GetTop(lb) && ((Canvas.GetTop(la)) >= (Canvas.GetTop(lb) + lb.ActualHeight)))
                {
                    return true;
                }
            }
            return false;
        }

        private void reposition(Label l,int direction) {
            double leftOffset = 0;
            double topOffset = 0;
            switch (direction)
            {
                case 0:
                    topOffset = 0;
                    leftOffset = 0;
                    break;
                case 1:
                    topOffset = 0;
                    leftOffset = -10;
                    break;
                case 2:
                    topOffset = -10;
                    leftOffset = -10;
                    break;
                case 3:
                    topOffset = -10;
                    leftOffset = 0;
                    break;
                case 4:
                    topOffset = -10;
                    leftOffset = 10;
                    break;
                case 5:
                    topOffset = 0;
                    leftOffset = 10;
                    break;
                case 6:
                    topOffset = 10;
                    leftOffset = 10;
                    break;
                case 7:
                    topOffset = 10;
                    leftOffset = 0;
                    break;
                case 8:
                    topOffset = 10;
                    leftOffset = -10;
                    break;
                case 9:
                    topOffset = -10;
                    leftOffset = -10;
                    break;
            }
            Canvas.SetLeft(l, Canvas.GetLeft(l)+ leftOffset);
            Canvas.SetTop(l, Canvas.GetTop(l) + topOffset);
        }

        private void hideModal(Object sender, RoutedEventArgs e) {
            modalBG.Visibility = Visibility.Collapsed;
        }

        private List<KeyValuePair<string, int>> centerFrequentWords(List<KeyValuePair<string, int>> list) {
            for (int i = list.Count-1;i>list.Count/2;i--) {
                KeyValuePair<string, int> w = list[i];
                list.RemoveAt(i);
                list.Insert(0,w);
            }
            //MessageBox.Show(list[0].Key);
            return list;
        }

    }
}
