using DOMAIN.Entities;
using DOMAIN.Interfaces;
using INFRAESTRUCTURE.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRAESTRUCTURE.Repositories
{
    public class CountryRepository(AppDBContext _ctx) : ICountryRepository
    {
        public async Task<Country> CreateCountry(Country country)
        {
            var found = await GetCountryByName(country.Name);
            if (found != null) return null;
            _ctx.Countries.Add(country);
            await _ctx.SaveChangesAsync();
            return country;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _ctx.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryById(int id)
        {
            return await _ctx.Countries.FindAsync(id);
        }

        public async Task<Country?> GetCountryByName(string name)
        {
            return await _ctx.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
