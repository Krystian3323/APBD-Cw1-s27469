namespace RentMePls.Domain;

public abstract class Hardware
{
    public Guid Id { get;} = Guid.NewGuid();
    public string Name { get; set; }
    public string Brand { get; set; }
    public string SerialNumber { get; set; }
    public bool IsAvailable { get; set; } = true;



    protected Hardware(string name, string brand, string serialNumber)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(serialNumber))
        {
            throw new ArgumentException("All fields are required! ");
        }
        Name = name;
        Brand = brand;
        SerialNumber = serialNumber;
    }


    public abstract string getDetails();


}