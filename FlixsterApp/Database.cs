using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Shapes;
using Path = System.IO.Path;

namespace FlixsterApp
{
    public class SQLiteDatabase
    {

        
        public static SQLiteConnection Connection()
        {

            String libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(libraryPath, "Film.db3");

            // Create Sqlite database connection
            SQLiteConnection connection = new SQLiteConnection(path);

            
            return connection;
        }


        public static void CreateTable()
        {
            SQLiteConnection connection = Connection();
            connection.CreateTable<Film>();
        }


        public static void Sauvegarder(Film film)
        {
            Connection().Insert(film);
            
        }

        public static List<Film> TakeToDataBase()
        {
            List<Film> films = new List<Film>();
            SQLiteConnection connection = Connection();
            var movie = Connection().Table<Film>();

            foreach (Film film in movie)
            {
                films.Add(film);
            }
            return films;
        }

    }
}

