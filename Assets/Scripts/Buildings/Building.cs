using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : ScriptableObject
{
	[SerializeField]
	int _cost;

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
}
