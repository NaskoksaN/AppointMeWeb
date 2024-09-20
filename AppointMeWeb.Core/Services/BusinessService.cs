using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace AppointMeWeb.Core.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IFactory factory;
        private readonly IRepository sqlService;
        public BusinessService( IFactory _factory
                , IRepository _sqlService)
        {
            
            this.factory = _factory;
            this.sqlService = _sqlService;
        }
        
    }
}
