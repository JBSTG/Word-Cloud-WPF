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
            maxCount = sortedList[0].Value;

            //Randomize the words
            //sortedList = sortedList.OrderBy(a => Guid.NewGuid()).ToList();


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
                l.Margin = new Thickness(0,0,0,0);
                l.Padding = new Thickness(0,0,0,0);
                cloudCanvas.Children.Add(l);
            }



            modalBG.Visibility = Visibility.Visible;
            //StackPanel modal = this.FindName("modalBG") as Panel;
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
