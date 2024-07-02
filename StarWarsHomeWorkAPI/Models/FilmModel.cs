namespace StarWarsHomeWorkAPI.Models
{

    public class FilmModel
    {
        public string Title { get; set; }
        public int Episode_Id { get; set; }
        public string Opening_Crawl { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public string Release_Date { get; set; }
        public List<string> Characters { get; set; } = new List<string>();
        public List<string> Planets { get; set; } = new List<string>();
        public List<string> Starships { get; set; } = new List<string>();
        public List<string> Vehicles { get; set; } = new List<string>();
        public List<string> Species { get; set; } = new List<string>();
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string Url { get; set; }
    }

}
