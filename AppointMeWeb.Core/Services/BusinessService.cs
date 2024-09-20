using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Infrastrucure.Data.Common;

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
