using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessManager : MonoBehaviour
{
	public static BusinessManager businessManager;

	[SerializeField]
	int _totalMoney = 20;

	[SerializeField]
	Text treasuryText;

	public int totalMoney
	{
		get
		{
			return _totalMoney;
		}
		set
		{
			_totalMoney = value;
		}
	}

	private void Awake()
	{
		InitSingleton();
	}

	void Start ()
	{
		treasuryText.text = "Money: $" + totalMoney;
	}

	void Update ()
	{

	}

	void InitSingleton()
	{
		if (businessManager == null)
			businessManager = this;

		else if (businessManager != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public IEnumerator DecreaseNumbers()
	{
		StopCoroutine(IncreaseNumbers());
		treasuryText.text = "Money: <color=red>$" + totalMoney + "</color>";

		yield return new WaitForSeconds(3f);

		treasuryText.text = "Money: $"+ totalMoney;
	}

	public IEnumerator IncreaseNumbers()
	{
		StopCoroutine(IncreaseNumbers());
		treasuryText.text = "Money: <color=green>$" + totalMoney + "</color>";

		yield return new WaitForSeconds(3f);

		treasuryText.text = "Money: $" + totalMoney;
	}
}
