using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast_API.Models
{
    // 예보지점 X,Y 좌표값 정의
    public class ForecastPostion
    {
        public string XPOS { get; set; }    // X 좌표값
        public string YPOS { get; set; }    // Y 좌표값
    }
}
