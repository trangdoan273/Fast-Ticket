namespace TICKETBOX.Models{
    public class SelectShowtimesModel{
        public int Id{get; set;}
        public string MovieName{get; set;}
        public string Content{get; set;}
        public string Director{get; set;}
        public string Actor{get; set;}
        public string Genre{get; set;}
        public string ReleaseDate{get; set;}
        public string Duration{get; set;}
        public string MovieImage{get; set;}
        public List<string> ShowDates{get;set;}
        public List<string> ShowTimes{get;set;}
        public List<string> Seat{get;set;}
        public int Price {get; set;}
    }
}
