using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlixsterApp
{
    public partial class MainPage : ContentPage
    {
        // Declaration des attributs de la classe
        public static int index = 0;
        public static Film currentFilm;
        public static List<Film> Movies ;
        public MainPage()
        {
            InitializeComponent();
            Internet();
            display(index);
        }

        //  Download les Films sur l'internet et  Afficher Les Films dans le forme
        public void display(int index)
        {
            if (index < 0)
            {
                Previous.IsEnabled = false;
                Next.IsEnabled = true;
            }
            else if (index == Movies.Count)
            {
                Next.IsEnabled = false;
                Previous.IsEnabled = true;
            }
            else
            { 
                if (Utilities.IsConnectedToInternet())
                {
                    Panel.BackgroundColor = Color.Blue;
                    currentFilm = Movies.ElementAt(index);
                    lblTitle.Text = currentFilm.title;
                    lblOverView.Text = currentFilm.overview;
                    Image.Source = ImageSource.FromUri(new Uri("https://image.tmdb.org/t/p/w342" + currentFilm.backdrop_path));
                }
                else
                {
                    Panel.BackgroundColor = Color.Red;
                    currentFilm = Movies[index];
                    lblTitle.Text = currentFilm.title;
                    lblOverView.Text = currentFilm.overview;
                    Image.Source = currentFilm.backdrop_path;
                }
            }
        }

        private void Previous_Clicked(object sender, EventArgs e)
        {
            index--;
            display(index);
        }

        private void Next_Clicked(object sender, EventArgs e)
        {
            index++;
            display(index);
        }
         public static void Internet()
        {
            SQLiteDatabase.CreateTable();

            if (Utilities.IsConnectedToInternet())
            {
                Movies = Utilities.getMovieDbList();
                foreach (Film film in Movies)
                {
                    SQLiteDatabase.Sauvegarder(film);
                }
            }
            else
            {
                Movies = SQLiteDatabase.TakeToDataBase();
            }

        }
        
         private async void Image_Clicked(object sender, EventArgs e)
         {
            await Navigation.PushModalAsync(new PageDetails(currentFilm));
         }
    }

}
