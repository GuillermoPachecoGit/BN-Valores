using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BNV.Models;
using Refit;

namespace BNV.ServicesWebAPI.Interfaces
{
    public interface IAPIService
    {
        [Get("/Acciones")]
        Task<List<ShareOfStock>> GetSharesStock(/*ItemsParamModel paramModel*/);

        [Get("/ExchangeRates")]
        Task<List<ChangeType>> GetExchangeRates(ItemsParamModel paramModel);

        [Get("/Bonos")]
        Task<List<Bono>> GetBonos(ItemsParamModel paramModel);

        [Get("/Reportos")]
        Task<List<Report>> GetReportos(ItemsParamModel paramModel);
    }
}
