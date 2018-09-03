using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{

	[SerializeField]
	Image[] buildingProductImages;

	[SerializeField]
	Text[] buildingProductText;

	Dictionary<Item, Image> _buildingProductImagesHash;

	Dictionary<Item, Text> _buildingProductTextHash;
	
	public Dictionary<Item, Image> buildingProductImagesHash
	{
		get
		{
			return _buildingProductImagesHash;
		}
		set
		{
			_buildingProductImagesHash = value;
		}
	}

	public Dictionary<Item, Text> buildingProductTextHash
	{
		get
		{
			return _buildingProductTextHash;
		}
		set
		{
			_buildingProductTextHash = value;
		}
	}

	void Start ()
	{
		StartCoroutine(InitData());
	}
	
	void Update ()
	{
		
	}

	IEnumerator InitData()
	{
		while (BuildingManager.buildingManager.selectedInventory == null)
			yield return null;

		_buildingProductImagesHash = new Dictionary<Item, Image>();
		_buildingProductTextHash = new Dictionary<Item, Text>();

		int i = 0;

		foreach(ProductItem item in BuildingManager.buildingManager.selectedInventory.products)
		{
			_buildingProductImagesHash.Add(item, buildingProductImages[i]);
			_buildingProductTextHash.Add(item, buildingProductText[i]);

			buildingProductImages[i].sprite = item.itemIcon;
			buildingProductText[i].text = BuildingManager.buildingManager.selectedInventory.inventory[item].ToString();

			i++;
		}
		
	}

	public IEnumerator DecreaseNumbers(ProductItem item)
	{
		StopCoroutine(IncreaseNumbers(item));
		buildingProductTextHash[item].text = "<color=red>" + BuildingManager.buildingManager.selectedInventory.inventory[item].ToString() + "</color>";

		yield return new WaitForSeconds(3f);

		buildingProductTextHash[item].text = BuildingManager.buildingManager.selectedInventory.inventory[item].ToString();
	}

	public IEnumerator IncreaseNumbers(ProductItem item)
	{
		StopCoroutine(DecreaseNumbers(item));
		buildingProductTextHash[item].text = "<color=green>" + BuildingManager.buildingManager.selectedInventory.inventory[item].ToString() + "</color>";

		yield return new WaitForSeconds(3f);

		buildingProductTextHash[item].text = BuildingManager.buildingManager.selectedInventory.inventory[item].ToString();
	}
}
