using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json.Linq;
namespace SpartaDungeonBattle
{
    public class Program
    {
        static void Main()
        {
            //GameManager.Instance.StartGame();
            /*Inventory inventory = new Inventory();
            inventory.AddItem();
            inventory.SaveInventoryData();*/

            Inventory ABC = new Inventory();
            ABC.InitItem();
            ABC.LoadInventoryData();

            ABC.ShowInventoryMenu();
            //inventory.AddItem();
            /*Inventory Abc = new Inventory();
            Abc.LoadInventoryData();*/
        }
    }
}
