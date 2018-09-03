using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {

    public static GridManager gridManager = null;

	[SerializeField]
    GameObject lotForSale, lotNotForSale;

    GameObject spawnObject;

    public int gridWidth = 2, gridHeight = 2;

    float paddingX = .5f, paddingY = .5f;

    Vector3[,] _grid;

    EmptyLot[,] _lots;

    public Vector3[,] grid
    {
        get
        {
            return _grid;
        }
    }

    public EmptyLot[,] lots
    {
        get
        {
            return _lots;
        }
    }

    void Awake()
    {
        if (gridManager == null)
            gridManager = this;

        else if (gridManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Init();
    }

    void Start ()
    {
        SpawnBuildings();
    }

    void Update ()
    {

    }

    void Init()
    {
        MakeGrid();
        MakeLots();
    }

    void MakeGrid()
    {
        _grid = new Vector3[gridWidth, gridHeight];

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                grid[i, j] = new Vector3(i, j, 0);
            }
        }
    }

    void MakeLots()
    {
        _lots = new EmptyLot[gridWidth, gridHeight];

        int numberOfLots = 1;
        int minimumForSale = Mathf.RoundToInt((gridWidth * gridHeight) *.333f); // Eventually this will be weighted against the districts/zones that the lot is located in. Shitty zones will have more lots for sale. Rich zones are high demand low supply.
        Debug.Log(minimumForSale);
        int currentForSale = 0;

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                lots[i, j] = new EmptyLot(Random.Range(20000f, 100000f), Random.Range(0, 2) == 1, false, "Lot " + numberOfLots, grid[i,j]);
                numberOfLots++;

                if (lots[i, j].forSale) currentForSale++;
                Debug.Log(lots[i, j].price);
            }
        }

        while(currentForSale < minimumForSale)
        {
            //make sure we have minimum # of lots for sale.
            EmptyLot lot = lots[Random.Range(0, gridWidth), Random.Range(0, gridHeight)];
            if (!lot.forSale)
            {
                lot.forSale = true;
                currentForSale++;
            }
        }
    }

    void SpawnBuildings()
    {
        for(int i = 0; i < gridWidth; i++)
        {
            for(int j = 0; j < gridHeight; j++)
            {
				if (lots[i, j].forSale)
					spawnObject = (GameObject)Instantiate(lotForSale, grid[i, j], Quaternion.identity);
				else
					spawnObject = (GameObject)Instantiate(lotNotForSale, grid[i, j], Quaternion.identity);
			}
		}
    }
}