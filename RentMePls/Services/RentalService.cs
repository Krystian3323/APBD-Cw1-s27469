using RentMePls.Domain;

namespace RentMePls.Services;

public class RentalService
{
    private List<Rental> _rentals = new();
    private List<Hardware> _inventory = new();
    private List<User> _users = new();
    
    public void AddUser(User user)
    {
        _users.Add(user);
    }
    
    public void AddHardware(Hardware hardware)
    {
        _inventory.Add(hardware);
    }

    public void RentItem(User user, Hardware item)
    {
        if (!item.IsAvailable)
        {
            throw new Exception("Item is currently unavailable (rented or in service)!");
        }
        
        int activeRentals = _rentals.Count(r => r.Renter == user && r.DateReturned == null);
    
        if (activeRentals >= user.MaxRentalLimit)
        {
            throw new Exception($"Rental limit exceeded! {user.Role}s can have maximum {user.MaxRentalLimit} items.");
        }
    
        var rental = new Rental(user, item, 7);
        _rentals.Add(rental);
        item.IsAvailable = false;
        Console.WriteLine($"SUCCESS: {user.FirstName} rented {item.Name}. Due date: {rental.DueDate.ToShortDateString()}.");
    }

    public void ReturnItem(Hardware item)
    {
        var rental = _rentals.FirstOrDefault(r => r.Item == item && r.DateReturned == null);

        if (rental == null)
        {
            throw new Exception("This item is not currently rented or does not exist in our records! ");
        }
        
        rental.MarkAsReturned();
        
        decimal fine = rental.CalculateFine();

        if (fine > 0)
        {
            Console.WriteLine($"Item '{item.Name}' returned LATE. Penalty: {fine} PLN.");
        }
        else
        {
            Console.WriteLine($"Item '{item.Name}' returned on time. No penalty.");
        }
    }

    public void ShowStatusReport()
    {
        Console.WriteLine("\n-------- STATUS RAPORT --------");
        foreach (var item in _inventory)
        {
            string status = item.IsAvailable ? "Available" : "Rented";
            Console.WriteLine($"[{item.Id}] {item.getDetails()} - STATUS: {status}");
            
        }
    }
    
    public void ShowOverdueReport()
    {
        var overdueRentals = _rentals.Where(r => r.DateReturned == null && DateTime.Now > r.DueDate).ToList();

        Console.WriteLine("\n--- OVERDUE RENTALS REPORT ---");
        if (!overdueRentals.Any())
        {
            Console.WriteLine("No overdue items. All users are disciplined!");
            return;
        }

        foreach (var r in overdueRentals)
        {
            int daysLate = (DateTime.Now - r.DueDate).Days;
            Console.WriteLine($"USER: {r.Renter.LastName} | ITEM: {r.Item.Name} | DAYS LATE: {daysLate} | FINE: {r.CalculateFine()} PLN");
        }
    }
    
    public void ShowAvailableHardware()
    {
        Console.WriteLine("\n--- AVAILABLE HARDWARE ---");
        var available = _inventory.Where(h => h.IsAvailable).ToList();
        if (!available.Any()) Console.WriteLine("None available.");
        foreach (var item in available) Console.WriteLine(item.getDetails());
    }
    
    public void ShowUserRentals(User user)
    {
        Console.WriteLine($"\n--- ACTIVE RENTALS FOR {user.FirstName} {user.LastName} ---");
        var userRentals = _rentals.Where(r => r.Renter == user && r.DateReturned == null).ToList();
        foreach (var r in userRentals) Console.WriteLine($"- {r.Item.Name} (Due: {r.DueDate.ToShortDateString()})");
    }
    
    
    
}