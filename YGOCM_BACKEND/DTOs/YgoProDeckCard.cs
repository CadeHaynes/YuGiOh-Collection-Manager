namespace YGOCM_BACKEND.DTOs
{
    public class YgoProDeckCard
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }

        public string? Race { get; set; }
        public string? Attribute { get; set; }
        public int? Level { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
    }
}
