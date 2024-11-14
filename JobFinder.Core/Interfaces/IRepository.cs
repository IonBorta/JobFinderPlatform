using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();        // Obține toate entitățile
        Task<T> GetByIdAsync(int id);              // Obține entitatea după ID
        Task AddAsync(T entity);                   // Adaugă o entitate
        Task UpdateAsync(T entity);                // Actualizează o entitate
        Task DeleteAsync(int id);                  // Șterge o entitate
    }
}
