namespace Labb3._1
{
    public class Användare
    {
        public string Namn { get; set; }

        public Användare(string namn)
        {
            Namn = namn;
        }

        public override string ToString()
        {
            return Namn;
        }
    }
}