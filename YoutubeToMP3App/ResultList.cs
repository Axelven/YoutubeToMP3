using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeToMP3App
{
    public partial class ResultList : UserControl
    {
        private readonly string destination;
        private readonly Func<bool> isCheckBoxChecked;

        public ResultList(string _destination, Func<bool> _isCheckBoxChecked)
        {
            destination = _destination;
            isCheckBoxChecked = _isCheckBoxChecked;
            InitializeComponent();
        }

        #region Properties
        private string _title;
        private string _url;
        private Color _iconBackground;
        private Image _thumbnail;

        [Category("Custom Props")]
        public string Title
        {
            get { return _title; }
            set { _title = value; labelTitle.Text = "Title: " + value; AdjustLabelSize(labelTitle); }
        }

        [Category("Custom Props")]
        public string URL
        {
            get { return _url; }
            set { _url = value; linkLabelURL.Text = value; AdjustLabelSize(linkLabelURL); }
        }

        [Category("Custom Props")]
        public Color IconBackground
        {
            get { return _iconBackground; }
            set { _iconBackground = value; panel1.BackColor = value; }
        }

        [Category("Custom Props")]
        public Image Thumbnail
        {
            get { return _thumbnail; }
            set { _thumbnail = value; ThumbnailBox.Image = value; AdjustImageSize(ThumbnailBox); }
        }
        #endregion

        private void ResultList_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }

        private void ResultList_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }


        private async void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(URL))
                    await DownloadAndMergeAsync(URL);
                else
                    MessageBox.Show("No URL provided", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error) { MessageBox.Show("Error: " + error.Message); }
            
        }

        private async Task DownloadAndMergeAsync(string url)
        {
            try
            {
                // Configure FFmpeg
                FFmpeg.SetExecutablesPath(Path.Combine(Application.StartupPath, "FFmpeg"));                

                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(url);
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);

                // Sanitize filename
                string sanitizedTitle = ReplaceInvalidChars(video.Title);

                string audioPath = Path.Combine(destination, $"{sanitizedTitle}_audio.mp3");

                var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, audioPath);

                // Paths for video and audio downloads
                if (isCheckBoxChecked())
                {
                    string videoPath = Path.Combine(destination, $"{sanitizedTitle}_video.mp4");
                    string outputPath = Path.Combine(destination, $"{sanitizedTitle}_final.mp4");

                    // Download video stream (highest quality)
                    var videoStreamInfo = streamManifest.GetVideoStreams().GetWithHighestVideoQuality();
                    await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, videoPath);

                    // Merge video and audio
                    await MergeVideoAudioAsync(videoPath, audioPath, outputPath);

                    // Clean up temporary files
                    File.Delete(videoPath);
                    File.Delete(audioPath);

                    MessageBox.Show("Video downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task MergeVideoAudioAsync(string videoPath, string audioPath, string outputPath)
        {
            try
            {
                // Ensure we have the correct using for this method
                //TODO: Fix arguments to make better resolution for videos
                var conversion = await FFmpeg.Conversions.New()
                    .AddParameter($"-i \"{videoPath}\" -i \"{audioPath}\" -c:v mpeg4 -b:v 4000k -c:a aac -b:a 192k -shortest \"{outputPath}\"")
                    .Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Merge Error: {ex.Message}", "Merge Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ReplaceInvalidChars(string filePath)
        {
            return string.Join("_", filePath.Split(Path.GetInvalidFileNameChars()));
        }

        private void AdjustLabelSize(Label label)
        {
            using (Graphics g = label.CreateGraphics())
            {
                SizeF size = g.MeasureString(label.Text, label.Font);
                label.Width = (int)size.Width + 10;
                label.Height = (int)size.Height + 10;
            }
        }

        private void AdjustImageSize(PictureBox pictureBox)
        {
            int pictureBoxWidth = panel1.Width - 20;
            int pictureBoxHeight = panel1.Height - 20;

            if(pictureBox.Image.Width > pictureBox.Image.Height)
            {
                pictureBox.Width = pictureBoxWidth;
                pictureBox.Height = (int)((float)pictureBox.Image.Height / pictureBox.Image.Width * pictureBox.Width);
            }
            else
            {
                pictureBox.Height = pictureBoxHeight;
                pictureBox.Width = (int)((float)pictureBox.Image.Width / pictureBox.Image.Height * pictureBox.Height);
            }

            pictureBox.Left = (panel1.Width - pictureBox.Width) / 2;
            pictureBox.Top = (panel1.Height - pictureBox.Height) / 2 ;
        }

        private void linkLabelURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            string url = linkLabel.Text;
            System.Diagnostics.Process.Start(url);
        }

        private void labelTitle_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }
    }
}
