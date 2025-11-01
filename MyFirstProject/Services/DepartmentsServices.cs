using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Models;

namespace MyFirstProject.Services
{
    public class DepartmentsServices : IDepartmentsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(Department department)
        {
            _unitOfWork.RepositoryDepartments.Add(department);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteByUid(string Uid)
        {
            var Department = _unitOfWork.RepositoryDepartments.GetByUid(Uid);
            if (Department == null)
            {
                return false;
            }
            _unitOfWork.RepositoryDepartments.Delete(Department.Id);
            _unitOfWork.SaveChanges();
            return true;
        }
          public IEnumerable<Employees> GetEmployees()
        {
            return _unitOfWork.Employeess.GetAll();
        }
        public IEnumerable<Department> GetAll()
        {
            return _unitOfWork.RepositoryDepartments.GetEmployeesWithDepartment();
        }

        public Department GetByUid(string Uid)
        {
            return _unitOfWork.RepositoryDepartments.GetByUid(Uid);
        }

        public bool Update(Department department)
        {
            var existing = _unitOfWork.RepositoryDepartments.GetByUid(department.Uid);
            if (existing == null)
                return false;

            existing.DepartmentName = department.DepartmentName;
            _unitOfWork.RepositoryDepartments.Update(existing);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
