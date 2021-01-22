namespace Core.Entities.Identity
{
    public class Adress
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Strada { get; set; }
        public string Oras { get; set; }
        public string CodPostal { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}