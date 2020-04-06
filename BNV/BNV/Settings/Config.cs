using System;
namespace BNV.Settings
{
    public class Config
    {
        public static string FilterCoin = "FilterCoin";

        public static string FilterSector = "FilterSector";

        public static string MainPage = "MainPage";

        public class MainPageTypes
        {
            public static string Reportos = "Reportos";
            public static string Bonos = "Bonos";
            public static string Acciones = "Acciones";
            public static string TipoCambio = "Tipo Cambio";
        }

        public class SectorTypes
        {
            public static string Privado = "Privado";
            public static string Mixto = "Mixto";
            public static string Public = "Público";
            public static string All = "Todos";
        }

        public class CoinTypes
        {
            public static string All = "Todas";
            public static string CoinColon = "Colones";
            public static string CoinDolar = "Dólares";
        }

    }
}
