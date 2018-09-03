using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryResearch : MonoBehaviour {

	[SerializeField]
	[Tooltip("List of products this building can make")]
	List<ResearchItem> _research;

	[SerializeField]
	InventoryPanel inventoryPanel;

	/*Dictionary<ResearchItem, Patent> _patents;

	public Dictionary<ResearchItem, Patent> patents
	{
		get
		{
			return _patents;
		}
		set
		{
			_patents = value;
		}
	}*/

	public List<ResearchItem> research
	{
		get
		{
			return _research;
		}
	}

	void Start()
	{
		if (_research != null)
			InitHashTables();
	}

	void Update()
	{

	}

	void InitHashTables()
	{
		/*foreach (ResearchItem research in _research)
		{
			if(research.unlocked  && you were first to unlock it )
				patents.Add(researc, Patent); // initialize inventories to 0
		}*/
	}

	void AddResearch(ResearchItem newItem)
	{
		if (_research.Count < 3)
		{
			_research.Add(newItem);
			//patents.Add(newItem, 0);
		}
	}

	void RemoveResearch(ResearchItem removeItem)
	{
		if (_research.Count > 0)
		{
			_research.Remove(removeItem);
			//patents.Remove(removeItem);
		}
	}

	void SellPatent(ResearchItem productSold)
	{
		//if (patents[productSold] > 0)
		//{
		//	//patents[productSold]--;
		//	BusinessManager.businessManager.totalMoney += productSold.sellsFor;
		//}
		//Debug.Log("Sell " + productSold.name + " at " + this.name);
	}
}
