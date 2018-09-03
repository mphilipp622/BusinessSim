using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BuildingData : MonoBehaviour, IComparable<BuildingData>
{

	[SerializeField]
	string _name;

	[SerializeField]
	string _owner;

	[SerializeField]
	[TextArea]
	string _description;

	[SerializeField]
	[Tooltip("Maximum # of employees this building can have")]
	int _maxNumberOfEmployees;

	int _currentNumberOfEmployees;

	[SerializeField]
	[Tooltip("Set the base wage an employee of this building will be making. Player can adjust this value on their own, in game")]
	int _employeeWage;

	[SerializeField]
	[Tooltip("Cost in whole number Dollars ($) that it takes to construct the building on an empty lot")]
	int _costToBuild;

	[SerializeField]
	[Tooltip("Default general maintenance cost of the building. This value will increase as the building ages.")]
	int _baseMaintenanceCost;

	[SerializeField]
	[Tooltip("This value will be multiplied with the general maintenance cost to determine the increase in maintenance cost per year.")]
	float _maintenaceScaleFactor = 1f;

	[SerializeField]
	[Tooltip("Initial utilities cost of the building. This will be affected by future upgrades, number of employees, and more.")]
	int _utilitiesCost;

	int _brandAwareness = 0;

	int _buildingAge = 0;

	int _income; // how much money this building is making

	int _profit; // income - operatingCost. If this is negative, you are losing money on this building.

	int _publicOpinion;

	/*
	[SerializeField]
	Upgrade[] _currentUpgrades;

	[SerializeField]
	Upgrade[] _upgradesForSale;

	[SerializeField]
	Regulation[] _regulations;
	*/

	public string name
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

	public string owner
	{
		get
		{
			return _owner;
		}
		set
		{
			_owner = value;
		}
	}

	public string description
	{
		get
		{
			return _description;
		}
	}

	public int maxNumberOfEmployees
	{
		get
		{
			return _maxNumberOfEmployees;
		}
		set
		{
			_maxNumberOfEmployees = value;
		}
	}

	public int currentNumberOfEmployees
	{
		get
		{
			return _currentNumberOfEmployees;
		}
		set
		{
			_currentNumberOfEmployees = value;
		}
	}

	public int costToBuild
	{
		get
		{
			return _costToBuild;
		}
		set
		{
			_costToBuild = value;
		}
	}

	public int brandAwareness
	{
		get
		{
			return _brandAwareness;
		}
		set
		{
			_brandAwareness = value;
		}
	}

	public int employeeWage
	{
		get
		{
			return _employeeWage;
		}
		set
		{
			_employeeWage = value;
		}
	}

	public int income
	{
		get
		{
			return _income;
		}
		set
		{
			_income = value;
		}
	}

	public int profit
	{
		get
		{
			return income - operatingCost;
		}
	}

	public int publicOpinion
	{
		get
		{
			return _publicOpinion;
		}
		set
		{
			_publicOpinion = value;
		}
	}

	public int maintenanceCost
	{
		get
		{
			return _baseMaintenanceCost;
		}
		//set
		//{
		//	_baseMaintenanceCost = value;
		//}
	}

	public int buildingAge
	{
		get
		{
			return _buildingAge;
		}
		set
		{
			_buildingAge = value;
		}
	}

	public float maintenanceScaleFactor
	{
		get
		{
			return _maintenaceScaleFactor;
		}
		set
		{
			_maintenaceScaleFactor = value;
		}
	}

	public int utilitiesCost
	{
		get
		{
			return _utilitiesCost;
		}
		set
		{
			_utilitiesCost = value;
		}
	}

	protected abstract int operatingCost { get; }

	protected abstract void PurchaseUpgrade();

	protected IEnumerator UpdateMaintenanceCost()
	{
		yield return StartCoroutine(CountdownOneYear());

		_baseMaintenanceCost += Mathf.RoundToInt((buildingAge * maintenanceScaleFactor));
		StartCoroutine(UpdateMaintenanceCost());
	}

	public int CompareTo(BuildingData other)
	{
		return other.name.CompareTo(this.name);
	}

	protected abstract IEnumerator UpdateOperatingCost();

	protected IEnumerator CountdownOneYear()
	{
		float targetTime = Time.time + 3600; // current time + 1 hour.

		while (Time.time < targetTime)
			yield return null;
	}

	protected IEnumerator CountdownOneMonth()
	{
		float targetTime = Time.time + 300; // current time + 5 minutes.

		while (Time.time < targetTime)
			yield return null;
	}

}
