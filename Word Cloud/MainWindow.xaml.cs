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

            cloudCanvas.Children.Clear();
            List<KeyValuePair<string, int>> sortedList = words.ToList();
            sortedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            //MessageBox.Show(words.Keys.Count.ToString()+" "+sortedList.Count.ToString());
            int maxCount = 0;
            int direction = 0;
            maxCount = sortedList[0].Value;

            //Randomize the words
            //sortedList = sortedList.OrderBy(a => Guid.NewGuid()).ToList();

            modalBG.Visibility = Visibility.Visible;
            //StackPanel modal = this.FindName("modalBG") as Panel;

            //sortedList = centerFrequentWords(sortedList);
            Debug.WriteLine("Start");

            for (int i = 0;i<sortedList.Count;i++) {
                Label l = new Label();
                l.Content = sortedList[i].Key+" ";
                int fSize = (int)(40.0 * ((double)sortedList[i].Value / (double)maxCount));
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
                    Debug.WriteLine(l.Content + " to " + labelToCheck.Content);
                    //Debug.WriteLine("Checking " + l.Content.ToString() + " against " +labelToCheck.Content.ToString());
                    bool needToCheckForCollisions = true;
                    bool movingRight = true;
                    bool movingDown = true;
                    Random r = new Random();
                    int xDir = r.Next(1, 10);
                    if (xDir > 5)
                    {
                        movingRight = true;
                    }
                    else
                    {
                        movingRight = false;
                    }
                    int yDir = r.Next(1, 10);
                    if (yDir > 5)
                    {
                        movingDown = true;
                    }
                    else
                    {
                        movingDown = false;
                    }
                    while (needToCheckForCollisions) {
                        if (checkForCollision(l,labelToCheck))
                        {
                            Debug.WriteLine("Collision!");
                            reposition(l, movingRight,movingDown);
                        }
                        else {
                            needToCheckForCollisions = false;
                        }
                    }

                }
                cloudCanvas.Children.Add(l);
            }
            Debug.WriteLine("Complete");
        }

        private bool checkForCollision(Label la,Label lb) {
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

        private void reposition(Label l,bool movingRight,bool movingDown) {
            double hOffset = 0;
            double vOffset = 0;

            if (movingRight)
            {
                hOffset = 10;
            }
            else {
                hOffset = -10;
            }

            if (movingDown)
            {
                vOffset = 10;
            }
            else
            {
                vOffset = -10;
            }

            Canvas.SetLeft(l, Canvas.GetLeft(l)+ hOffset);
            Canvas.SetTop(l, Canvas.GetTop(l) + vOffset);
        }

        private void hideModal(Object sender, RoutedEventArgs e) {
            modalBG.Visibility = Visibility.Collapsed;
        }
    }
}
