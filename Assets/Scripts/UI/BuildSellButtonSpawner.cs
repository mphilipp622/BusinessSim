using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSellButtonSpawner : MonoBehaviour
{

	List<GameObject> currentItems;

	[SerializeField]
	GameObject itemButtonPrefab;

	[SerializeField]
	GameObject buildSellScrollView, researchScrollView;
	

	void Start ()
	{
		currentItems = new List<GameObject>();
		buildSellScrollView.SetActive(false);
		
	}
	
	void Update ()
	{
		if (researchScrollView.activeSelf)
			DisplayUnlockedProductItems();
	}

	public void ShowTransform()
	{
		
	}

	public void DisplayUnlockedProductItems()
	{

		if (buildSellScrollView.activeSelf)
		{
			buildSellScrollView.SetActive(false);
			if (currentItems.Count > 0)
			{
				foreach (GameObject go in currentItems)
					Destroy(go);

				currentItems.Clear();
			}

			return;
		}
		else
			buildSellScrollView.SetActive(true);

		GameObject newObj;

		if (BuildingManager.buildingManager.selectedBuilding.GetType() != typeof(ResearchBuilding))
		{
			foreach (ProductItem item in BuildingManager.buildingManager.productInventories[BuildingManager.buildingManager.selectedBuilding].products)
			{
				newObj = (GameObject)Instantiate(itemButtonPrefab, transform);
				newObj.GetComponent<BuildSellButtonHandler>().thisItem = item;
				currentItems.Add(newObj);
			}
		}
		/*if (BuildingManager.buildingManager.selectedBuilding.GetType() == typeof(ResearchBuilding))
		{
			foreach (ResearchItem item in BuildingManager.buildingManager.researchInventories[BuildingManager.buildingManager.selectedBuilding].research)
			{
				costText.text = item.cost.ToString();
				sellsForText.text = item.sellsFor.ToString();
				itemNameText.text = item.itemName;
				itemImage.sprite = item.itemIcon;
				newObj = (GameObject)Instantiate(itemButtonPrefab, transform);
				currentItems.Add(newObj);
			}
		}*/


		/*	foreach (KeyValuePair<ProductItem, ProductItem> item in ItemManager.itemManager.unlockedProducts)
		{
			costText.text = item.Value.cost.ToString();
			sellsForText.text = item.Value.sellsFor.ToString();
			itemNameText.text = item.Value.itemName;
			itemImage.sprite = item.Value.itemIcon;
			newObj = (GameObject)Instantiate(itemButtonPrefab, transform);
			currentItems.Add(newObj);
		}*/
	}
}
