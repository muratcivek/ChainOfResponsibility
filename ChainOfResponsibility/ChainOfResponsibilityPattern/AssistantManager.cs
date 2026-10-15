using ChainOfResponsibility.DataAccess;
using ChainOfResponsibility.Entities;
using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.ChainOfResponsibilityPattern
{
    public class AssistantManager(AppDbContext _context) : Employee
    {
        public override void ProcessRequest(CustomerProcessViewModel request)
        {
            if(request.Amount <= 200000)
            {
                var customerProcess = new CustomerProcess
                {
                    FullName = request.FullName,
                    Amount = request.Amount,
                    EmployeeName = "Müdür Yardımcısı - Elif Yılmaz",
                    Description = "Para çekme işlemi müdür yardımcısı tarafından onaylandı"
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
                    EmployeeName = "Müdür Yardımcısı - Elif Yılmaz",
                    Description = "Para çekme tutarı müdür yardımcısının ödeyebileceği miktarı aştığı için işlem müdüre yönlendirildi"
                };
                _context.CustomerProcesses.Add(customerProcess);
                _context.SaveChanges();
                NextApprover.ProcessRequest(request);
            }
        }
    }
}
