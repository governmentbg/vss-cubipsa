using System;

namespace Legalacts.ECLI.Integrator.Jobs
{
    public interface IJob : IDisposable
    {
        void Start();
    }
}