namespace SuperHeroAPI.Entities
{
    public class SuperHero
    {
        public int Id {get; set;}
        public required string Name { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Place { get; set; }
    }
}