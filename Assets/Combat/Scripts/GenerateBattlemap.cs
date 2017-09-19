using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBattlemap : MonoBehaviour {

    private RectangularSquareGridGenerator gridGen = new RectangularSquareGridGenerator();
    public GameObject squarePrefab;
    public Transform cellsParent;
    public int height;
    public int width;
    private string mapName = "default";
    private Maps currentMap;
    private List<Obstacles> obs = new List<Obstacles>();
    private List<Tiles> tiles = new List<Tiles>();

    private Tiles[] tileList;
    private Maps[] mapList;
    private Obstacles[] obsList;

    private List<string> usedObstacles;
    private List<string> usedTiles;

    private void Awake()
    {
        LoadMap();
        GenerateGrid();
        PopulateGrid();
        
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void GenerateGrid()
    {
        gridGen.Height = height;
        gridGen.Width = width;
        gridGen.SquarePrefab = squarePrefab;
        gridGen.CellsParent = cellsParent;
        gridGen.GenerateGrid();
    }

    void PopulateGrid()
    {
        int gridIndex = 0;
        foreach (Transform child in cellsParent)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            Sprite[] newSpriteSheet;
            if (gridIndex < currentMap.tileLayout.Length)
            {
                newSpriteSheet = Resources.LoadAll<Sprite>(tiles[currentMap.tileLayout[gridIndex]].spriteSheet);
                sr.sprite = newSpriteSheet[tiles[currentMap.tileLayout[gridIndex]].sprite];
            }
            else
            {
                newSpriteSheet = Resources.LoadAll<Sprite>(tiles[0].spriteSheet);
                sr.sprite = newSpriteSheet[tiles[0].sprite];
            }
            
            gridIndex++;
        }

    }

    void LoadMap()
    {
        mapList = FindMaps();
        tileList = FindTiles();
        obsList = FindObstacles();
        int mapIndex = findElementOfName<Maps>(mapName, mapList);
        currentMap = mapList[mapIndex];
        height = currentMap.height;
        width = currentMap.width;
        loadList<Tiles>(currentMap.tiles, tiles, tileList);
        loadList<Obstacles>(currentMap.obstacles, obs, obsList);
    }

    Obstacles[] FindObstacles()
    {
        string obs = System.IO.File.ReadAllText("Assets/Combat/obstacles.json");
        Obstacles[] obsList = JsonHelper.getJsonArray<Obstacles>(obs);
        return obsList;
    }

    Tiles[] FindTiles()
    {
        string tiles = System.IO.File.ReadAllText("Assets/Combat/tiles.json");
        Tiles[] tileList = JsonHelper.getJsonArray<Tiles>(tiles);
        return tileList;
    }

    Maps[] FindMaps()
    {
        string maps = System.IO.File.ReadAllText("Assets/Combat/maps.json");
        Maps[] mapList = JsonHelper.getJsonArray<Maps>(maps);
        return mapList;
    }

    int findElementOfName<T>(string name, T[] array) where T: Assets 
    {
        int index = 0;
        foreach(T elem in array)
        {
            if (elem.name == name)
            {
                break;
            }
            index++;
        }
        print(index);
        return index;
    }

    void loadList<T>(string[] names, List<T> list, T[] array) where T : Assets
    {
        foreach (string elem in names)
        {
            int index = findElementOfName<T>(elem, array);
            list.Add(array[index]);
        }
    }


}
