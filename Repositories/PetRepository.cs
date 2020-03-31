using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Pet> GetPetsByUserId(string userId)
        {
            return _context.Pets.Where(x => x.UserId == userId);
        }

        public Pet GetSinglePet(int id)
        {
            return _context.Pets.Find(id);
        }

        public IEnumerable<Pet> SearchPets(string search)
        {
            int age;
            bool succes = Int32.TryParse(search, out age);
            if (!succes)
            {
                age = 0;
            }
            return _context.Pets.Where(p => p.Name.ToLower().Contains(search.ToLower())
                                || p.Color.ToLower().Contains(search.ToLower())
                                || p.Age == age);
        }

        public bool VerifyName(string name)
        {
            return _context.Pets.Any(s => s.Name == name);
        }

  
    }
}