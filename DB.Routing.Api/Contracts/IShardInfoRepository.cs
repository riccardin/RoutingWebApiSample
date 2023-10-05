using DB.Routing.Api.Models;
using System.Threading.Tasks;

namespace DB.Routing.Api.Contracts
{
    public interface IShardInfoRepository
    {
        ShardInformation getShardInfo();
        Task<ShardInformation> getShardsInfoAsync();
    }
}
