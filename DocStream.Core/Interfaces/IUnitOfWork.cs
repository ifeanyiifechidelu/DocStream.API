using DocStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Applicant> Applicants { get; }
        IRadioButtonServiceRepository<ApplicantLegalStatus> ApplicantLegalStatuses { get; }
        IGenericRepository<Banker> Bankers { get; }
        IGenericRepository<ContactPerson> ContactPeople { get; }
        IGenericRepository<Director> Directors { get; }
        IGenericRepository<ProposedBusinessName> ProposedBusinessNames { get; }
        IGenericRepository<Shareholder> Shareholders { get; }
        Task Save();
    }
}
