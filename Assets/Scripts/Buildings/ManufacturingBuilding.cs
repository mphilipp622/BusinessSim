using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManufacturingBuilding : BuildingData
{

	int _moneySpentOnResourcesThisMonth, _availableResources, _brandAwareness, _workplaceIncidents;

	public int availableResources
	{
		get
		{
			return _availableResources;
		}
		set
		{
			_availableResources = value;
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

	public int workplaceIncidents
	{
		get
		{
			return _workplaceIncidents;
		}
		set
		{
			_workplaceIncidents = value;
		}
	}

	public int moneySpentOnResourcesThisMonth
	{
		get
		{
			return _moneySpentOnResourcesThisMonth;
		}
		set
		{
			_moneySpentOnResourcesThisMonth = value;
		}
	}

	protected override int operatingCost
	{
		get
		{
			return (this.employeeWage * this.currentNumberOfEmployees) + this.maintenanceCost + this.utilitiesCost + moneySpentOnResourcesThisMonth;
		}
	}

	protected override void PurchaseUpgrade()
	{
		throw new NotImplementedException();
	}

	protected override IEnumerator UpdateOperatingCost()
	{
		yield return StartCoroutine(CountdownOneMonth());

		moneySpentOnResourcesThisMonth = 0;
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		
	}
}
