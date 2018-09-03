using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	public static BuildingManager buildingManager;

	BuildingData _selectedBuilding;
	Inventory _selectedInventory;

	Dictionary<BuildingData, BuildingData> _buildings;
	Dictionary<BuildingData, Inventory> _productInventories;
	Dictionary<BuildingData, InventoryResearch> _researchInventories;

	public BuildingData selectedBuilding
	{
		get
		{
			return _selectedBuilding;
		}
		set
		{
			_selectedBuilding = value;
		}
	}

	public Inventory selectedInventory
	{
		get
		{
			return _selectedInventory;
		}
		set
		{
			_selectedInventory = value;
		}
	}

	public Dictionary<BuildingData, BuildingData> buildings
	{
		get
		{				
			return _buildings;
		}
	}

	public Dictionary<BuildingData, Inventory> productInventories
	{
		get
		{
			return _productInventories;
		}
	}

	public Dictionary<BuildingData, InventoryResearch> researchInventories
	{
		get
		{
			return _researchInventories;
		}
	}

	private void Awake()
	{
		InitSingleton();
		InitHashtables();
		_selectedBuilding = GameObject.Find("Garage").GetComponent<BuildingData>();
		_selectedInventory = GameObject.Find("Garage").GetComponent<Inventory>();
	}

	void Start ()
	{
		foreach(KeyValuePair<BuildingData, Inventory> inventory in productInventories)
		{
			inventory.Key.brandAwareness = Random.Range(0, 100);
			foreach(ProductItem item in inventory.Value.products)
			{
				//if (item.itemName == "Pie")
				//{
					item.quality = Random.Range(0, 100);
					item.sellsFor = Random.Range(1, 4);
					//inventory.Value.inventory[item] = 3;
				//}
			}
		}

		/*foreach (KeyValuePair<BuildingData, Inventory> inventory in productInventories)
		{
			foreach (ProductItem item in inventory.Value.products)
			{
				if (item.itemName == "Pie") Debug.Log(inventory.Key.name + " " + item.itemName + " quality: " + item.quality);
			}
		}*/
	}
	
	void Update ()
	{
		
	}

	void InitSingleton()
	{
		if (buildingManager == null)
			buildingManager = this;

		else if (buildingManager != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void InitHashtables()
	{
		if(_buildings == null)
			_buildings = new Dictionary<BuildingData, BuildingData>();
		if (_productInventories == null)
			_productInventories = new Dictionary<BuildingData, Inventory>();
		if (_researchInventories == null)
			_researchInventories = new Dictionary<BuildingData, InventoryResearch>();

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Building"))
		{
			BuildingData temp = go.GetComponent<BuildingData>();

			buildings.Add(temp, temp);

			if (go.GetComponent<Inventory>())
				productInventories.Add(temp, go.GetComponent<Inventory>());
			else if (go.GetComponent<InventoryResearch>())
				researchInventories.Add(temp, go.GetComponent<InventoryResearch>());
		}
	}
}
