using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour, IComparable<Inventory> {

	[SerializeField]
	[Tooltip("List of products this building can make")]
	List<ProductItem> _products;

	Dictionary<string, ProductItem> productLookup;
	Dictionary<ProductItem, int> _inventory;
	Dictionary<ProductItem, float> _timeRemaining;
	Dictionary<ProductItem, bool> itemsBeingProduced;

	[SerializeField]
	InventoryPanel _inventoryPanel;

	[SerializeField]
	int _maxInventory = 10;

	int currentInventory = 0;

	/*public Dictionary<string, ProductItem> productLookup
	{
		get
		{
			return _productLookup;
		}
	}*/

	public Dictionary<ProductItem, int> inventory
	{
		get
		{
			return _inventory;
		}
		set
		{
			_inventory = value;
		}
	}

	public Dictionary <ProductItem, float> timeRemaining
	{
		get
		{
			return _timeRemaining;
		}
		set
		{
			_timeRemaining = value;
		}
	}

	public InventoryPanel inventoryPanel
	{
		get
		{
			return _inventoryPanel;
		}
		set
		{
			_inventoryPanel = value;
		}
	}

	public List<ProductItem> products
	{
		get
		{
			return _products;
		}
	}

	public int maxInventory
	{
		get
		{
			return _maxInventory;
		}
		set
		{
			_maxInventory = value;
		}
	}

	private void Awake()
	{
		if (_products != null)
			InitHashTables();
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	public int CompareTo(Inventory other)
	{
		return other.name.CompareTo(this.name);
	}

	void InitHashTables()
	{
		if (_inventory == null)
			_inventory = new Dictionary<ProductItem, int>();
		if (_timeRemaining == null)
			_timeRemaining = new Dictionary<ProductItem, float>();
		if (itemsBeingProduced == null)
			itemsBeingProduced = new Dictionary<ProductItem, bool>();
		if (productLookup == null)
			productLookup = new Dictionary<string, ProductItem>();

		// Create unique instances of the items so they don't universally share the same values across buildings
		ProductItem[] newItems = new ProductItem[products.Count]; // create temporary array

		for(int i = 0; i < products.Count; i++)
		{
			// populate temporary array with new product items whose values are copied from the prefab's items
			ProductItem newItem = new ProductItem(products[i]);
			newItems[i] = newItem;
		}

		products.Clear(); // clear our prefab list

		for (int i = 0; i < newItems.Length; i++)
			products.Add(newItems[i]); // repopulate prefab list with unique instances of the items

		foreach (ProductItem product in _products)
		{
			_inventory.Add(product, 0); // initialize inventories to 0
			_timeRemaining.Add(product, 0f);
			productLookup.Add(product.itemName, product);
			itemsBeingProduced.Add(product, false);
		}
	}

	void AddProduct(ProductItem newItem)
	{
		if (_products.Count < 3)
		{
			_products.Add(newItem);
			inventory.Add(newItem, 0);
		}
	}

	void RemoveProduct(ProductItem removeItem)
	{
		if (_products.Count > 0)
		{
			_products.Remove(removeItem);
			inventory.Remove(removeItem);
		}
	}

	public void SellProduct(ProductItem productSold)
	{
		

		productSold = productLookup[productSold.itemName];

		Debug.Log("Consumer Buying " + productSold.itemName + " from: " + GetComponent<BuildingData>().name + ". $" + productSold.sellsFor + " Added to Treasury.");

		if (inventory[productSold] > 0)
		{
			inventory[productSold]--;
			BusinessManager.businessManager.totalMoney += productSold.sellsFor;

			if (GetComponent<BuildingData>().owner == "Player") 
				StartCoroutine(BusinessManager.businessManager.IncreaseNumbers());

			if(GetComponent<BuildingData>() == BuildingManager.buildingManager.selectedBuilding)
				StartCoroutine(inventoryPanel.DecreaseNumbers(productSold));
		}

		
		//Debug.Log("Sell " + productSold.name + " at " + this.name);
	}

	public void StartProduction(ProductItem item, BuildSellButtonHandler handler)
	{
		// called by button handler to start production.
		if (BusinessManager.businessManager.totalMoney < item.cost)
			return;

		timeRemaining[item] = item.time;
		itemsBeingProduced[item] = true;
		StartCoroutine(ExecuteProduction(item, handler));
	}

	IEnumerator ExecuteProduction(ProductItem item, BuildSellButtonHandler handler)
	{
		while(currentInventory < maxInventory && itemsBeingProduced[item] && BusinessManager.businessManager.totalMoney >= item.cost)
		{
			yield return WaitForItemTime(item, handler);
			AddInventory(item, handler);
		}
	}

	IEnumerator WaitForItemTime(ProductItem item, BuildSellButtonHandler handler)
	{
		// Create custom wait coroutine so we can check if inventory has exceeded capacity every frame instead of every x seconds
		float targetTime = Time.time + item.time;
		while (Time.time < targetTime && currentInventory < maxInventory && itemsBeingProduced[item] && BusinessManager.businessManager.totalMoney >= item.cost)
		{
			timeRemaining[item] = targetTime - Time.time;
			yield return null;
		}

		if (!itemsBeingProduced[item]) yield break;

		if (currentInventory >= maxInventory)
			StartCoroutine(WaitForInventorySpace(item, handler));
	}

	IEnumerator WaitForInventorySpace(ProductItem item, BuildSellButtonHandler handler)
	{
		// wait for inventory space to clear
		while (currentInventory >= maxInventory && itemsBeingProduced[item] && BusinessManager.businessManager.totalMoney >= item.cost)
			yield return null;

		// restart production
		StartCoroutine(ExecuteProduction(item, handler));
	}

	void AddInventory(ProductItem item, BuildSellButtonHandler handler)
	{
		if (currentInventory >= maxInventory)
			return;
		if (!itemsBeingProduced[item])
			return;

		inventory[item] += 1;
		currentInventory += 1;
		BusinessManager.businessManager.totalMoney -= item.cost;
		StartCoroutine(BusinessManager.businessManager.DecreaseNumbers());
		timeRemaining[item] = item.time;
		handler.UpdateInventory();
		StartCoroutine(inventoryPanel.IncreaseNumbers(item));
	}

	public void StopProduction(ProductItem item)
	{
		itemsBeingProduced[item] = false;
		timeRemaining[item] = item.time;
	}

	/*public void SetTime(ProductItem item, int time, int numberToBuild)
	{
		// called by button handler to start countdown.
		timeRemaining[item] = time;
		StartCoroutine(StartTimer(item, time, numberToBuild));
	}

	IEnumerator StartTimer(ProductItem item, int totalTime, int numberToBuild)
	{
		//int targetTime = (int)Time.time + totalTime;
		//timeRemaining[item] = targetTime - (int)Time.time;

		while (timeRemaining[item] > 0)
		{
			yield return new WaitForSeconds(1f);
			timeRemaining[item] -= 1;
		}

		AddInventory(item, numberToBuild);
	}

	void AddInventory(ProductItem item, int qty)
	{
		//Debug.Log("adding " + qty + " to " + item.itemName + " inventory");
		inventory[item] += qty;
		StartCoroutine(inventoryPanel.UpdateNumbers(item));
	}*/


}
