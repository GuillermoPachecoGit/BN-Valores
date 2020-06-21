using System.Collections.Generic;
using System.Threading.Tasks;
using BNV.Models;
using Refit;

namespace BNV.ServicesWebAPI.Interfaces
{
    public interface IAPIService
    {
        [Get("/Acciones")]
        Task<List<ShareOfStock>> GetSharesStock(ItemsParamModel param);

        [Get("/ExchangeRates")]
        Task<List<ChangeType>> GetExchangeRates(ItemsParamModel param);

        [Get("/Bonos")]
        Task<List<Bono>> GetBonos(ItemsParamModel param);

        [Get("/Reportos")]
        Task<List<Report>> GetReportos(ItemsParamModel param);

        [Get("/Reportos/{id}")]
        Task<Details> GetReportoDetails([AliasAs("id")] string id, DetailParamModel param);

        [Get("/Bonos/{id}")]
        Task<Details> GetBonoDetails([AliasAs("id")] string id, DetailParamModel param);

        [Get("/Acciones/{id}")]
        Task<Details> GetShareOfStockDetails([AliasAs("id")] string id, DetailParamModel param);

        [Get("/ExchangeRates/{id}")]
        Task<Details> GetExchangeDetails([AliasAs("id")] string id, DetailParamModel param);

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

        [Post("/Signup")] // TODO ver respuesta???
        Task<string> PostUser(RegisterParam param);

        [Post("/Login")] 
        Task<LoginTokenModel> PostLogin([Body(BodySerializationMethod.UrlEncoded)] LoginParam param);

        [Post("/RecoverPassword")] // TODO TRABAJAR SOBRE LA RESPONSE, TIENE QUE SER UNIFORME
        Task<string> PostRecoverPassword(RecoveryPassParam param);

        [Get("/General/ContactLabel")] 
        Task<string> GetContactInfo();
    }
}
