using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Wintellect.PowerCollections;

public class Consumer
{

	//SHOULD CONSUMERS HAVE MONEY TO SPEND?

	[SerializeField]
	List<ProductItem> desiredItems;

	//Dictionary<ProductItem, float> currentDesire; // how much this consumer desires a given item at this time
	OrderedMultiDictionary<float, ProductItem> currentDemand;
	Dictionary<ProductItem, float> weights;
	Dictionary<ProductItem, float> demandThresholds; // minimum amount of demand needed for consumer to pursue an item.

	bool hasBought = false;

	/*SortedDictionary<int, BuildingData> sortedPrices;
	SortedDictionary<int, BuildingData> sortedQuality;
	SortedDictionary<int, BuildingData> sortedAwareness;*/

	//SortedDictionary<float, Item> itemPurchasePriority;

	/*string _name;

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
	}*/


	public Consumer()
	{
		//name = newName;
		InitDesiredItems();
		InitWeights();
		InitDemand();
		InitMoney();
		
		/*SortedDictionary<float, Item> test = new SortedDictionary<float, Item>(new DescendingComparer<float>(Comparer<float>.Default));

		foreach (Item testVal in desiredItems)
			test.Add(Random.Range(0f, 100f), testVal);

		foreach (KeyValuePair<float, Item> testVal in test)
			Debug.Log(testVal.Value.itemName + " = " + testVal.Key);*/
	}

	void InitDesiredItems()
	{
		desiredItems = new List<ProductItem>(2);
		//currentDesire = new Dictionary<ProductItem, float>(desiredItems.Capacity);

		ProductItem[] temp = (ProductItem[])Resources.LoadAll<ProductItem>("Data/Items/Products");

		/*for (int i = 0; i < temp.Length; i++)
		{
			//if (temp[i].itemName == "Pie")
			//{
				desiredItems.Add(temp[i]);
				//currentDesire.Add(temp[i], 50);
			//}
		}*/

		//Debug.Log("Consumer desires: " + desiredItems[0].itemName);

		for (int i = 0; i < desiredItems.Capacity; i++)
		{
				int rand = Random.Range(0, temp.Length);
				while (desiredItems.Contains(temp[rand]))
					// Reroll RNG until we get non-duplicate items
					rand = Random.Range(0, temp.Length); // This could potentially lock program or take really long to load as number of items scales up

				desiredItems.Add(temp[rand]);
			//	currentDesire.Add(temp[rand], Random.Range(0, 100));
		}

		Debug.Log("Consumer desires: " + desiredItems[0].itemName + ", " + desiredItems[1].itemName);
	}

	void InitDemand()
	{
		currentDemand = new OrderedMultiDictionary<float, ProductItem>(false, new DescendingComparer<float>(Comparer<float>.Default));
		demandThresholds = new Dictionary<ProductItem, float>();

		int rand = Random.Range(0, 100);

		foreach (ProductItem temp in desiredItems)
		{
			while (currentDemand.ContainsKey(rand))
				rand = Random.Range(0, 100);

			currentDemand.Add(rand, temp);
			demandThresholds.Add(temp, 100 - (weights[temp] * 100)); // create specific thresholds for each item. Difference between weighted % of 100
			Debug.Log(temp.itemName + " CurrentDemand: " + rand);
			Debug.Log(temp.itemName + " Threshold: " + demandThresholds[temp]);
		}
	}

	void InitWeights()
	{
		weights = new Dictionary<ProductItem, float>(desiredItems.Count);

		foreach (ProductItem temp in desiredItems)
			weights.Add(temp, Random.Range(0f, 1f));
	}

	void InitMoney()
	{

	}

