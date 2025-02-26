using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;
using MessageBox = System.Windows.MessageBox;

namespace ScreenshotApp
{
    public partial class MainWindow : Window
    {
        private string screenshotFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Screenshots");
        

        private static readonly string botToken = "yourbootToken"; // 🔹 Replace with your Telegram Bot Token
        private static readonly long chatId = 00000000; // 🔹 Replace with your Chat ID (as long)

        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory(screenshotFolder);
        }

        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    screenshotFolder = dialog.SelectedPath;
                    System.Windows.MessageBox.Show($"Save location changed to: {screenshotFolder}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private string latestScreenshotFilePath;

        private void TakeScreenshot_Click(object sender, RoutedEventArgs e)
        {
            latestScreenshotFilePath = Path.Combine(screenshotFolder, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            TakeScreenshot(latestScreenshotFilePath);
            ShowScreenshot(latestScreenshotFilePath);
        }

        private static void TakeScreenshot(string filePath)
        {
            int screenWidth = (int)SystemParameters.PrimaryScreenWidth;
            int screenHeight = (int)SystemParameters.PrimaryScreenHeight;

            using (Bitmap bmp = new Bitmap(screenWidth, screenHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                }
                bmp.Save(filePath, ImageFormat.Png);
            }
        }

        private void TakeScreenshotArea_Click(object sender, RoutedEventArgs e)
        {
            using (AreaSelectionForm areaForm = new AreaSelectionForm())
            {
                if (areaForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Rectangle selectedArea = areaForm.SelectedRectangle;

                    if (selectedArea.Width > 0 && selectedArea.Height > 0)
                    {
                        string filePath = Path.Combine(screenshotFolder, $"area_screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                        TakeScreenshotArea(filePath, selectedArea);
                        ShowScreenshot(filePath);

                        // Update the latestScreenshotFilePath with the area screenshot path
                        latestScreenshotFilePath = filePath;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Invalid selection! Please select a valid area.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }


        private static void TakeScreenshotArea(string filePath, Rectangle area)
        {
            // Adjust for negative width/height in case of reverse dragging
            int x = Math.Min(area.Left, area.Right);
            int y = Math.Min(area.Top, area.Bottom);
            int width = Math.Abs(area.Width);
            int height = Math.Abs(area.Height);

            using (Bitmap bmp = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height));
                }
                bmp.Save(filePath, ImageFormat.Png);
            }
        }


        private void ShowScreenshot(string filePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            ScreenshotImage.Source = bitmap;
        }

        private static async Task SendToTelegram(string filePath)
        {
            var botClient = new TelegramBotClient(botToken);

            try
            {
                using (var fileStream = File.Open(filePath, FileMode.Open))
                {
                    await botClient.SendPhotoAsync(chatId, new InputFileStream(fileStream, Path.GetFileName(filePath)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending screenshot: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void SendScreenshot_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(latestScreenshotFilePath) && File.Exists(latestScreenshotFilePath))
            {
                await SendToTelegram(latestScreenshotFilePath);
                System.Windows.MessageBox.Show("Screenshot sent to Telegram!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Screenshot file not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
