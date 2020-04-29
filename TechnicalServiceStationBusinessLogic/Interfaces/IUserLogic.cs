using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        List<UserViewModel> Read(UserBindingModel model);

        void CreateOrUpdate(UserBindingModel model);

        void Delete(UserBindingModel model);
    }
}
