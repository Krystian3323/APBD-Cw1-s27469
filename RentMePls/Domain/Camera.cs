namespace RentMePls.Domain;

public class Camera : Hardware
{
    public string Resolution { get; set; }
    public string LensType { get; set; }
    
    public Camera(string name, string brand, string serialNumber, string resolution, string lensType) : base(name, brand, serialNumber)
    {
        if (string.IsNullOrEmpty(resolution) || string.IsNullOrEmpty(lensType))
        {
            throw new ArgumentException("All fields are required! ");
        }
        Resolution = resolution;
        LensType = lensType;
    }
    
    public override string getDetails()
    {
        return $"{Name} - {Brand} - SerialNumber: {SerialNumber} - Resolution {Resolution} - Lens: {LensType}";
    }
}