namespace RentingSystemAPI.BAL.Entities
{
    public interface IItem
    {
        int ItemId { get; set; }
        int Quantity { get; set; }
    }
}