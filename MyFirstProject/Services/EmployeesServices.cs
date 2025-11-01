using Microsoft.EntityFrameworkCore;
using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Models;

namespace MyFirstProject.Services
{
    public class EmployeesServices : IEmployeesServices
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(Employees employee)
        {
            _unitOfWork.Employeess.Add(employee);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteByUid(string Uid)
        {
            var Employees = _unitOfWork.RepositoryEmployees.GetByUid(Uid);
            if (Employees == null)
            {
                return false;
            }
            _unitOfWork.RepositoryEmployees.Delete(Employees.Id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<Employees> GetAll()
        {
            return _unitOfWork.RepositoryEmployees.GetEmployeesWithRelations();
        }

       


        public Employees GetByUid(string Uid)
        {
           return _unitOfWork.RepositoryEmployees.GetByUid(Uid);
        }

        public IEnumerable<City> GetCities()
        {
            return _unitOfWork.Cities.GetAll();
        }


        public IEnumerable<Jobs> GetEmployeesJobs()
        {
            return _unitOfWork.Job.GetAll();
        }

        public IEnumerable<EmployeeStatus> GetEmployeeStatuses()
        {
            return _unitOfWork.EmployeeStatuses.GetAll();
        }

        public bool Update(Employees employee)
        {
            var existing = _unitOfWork.RepositoryEmployees.GetByUid(employee.Uid);
            if (existing == null)
                return false;

            existing.Name = employee.Name;
            existing.Address = employee.Address;
            existing.PhoneNumber = employee.PhoneNumber;
            existing.Email = employee.Email;
            existing.CityId = employee.CityId;
            existing.jobs = employee.jobs;
            existing.EmployeeStatusId = employee.EmployeeStatusId;
            existing.JoiningDate = employee.JoiningDate;
            existing.NationalID = employee.NationalID;
            existing.TypeUser = employee.TypeUser;
            existing.Password = employee.Password;
            


            _unitOfWork.RepositoryEmployees.Update(existing);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
