using RentMePls.Domain;
using RentMePls.Services;

public class Program
{
    public static void Main(string[] args)
    {
        RentalService service = new RentalService();

        var laptop = new Laptop("ThinkPad T14", "Lenovo", "SN-LT-001", 16, 512);
        var camera = new Camera("EOS R5", "Canon", "SN-CAM-099", "4K", "24-105mm");
        var printer = new Printer("LaserJet Pro", "HP", "SN-PR-555", false, "A4");

        service.AddHardware(laptop);
        service.AddHardware(camera);
        service.AddHardware(printer);

        var student = new User("Jan", "Kowalski", "student");
        var employee = new User("Anna", "Nowak", "employee");

        service.AddUser(student);
        service.AddUser(employee);

        Console.WriteLine("--- TEST 1: Valid Rental ---");
        service.RentItem(student, laptop);

        Console.WriteLine("\n--- TEST 2: Double Rental Same Item ---");
        try 
        {
            service.RentItem(employee, laptop);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\n--- TEST 3: Student Limit (Max 2) ---");
        try
        {
            service.RentItem(student, camera);
            service.RentItem(student, printer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\n--- TEST 4: User Active Rentals ---");
        service.ShowUserRentals(student);

        Console.WriteLine("\n--- TEST 5: Available Hardware Only ---");
        service.ShowAvailableHardware();

        Console.WriteLine("\n--- TEST 6: Return Item ---");
        service.ReturnItem(laptop);

        Console.WriteLine("\n--- TEST 7: Overdue Report ---");
        service.ShowOverdueReport();

        Console.WriteLine("\n--- TEST 8: Final Status Report ---");
        service.ShowStatusReport();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}