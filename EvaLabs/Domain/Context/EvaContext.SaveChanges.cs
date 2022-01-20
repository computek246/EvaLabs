using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EvaLabs.Domain.Models;
using EvaLabs.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EvaLabs.Domain.Context
{
    public sealed partial class EvaContext
    {
        private readonly ICurrentUserService<int> _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public EvaContext(
            DbContextOptions<EvaContext> options,
            IDateTimeService dateTimeService,
            ICurrentUserService<int> currentUserService
        )
            : this(options)
        {
            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        public override int SaveChanges()
        {
            SetAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            SetAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetAudit()
        {
            var entityEntries = ChangeTracker.Entries<Auditable>().ToList();
            foreach (var entry in entityEntries)
                switch (entry.State)
                {
                    case EntityState.Added:
                        OnAdded(entry);
                        break;
                    case EntityState.Modified:
                        OnModified(entry);
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        OnDeleted(entry);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        private void OnDeleted(EntityEntry<Auditable> entry)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsActive = false;
            entry.Entity.IsDeleted = true;
            OnModified(entry);
        }

        private void OnModified(EntityEntry<Auditable> entry)
        {
            entry.Entity.ModifierId = _currentUserService.UserId;
            entry.Entity.LastModifiedDate = _dateTimeService.Now;
        }

        private void OnAdded(EntityEntry<Auditable> entry)
        {
            entry.Entity.IsActive = true;
            entry.Entity.IsDeleted = false;
            entry.Entity.CreatorId = _currentUserService.UserId;
            entry.Entity.CreationDate = _dateTimeService.Now;
            OnModified(entry);
        }
    }
}