using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getLocateMap.Logics
{
    public class htmlString
    {
        string HtmlUriString = $@"<!DOCTYPE html>
		 					      <html>
			     				  <head>
								      <title>SOP Javascript : Map create sample</title>
									  <meta charset=""utf-8"">
									  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
									  <script type=""text/javascript"" src=""https://sgisapi.kostat.go.kr/OpenAPI3/auth/javascriptAuth?consumer_key=84752d8a83f14b3f8a10""></script>
								  </head>
								  <body>
									  <div id=""map"" style=""width:100%-20px;height:480px""></div>
									  <script type=""text/javascript"">
										  var map = sop.map(""map"");
										  map.setView(sop.utmk(1145117, 1689004), 9);
									  </script>
								  </body>
								  </html>";
    }
}
