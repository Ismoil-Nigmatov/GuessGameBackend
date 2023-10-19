namespace GuessNumber.Entity
{
    public class Game
    {
        public int Id { get; set; }
        public int Attempts { get; set; }
        public bool Win { get; set; }
        public List<string> Description { get; set; }
    }
}
