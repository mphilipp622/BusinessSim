using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButtonHandler : MonoBehaviour
{

	List<GameObject> currentItems;

	[SerializeField]
	GameObject itemButtonPrefab;

	[SerializeField]
	Text costText, itemNameText;

	[SerializeField]
	Image itemImage;

	[SerializeField]
	GameObject researchScrollView, buildSellScrollView;

	void Start ()
	{
		currentItems = new List<GameObject>();
		researchScrollView.SetActive(false);
	}
	
	void Update ()
	{
		if (buildSellScrollView.activeSelf)
			DisplayLockedResearchItems();
	}

	/*public void ShowTransform()
	{
		
	}*/

	public void DisplayLockedResearchItems()
	{

		if (researchScrollView.activeSelf)
		{
			researchScrollView.SetActive(false);
			if (currentItems.Count > 0)
			{
				foreach (GameObject go in currentItems)
					Destroy(go);
				currentItems.Clear();
			}
			return;
		}
		else
			researchScrollView.SetActive(true);

		GameObject newObj;

		foreach(KeyValuePair<ResearchItem, ResearchItem> item in ItemManager.itemManager.lockedResearch)
		{
			costText.text = item.Value.cost.ToString();
			itemNameText.text = item.Value.itemName;
			itemImage.sprite = item.Value.itemIcon;
			newObj = (GameObject)Instantiate(itemButtonPrefab, transform);
			currentItems.Add(newObj);
		}
	}
}
