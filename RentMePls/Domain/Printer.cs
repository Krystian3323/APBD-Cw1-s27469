namespace RentMePls.Domain;

public class Printer : Hardware
{
    public bool IsColor { get; set; }
    public string Format { get; set; }

    public Printer(string name, string brand, string serialNumber, bool isColor, string format) : base(name, brand, serialNumber)
    {
        if (string.IsNullOrEmpty(format))
        {
            throw new ArgumentException("All fields are required! ");
        }
        IsColor = isColor;
        Format = format;
    }
    
    public override string getDetails()
    {
        return $"{Name} - {Brand} - SerialNumber: {SerialNumber} - Print in color: {IsColor} - Format : {Format}";
    }
    
}