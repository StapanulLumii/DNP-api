using System.Collections.Generic;

namespace Models {
public class Child : Person {
    
    public List<Interest> Interests { get; set; }
    public List<Pet> Pets { get; set; }
    public override string ToString()
    {
        return $"Full name: {FirstName} {LastName} \n Hair: {HairColor} Eyes: {EyeColor} Age: {Age} Weight: {Weight} Height: {Height} Gender: {Sex}" +
               $" Interests: {string.Join("\n",Interests)} Pets: {string.Join("\n",Pets)}";
    }
}
}