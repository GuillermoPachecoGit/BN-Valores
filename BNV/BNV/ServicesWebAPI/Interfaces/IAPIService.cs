using System.Collections.Generic;
using System.Threading.Tasks;
using BNV.Models;
using BNV.ViewModels;
using Refit;

namespace BNV.ServicesWebAPI.Interfaces
{
    public interface IAPIService
    {
        [Get("/Acciones")]
        Task<List<ShareOfStock>> GetSharesStock([Header("Authorization")] string token, ItemsParamModel param);

        [Get("/ExchangeRates")]
        Task<List<ChangeType>> GetExchangeRates([Header("Authorization")] string token, ItemsParamModel param);

        [Get("/Bonos")]
        Task<List<Bono>> GetBonos([Header("Authorization")] string token, ItemsParamModel param);

        [Get("/Reportos")]
        Task<List<Report>> GetReportos([Header("Authorization")] string token, ItemsParamModel param);

        [Get("/Reportos/{id}")]
        Task<Details> GetReportoDetails([Header("Authorization")] string token, [AliasAs("id")] string id, DetailParamModel param);

        [Get("/Bonos/{id}")]
        Task<Details> GetBonoDetails([Header("Authorization")] string token, [AliasAs("id")] string id, DetailParamModel param);

        [Get("/Acciones/{id}")]
        Task<Details> GetShareOfStockDetails([Header("Authorization")] string token, [AliasAs("id")] string id, DetailParamModel param);

        [Get("/ExchangeRates/{id}")]
        Task<Details> GetExchangeDetails([Header("Authorization")] string token, [AliasAs("id")] string id, DetailParamModel param);

        [Get("/General/Currency")]
        Task<List<Currency>> GetCurrency();

        [Get("/General/Sector")]
        Task<List<Sector>> GetSectors();

        [Get("/General/IdTypes")]
        Task<List<IdentificationType>> GetIdentificationTypes();

        [Get("/General/Pais")]
        Task<List<Country>> GetCountries();

        [Get("/General/Genero")]
        Task<List<Gender>> GetGender();

        [Get("/LoginVerify")]
        Task<UserVerificationModel> GetVerifyUser(UserVerifyParam param);

        [Post("/Signup")]
        Task<string> PostUser(RegisterParam param);

        [Post("/login")] 
        Task<LoginTokenModel> PostLogin([Body(BodySerializationMethod.UrlEncoded)] LoginParam param);

        [Post("/RecoverPassword")]
        Task<string> PostRecoverPassword(RecoveryPassParam param);

        [Post("/RecoverPassword")]
        Task<string> PostRecoverPassword([Header("Authorization")] string token, RecoveryPassParam param);

        [Get("/General/ContactLabel")] 
        Task<string> GetContactInfo();

        [Delete("/Logout")]
        Task<string> CloseSession([Header("Authorization")] string token);

        [Put("/Password")]
        Task<string> NewPassword([Header("Authorization")] string token, NewPasswordParam param);

        [Get("/Settings")]
        Task<SettingResponse> GetSettings([Header("Authorization")] string token);

        [Put("/Settings")]
        Task<string> UpdateSettings([Header("Authorization")] string token, SettingsModel param);

        [Get("/Settings/SystemDate")]
        Task<List<SystemDate>> GetDates([Header("Authorization")] string token);

        [Post("/UserDevice")]
        Task<string> UserDevice([Header("Authorization")] string token, UserDeviceParam param);
    }
}
