using MyFirstProject.Models;

namespace MyFirstProject.Interfaces
{
    public interface IRepositoryLeaveRequests : IRepository<LeaveRequests>
    {
        IEnumerable<LeaveRequests> GetAllLeaveRequestsDetails();

    }
}
