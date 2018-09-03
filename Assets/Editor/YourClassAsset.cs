using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
	[MenuItem("Renegade Tools/Create New Item/Research Item")]
	public static void CreateResearchItem()
	{
		ScriptableObjectUtility.CreateItemAsset<ResearchItem>();
	}

	[MenuItem("Renegade Tools/Create New Item/Product Item")]
	public static void CreateProductItem()
	{
		ScriptableObjectUtility.CreateItemAsset<ProductItem>();
	}

	[MenuItem("Renegade Tools/Create New Building/Service Building")]
	public static void CreateServiceBuilding()
	{
		NamePopupWindow.Init("Service");
	}

	[MenuItem("Renegade Tools/Create New Building/Manufacturing Building")]
	public static void CreateManufacturingBuilding()
	{
		NamePopupWindow.Init("Manufacturing");
	}

	[MenuItem("Renegade Tools/Create New Building/Research Building")]
	public static void CreateResearchBuilding()
	{
		NamePopupWindow.Init("Research");
	}

	[MenuItem("Renegade Tools/Create New Building/Natural Resources Building")]
	public static void CreateNaturalResourcesBuilding()
	{
		NamePopupWindow.Init("NaturalResources");
	}
}