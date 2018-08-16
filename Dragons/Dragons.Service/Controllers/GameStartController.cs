using Dragons.Core;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dragons.Service.Controllers
{
    [ValidationActionFilter]
    public class GameStartController : ApiController
    {
        public async Task Put([FromBody]GameStart gameStart)
        {
            await WebApiApplication.GameService.InsertGameStartAsync(gameStart);
        }
    }
}