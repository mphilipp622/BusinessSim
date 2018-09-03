using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
	[SerializeField]
	[Tooltip("Cost of the item in Dollars ($). FOR PRODUCTS THIS IS COST PER UNIT")]
	int _cost;

	[SerializeField]
	[Tooltip("How much an item sells for in Dollars ($).")]
	int _sellsFor;

	[SerializeField]
	[Tooltip("Check this box if item is unlocked by default. Leave unchecked if item is not unlocked by default.")]
	bool _unlocked = false;

	[SerializeField]
	[Tooltip("Time in seconds that it takes for item to be researched or produced.")]
	int _time;

	[SerializeField]
	string _name;

	[SerializeField]
	[TextArea]
	string _description;

	[SerializeField]
	Sprite _itemIcon;

	public int cost
	{
		get
		{
			return _cost;
		}
		set
		{
			_cost = value;
		}
	}

	public int sellsFor
	{
		get
		{
			return _sellsFor;
		}
		set
		{
			_sellsFor = value;
		}
	}

	public bool unlocked
	{
		get
		{
			return _unlocked;
		}
		set
		{
			_unlocked = value;
		}
	}

	public int time
	{
		get
		{
			return _time;
		}
		set
		{
			_time = value;
		}
	}

	public string itemName
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	public string description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}

	public Sprite itemIcon
	{
		get
		{
			return _itemIcon;
		}
		set
		{
			_itemIcon = value;
		}
	}

	//protected abstract void DisplayInfo();
	//protected abstract IEnumerator Countdown();
}
