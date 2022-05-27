using Microsoft.EntityFrameworkCore;
using PackyAPI.Data;
using PackyAPI.IRepository;

namespace PackyAPI.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DatabaseContext _databaseContext;
        private IGenericRepository<Country> _countries;

        private IGenericRepository<Hotel> _hotels;
        public UnitofWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            //_db = databaseContext.Database;
        }
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_databaseContext);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_databaseContext);

        public void Dispose()
        {
            _databaseContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
