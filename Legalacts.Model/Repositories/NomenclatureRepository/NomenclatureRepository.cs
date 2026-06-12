using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legalacts.Model.Entities;
using Legalacts.Model.Repositories;

namespace Legalacts.Model.Repositories
{
    #region Interface

    public interface INomenclatureRepository : IRepository
    {
        Court GetCourtById(int id);
        IQueryable<Court> GetAllActiveCourts();

        CaseKind GetCaseKindById(int id);
        IQueryable<CaseKind> GetAllCaseKinds();
        IQueryable<CaseKind> GetAllActiveCaseKinds();

        ActKind GetActKindById(int id);
        IQueryable<ActKind> GetAllActKinds();
        IQueryable<ActKind> GetAllActiveActKinds();

        IQueryable<Status> GetAllActiveStatuses();
        IQueryable<ResultsOfAppeal> GetAllActiveResultsOfAppeals();

        IQueryable<ActionLogType> GetAllActiveActionLogTypes();
    }

    #endregion

    #region Class

    public class NomenclatureRepository : RepositoryBase, INomenclatureRepository
    {
        #region Courts

        public Court GetCourtById(int id)
        {
            return DataContext.Courts.FirstOrDefault(e => e.CourtId == id);
        }

        public IQueryable<Court> GetAllActiveCourts()
        {
            return DataContext.Courts.Where(e => e.IsActive);
        }

        #endregion

        #region CaseKinds

        public CaseKind GetCaseKindById(int id)
        {
            return DataContext.CaseKinds.FirstOrDefault(e => e.CaseKindId == id);
        }

        public IQueryable<CaseKind> GetAllCaseKinds()
        {
            return DataContext.CaseKinds;
        }

        public IQueryable<CaseKind> GetAllActiveCaseKinds()
        {
            return DataContext.CaseKinds.Where(e => e.IsActive);
        }

        #endregion

        #region ActKinds

        public ActKind GetActKindById(int id)
        {
            return DataContext.ActKinds.FirstOrDefault(e => e.ActKindId == id);
        }

        public IQueryable<ActKind> GetAllActKinds()
        {
            return DataContext.ActKinds;
        }

        public IQueryable<ActKind> GetAllActiveActKinds()
        {
            return DataContext.ActKinds.Where(e => e.IsActive);
        }

        #endregion

        #region Status

        public IQueryable<Status> GetAllActiveStatuses()
        {
            return DataContext.Statuses.Where(e => e.IsActive);
        }

        #endregion

        #region ResultsOfAppeals

        public IQueryable<ResultsOfAppeal> GetAllActiveResultsOfAppeals()
        {
            return DataContext.ResultsOfAppeals.Where(e => e.IsActive);
        }

        #endregion

        #region ActionLogTypes

        public IQueryable<ActionLogType> GetAllActiveActionLogTypes()
        {
            return DataContext.ActionLogTypes.Where(e => e.IsActive);
        }

        #endregion

    }

    #endregion
}
