using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceBuilding : BuildingData
{
	[SerializeField]
	[Tooltip("Maximum number of customers that can shop at this building at one time")]
	int _maxNumberOfCustomers;

	int _numberOfCustomers;

	[SerializeField]
	[Tooltip("Current Consumer Rating of this building based on customer reviews")]
	[Range(1, 5)]
	short _consumerRating = 3;

	//Customer[] _currentCustomers;

	public int maxNumberOfCustomers
	{
		get
		{
			return _maxNumberOfCustomers;
		}
		set
		{
			_maxNumberOfCustomers = value;
		}
	}

	public int numberOfCustomers
	{
		get
		{
			return _numberOfCustomers;
		}
		set
		{
			_numberOfCustomers = value;
		}
	}

	protected override int operatingCost
	{
		get
		{
			return (this.employeeWage * this.currentNumberOfEmployees) + maintenanceCost + utilitiesCost;
		}
	}

	private void Start()
	{

	}

	private void Update()
	{
		
	}

	protected override void PurchaseUpgrade()
	{
		throw new NotImplementedException();
	}

	protected override IEnumerator UpdateOperatingCost()
	{
		throw new NotImplementedException();
	}
}
