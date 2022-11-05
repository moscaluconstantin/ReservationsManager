﻿using EFCoreMappingApp;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.Domain.Models;

namespace ReservationsManager.DAL.Repositories
{
    public class ActionEmployeesRepository : GenericRepository<ActionEmployee>, IActionEmployeesRepository
    {
        public ActionEmployeesRepository(RezervationsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ActionEmployee>> GetAllByEmployeeIdAsync(int employeeId) =>
            await _context.ActionEmployees
            .Include(x=>x.Action)
            .Where(x => x.EmployeeID == employeeId).ToListAsync();
    }
}
