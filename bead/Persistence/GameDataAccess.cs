using System.IO;
using System.Threading.Tasks;

namespace bead.Persistence
{
    public class GameDataAccess : IGameDataAccess
    {
        public async Task<GameTable> LoadAsync(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var line = await reader.ReadLineAsync();
                    var size = line.Split(' ');
                    var tableX = int.Parse(size[0]);
                    var tableY = int.Parse(size[1]);
                    var table = new GameTable(tableX, tableY);

                    for (var i = 0; i < tableY; ++i)
                    {
                        line = await reader.ReadLineAsync();
                        for (var j = 0; j < tableX; ++j) table.setFieldOnInit(j, i, line[j]);
                    }

                    return table;
                }
            }
            catch
            {
                throw new GameDataException();
            }
        }

        public async Task SaveAsync(string path, GameTable table)
        {
            try
            {
                await using (var writer = new StreamWriter(path))
                {
                    var size = table.Size;
                    await writer.WriteLineAsync(size.ToString());

                    for (var i = 0; i < size.Item1; ++i)
                    {
                        for (var j = 0; j < size.Item2; ++j) await writer.WriteAsync(table.GetField(i, j));
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new GameDataException();
            }
        }
    }
}