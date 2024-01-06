namespace HoteListing.API.Data
{
    public class Country
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ShortName { get; set; }


        public virtual IList<Hotel>? Hotels { get; set;}
        
    }
}