using ChainOfResponsibility.DataAccess;
using ChainOfResponsibility.Entities;
using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.ChainOfResponsibilityPattern
{
    public class Manager(AppDbContext _context) : Employee
    {
        public override void ProcessRequest(
    CustomerProcessViewModel request)
        {
            if (request.Amount <= 500000)
            {
                var customerProcess = new CustomerProcess
                {
                    FullName = request.FullName,
                    Amount = request.Amount,
                    EmployeeName = "Şube Müdürü - Ahmet Demir",
                    Description = "Para çekme işlemi müdür tarafından onaylandı."
                };

                _context.CustomerProcesses.Add(customerProcess);
                _context.SaveChanges();
            }
            else if (NextApprover is not null)
            {
                var customerProcess = new CustomerProcess
                {
                    FullName = request.FullName,
                    Amount = request.Amount,
                    EmployeeName = "Şube Müdürü - Ahmet Demir",
                    Description = "Yetki limiti aşıldığı için işlem bölge müdürüne yönlendirildi."
                };

                _context.CustomerProcesses.Add(customerProcess);
                _context.SaveChanges();

                NextApprover.ProcessRequest(request);
            }
        }
    }
}
