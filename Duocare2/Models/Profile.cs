namespace Duocare2.Models;

public class Profile
{
    public string Name { get; set; }
    public string Photo { get; set; }   // image path or file name
    public string Type { get; set; }    // "Child", "Pet" or "Both"
}
