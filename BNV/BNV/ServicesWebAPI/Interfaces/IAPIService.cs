using System.Collections.Generic;
using System.Threading.Tasks;
using BNV.Models;
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

        [Get("/General/ContactLabel")] 
        Task<string> GetContactInfo();

        [Delete("/Logout")]
        Task<string> CloseSession([Header("Authorization")] string token);

        [Put("/Password")]
        Task<string> NewPassword([Header("Authorization")] string token, NewPasswordParam param);
    }
}
