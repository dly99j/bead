using System;
using System.Threading.Tasks;

namespace bead.Persistence
{
    public interface IGameDataAccess
    {
        Task<GameTable> LoadAsync(String path);
        Task SaveAsync(String path, GameTable table);
    }
}
