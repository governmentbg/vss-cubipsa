using Legalacts.Model.Entities;
using Legalacts.Model.Sitemap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalacts.Model.Repositories.SitemapRepository
{
    #region Interface

    public interface ISitemapRepository
    {
        Route GetRoute(DateTime date);
        IQueryable<Route> GetAllRoutes();
        IQueryable<Index> GetAllIndexes();
        IQueryable<Sitemap.Entities.Sitemap> GetAllSitemaps();
    }

    #endregion


    public class SitemapRepository : ISitemapRepository, IDisposable
    {
        private SitemapContext DataContext;

        public SitemapRepository()
        {
            DataContext = new SitemapContext();
        }

        public Route GetRoute(DateTime date)
        {
            return DataContext.Routes.FirstOrDefault(e => e.Date == date);
        }

        public IQueryable<Route> GetAllRoutes()
        {
            return DataContext.Set<Route>().AsQueryable();
        }

        public IQueryable<Index> GetAllIndexes()
        {
            return DataContext.Set<Index>().AsQueryable();
        }

        public IQueryable<Sitemap.Entities.Sitemap> GetAllSitemaps()
        {
            return DataContext.Set<Sitemap.Entities.Sitemap>().AsQueryable();
        }

        public void Dispose()
        {
            DataContext.Dispose();
        }
    }
}