	public IEnumerator StartAI()
	{
		/// <summary>
		/// Current demand for any given item will be dictated by time and a weight.Every 5 seconds of time, demands update. They
		/// increase if they haven't purchased the 
		/// demanded item recently.
		/// 
		/// Every time update, iterate through current demand and do Currentdemand[i] += (5) * weight[currentdemand[i]
		/// 
		/// NEED PRIORITY QUEUE itemsToBuy
		/// For each item in currentDemand itemToBuy.add(item)
		/// 
		/// If item is available, purchase desired item. 
		/// 
		/// Currentdemand[itemtobuy] = 0;
		/// ItemToBuy.pop;
		/// </summary>
		/// 

	

		while (true)
		{
			hasBought = false;
			float keyToWipe = 0f;
			ProductItem itemToWipe = null;

			Dictionary<ProductItem, float> itemsToChange = new Dictionary<ProductItem, float>();
			//List<float> keysToChange = new List<float>(currentDemand.Count);
			//List<ProductItem> itemsToChange = new List<ProductItem>(currentDemand.Count);
			//if(currentDemand.Keys.First() >= demandThreshold)
			//	BuyProduct(currentDemand.Values.First());
			yield return new WaitForSeconds(ConsumerManager.consumerManager.AIUpdateTime);

			foreach (KeyValuePair<float, ProductItem> demand in currentDemand.KeyValuePairs)
			{

				if (demand.Key >= demandThresholds[demand.Value] && !hasBought)
				{
					// consumer can only buy one product in a single update. However, their highest demand product
					// might be out of stock. So we need to check hasBought bool after attempting a purchase.
					// This makes some of the code redundant, which is unfortunate. Will probably refactor later.
					BuyProduct(demand.Value);

					//while(tryingToBuy) yield return null;

					if (hasBought)
					{
						keyToWipe = demand.Key;
						itemToWipe = demand.Value;
					}
					else
					{
						//keysToChange.Add(demand.Key);
						itemsToChange.Add(demand.Value, demand.Key);
					}
				}
				else
				{
				//	keysToChange.Add(demand.Key);
					itemsToChange.Add(demand.Value, demand.Key);
				}
			}

			if (hasBought)
				WipeDemand(keyToWipe, itemToWipe);

			IncreaseDemand(itemsToChange);

			//BuyProduct(desiredItems[0]);
			//float quality = (value[index] / maxqualityinlist) * 0.6f;
			// float price = (sqrt(highestpriceinlist / price[index])) / sqrt(largestpriceinlist / lowestprice)) * .4
			// float awareness = (awareness[index] / highestawarenessinlist) * .2
			// float buildingToBuy = quality + price + awareness;

			//Debug.Log("Hi");
		}
	}

	void WipeDemand(float index, ProductItem item)
	{
		currentDemand.Remove(index, item);
		currentDemand.Add(0f, item);
	}

	void IncreaseDemand(Dictionary<ProductItem, float> items)
	{

		foreach(KeyValuePair<ProductItem, float> value in items)
		{
			//if (!currentDemand.ContainsKey(values[i])) continue;

			float newVal = Mathf.Clamp(value.Value + ConsumerManager.consumerManager.consumerDemandIncreaseRate, 0f, 100f);
			Debug.Log("Increase " + value.Key.itemName + " Demand from " + value.Value + " to new Value: " + newVal);
			currentDemand.Remove(value.Value, value.Key);
			currentDemand.Add(newVal, value.Key);
		}
		// FOR NOW: Demand will increase at a constant rate of 5. Essentially it increases by 1 per second
		
		//Currentdemand[i] += (5) * weight[currentdemand[i]
		/*foreach (KeyValuePair<float, ProductItem> temp in currentDemand)
		{
			float newVal = temp.Key + currentDesire[temp.Value] * weights[temp.Value];
			currentDemand.Remove(temp.Key);
			currentDemand.Add(newVal, temp.Value);
		}*/
	}

