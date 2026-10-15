using ChainOfResponsibility.DataAccess;
using ChainOfResponsibility.Entities;
using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.ChainOfResponsibilityPattern
{
    public class Treasurer(AppDbContext _context) : Employee
    {
        public override void ProcessRequest(CustomerProcessViewModel request)
        {
            if(request.Amount <= 50000)
            {
                var customerProcess = new CustomerProcess
                {
                    FullName = request.FullName,
                    Amount = request.Amount,
                    EmployeeName = "Vezdedar - Barış Bingöl",
                    Description ="Para çekme işlemi veznedar tarafından onaylandı"
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
                    EmployeeName = "Vezdedar - Barış Bingöl",
                    Description = "Para çekme tutarı veznedarın ödeyebileceği miktarı aştığı için işlem müdür yardımcısına yönlendirildi"
                };
                _context.CustomerProcesses.Add(customerProcess);
                _context.SaveChanges();

                NextApprover.ProcessRequest(request);
            }
        }
    }
}
