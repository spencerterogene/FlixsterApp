using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlixsterApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDetails : ContentPage
    {

        public static Film currentFilm;
        public const String VIDEO_URL = "https://api.themoviedb.org/3/movie/{0}/videos?api_key=a07e22bc18f5cb106bfe4cc1f83ad8ed";
        public PageDetails(Film film)
        {

            InitializeComponent();
            currentFilm = film;

            
            Video.Source = "https://www.youtube.comwatch?v=" + getYoutubeKey();
            lblTitle.Text = currentFilm.title;
            lblOverView.Text = currentFilm.overview;
            lblOriginale_Language.Text = currentFilm.original_language;
            lblRelease_date.Text = currentFilm.release_date;
            lblPopularity.Text = Convert.ToString(currentFilm.popularity);
            lblVote_Average.Text = Convert.ToString(currentFilm.vote_average);
            lblVote_Count.Text = Convert.ToString(currentFilm.vote_count);
        }

        private String getYoutubeKey()
        {

            String reponse = "";
            String youtubeKey = "";

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    reponse = webClient.DownloadString(String.Format(VIDEO_URL, currentFilm.id));
                }

                using (JsonDocument document = JsonDocument.Parse(reponse))
                {
                    JsonElement root = document.RootElement;
                    JsonElement resultsList = root.GetProperty("results");

                    int i = 0;
                    while (true)
                    {
                        String type = resultsList[i].GetProperty("type").ToString();
                        youtubeKey = resultsList[i].GetProperty("key").ToString();
                        if (type.Equals("Clip"))
                        {
                            break;
                        }
                        i++;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return youtubeKey;
        }
    }
}