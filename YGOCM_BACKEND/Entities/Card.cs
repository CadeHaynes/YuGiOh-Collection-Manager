namespace YGOCM_BACKEND.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string YgoProId { get; set; }
        public string CardType { get; set; }
        public string Description { get; set; }

        public string? MonsterType { get; set; }
        public string? MonsterAttribute { get; set; }
        public int? MonsterLevel { get; set; }
        public int? MonsterAttack { get; set; }
        public int? MonsterDefense { get; set; }
    }
}
