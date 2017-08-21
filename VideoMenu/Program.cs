using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoMenu
{
    internal class Program
    {
        private static int movieId;
        public static List<string> DefaultMenuItems { get; private set; }
        public static List<Movie> MovieList { get; private set; }
        public static List<Movie> SearchedMovieList { get; private set; }

        private static void Main(string[] args)
        {
            MovieList = new List<Movie>();
            SearchedMovieList = new List<Movie>();

            MainMenu();

            Console.ReadLine();
        }

        /// <summary>
        ///     Loads the main menu view.
        /// </summary>
        public static void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome to MovieBuster, your personal movie database.\n\n");

            var AddMovie = "Add a movie to the database.";
            var ListMovie = "List all movies in the database.";
            var DeleteMovie = "Delete a movie from the database.";
            var UpdateMovie = "Update a movie's information e.g. the movie title.";
            var SearchMovie = "Search for a desired movie from the database.";
            DefaultMenuItems = new List<string> {AddMovie, ListMovie, DeleteMovie, UpdateMovie, SearchMovie};

            var i = 0;
            foreach (var defaultMenuItem in DefaultMenuItems)
            {
                i++;
                Console.WriteLine("{0} - {1}", i, defaultMenuItem + "\n");
            }

            HandleUserInput();
        }

        /// <summary>
        ///     Adds a movie to the movie list.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="releaseYear"></param>
        public static void AddMovie()
        {
            Console.WriteLine("Title:");
            var title = Console.ReadLine();
            Console.WriteLine("Genre:");
            var genre = Console.ReadLine();
            Console.WriteLine("Release Year:");
            uint releaseYear;
            var success = uint.TryParse(Console.ReadLine(), out releaseYear);
            if (success)
                MovieList.Add(new Movie(movieId, title, genre, releaseYear));
            else
                MovieList.Add(new Movie(movieId, title, genre, 0));

            movieId++;
        }

        public static void HandleUserInput()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    AddMovie();
                    break;
                case "2":
                    ListMovies(MovieList);
                    break;

                case "3":
                    DeleteMovie();
                    break;

                case "4":
                    UpdateMovie();
                    break;

                case "5":
                    SearchMovie();
                    break;

                default:
                    Console.WriteLine("Oops! That's not a category!");
                    Console.ReadLine();
                    MainMenu();
                    return;
            }

            Console.WriteLine("Press the return key to return to the main menu.");
            Console.ReadLine();
            MainMenu();
        }

        private static void SearchMovie()
        {
            Console.WriteLine("Search for a movie:");
            var searchQuery = Console.ReadLine();
            if (searchQuery != null)
            {
                SearchedMovieList.AddRange(MovieList.Where(m =>
                    m.Title.ToLower().Contains(searchQuery.ToLower())));
                ListMovies(SearchedMovieList);
            }

            SearchedMovieList.Clear();
        }

        private static void UpdateMovie()
        {
            Console.WriteLine("Use a movie's Id to update its content.\n");
            ListMovies(MovieList);

            Movie movie = null;

            int movieId;
            var idSuccess = int.TryParse(Console.ReadLine(), out movieId);
            if (idSuccess)
                movie = MovieList.FirstOrDefault(m => m.Id == movieId);
            else
                MainMenu();

            Console.WriteLine("Title:");
            var title = Console.ReadLine();
            Console.WriteLine("Genre:");
            var genre = Console.ReadLine();
            Console.WriteLine("Release Year:");
            uint releaseYear;
            var success = uint.TryParse(Console.ReadLine(), out releaseYear);
            if (success)
            {
                movie.Title = title;
                movie.Genre = genre;
                movie.ReleaseYear = releaseYear;
            }
            else
            {
                movie.Title = title;
                movie.Genre = genre;
                movie.ReleaseYear = 0;
            }
        }

        private static void DeleteMovie()
        {
            Console.WriteLine("Delete a movie by its Id.\n");
            ListMovies(MovieList);
            int movieId;
            var success = int.TryParse(Console.ReadLine(), out movieId);
            if (success)
                MovieList.Remove(MovieList.FirstOrDefault(m => m.Id == movieId));
        }

        private static void ListMovies(List<Movie> movieList)
        {
            foreach (var movie in movieList)
                Console.WriteLine("{0}, \tTitle: {1}\n  \tGenre: {2}\n  \tRelease Year: {3}\n", movie.Id, movie.Title,
                    movie.Genre, movie.ReleaseYear);
        }
    }
}