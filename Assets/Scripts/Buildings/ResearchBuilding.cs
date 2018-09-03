using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchBuilding : BuildingData
{
	int _moneySpentOnResearchThisMonth;

	float _researchProgress;

	ResearchItem _currentResearch;

	int _patentsOwned;

	int _prestige;

	int _ethicsViolations;

	public int moneySpentOnResearchThisMonth
	{
		get
		{
			return _moneySpentOnResearchThisMonth;
		}
		set
		{
			moneySpentOnResearchThisMonth = value;
		}
	}

	public float researchProgress
	{
		get
		{
			return _researchProgress;
		}
		set
		{
			_researchProgress = value;
		}
	}

	public int patentsOwned
	{
		get
		{
			return _patentsOwned;
		}
		set
		{
			_patentsOwned = value;
		}
	}

	public ResearchItem currentResearch
	{
		get
		{
			return _currentResearch;
		}
		set
		{
			_currentResearch = value;
		}
	}

	public int prestige
	{
		get
		{
			return _prestige;
		}
		set
		{
			_prestige = value;
		}
	}

	public int ethicsViolations
	{
		get
		{
			return _ethicsViolations;
		}
		set
		{
			_ethicsViolations = value;
		}
	}

	protected override int operatingCost
	{
		get
		{
			return (this.employeeWage * this.currentNumberOfEmployees) + this.maintenanceCost + this.utilitiesCost + moneySpentOnResearchThisMonth;
		}
	}

	protected override void PurchaseUpgrade()
	{
		throw new NotImplementedException();
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		
	}

	protected override IEnumerator UpdateOperatingCost()
	{
		yield return StartCoroutine(CountdownOneMonth());

		moneySpentOnResearchThisMonth = 0;
	}
}
