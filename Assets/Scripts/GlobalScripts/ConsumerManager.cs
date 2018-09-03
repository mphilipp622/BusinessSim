using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerManager : MonoBehaviour
{
	public static ConsumerManager consumerManager;

	[SerializeField]
	float _qualityWeight = 0.5f; // quality weighs 50%
	[SerializeField]
	float _priceWeight = 0.35f; // price weighs 35%
	[SerializeField]
	float _awarenessWeight = 0.15f; // awareness weighs 15%

	[SerializeField]
	float _AIUpdateTime = 5f, _consumerDemandIncreaseRate = 5f; 

	[SerializeField]
	float _demandThreshold = 50f;

	[SerializeField]
	float _highestQuality = 100f;

	[SerializeField]
	float _highestAwareness = 100f;

	[SerializeField]
	string[] namePool;

	[SerializeField]
	List<Consumer> _consumers;

	public float qualityWeight
	{
		get
		{
			return _qualityWeight;
		}
	}
	public float priceWeight
	{
		get
		{
			return _priceWeight;
		}
	}

	public float awarenessWeight
	{
		get
		{
			return _awarenessWeight;
		}
	}

	public float AIUpdateTime
	{
		get
		{
			return _AIUpdateTime;
		}
	}

	public float consumerDemandIncreaseRate
	{
		// demand increase rate will alter the amount that consumer demand increases on AI updates. This can affect whether the market is slowing or accelerating.
		get
		{
			return _consumerDemandIncreaseRate;
		}
	}

	public float highestQuality
	{
		get
		{
			return _highestQuality;
		}
	}

	public float highestAwareness
	{
		get
		{
			return _highestAwareness;
		}
	}

	public List<Consumer> consumers
	{
		get
		{
			return _consumers;
		}
	}

	private void Awake()
	{
		InitSingleton();
		
	}

	void Start ()
	{
		InitConsumers();
		StartAI();

	}

	void Update ()
	{
		
	}

	void InitConsumers()
	{
		_consumers = new List<Consumer>(3);

		for (int i = 0; i < consumers.Capacity; i++)
		{
			consumers.Add(new Consumer());
			//StartCoroutine(consumers[i].StartAI());
		}
	}

	void InitSingleton()
	{
		if (consumerManager == null)
			consumerManager = this;

		else if (consumerManager != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void StartAI()
	{
		for(int i = 0; i < _consumers.Count; i++)
			StartCoroutine(_consumers[i].StartAI());
	}
}
