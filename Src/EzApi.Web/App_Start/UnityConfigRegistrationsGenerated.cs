/*------------------------------------------------------------------------------
<auto-generated>
     This code was generated from a template.

     Manual changes to this file may cause unexpected behavior in your application.
     Manual changes to this file will be overwritten if the code is regenerated.
</auto-generated>
------------------------------------------------------------------------------*/
using Ez.Web;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Unity;

namespace Ez.Web
{
    /// <summary></summary>
    public static class UnityConfigRegistrationsGenerated
    {
        /// <summary></summary>
        public static void RegisterTypes(IUnityContainer container)
        {
            RegisterTypes(container, UnityRegistrationLifetimeManagerType.PerRequest);
        }
        /// <summary></summary>
        public static void RegisterTypesOwin(IUnityContainer container)
        {
            RegisterTypes(container, UnityRegistrationLifetimeManagerType.Transient);
        }
        /// <summary></summary>
        public static void RegisterTypes(IUnityContainer container, UnityRegistrationLifetimeManagerType lifetimeManagerType)
        {
            //container 
            //;
        }
    }
}