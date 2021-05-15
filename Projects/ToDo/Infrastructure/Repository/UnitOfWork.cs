using Core.Entites;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private  IRepository _entity;
        public IRepository Entity
        {
            get{
                return _entity ?? (_entity = new Repository(_dbContext));
            }
        }

        public UnitOfWork(AppDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        public void SaveItem()
        {
            _dbContext.SaveChanges();
        }
    }
}
