using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace FileData
{
    public class FileContext
    {
        private IList<Family> Families;
        public int index { get; }

        private readonly string familiesFile = "families.json";
        private readonly string adultsFile = "adults.json";

        public FileContext()
        {
            Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
            index = 1;
            foreach (var f in Families)
            {
                f.Id = index;
                index++;
            }
        }

        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(familiesFile))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }
        public async Task<IList<Family>> GetFamilies()
        {
            List<Family> tmp = new List<Family>(Families);
            return tmp;
        }
        public async Task RemoveFamilyAsync(int familyId) {
            Family toRemove = Families.First(f => f.Id == familyId);
            Families.Remove(toRemove);
            int i = 1;
            foreach (var family in Families)
            {
                family.Id = i;
                i++;
            }
            SaveChanges();
        }
        public async Task<Family> UpdateAsync(Family family) {
            Family toUpdate = Families.FirstOrDefault(f => f.Id == family.Id);
            if(toUpdate == null) throw new Exception($"Did not find a family with id: {family.Id}");
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.StreetName = family.StreetName;
            SaveChanges();
            return toUpdate;
        }
        public async Task<Family> AddFamilyAsync(Family family) {
            int max = Families.Max(f => f.Id);
            family.Id = (++max);
            Families.Add(family);
            SaveChanges();
            return family;
        }

        public void SaveChanges()
        {
            // storing families
            string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(familiesFile, false))
            {
                outputFile.Write(jsonFamilies);
            }
        }
    }
}