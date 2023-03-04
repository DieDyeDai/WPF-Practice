using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string textBox2Contents;

        // private ResourceDictionary imgEyesList;
        
        static Uri resourceUri9 = new Uri("/Psyc58H Images/9.jpg", UriKind.Relative);
        static Uri resourceUri12 = new Uri("/Psyc58H Images/12.jpg", UriKind.Relative);
        static Uri resourceUri19 = new Uri("/Psyc58H Images/19.jpg", UriKind.Relative);
        static Uri resourceUri22 = new Uri("/Psyc58H Images/22.jpg", UriKind.Relative);
        static Uri resourceUri24 = new Uri("/Psyc58H Images/24.jpg", UriKind.Relative);
        static Uri resourceUri27 = new Uri("/Psyc58H Images/27.jpg", UriKind.Relative);
        static Uri resourceUri32 = new Uri("/Psyc58H Images/32.jpg", UriKind.Relative);
        static Uri resourceUri33 = new Uri("/Psyc58H Images/33.jpg", UriKind.Relative);
        static Uri resourceUri35 = new Uri("/Psyc58H Images/35.jpg", UriKind.Relative);
        static Uri resourceUri36 = new Uri("/Psyc58H Images/36.jpg", UriKind.Relative);

        private BitmapImage[] imgEyesArray = {
            new BitmapImage(resourceUri9),
            new BitmapImage(resourceUri12),
            new BitmapImage(resourceUri19),
            new BitmapImage(resourceUri22),
            new BitmapImage(resourceUri24),
            new BitmapImage(resourceUri27),
            new BitmapImage(resourceUri32),
            new BitmapImage(resourceUri33),
            new BitmapImage(resourceUri35),
            new BitmapImage(resourceUri36),

        };

        private String[,] AnswersArray = new String[,] {
            {"annoyed","hostile","horrified","preoccupied" }, // 9
            {"indifferent","embarassed","skeptical","dispirited" }, // 12
            {"arrogant","grataeful","sarcastic","tentative" }, //19
            {"preoccupied","grateful","insisting","imploring" }, // 22
            {"pensive","irritated","excited","hostile" }, // 24
            {"joking","cautious","arrogant","reassuring" }, // 27
            {"serious","ashamed","bewildered","alarmed"}, // 32
            {"embarassed","guilty","fantasizing","concerned" }, // 33
            {"puzzled","nervous","insisting","contemplative" }, // 35
            {"ashamed","nervous","suspicious","indecisive" } // 36
        }; 

        private int imgIndex = -1;
        private static System.Timers.Timer imgTimer = new System.Timers.Timer();
        private static DispatcherTimer imgDispatcherTimer = new System.Windows.Threading.DispatcherTimer(DispatcherPriority.Send);

        private BitmapImage blankImage = new BitmapImage(new Uri("/Psyc58H Images/9", UriKind.Relative));

        private static DateTime timerStart;

        public MainWindow()
        {
            InitializeComponent(); // default constructor of a Window: calls System.Windows.Application.LoadComponent
            // This reads the BAML and builds the window

            // ALL CONSTRUCTORS must call InitializeComponent()  !!!

            // Grid grid1 = new Grid();

            /*
            imgEyesList.Add("9", new BitmapImage(new Uri("/Psyc58H Images/9", UriKind.Relative)));
            imgEyesList.Add("12", new BitmapImage(new Uri("/Psyc58H Images/12", UriKind.Relative)));
            imgEyesList.Add("19", new BitmapImage(new Uri("/Psyc58H Images/19", UriKind.Relative)));
            imgEyesList.Add("22", new BitmapImage(new Uri("/Psyc58H Images/22", UriKind.Relative)));
            imgEyesList.Add("24", new BitmapImage(new Uri("/Psyc58H Images/24", UriKind.Relative)));
            imgEyesList.Add("27", new BitmapImage(new Uri("/Psyc58H Images/27", UriKind.Relative)));
            imgEyesList.Add("32", new BitmapImage(new Uri("/Psyc58H Images/32", UriKind.Relative)));
            imgEyesList.Add("33", new BitmapImage(new Uri("/Psyc58H Images/33", UriKind.Relative)));
            imgEyesList.Add("35", new BitmapImage(new Uri("/Psyc58H Images/35", UriKind.Relative)));
            imgEyesList.Add("36", new BitmapImage(new Uri("/Psyc58H Images/36", UriKind.Relative)));
            */

            // Binding imgTimerBinding = new Binding("imgTimer");


            MainDockPanel.MouseUp += new MouseButtonEventHandler(MainDockPanel_MouseUp);

            // imgEyes.Source = imgEyesArray[0];
            imgTimer.Elapsed += onImgTimerEnd;
            imgDispatcherTimer.Tick += updateimgTimerDisplay;
            
            foreach (var control in AnswerChoices.Children)
            {
                RadioButton radio = control as RadioButton;

                radio.Click += onAnswerSelected;
            }

            LinearGradientBrush unusedFancyBrush = new LinearGradientBrush();

            unusedFancyBrush.GradientStops.Add(new GradientStop(offset:0.0, color: Colors.Blue));
            unusedFancyBrush.GradientStops.Add(new GradientStop(offset: 0.3, color: Colors.ForestGreen));
            unusedFancyBrush.GradientStops.Add(new GradientStop(offset: 1.0, color: Colors.Purple));


            grid1.Background = new SolidColorBrush(Colors.Beige);

            //Button button1 = new Button();
            button1.Foreground = SystemColors.ActiveCaptionBrush;

            textBox2.Text = this.FindResource("AppResource").ToString();
            //  textBox1.TextWrapping = TextWrapping.Wrap;

            /*
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();

            grid1.RowDefinitions.Add(row1);
            grid1.RowDefinitions.Add(row2);
            grid1.RowDefinitions.Add(row3);
            */
            // Grid.SetRow(button1, 1);

            // grid1.Children.Add(button1);

            //var filesList = Directory.GetFiles("/Psyc58H Images");
            //eyesList.ItemsSource = filesList.ToList();

        }

        private void onAnswerSelected(object sender, RoutedEventArgs e)
        {
            nextImgButton.IsEnabled = true;
        }


        /*
         * private void imgEyes_MouseUp(object sender, MouseButtonEventArgs e)
        {
            nextImage();

        }
        */

        private void nextImgButton_Click(object sender, RoutedEventArgs e)
        {
            imgIndex++;

            if (imgIndex == 0)
            {
                nextImgButton.IsEnabled = false;
                Choice1.IsEnabled = true;
                Choice2.IsEnabled = true;
                Choice3.IsEnabled = true;
                Choice4.IsEnabled = true;
                nextImage();
                updateAnswerChoices();
                return;
            }

            nextImgButton.IsEnabled = false;

            try
            {
                var checkedAnswer = AnswerChoices.Children.OfType<RadioButton>().FirstOrDefault(p => (p.IsChecked == true));
                if (checkedAnswer != null)
                {
                    ListBox1.Items.Add(checkedAnswer.Content.ToString());
                    checkedAnswer.IsChecked = false;

                } else { return; }
            }
            catch (InvalidCastException _e)
            {
                return;
            }

            if (imgIndex < imgEyesArray.Length)
            {
                nextImage();
                updateAnswerChoices();

                return;
            }

            nextImgButton.IsEnabled = false;
            imgEyes.Visibility = Visibility.Hidden;
            imgTimerDisplay.Visibility = Visibility.Hidden;
            resetAnswerChoices();
        }

        // get radio button checked - copied from somewhere else
        /*
        RadioButton getCheckedRadio(Control container, String groupName = "no group")
        {
            foreach (var control in container.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked && (groupName == "no group" || radio.GroupName == groupName))
                {
                    return radio;
                }
            }
            return null;
        }
        */

        private void updateAnswerChoices()
        {

            Choice1.Content = AnswersArray[imgIndex, 0];
            Choice2.Content = AnswersArray[imgIndex, 1];
            Choice3.Content = AnswersArray[imgIndex, 2];
            Choice4.Content = AnswersArray[imgIndex, 3];

            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => EventsListBox.Items.Add("update answer choices")));
        }

        private void resetAnswerChoices()
        {
            Choice1.Content = "";
            Choice2.Content = "";
            Choice3.Content = "";
            Choice4.Content = "";
            Choice1.IsEnabled = false;
            Choice2.IsEnabled = false;
            Choice3.IsEnabled = false;
            Choice4.IsEnabled = false;
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => EventsListBox.Items.Add("reset answer choices")));
        }

        private void nextImage()
        {
            imgEyes.Visibility = Visibility.Visible;

            imgTimer.Interval = 3000;
            imgTimer.AutoReset = false;

            timerStart = DateTime.Now;
            // imgTimer.Elapsed += onImgTimerEnd; // moved to main method

            // imgDispatcherTimer.Tick += // same here
            imgDispatcherTimer.Interval = new TimeSpan(hours: 0, minutes: 0, seconds: 1);

            if (imgIndex == imgEyesArray.Length)
            {
                imgIndex = 0;
            }

            // imgEyesArray[imgIndex].Freeze();
            imgEyes.Source = imgEyesArray[imgIndex];

            imgTimer.Start();
            imgDispatcherTimer.Start();
            imgTimerDisplay.Content = "3";
            EventsListBox.Items.Add("nextImage called");
            
        }

        private void onImgTimerEnd(object sender, ElapsedEventArgs e)
        {
            // EventsListBox.Items.Add("timer end");
            imgTimer.Stop();
            imgDispatcherTimer.Stop();
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new Action(() => imgTimerDisplay.Content = ""));
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => EventsListBox.Items.Add("timer end")));
            hideImage();

            
        }

        private void updateimgTimerDisplay(object sender, EventArgs e)
        {
            imgTimerDisplay.Content = (Convert.ToString(Convert.ToInt16((timerStart - DateTime.Now + new TimeSpan(0, 0, 3)).TotalSeconds)));
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => EventsListBox.Items.Add("update img timer display")));

            // updateImgButton.Content = Convert.ToString(DateTime.Now - timerStart);
        }

        private void hideImage()
        {
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new Action(() => imgEyes.Visibility = Visibility.Hidden));
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => EventsListBox.Items.Add("hideImage called")));
            //imgEyes.Visibility = Visibility.Hidden;
            //imgEyes.Source.Dispatcher.Invoke(() => imgEyes.Visibility = Visibility.Hidden);
            
            imgTimer.Stop();
        }

        private void MainDockPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ResourceDisplayer.Text += '\n' + e.GetPosition(this).ToString();
            //MessageBox.Show(this.FindResource("ComboBoxItems").ToString());
        }

        private void button1_click(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Add(ComboBox1.SelectedItem);
            
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void hideRightGridChecked(object sender, RoutedEventArgs e)
        {
            rightgrid.Visibility = Visibility.Collapsed;
        }

        private void hideRightGridUnchecked(object sender, RoutedEventArgs e)
        {
            rightgrid.Visibility = Visibility.Visible;
        }


        // Can use if don't need Send priority, I guess? Not sure
        public static void InvokeIfNecessary(Action action)
        {
            if (Thread.CurrentThread == Application.Current.Dispatcher.Thread)
                action();
            else
            {
                Application.Current.Dispatcher.Invoke(action);
            }
        }



    }
}
