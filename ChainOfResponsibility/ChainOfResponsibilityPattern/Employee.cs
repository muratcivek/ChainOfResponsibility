using ChainOfResponsibility.Models;

namespace ChainOfResponsibility.ChainOfResponsibilityPattern
{
    public abstract class Employee
    {
        protected Employee NextApprover;

        public void  SetNextApprover(Employee supervisor)
        {
            this.NextApprover = supervisor;
        }

        public abstract void ProcessRequest(CustomerProcessViewModel request);
    }
}
