using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();

        Task<Country> GetCountryById(int id);

        Task<Country> GetCountryByName(string name);

        Task<Country> CreateCountry(Country country);
    }
}
