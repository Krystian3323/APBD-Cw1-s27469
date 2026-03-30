namespace RentMePls.Domain;

public class Laptop : Hardware
{
    
    public int Ram { get; set; }
    public int Hdd { get; set; }
    
    
    public Laptop(string name, string brand, string serialNumber, int ram, int hdd) : base(name, brand, serialNumber)
    {
        if (ram < 2 || hdd < 20)
        {
            throw new ArgumentException("WTF is this thing?");
        }
        Ram = ram;
        Hdd = hdd;
    }
    
    public override string getDetails()
    {
        return $"{Name} - {Brand} - SerialNumber: {SerialNumber} - {Ram}GB RAM - {Hdd}GB HDD";
    }
    
}