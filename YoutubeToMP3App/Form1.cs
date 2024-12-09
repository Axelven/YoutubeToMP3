using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;

namespace YoutubeToMP3App
{
    public partial class Form1 : Form 
    {
        string APILoc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "env\\private.json");
        string destination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Songs");
        string APIKey;

        public Form1()
        {
            InitializeComponent();
            if (File.Exists(APILoc))
            {
                var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(APILoc));
                APIKey = config["APIKey"];
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //PopulateItems();
            if(!Directory.Exists(destination)) Directory.CreateDirectory(destination);
        }

        private async void buttonConvert_Click(object sender, EventArgs e)
        {
            if (textBoxURLOrSongName.Text == "")
                return;

            try
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = APIKey,
                    ApplicationName = "YoutubeToMP3App"
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = textBoxURLOrSongName.Text;
                searchListRequest.Type = "video";
                searchListRequest.MaxResults = 10;

                var searchListResponse = await searchListRequest.ExecuteAsync();
                ResultList[] resultList = new ResultList[10];
                int i = 0;
                if (flowLayoutPanel1.Controls.Count > 0)
                    flowLayoutPanel1.Controls.Clear();
                foreach (var searchResult in searchListResponse.Items)
                {
                    switch (searchResult.Id.Kind)
                    {
                        case "youtube#video":
                            resultList[i] = new ResultList(destination,() => checkBoxVideo.Checked);
                            resultList[i].Title = HttpUtility.HtmlDecode(searchResult.Snippet.Title);
                            resultList[i].URL = $"https://www.youtube.com/watch?v={searchResult.Id.VideoId}";
                            resultList[i].Thumbnail = GetImageFromURL(searchResult.Snippet.Thumbnails.Default__.Url);
                            
                            flowLayoutPanel1.Controls.Add(resultList[i]);
                            i++;
                            break;
                    }
                }
                AdjustFormSize();
            }
            catch (Exception error) { MessageBox.Show($"Error occured: {error.Message}"); }
        }

        private static Image GetImageFromURL(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                byte[] imageData = webClient.DownloadData(url);

                using (var memoryStream = new System.IO.MemoryStream(imageData))
                {
                    Image image = Image.FromStream(memoryStream);
                    return image;
                }
            }
            catch(Exception error) { MessageBox.Show("Couldn't convert the image due to: " + error.Message);  return null; }
        }

        private void AdjustFormSize()
        {
            //int totalHeight = flowLayoutPanel1.Controls.Cast<Control>().Sum(c => c.Height + c.Margin.Vertical);
            int totalWidth = flowLayoutPanel1.Controls.Cast<Control>().Max(c => c.Width + c.Margin.Horizontal);

            this.Width = Math.Min(totalWidth + 40, Screen.PrimaryScreen.WorkingArea.Width);
            //this.Height = Math.Min(totalHeight + 40, Screen.PrimaryScreen.WorkingArea.Height);

            //flowLayoutPanel1.AutoScroll = this.Height == Screen.PrimaryScreen.WorkingArea.Height || this.Width == Screen.PrimaryScreen.WorkingArea.Width;

            this.MinimumSize = new Size(900, 550);
        }
    }
}
