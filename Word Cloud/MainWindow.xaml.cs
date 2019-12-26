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

            if (rawWords.Length == 1 && rawWords[0] == "") {
                tw.Text = "Total Words: " + 0;
                uw.Text = "Unique Words: " + 0;
            }
            buildButton.IsEnabled = true;
            //MessageBox.Show(text);
        }
        private void viewCloud(Object sender, RoutedEventArgs e) {

            cloudCanvas.Children.Clear();
            List<KeyValuePair<string, int>> sortedList = words.ToList();
            sortedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            int maxCount = sortedList[0].Value;
            modalBG.Visibility = Visibility.Visible;
            Debug.WriteLine("Start");
            int max = sortedList.Count;
            if (sortedList.Count > 100)
            {
                max = 100;
            }
            
            for (int i = 0; i < max; i++) {
                //This function adds a new word label to the Center
                Label l = addNewWordLabel(cloudCanvas,sortedList[i],maxCount);
                //Check for collisions.
                bool needToCheckForCollisions = true;
                int j = 0;
                Random rDeg = new Random();
                double deg = rDeg.Next(0,360);
                int distance = 1;
                while (needToCheckForCollisions&&cloudCanvas.Children.Count>0) {
                    Label labelToCheck = cloudCanvas.Children[j] as Label;
                    //Debug.WriteLine(l.Content + " to " + labelToCheck.Content);
                    //bool movingRight = setDirection();
                    //bool movingDown = setDirection();
                    if (checkForCollision(l, labelToCheck))
                    {
                        j = -1;
                        Debug.WriteLine("Checking "+l.Content+" against "+labelToCheck.Content);
                        reposition(l,distance,deg);
                        distance++;
                        deg++;
                        if (deg >= 360) {
                            deg = 0;
                        }
                    }
                    else {
                        needToCheckForCollisions = false;
                    }
                    j = j==-1 ? j = 0:j;
                    j = j==(cloudCanvas.Children.Count - 1) ? j = 0 : j++;
                }
                cloudCanvas.Children.Add(l);
            }
            Debug.WriteLine("Complete");
        }

        private bool checkForCollision(Label la, Label lb) {
            System.Drawing.RectangleF a = new System.Drawing.RectangleF();
            System.Drawing.RectangleF b = new System.Drawing.RectangleF();

            a.X = (float)Canvas.GetLeft(la);
            a.Y = (float)Canvas.GetTop(la);
            a.Width = (float)la.ActualWidth;
            a.Height = (float)la.ActualHeight;

            b.X = (float)Canvas.GetLeft(lb);
            b.Y = (float)Canvas.GetTop(lb);
            b.Width = (float)lb.ActualWidth;
            b.Height = (float)lb.ActualHeight;

            return a.IntersectsWith(b);
        }

        private void reposition(Label l, int distance,double degrees) {
            int x = (int)(distance*Math.Cos(degrees*Math.PI/180.0));
            int y = (int)(distance * Math.Sin(degrees * Math.PI / 180.0));
            //double hOffset = 0;
            //double vOffset = 0;
            Canvas.SetLeft(l, Canvas.GetLeft(l) + x);
            Canvas.SetTop(l, Canvas.GetTop(l) + y);
        }

        private void hideModal(Object sender, RoutedEventArgs e) {
            modalBG.Visibility = Visibility.Collapsed;
        }

        private bool setDirection() {
            Random r = new Random();
            bool direction;
            int xDir = r.Next(1, 10);
            if (xDir > 5)
            {
                direction = true;
            }
            else
            {
                direction = false;
            }
            return direction;
        }

        private Brush getRandomColor() {
            Random r = new Random();
            int brush = r.Next(1,10);
            switch (brush) {
                case 1:
                    return Brushes.Crimson;
                    break;
                case 2:
                    return Brushes.SkyBlue;
                    break;
                case 3:
                    return Brushes.DarkOliveGreen;
                    break;
                case 4:
                    return Brushes.Orange;
                    break;
                case 5:
                    return Brushes.HotPink;
                    break;
                case 6:
                    return Brushes.LightGoldenrodYellow;
                    break;
                case 7:
                    return Brushes.PaleVioletRed;
                    break;
                case 8:
                    return Brushes.DarkSlateBlue;
                    break;
                case 9:
                    return Brushes.DarkSlateGray;
                    break;
                case 10:
                    return Brushes.LightSeaGreen;
                    break;
                default:
                    return Brushes.Black;
                    break;
            }
        }
        private Label addNewWordLabel(Canvas cloudCanvas,KeyValuePair<string,int> wordAndCount,int maxCount) {
            Label l = new Label();
            l.Content = wordAndCount.Key + " ";
            int fSize = (int)(40.0 * ((double)wordAndCount.Value / (double)maxCount));
            l.FontSize = fSize+1;
            l.Background = Brushes.DarkSlateGray;
            l.FontFamily = new FontFamily("Impact");
            l.Padding = new Thickness(0, 0, 0, 0);
            l.HorizontalAlignment = HorizontalAlignment.Center;
            l.Foreground = getRandomColor();
            //Assign dimentions to our new word.
            l.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            l.Arrange(new Rect(l.DesiredSize));
            Canvas.SetLeft(l, (cloudCanvas.Width - l.ActualWidth) / 2);
            Canvas.SetTop(l, (cloudCanvas.Height - l.ActualHeight) / 2);
            return l;
        }
        
    }
}
