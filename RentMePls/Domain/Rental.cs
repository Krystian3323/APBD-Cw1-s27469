namespace RentMePls.Domain;

public class Rental
{
    public User Renter { get;}
    public Hardware Item { get;}
    public DateTime DateRented { get; }
    public DateTime DueDate { get; }
    public DateTime? DateReturned { get; private set; }

    public Rental(User renter, Hardware item, int daysForRental)
    {
        Renter = renter;
        Item = item;
        DateRented = DateTime.Now;
        DueDate = DateRented.AddDays(daysForRental);
        Item.IsAvailable = false;
    }

    public decimal CalculateFine()
    {
        DateTime effectiveReturnDate = DateReturned ?? DateTime.Now;

        if (effectiveReturnDate > DueDate)
        {
            int delay = (effectiveReturnDate - DueDate).Days;
            return delay * 10.0m;
        }
        return 0;
    }
    
    public void MarkAsReturned()
    {
        DateReturned = DateTime.Now;
        Item.IsAvailable = true;
    }
}