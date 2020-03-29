using System.Collections.Generic;
using Demo.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories
{
    public class PetRepository:IPetRepository
    {
        private readonly ApplicationDbContext _context;
        public PetRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public void Create(Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
        }

        public void Delete(Pet pet)
        {
            _context.Pets.Remove(pet);
            _context.SaveChanges();
        }

        public void Edit(Pet pet)
        {
            _context.Update(pet);
            _context.SaveChanges();
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _context.Pets;
        }

        public Pet GetSinglePet(int id)
        {
            return _context.Pets.Find(id);
        }
    }
}