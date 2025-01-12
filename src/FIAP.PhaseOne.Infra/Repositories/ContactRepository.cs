﻿using FIAP.PhaseOne.Domain.ContactAggregate;
using FIAP.PhaseOne.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.PhaseOne.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Contact contact, CancellationToken ct)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Contact?> GetById(Guid id, CancellationToken ct) =>
            await _context.Contacts.Include(c => c.Phone)
                                    .Include(c => c.Address)
                                    .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task Update(Contact contact, CancellationToken ct)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Remove(Guid id, CancellationToken ct)
        {
            var contact = await GetById(id, ct);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<(IEnumerable<Contact> Items, int Total)> GetAll(
            int page, 
            int limit, 
            CancellationToken ct,
            int? ddd = null)
        {
            var query = _context.Contacts
                .Include(c => c.Phone)
                .Include(c => c.Address)
                .AsQueryable();
                    
            if (ddd.HasValue)
                query = query.Where(x => x.Phone.DDD == ddd);

            var total = await query.CountAsync(ct);

            query = query
                .Skip((page - 1) * limit)
                .Take(limit);

            return (await query.ToListAsync(ct), total);
        }

        public async Task SaveChanges(CancellationToken ct) => await _context.SaveChangesAsync(ct);
    }

}