	void BuyProduct(ProductItem itemToBuy)
	{
		//tryingToBuy = true;
		//Debug.Log("AI Start Searching For: " + itemToBuy.itemName);

		OrderedMultiDictionary<float, BuildingData> sortedQuality = new OrderedMultiDictionary<float, BuildingData>(false);
		OrderedMultiDictionary<float, BuildingData> sortedPrice = new OrderedMultiDictionary<float, BuildingData>(false);
		OrderedMultiDictionary<float, BuildingData> sortedAwareness = new OrderedMultiDictionary<float, BuildingData>(false);
		OrderedMultiDictionary<float, Inventory> buildingsToBuyFrom = new OrderedMultiDictionary<float, Inventory>(false);

		/*SortedDictionary<BuildingData, float> sortedQuality = new SortedDictionary<BuildingData, float>();
		SortedDictionary<BuildingData, float> sortedPrice = new SortedDictionary<BuildingData, float>();
		SortedDictionary<BuildingData, float> sortedAwareness = new SortedDictionary<BuildingData, float>();
		SortedDictionary<Inventory, float> buildingsToBuyFrom = new SortedDictionary<Inventory, float>();

		sortedQuality = sortedQuality.OrderBy(x => x.Key.Parse(x.Key)).ToList();*/

		foreach (KeyValuePair<BuildingData, Inventory> value in BuildingManager.buildingManager.productInventories)
		{
			// search through every building to find out if they have the product we need. If they do, add them to
			// some Priority queues.
			foreach (ProductItem item in value.Value.products)
			{
				if (item.itemName == itemToBuy.itemName && value.Value.inventory[item] > 0)
				{
					// grab the data from this building's item.
					sortedQuality.Add(item.quality, value.Key);
					sortedPrice.Add(item.sellsFor, value.Key);
					sortedAwareness.Add(value.Key.brandAwareness, value.Key);
					break;
				}
			}
		}

		if (sortedQuality.Count <= 0)
		{
			Debug.Log("No inventory of " + itemToBuy.itemName + " Available");
			//tryingToBuy = false;
			hasBought = false;
			return;
		}
		else if (sortedQuality.Count == 1)
		{
			//if only 1 building is selling the product, we don't need to run any more calculation.
			BuildingManager.buildingManager.productInventories[sortedQuality.Values.First()].SellProduct(itemToBuy);
			hasBought = true;
			//tryingToBuy = false;
			return;
		}
		else
		{
			// priority queues are sorted. Now do some math
			float highestQuality = sortedQuality.Keys.Last();
			float highestPrice = sortedPrice.Keys.Last();
			float lowestPrice = sortedPrice.Keys.First();
			float highestAwareness = sortedAwareness.Keys.Last();

			float quality, price, awareness, buildingToBuy;

			foreach (KeyValuePair<BuildingData, Inventory> value in BuildingManager.buildingManager.productInventories)
			{
				foreach (ProductItem item in value.Value.products)
				{
					if (item.itemName == itemToBuy.itemName && value.Value.inventory[item] > 0)
					{
					//	Debug.Log("Data for " + value.Key.name);
					//	Debug.Log("Initial item quality: " + item.quality);
						//if (highestQuality <= 0)
						//	quality = 0;
						//else
						quality = (item.quality / ConsumerManager.consumerManager.highestQuality) * ConsumerManager.consumerManager.qualityWeight;
						//quality = (item.quality / highestQuality) * qualityWeight;
					//	Debug.Log("Post calculation item quality " + quality);
					//	Debug.Log("Initial item Price: " + item.sellsFor);
						price = (Mathf.Sqrt(highestPrice / item.sellsFor)) / (Mathf.Sqrt(highestPrice / lowestPrice)) * ConsumerManager.consumerManager.priceWeight;
					//	Debug.Log("Post Calc item price: " + price);
					//	Debug.Log("Initial Awareness: " + value.Key.brandAwareness);
						//if (highestAwareness <= 0)
						//	awareness = 0;
						//else
						awareness = (value.Key.brandAwareness / ConsumerManager.consumerManager.highestAwareness) * ConsumerManager.consumerManager.awarenessWeight;

						//awareness = (value.Key.brandAwareness / highestAwareness) * awarenessWeight;
					//	Debug.Log("Post Awareness: " + awareness);

						buildingToBuy = quality + price + awareness;
					//	Debug.Log(value.Key.name + " value: " + buildingToBuy);



						buildingsToBuyFrom.Add(buildingToBuy, value.Value);
					}
				}
			}

			buildingsToBuyFrom.Values.Last().SellProduct(itemToBuy);

			hasBought = true;
			//tryingToBuy = false;

			//buildingsToBuyFrom.Values.First()
		}
	}
}
