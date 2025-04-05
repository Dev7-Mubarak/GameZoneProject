using GameZone.Areas.Admin.ViewModels;
using GameZone.Models;
using System.Threading.Tasks;

namespace GameZone.Areas.Admin.Services
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAllGames();
        public Game? GetById(int id);
        Task CreateGame(CreateGameFormVM model);
        Task<Game?> EditGame(EditGameFormVM model);
        bool Delete(int id);
    }
}
