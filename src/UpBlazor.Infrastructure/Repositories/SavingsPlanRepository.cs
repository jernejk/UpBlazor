﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using UpBlazor.Core.Models;
using UpBlazor.Core.Repositories;

namespace UpBlazor.Infrastructure.Repositories
{
    public class SavingsPlanRepository : GenericRepository<SavingsPlan>, ISavingsPlanRepository
    {
        public SavingsPlanRepository(IDocumentStore store) : base(store) { }

        public async Task<SavingsPlan> GetByIdAsync(Guid id)
        {
            using var session = Store.QuerySession();

            return await session.Query<SavingsPlan>()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<SavingsPlan>> GetAllByIncomeIdAsync(Guid incomeId)
        {
            using var session = Store.QuerySession();

            return await session.Query<SavingsPlan>()
                .Where(x => x.IncomeId == incomeId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<SavingsPlan>> GetAllByUserIdAsync(string userId)
        {
            using var session = Store.QuerySession();

            var userIncomeIds = (await session.Query<Income>()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .ToListAsync()).ToArray();

            return await session.Query<SavingsPlan>()
                .Where(x => x.IncomeId.IsOneOf(userIncomeIds))
                .ToListAsync();        
        }
    }
}