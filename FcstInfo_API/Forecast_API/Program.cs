using Forecast_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast_API
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            RequestForecastWebApi forecastApiInfo = new RequestForecastWebApi();
            PushDB pushDB = new PushDB();
            pushDB.InsertDB(forecastApiInfo.GetForecastWebApi());
        }
    }
}
