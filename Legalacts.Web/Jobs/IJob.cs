using System;

namespace Legalacts.Web.Jobs
{
    public interface IJob : IDisposable
    {
        void Start();
    }
}