using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public static ItemManager itemManager = null;

	Dictionary<ResearchItem, ResearchItem> _lockedResearch;
	Dictionary<ResearchItem, ResearchItem> _unlockedResearch;
	Dictionary<ProductItem, ProductItem> _lockedProducts;
	Dictionary<ProductItem, ProductItem> _unlockedProducts;
	Dictionary<BuildingData, BuildingData> _lockedBuildings;
	Dictionary<BuildingData, BuildingData> _unlockedBuildings;

	public Dictionary<ResearchItem, ResearchItem> lockedResearch
	{
		get
		{
			return _lockedResearch;
		}
	}

	public Dictionary<ResearchItem, ResearchItem> unlockedResearch
	{
		get
		{
			return _unlockedResearch;
		}
	}

	public Dictionary<ProductItem, ProductItem> lockedProducts
	{
		get
		{
			return _lockedProducts;
		}
	}

	public Dictionary<ProductItem, ProductItem> unlockedProducts
	{
		get
		{
			return _unlockedProducts;
		}
	}

	public Dictionary<BuildingData, BuildingData> lockedBuildings
	{
		get
		{
			return _lockedBuildings;
		}
	}

	public Dictionary < BuildingData, BuildingData> unlockedBuildings
	{
		get
		{
			return _unlockedBuildings;
		}
	}

	void Awake()
	{
		InitSingleton();
		InitHashTables();
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	void InitHashTables()
	{
		//ProductItem[] temp = (ProductItem[])Resources.LoadAll<ProductItem>("Data/Items/Products");
		if (_lockedResearch == null)
			_lockedResearch = new Dictionary<ResearchItem, ResearchItem>();
		if (_unlockedResearch == null)
			_unlockedResearch = new Dictionary<ResearchItem, ResearchItem>();
		if (_lockedProducts == null)
			_lockedProducts = new Dictionary<ProductItem, ProductItem>();
		if (_unlockedProducts == null)
			_unlockedProducts = new Dictionary<ProductItem, ProductItem>();
		if (_lockedBuildings == null)
			_lockedBuildings = new Dictionary<BuildingData, BuildingData>();
		if (_unlockedBuildings == null)
			_unlockedBuildings = new Dictionary<BuildingData, BuildingData>();

		foreach (ProductItem item in (ProductItem[])Resources.LoadAll<ProductItem>("Data/Items/Products"))
		{
			if (item.unlocked) unlockedProducts.Add(item, item);
			else lockedProducts.Add(item, item);
		}

		foreach(ResearchItem item in (ResearchItem[])Resources.LoadAll<ResearchItem>("Data/Items/Research"))
		{
			if (item.unlocked) unlockedResearch.Add(item, item);
			else lockedResearch.Add(item, item);
		}

		// INIT BUILDING HASH TABLES LATER
	}

	void InitSingleton()
	{
		if (itemManager == null)
			itemManager = this;

		else if (itemManager != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}
}
