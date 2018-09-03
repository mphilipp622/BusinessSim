#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public static class ScriptableObjectUtility
{
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>

	static GameObject newObj;
	static string pathAndName, name;

	public static void CreateItemAsset<T>() where T : Item
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string assetPathAndName = null;

		if (asset is ResearchItem)
			assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Data/Items/Research/NewResearchItem.asset");
		else if (asset is ProductItem)
			assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Data/Items/Products/NewProductItem.asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

	public static void CreateBuildingAsset(string buildingType, string buildingName) 
	{

		CreateScript(buildingName, buildingType);
		/*if (buildingType == "Service")
		{
			//newBuilding = new ServiceBuilding();
			//scriptPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Scripts/Buildings/Service/NewServiceBuilding.cs");
			CreateServiceScript(buildingName, buildingType);
			//newObj.GetComponent<CreateClass>().CreateServiceScript(buildingName, asset);
			//CreateClass.instance.StartCoroutine(CreateServiceScript(buildingName, asset));
			//StartCoroutine(CreateServiceScript(buildingName, asset));
			//IEnumerator create = CreateServiceScript(buildingName, asset);
			//newBuilding = new ServiceBuilding();
			//newObj.AddComponent<ServiceBuilding>();
		}*/
		/*else if (buildingType == "Manufacturing")
		{
			assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/Buildings/Manufacturing/NewManufacturingBuilding.prefab");
			newObj.AddComponent<ManufacturingBuilding>();
		}
		else if (buildingType == "Research")
		{
			assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/Buildings/Research/NewResearchBuilding.prefab");
			newObj.AddComponent<ResearchBuilding>();
		}
		else if (buildingType == "NaturalResources")
		{
			assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/Buildings/NaturalResources/NewResourcesBuilding.prefab");
			newObj.AddComponent<NaturalResourcesBuilding>();
		}*/
	}

	public static void CreateScript(string newBuildingName, string tempType)
	{

		// remove whitespace and minus
		name = newBuildingName.Replace(" ", "");
		name = name.Replace("-", "");
		//name = name.Replace("-", "_");
		string copyPath = "Assets/Scripts/Buildings/" + tempType + "/" + name + ".cs";

		if (File.Exists(copyPath) == false)
		{ // do not overwrite
			using (StreamWriter outfile =
				new StreamWriter(copyPath))
			{
				outfile.WriteLine("using System;");
				outfile.WriteLine("using System.Collections;");
				outfile.WriteLine("using System.Collections.Generic;");
				outfile.WriteLine("using UnityEngine;");
				outfile.WriteLine(" ");
				outfile.WriteLine("public class " + name + " : " + tempType + "Building \n{");
				outfile.WriteLine(" ");
				outfile.WriteLine("\tvoid Start () \n\t{");
				outfile.WriteLine(" ");
				outfile.WriteLine("\t}");
				outfile.WriteLine(" ");
				outfile.WriteLine("\tvoid Update () \n\t{");
				outfile.WriteLine(" ");
				outfile.WriteLine("\t}");
				outfile.WriteLine("}");
			}//File written

			//AssetDatabase.ImportAsset(copyPath, ImportAssetOptions.ForceSynchronousImport);
			//yield return null;
		}

		//	while (EditorApplication.isCompiling) yield return null;

		AssetDatabase.ImportAsset(copyPath, ImportAssetOptions.ForceUpdate);
		//AssetDatabase.Refresh();

		//pathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/Buildings/Service/" + name + ".prefab");

		//PublishAsset();
	}

	public static void PublishAsset(string newName, string thisBuildingType)
	{
		name = newName.Replace(" ", "");
		name = name.Replace("-", "");

		string prefabPath = "Assets/Prefabs/Buildings/" + thisBuildingType + "/" + name + ".prefab";

		newObj = new GameObject();
		newObj.AddComponent<SpriteRenderer>();

		if (thisBuildingType == "Research")
			newObj.AddComponent<InventoryResearch>();
		else
			newObj.AddComponent<Inventory>();

		newObj.AddComponent(Type.GetType(name));
		newObj.GetComponent<BuildingData>().name = name;
		newObj.tag = "Building";
		
		//GameObject.DestroyImmediate(gameObject.GetComponent<CreateClass>());
		PrefabUtility.CreatePrefab(prefabPath, newObj);
		//AssetDatabase.CreateAsset(scriptAsset, scriptPathAndName);
		GameObject.DestroyImmediate(newObj);

		//AssetDatabase.SaveAssets();
		//AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		//Selection.activeObject = finalAsset;

		Debug.Log("Finished Compiling " + name);
	}
}
#endif