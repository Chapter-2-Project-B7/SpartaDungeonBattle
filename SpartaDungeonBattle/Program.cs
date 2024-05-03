using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json.Linq;
namespace SpartaDungeonBattle
{
    public class Program
    {
        static void Main()
        {
            GameManager.Instance.StartGame();
        }
    }
}
