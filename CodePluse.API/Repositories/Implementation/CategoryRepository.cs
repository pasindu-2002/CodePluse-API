﻿using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CodePluse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
           return await dbContext.Categories.ToListAsync();
        }


        public async Task<Category?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Category?> UpadateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null) 
            { 
                return null;
            }

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;

        }
    }
}
