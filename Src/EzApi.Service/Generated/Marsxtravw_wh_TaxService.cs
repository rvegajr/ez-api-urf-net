/*------------------------------------------------------------------------------
<auto-generated>

    This code was generated from a template.

    Manual changes to this file may cause unexpected behavior in your application.
    Manual changes to this file will be overwritten if the code is regenerated.

</auto-generated>
------------------------------------------------------------------------------ */
using Ez.Entities.Models;
using Repository.Pattern.Repositories;
using Ez.Repository;
using Service.Pattern;

namespace Ez.Service
{
    /// <summary></summary>
    public partial interface IEzxtravw_wh_TaxService : IServiceEx<Ezxtravw_wh_Tax>
    {
    }

    /// <summary></summary>
    public partial class Ezxtravw_wh_TaxService : ServiceEx<Ezxtravw_wh_Tax>, IEzxtravw_wh_TaxService
    {
        public Ezxtravw_wh_TaxService(IRepositoryAsync<Ezxtravw_wh_Tax> repository) : base(repository)
        {
        }
    }
}