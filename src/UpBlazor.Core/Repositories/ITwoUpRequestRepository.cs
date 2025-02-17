﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UpBlazor.Core.Models;

namespace UpBlazor.Core.Repositories
{
    public interface ITwoUpRequestRepository : IGenericRepository<TwoUpRequest>
    {
        Task<TwoUpRequest> GetByRequesterAndRequesteeAsync(string requesterId, string requesteeId);
        Task<IReadOnlyList<TwoUpRequest>> GetAllByRequesterAsync(string requesterId);
        Task<IReadOnlyList<TwoUpRequest>> GetAllByRequesteeAsync(string requesteeId);
    }
}