using System.Collections.Generic;
using Demo.Models;

namespace Demo.Repositories
{
    public interface IPetRepository
    {
         void Create(Pet pet);
         void Edit(Pet pet);
         void Delete(Pet pet);
         Pet GetSinglePet(int id);
         IEnumerable<Pet> GetAllPets();
        bool VerifyName(string name);
        IEnumerable<Pet> SearchPets(string search);
    }
}