namespace RentingSystemAPI.Interfaces
{
    public interface IItem
    {
        public int ItemId { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
    }
}