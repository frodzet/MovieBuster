namespace VideoMenu
{
    public class Movie
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public uint ReleaseYear { get; set; }

        public Movie(int id, string title, string genre, uint releaseYear)
        {
            Id = id;
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
        }

    }
}