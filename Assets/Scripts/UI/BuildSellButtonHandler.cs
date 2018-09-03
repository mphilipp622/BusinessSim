using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSellButtonHandler : MonoBehaviour
{

	int _numToBuild = 0;

	int time = 0;

	[SerializeField]
	Text costText, itemNameText, sellsForText, timeText, inventoryText;

	[SerializeField]
	Image itemImage;

	[SerializeField]
	Button buildButton, cancelButton;

	[SerializeField]
	Slider timeSlider;

	ProductItem _thisItem;

	public ProductItem thisItem
	{
		get
		{
			return _thisItem;
		}
		set
		{
			_thisItem = value;
		}
	}

	public int numToBuild
	{
		get
		{
			return _numToBuild;
		}
	}

	void Start ()
	{
		InitPanel();
	}
	
	void Update ()
	{
		
	}

	private void OnGUI()
	{
		UpdateInventory();
	}

	public void IncreaseQty()
	{
		//numToBuild++;
		_numToBuild = Mathf.Clamp(++_numToBuild, 0, 999);
		//qtyNumberText.text = numToBuild.ToString();
		UpdateTotals();
	}

	public void DecreaseQty()
	{
		//numToBuild--;
		_numToBuild = Mathf.Clamp(--_numToBuild, 0, 999);
		//qtyNumberText.text = numToBuild.ToString();
		UpdateTotals();
	}

	void UpdateTotals()
	{
		//totalCostText.text = "Total Cost: <color=red>$" + (numToBuild * thisItem.cost).ToString() + "</color>";

		time = numToBuild * thisItem.time;
		int seconds = (numToBuild * thisItem.time) % 60;
		int minutes = ((numToBuild * thisItem.time) % 3600) / 60;
		int hours = (numToBuild * thisItem.time) / 3600;

		if (hours > 0)
			timeText.text = string.Format("Time: <color=red>{0}h {1}m {2}s</color>", hours, minutes, seconds);
		else if (minutes > 0)
			timeText.text = string.Format("Time: <color=red>{0}m {1}s</color>", minutes, seconds);
		else
			timeText.text = string.Format("Time: <color=red>{0}s</color>", seconds);
	}

	/*void DisableButtons()
	{
		buildButton.enabled = false;
		buildButton.image.color = new Color(buildButton.image.color.r, buildButton.image.color.g, buildButton.image.color.b, 0.2f);
		upButton.enabled = false;
		upButton.image.color = new Color(buildButton.image.color.r, buildButton.image.color.g, buildButton.image.color.b, 0.2f);
		downButton.enabled = false;
		downButton.image.color = new Color(buildButton.image.color.r, buildButton.image.color.g, buildButton.image.color.b, 0.2f);
	}

	void EnableButtons()
	{
		buildButton.enabled = true;
		buildButton.image.color = new Color(buildButton.image.color.r, buildButton.image.color.g, buildButton.image.color.b, 1f);
		upButton.enabled = true;
		upButton.image.color = new Color(buildButton.image.color.r, buildButton.image.color.g, buildButton.image.color.b, 1f);
		downButton.enabled = true;
		downButton.image.color = new Color(buildButton.image.color.r, buildButton.image.color.g, buildButton.image.color.b, 1f);
	}*/

	IEnumerator Countdown()
	{
		//DisableButtons();

		//while (BuildingManager.buildingManager.selectedInventory.timeRemaining[thisItem] <= 0)
		//	yield return null; // pause until timeremaining updates properly.

		while (BuildingManager.buildingManager.selectedInventory.timeRemaining[thisItem] > 0)
		{
			UpdateSlider();
			yield return null;
		//	SetTimeRemaining();
		}
		//UpdatePanel();
	}

	void SetTimeRemaining()
	{
		int seconds = (int) BuildingManager.buildingManager.selectedInventory.timeRemaining[thisItem] % 60;
		int minutes = (int) (BuildingManager.buildingManager.selectedInventory.timeRemaining[thisItem] % 3600) / 60;
		int hours = (int) BuildingManager.buildingManager.selectedInventory.timeRemaining[thisItem] / 3600;

		if (hours > 0)
			timeText.text = string.Format("Time: <color=red>{0}h {1}m {2}s</color>", hours, minutes, seconds);
		else if (minutes > 0)
			timeText.text = string.Format("Time: <color=red>{0}m {1}s</color>", minutes, seconds);
		else
			timeText.text = string.Format("Time: <color=red>{0}s</color>", seconds);
	}

	public void UpdateInventory()
	{
		//Debug.Log("UpdatePanel");
		inventoryText.text = string.Format("Inventory: <color=green>{0}</color>", BuildingManager.buildingManager.selectedInventory.inventory[thisItem]);
		//timeText.text = "Time: 0s";

		//EnableButtons();
	}

	void UpdateSlider()
	{
		timeSlider.value = (float)thisItem.time - BuildingManager.buildingManager.selectedInventory.timeRemaining[thisItem];
	}

	void InitPanel()
	{
		cancelButton.gameObject.SetActive(false);
		costText.text = "Cost: $" + thisItem.cost.ToString() + " ea.";
		sellsForText.text = "Sell: $" + thisItem.sellsFor.ToString() + " ea.";
		itemNameText.text = thisItem.itemName;
		itemImage.sprite = thisItem.itemIcon;
		inventoryText.text = string.Format("Inventory: <color=green>{0}</color>", BuildingManager.buildingManager.selectedInventory.inventory[thisItem]);

		timeSlider.maxValue = thisItem.time;
		timeSlider.minValue = 0;
		timeSlider.value = 0;
		//qtyNumberText.text = "0";
		//totalCostText.text = "Total Cost: $0";


		/*if (BuildingManager.buildingManager.selectedInventory.timeRemaining[thisItem] > 0)
		{
			SetTimeRemaining();
			StartCoroutine(Countdown());
		}
		else*/
		timeText.text = "Build Time: " + thisItem.time;
	}

	public void SendBuild()
	{
		//if (numToBuild == 0)
		//	return;
		if (BusinessManager.businessManager.totalMoney < thisItem.cost) return;
		BuildingManager.buildingManager.selectedInventory.StartProduction(thisItem, this);
		StartCoroutine(Countdown());
		SetButtons();
		//_numToBuild = 0;
		//qtyNumberText.text = "0";

		//StartCoroutine(Countdown());
	}

	public void StopBuild()
	{
		BuildingManager.buildingManager.selectedInventory.StopProduction(thisItem);
		SetButtons();
	}

	void SetButtons()
	{
		if(buildButton.gameObject.activeSelf)
		{
			buildButton.gameObject.SetActive(false);
			cancelButton.gameObject.SetActive(true);
		}
		else
		{
			buildButton.gameObject.SetActive(true);
			cancelButton.gameObject.SetActive(false);
		}
	}
}
