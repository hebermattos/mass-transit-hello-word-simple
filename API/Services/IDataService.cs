using System.Threading.Tasks;

namespace services
{
    public interface IDataService
    {
        Task<string> Get(string key);
    }
}