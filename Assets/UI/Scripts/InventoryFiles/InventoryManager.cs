using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;
//This script is responsible for reading the database and loading it to an instance
//Part of the darkest dungeon script
public class InventoryManager : MonoBehaviour
{
	public static InventoryManager Instanse { get; private set; }
	public static Database Data { get {return Instanse.database;}}
	public static MenuInventory MenuInventory { get{ return Instanse.menuInventory; } }
	private Database database;
	private MenuInventory menuInventory;


    private void Awake()
    {
        if (Instanse == null)
        {
            Instanse = this;
            //DontDestroyOnLoad(gameObject);
			database = GetComponent<Database>();
			Debug.Log ("Before Load");
            database.Load();
			Debug.Log ("After Load");
			SaveCampaignData saveData = new SaveCampaignData ();
			menuInventory = new MenuInventory(saveData);
            Debug.Log(database.Items["passive"]["1"]);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}