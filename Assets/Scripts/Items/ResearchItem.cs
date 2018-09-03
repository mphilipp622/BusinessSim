using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchItem : Item
{
	[SerializeField]
	[Tooltip("Amount of Research Points this Research costs")]
	int _researchCost;

	[SerializeField]
	[Tooltip("Items that are unlocked upon the completion of this research. Items can include other Research items or Product items.\n\n" +
		"HOW TO USE: Set the size of the array then press ENTER. Once that's done, a list of items should show up. Drag and drop items from project manager window into these slots to set the dependent items.")]
	Item[] _dependentItems;

	[SerializeField]
	[Tooltip("Buildings that are unlocked upon the completion of this research.\n\n" +
		"HOW TO USE: Set the size of the array then press ENTER. Once that's done, a list of buildings should show up. Drag and drop buildings from project manager window into these slots to set the dependent buildings.")]
	BuildingData[] _dependentBuildings;

	public int researchCost
	{
		get
		{
			return _researchCost;
		}
		set
		{
			_researchCost = value;
		}
	}

	Item[] dependentItems
	{
		get
		{
			return _dependentItems;
		}
	}

	/*protected override IEnumerator Countdown()
	{
		float targetTime = Time.time + this.time;

		while (Time.time < targetTime)
			yield return null;

		UnlockItems();
	}*/

	protected void UnlockItems()
	{
		this.unlocked = true;

		foreach (Item item in dependentItems)
			item.unlocked = true;
	}
}
