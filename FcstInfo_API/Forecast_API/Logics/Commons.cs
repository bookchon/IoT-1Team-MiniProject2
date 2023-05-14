using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Forecast_API.Logics
{
    public class Commons
    {
        // MySQL용
        public static readonly string myConnString = "Server=210.119.12.66;" +
                                                     "Port=3306;" +
                                                     "Database=miniproject01;" +
                                                     "Uid=root;" +
                                                     "Pwd=12345;";
    }
}
