using System.Threading.Tasks;

namespace bead.Persistence
{
    public interface IGameDataAccess
    {
        Task<GameTable> LoadAsync(string path);
    }
}