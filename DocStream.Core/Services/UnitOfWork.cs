using DocStream.Core.Interfaces;
using DocStream.Data;
using DocStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Core.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        IGenericRepository<Applicant> _applicants;
        IRadioButtonServiceRepository<ApplicantLegalStatus> _applicationStatuses;
        IGenericRepository<Banker> _bankers;
        IGenericRepository<ContactPerson> _contactPeople;
        IGenericRepository<Director> _directors;
        IGenericRepository<ProposedBusinessName> _proposedBusinessNames;
        IGenericRepository<Shareholder> _shareholders;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<Applicant> Applicants => _applicants ??= new GenericRepository<Applicant>(_context);

        public IRadioButtonServiceRepository<ApplicantLegalStatus> ApplicantLegalStatuses => _applicationStatuses ??= new RadioButtonServiceRepository<ApplicantLegalStatus>(_context);

        public IGenericRepository<Banker> Bankers => _bankers ??= new GenericRepository<Banker>(_context);

        public IGenericRepository<ContactPerson> ContactPeople => _contactPeople ??= new GenericRepository<ContactPerson>(_context);

        public IGenericRepository<Director> Directors => _directors ??= new GenericRepository<Director>(_context);

        public IGenericRepository<ProposedBusinessName> ProposedBusinessNames => _proposedBusinessNames ??= new GenericRepository<ProposedBusinessName>(_context);

        public IGenericRepository<Shareholder> Shareholders => _shareholders ??= new GenericRepository<Shareholder>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
