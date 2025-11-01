using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Models;
using MyFirstProject.Repository;
using Microsoft.AspNetCore.Hosting;
namespace MyFirstProject.Services
{
    public class LeaveRequestsServices : ILeaveRequestsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public LeaveRequestsServices(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public bool Create(LeaveRequests leaveRequest)
        {
            _unitOfWork.RepositoryLeaveRequests.Add(leaveRequest);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteByUid(string Uid)
        {
            var LeaveRequests = _unitOfWork.RepositoryLeaveRequests.GetByUid(Uid);
            if (LeaveRequests == null)
            {
                return false;
            }
            _unitOfWork.RepositoryLeaveRequests.Delete(LeaveRequests.LeaveID);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<LeaveRequests> GetAll()
        {
            return _unitOfWork.RepositoryLeaveRequests.GetAllLeaveRequestsDetails();
        }

        public LeaveRequests GetByUid(string Uid)
        {
            return _unitOfWork.RepositoryLeaveRequests.GetByUid(Uid);
        }

        public IEnumerable<Employees> GetEmployees()
        {
           return _unitOfWork.Employeess.GetAll();
        }

        public IEnumerable<LeaveType> GetLeaveTypes()
        {
            return _unitOfWork.LeaveTypes.GetAll();
        }

        public async Task<bool> UpdateAsync(LeaveRequests model)
        {
            var existing = _unitOfWork.RepositoryLeaveRequests.GetByUid(model.Uid);
            if (existing == null)
                return false;

            existing.EmployeesId = model.EmployeesId;
            existing.LeaveTypeId = model.LeaveTypeId;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.LeaveDays = model.LeaveDays;
            existing.Status = model.Status;

            if (model.AttachmentFile != null && model.AttachmentFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.AttachmentFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AttachmentFile.CopyToAsync(stream);
                }

                // حفظ المسار الجديد فقط
                existing.AttachmentPath = "/uploads/" + fileName;
            }

            _unitOfWork.RepositoryLeaveRequests.Update(existing);
            _unitOfWork.SaveChanges();
            return true;
        }


        public async Task CreateLeaveRequestAsync(LeaveRequests model)
        {
            // حفظ المرفق (PDF) في السيرفر
            if (model.AttachmentFile != null && model.AttachmentFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.AttachmentFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AttachmentFile.CopyToAsync(stream);
                }

                model.AttachmentPath = "/uploads/" + fileName;
            }

            // حفظ الطلب في قاعدة البيانات
            _unitOfWork.LeaveRequests.Add(model);
            _unitOfWork.SaveChanges();
        }
    }
}
