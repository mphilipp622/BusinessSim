using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProductItem : Item, IComparable<ProductItem>
{
	int _quality = 0;

	public int quality
	{
		get
		{
			return _quality;
		}
		set
		{
			_quality = value;
		}
	}

	public ProductItem(ProductItem copy)
	{
		quality = 0;
		cost = copy.cost;
		sellsFor = copy.sellsFor;
		unlocked = copy.unlocked;
		time = copy.time;
		itemName = copy.name;
		description = copy.description;
		itemIcon = copy.itemIcon;
	}

	public int CompareTo(ProductItem other)
	{
		return other.name.CompareTo(this.name);
	}

	/*public int CompareTo(ProductItem other)
	{
		return other.quality.CompareTo(this.quality);
	}*/

	/*public int Compare(ProductItem item1, ProductItem, item2)
	{
		if()
	}*/

	/*[SerializeField]
	[Tooltip("These are the resources that are required to produce this item.")]
	Resource[] requiredResources;*/

	/*protected override IEnumerator Countdown()
	{
		float targetTime = Time.time + this.time;

		while (Time.time < targetTime)
			yield return null;

		//AddItemToInventory();
	}*/
}
