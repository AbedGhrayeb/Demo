namespace Demo.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
        public virtual AppUser User { get; set; }
        public string UserId { get; set; }
    }
}