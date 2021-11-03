namespace Models
{
    public class Adult : Person
    {
        public Job JobTitle { get; set; }

        public override string ToString()
        {
            return $"Full name: {FirstName} {LastName} Hair: {HairColor} Eyes: {EyeColor} Age: {Age} Weight: {Weight} Height: {Height} Gender: {Sex}" +
                   $" Job: {JobTitle.JobTitle} Salary: {JobTitle.Salary}";
        }
    }
}