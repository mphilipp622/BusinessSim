using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResourcesBuilding : BuildingData
{
	int _resourcesLeft, _currentInventory;

	[SerializeField]
	[Tooltip("The base amount of pollution this type of building produces when it has a single employee")]
	int _basePollution;

	int _pollution;

	[SerializeField]
	[Tooltip("This value determines the scale of pollution increase per employee that is working here. 0.2 would be 20% increase per employee, 1 would be 100% increase per employee, etc.")]
	float _pollutionScalePerEmployee;

	public int resourcesLeft
	{
		get
		{
			return _resourcesLeft;
		}
		set
		{
			_resourcesLeft = value;
		}
	}

	public int currentInventory
	{
		get
		{
			return _currentInventory;
		}
		set
		{
			_currentInventory = value;
		}
	}

	public int basePollution
	{
		get
		{
			return _basePollution;
		}
	}

	public float pollutionScalePerEmployee
	{
		get
		{
			return _pollutionScalePerEmployee;
		}
	}

	public int pollution
	{
		get
		{
			if (currentNumberOfEmployees == 0)
				return 0;
			else
			{
				_pollution = basePollution;
				for (int i = 0; i < currentNumberOfEmployees; i++)
					_pollution += Mathf.RoundToInt((_pollution * _pollutionScalePerEmployee));

				return _pollution;
			}
		}
	}

	protected override int operatingCost
	{
		get
		{
			return (this.employeeWage * this.currentNumberOfEmployees) + this.maintenanceCost + this.utilitiesCost;
		}
	}

	protected override void PurchaseUpgrade()
	{
		throw new NotImplementedException();
	}

	protected override IEnumerator UpdateOperatingCost()
	{
		throw new NotImplementedException();
	}

	private void Start()
	{

	}

	private void Update()
	{
		
	}
}
