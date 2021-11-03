using System.ComponentModel.DataAnnotations;

namespace Models {
public class Pet {
    public int Id { get; set; }
    public string Species { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public override string ToString()
    {
        return $"Name: {Name} Species: {Species} Age:{Age}";
    }
}
}