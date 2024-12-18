using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
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
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "photos.txt");
        List<Photo> photos = new List<Photo>();
        Photo current_photo { get; set; }
        int index = 0;
        private System.Timers.Timer timer;
        bool slide = false;


        public MainWindow()
        {
            InitializeComponent();
            TxtFileHandler.CreatePhotoDatabase(path);

            photos = TxtFileHandler.GetPhotosData(path);

            DataContext = photos.Count > 0 ? photos[index] : null;

            // Inicjalizacja timera
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            if (photos.Count == 0)
            {
                return;
            }
                
            index = (index + 1) % photos.Count;
            UpdatePhoto();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (photos.Count == 0)
            {
                return;
            }
                
            index = (index - 1 + photos.Count) % photos.Count;
            UpdatePhoto();
        }

        private void UpdatePhoto()
        {
            current_photo = photos[index];
            this.DataContext = current_photo;
        }

        private void StartAutoSlide(object sender, RoutedEventArgs e)
        {
            slide = !slide;

            if (slide)
            {
                timer.Start();
                auto_slide_button.Content = "Stop";
                prev_photo_button.IsEnabled = false;
                next_photo_button.IsEnabled = false;
            }
            else
            {
                timer.Stop();
                auto_slide_button.Content = "Start";
                prev_photo_button.IsEnabled = true;
                next_photo_button.IsEnabled = true;
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (photos.Count == 0)
                return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                index = (index + 1) % photos.Count;
                UpdatePhoto();
            });
        }
    }
}