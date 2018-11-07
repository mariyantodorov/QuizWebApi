﻿using Contracts.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.Data.Repositories.Contracts
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        public BaseRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        //working???
        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = this.dbSet;
            return query;
        }

        public async virtual Task<ICollection<T>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public virtual T GetById(object id)
        {
            T entity = this.dbSet.Find(id);
            return entity;
        }

        public async virtual Task<T> GetByIdAsync(object id)
        {
            Task<T> result = this.dbSet.FindAsync(id);
            return await result;
        }

        public virtual void Add(T entity)
        {
            this.dbSet.Add(entity);
            this.Save();
        }

        public async virtual Task AddAsync(T entity)
        {
            this.dbSet.Add(entity);
            await this.SaveAsync();
        }

        public virtual void Delete(T entity)
        {
            this.dbSet.Remove(entity);
            this.Save();
        }

        public async virtual Task DeleteAsync(T entity)
        {
            this.dbSet.Remove(entity);
            await this.SaveAsync();
        }

        public virtual void Update(T itemToUpdate)
        {
            this.dbSet.Update(itemToUpdate);
            this.Save();
        }

        public async virtual Task UpdateAsync(T itemToUpdate)
        {
            this.dbSet.Update(itemToUpdate);
            await this.SaveAsync();
        }

        public virtual int Count()
        {
            int count = this.dbSet.Count();
            return count;
        }

        public async virtual Task<int> CountAsync()
        {
            return await this.dbSet.CountAsync();
        }

        public virtual void Save()
        {
            this.context.SaveChanges();
        }

        public async virtual Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
