using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBattlemap : MonoBehaviour {

    private RectangularSquareGridGenerator gridGen = new RectangularSquareGridGenerator();
    private Transform cellsParent;
    public Transform ObstaclesParent;
    public Transform UnitParent;

    public GameObject squarePrefab;
    public GameObject obstaclePrefab;
    public GameObject enemyPrefab;

    public int height = 1;
    public int width = 1;
    private string mapName = "default";
    private int enemyLayoutID = 0;

    private Maps currentMap;
    private ObstacleLayout obstacleLayout;
    private EnemyLayout enemyLayout;

    private List<Obstacles> obs = new List<Obstacles>();
    private List<Tiles> tiles = new List<Tiles>();
    private List<Enemy> enemies = new List<Enemy>();

    //lists of map objects
    private Tiles[] tileList;
    private Maps[] mapList;
    private Obstacles[] obsList;
    private ObstacleLayout[] obsLayoutList;
    private Enemy[] enemyList;
    private EnemyLayout[] enemyLayoutList;

    //obstacles and tiles used for the map
    private List<string> usedEnemies;
    private List<string> usedObstacles;
    private List<string> usedTiles;

    //initialize map
    private void Awake()
    {
        cellsParent = transform;
        squarePrefab = Resources.Load("Prefabs/SquareTile", typeof(GameObject)) as GameObject;
        LoadMap();
        CreateGrid();
        PopulateTiles();
        PopulateObstacles();
        PopulateEnemies();

    }

    //set grid height, width, and create the grid
    public void CreateGrid()
    {
        gridGen.Height = height;
        gridGen.Width = width;
        gridGen.SquarePrefab = squarePrefab;
        gridGen.CellsParent = cellsParent;
        gridGen.GenerateGrid();
    }

    //Load map onto the existing grid
    void LoadMap()
    {
        mapList = LoadMaps();
        tileList = LoadTiles();
        obsList = LoadObstacles();
        obsLayoutList = LoadObstacleLayout();
        enemyList = LoadEnemies();
        enemyLayoutList = LoadEnemyLayout();

        int mapIndex = findElementOfName<Maps>(mapName, mapList);

        print("Map Index Found!");

        currentMap = mapList[mapIndex];
        int obstacleID = Random.Range((int)0, currentMap.obstaclePossible.Length);
        obstacleLayout = obsLayoutList[obstacleID];
        enemyLayout = enemyLayoutList[enemyLayoutID];

        height = currentMap.height;
        width = currentMap.width;
        loadList<Tiles>(currentMap.tiles, tiles, tileList);
        loadList<Obstacles>(obsLayoutList[obstacleID].types, obs, obsList);
        loadList<Enemy>(enemyLayoutList[enemyLayoutID].types, enemies, enemyList);
    }

    //load obstacle json
    Obstacles[] LoadObstacles()
    {
        string obs = System.IO.File.ReadAllText("Assets/Combat/Json/Obstacles.json");
        Obstacles[] obsList = JsonHelper.getJsonArray<Obstacles>(obs);
        return obsList;
    }

    //load tiles json
    Tiles[] LoadTiles()
    {
        string tiles = System.IO.File.ReadAllText("Assets/Combat/Json/Tiles.json");
        Tiles[] tileList = JsonHelper.getJsonArray<Tiles>(tiles);
        return tileList;
    }

    //load map json
    Maps[] LoadMaps()
    {
        string maps = System.IO.File.ReadAllText("Assets/Combat/Json/Maps.json");
        Maps[] mapList = JsonHelper.getJsonArray<Maps>(maps);
        return mapList;
    }

    ObstacleLayout[] LoadObstacleLayout()
    {
        string obsLayout = System.IO.File.ReadAllText("Assets/Combat/Json/ObstacleLayout.json");
        ObstacleLayout[] layoutList = JsonHelper.getJsonArray<ObstacleLayout>(obsLayout);
        return layoutList;
    }

    Enemy[] LoadEnemies()
    {
        string enemies = System.IO.File.ReadAllText("Assets/Combat/Json/Enemies.json");
        Enemy[] list = JsonHelper.getJsonArray<Enemy>(enemies);
        return list;
    }

    EnemyLayout[] LoadEnemyLayout()
    {
        string enemies = System.IO.File.ReadAllText("Assets/Combat/Json/EnemyLayout.json");
        EnemyLayout[] list = JsonHelper.getJsonArray<EnemyLayout>(enemies);
        return list;
    }

    //helper for loadlist
    int findElementOfName<T>(string name, T[] array) where T : Assets
    {
        int index = 0;
        foreach (T elem in array)
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

    //load list of assets
    void loadList<T>(string[] names, List<T> list, T[] array) where T : Assets
    {
        foreach (string elem in names)
        {
            int index = findElementOfName<T>(elem, array);
            list.Add(array[index]);
        }
    }


    //populate list with the tile layout
    void PopulateTiles()
    {
        print("Populating... ");
        int gridIndex = 0;
        foreach (Transform child in cellsParent)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            Sprite[] newSpriteSheet;
            //load tile sprite onto tile
            if (gridIndex < currentMap.tileLayout.Length)
            {     
                newSpriteSheet = Resources.LoadAll<Sprite>(tiles[currentMap.tileLayout[gridIndex]].spriteSheet);
                print("Sprite Changed: "+ newSpriteSheet[tiles[currentMap.tileLayout[gridIndex]].sprite]);
                try
                {
                    sr.sprite = newSpriteSheet[tiles[currentMap.tileLayout[gridIndex]].sprite];
                }
                catch { }
            }
            //if nothing is indicated, load the default tile
            else
            {
                newSpriteSheet = Resources.LoadAll<Sprite>(tiles[0].spriteSheet);
                try
                {
                    int insert = Random.Range(0, 9);
                    if (insert != 0)
                    {
                        sr.sprite = newSpriteSheet[tiles[0].sprite];
                    }
                    else
                    {
                        sr.sprite = newSpriteSheet[tiles[tiles.Count-1].sprite];
                    }
                    
                }
                catch
                {
                }

                
            }
            gridIndex++;

        }
    }

    void PopulateObstacles()
    {
        Sprite[] newSpriteSheet;
        List<Sprite> obsSprites = new List<Sprite>();
        foreach (string name in obstacleLayout.types)
        {
            int obsIndex = findElementOfName<Obstacles>(name, obsList);
            newSpriteSheet = Resources.LoadAll<Sprite>(obsList[obsIndex].spriteSheet);
            
            obsSprites.Add(newSpriteSheet[obsList[obsIndex].sprite]);
        }
        //print("Layout Length: " + obstacleLayout.layout);
        for (int i = 0; i< obstacleLayout.layout.Length; i+=3)
        {
            int x = obstacleLayout.layout[i];
            int y = obstacleLayout.layout[i+1];
            Sprite s = obsSprites[obstacleLayout.layout[i+2]];

            Transform cell = cellsParent.GetChild(x + y * currentMap.height);
            cell.GetComponent<Cell>().IsTaken = true;

            GameObject obstacle = Instantiate(obstaclePrefab);
            obstacle.GetComponent<SpriteRenderer>().sprite = s;
            obstacle.transform.position = cell.position + new Vector3(0, 0, 0.1f);
            obstacle.transform.parent = ObstaclesParent.transform;
        }
    }

    void PopulateEnemies()
    {
        Sprite[] newSpriteSheet;
        List<Sprite> enemySprites = new List<Sprite>();
        List<Enemy> enemyStats = new List<Enemy>();
        
        foreach (string name in enemyLayout.types)
        {
            int enemyIndex = findElementOfName<Enemy>(name, enemyList);
            newSpriteSheet = Resources.LoadAll<Sprite>(enemyList[enemyIndex].spriteSheet);

            enemySprites.Add(newSpriteSheet[enemyList[enemyIndex].sprite]);
            enemyStats.Add(enemyList[enemyIndex]);
        }
        print("Layout Length: " + enemyLayout.layout.Length);
        for (int i = 0; i < enemyLayout.layout.Length; i += 3)
        {
            int x = enemyLayout.layout[i];
            int y = enemyLayout.layout[i + 1];
            Sprite s = enemySprites[enemyLayout.layout[i + 2]];
            Enemy e = enemyStats[enemyLayout.layout[i + 2]];

            Transform cell = cellsParent.GetChild(x + y * currentMap.height);

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = s;
            enemy.transform.position = cell.position;
            enemy.transform.parent = UnitParent.transform;

            enemy.GetComponent<Unit>().HitPoints = e.hp;
            enemy.GetComponent<Unit>().Armor = e.armor;
            enemy.GetComponent<Unit>().AttackFactor = e.attack;
            enemy.GetComponent<Unit>().gunAttack = e.gun;
            enemy.GetComponent<Unit>().AttackRange = e.range;
            enemy.GetComponent<Unit>().MovementSpeed = e.movement;
            enemy.GetComponent<Unit>().name = e.name;
            enemy.GetComponent<Unit>().Speed = e.speed;

            enemy.GetComponent<Unit>().Initialize();
        }
    }
}
