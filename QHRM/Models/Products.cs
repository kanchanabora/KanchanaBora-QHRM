namespace QHRM.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        //public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;


    }
}
