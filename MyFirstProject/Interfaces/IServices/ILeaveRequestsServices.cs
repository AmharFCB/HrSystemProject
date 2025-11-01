using MyFirstProject.Models;

namespace MyFirstProject.Interfaces.IServices
{
    public interface ILeaveRequestsServices
    {
        IEnumerable<LeaveRequests> GetAll();
        LeaveRequests GetByUid(string Uid);
        bool Create(LeaveRequests leaveRequest);
        Task<bool> UpdateAsync(LeaveRequests model);
        bool DeleteByUid(string Uid);
        Task CreateLeaveRequestAsync(LeaveRequests model);
        

        IEnumerable<Employees> GetEmployees();
        IEnumerable<LeaveType> GetLeaveTypes();
    }
}
