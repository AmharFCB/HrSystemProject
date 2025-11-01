using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Interfaces;
using MyFirstProject.Models;

namespace MyFirstProject.Repository
{
    public class RepositoryLeaveRequests : MainRepository<LeaveRequests>, IRepositoryLeaveRequests
    {
        private readonly HrDbContext _context;
        public RepositoryLeaveRequests(HrDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<LeaveRequests> GetAllLeaveRequestsDetails()
        {
            return _context.LeaveRequestss
                .Include(e => e.employees)
                .Include(e => e.leaveType)
                .ToList();
        }

        public async Task AddAsync(LeaveRequests model)
        {
            await _context.LeaveRequestss.AddAsync(model);
        }
    }
}
        
    

