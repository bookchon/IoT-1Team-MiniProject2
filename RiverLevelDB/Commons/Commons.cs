using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

namespace RiverLevelDB.Logics
{
    public class Commons
    {
        public static readonly string connString = "Server=localhost;" +
                                                    "Port=3306;" +
                                                    "Database=miniproject01;" +
                                                    "Uid=root;" + "Pwd=1234;";
    }
}