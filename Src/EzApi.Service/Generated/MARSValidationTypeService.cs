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
    public partial interface IEzValidationTypeService : IServiceEx<EzValidationType>
    {
    }

    /// <summary></summary>
    public partial class EzValidationTypeService : ServiceEx<EzValidationType>, IEzValidationTypeService
    {
        public EzValidationTypeService(IRepositoryAsync<EzValidationType> repository) : base(repository)
        {
        }
    }
}