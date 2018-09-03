using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class NamePopupWindow : EditorWindow
{
	static string buildingType;

	string nonStaticBuildingType;

	string _buildingName;
	bool _isClosed = false;

	bool compiling = false;
	double targetTime;

	double waitTime = 5;

	public string buildingName
	{
		get
		{
			return _buildingName;
		}
		set
		{
			_buildingName = value;
		}
	}

	public static void Init(string newBuildingType)
	{
		buildingType = newBuildingType;
		NamePopupWindow window = ScriptableObject.CreateInstance<NamePopupWindow>();
		window.position = new Rect(Screen.width / 2, Screen.height / 2, 256, 256);
		window.ShowPopup();
	}

	void OnGUI()
	{

		this.Repaint();

		if (!compiling)
		{
			EditorGUILayout.LabelField("Enter a Name For This Building", EditorStyles.wordWrappedLabel);
			GUILayout.Space(20);

			buildingName = EditorGUILayout.TextField(buildingName);
			//Debug.Log(buildingType);

			if (GUILayout.Button("Accept"))
			{
				compiling = true;
				targetTime = EditorApplication.timeSinceStartup + waitTime;
				CreateScript();
			}
			else if (GUILayout.Button("Cancel"))
				this.Close();
		}
		else if (compiling)
		{
			
			if (EditorApplication.timeSinceStartup >= targetTime)
			{
				//this.Close();
				ScriptableObjectUtility.PublishAsset(buildingName, nonStaticBuildingType);
				compiling = false;
				this.Close();
			}
		}
	}


	void CreateScript()
	{
		nonStaticBuildingType = buildingType;
		ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
		/*else if(buildingType == "Manufacturing")
			ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
		else if (buildingType == "Research")
			ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
		else if (buildingType == "NaturalResources")
			ScriptableObjectUtility.CreateBuildingAsset(buildingType, buildingName);
			*/
	}
}
#endif