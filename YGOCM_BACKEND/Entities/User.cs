namespace YGOCM_BACKEND.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Collection information - owns many CollectionEntries
        public ICollection<CollectionEntry> Collection = new List<CollectionEntry>();
    }
}
