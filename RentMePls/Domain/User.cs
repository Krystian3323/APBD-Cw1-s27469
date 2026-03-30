namespace RentMePls.Domain;

public  class User
{
    public int Id { get; set; }
    private static int _counter = 1;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }


    public User(string firstName, string lastName, string role)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(role))
        {
            throw new ArgumentException("All fields are required! ");
        }
        
        string r = role.ToLower();
        
        if (r != "student" && r != "employee")
        {
            throw new ArgumentException("Role must be either student or employee! ");
        }

        Id = _counter++;
        FirstName = firstName;
        LastName = lastName;
        Role = r;
    }
    
    public int MaxRentalLimit => Role == "student" ? 2 : 5;

}