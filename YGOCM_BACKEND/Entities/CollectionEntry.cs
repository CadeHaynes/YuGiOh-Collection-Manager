namespace YGOCM_BACKEND.Entities
{
    public class CollectionEntry
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        
        // Card information - references Card entity
        public int CardId { get; set; }
        public Card Card { get; set; }

        // User information - owned by a User entity
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
