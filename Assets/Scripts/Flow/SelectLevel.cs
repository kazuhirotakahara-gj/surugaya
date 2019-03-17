using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static public class CurrentLevel
{
	static public bool GameStarted = false;
	static public bool GamePaused = false;

    static public GameObject[] CondidateItems = null;
    static public GameObject[] AllItems = null;
	static public float PurchaseOrderTimeLimitMin = 4;
	static public float PurchaseOrderTimeLimitByItem = 2;

	static public int OrderMax = 3;
	static public float ConveyorMoveSpeed = 1;

	static public float AnotherSpawnProbability = 33f;
	static public float NextSpawnProbability = 33f;
}

public class SelectLevel : MonoBehaviour
{
    public GameObject[] EasyCondidateItems = new GameObject[] { };
	public float EasyPurchaseOrderTimeLimitMin = 4;
	public float EasyPurchaseOrderTimeLimitByItem = 2;
	[Range(1,3)] public int EasyOrderMax = 2;
	[Range(0,100)] public float EasyAnotherSpawnProbability = 33f;
	[Range(0,100)] public float EasyNextSpawnProbability = 33f;


    public GameObject[] NormalCondidateItems = new GameObject[] { };
	public float NormalPurchaseOrderTimeLimitMin = 4;
	public float NormalPurchaseOrderTimeLimitByItem = 2;
	[Range(1,3)] public int NormalOrderMax = 3;
	[Range(0,100)] public float NormalAnotherSpawnProbability = 33f;
	[Range(0,100)] public float NormalNextSpawnProbability = 33f;

    public GameObject[] HardCondidateItems = new GameObject[] { };
	public float HardPurchaseOrderTimeLimitMin = 4;
	public float HardPurchaseOrderTimeLimitByItem = 2;
	[Range(1,3)] public int HardOrderMax = 3;
	[Range(0,100)] public float HardAnotherSpawnProbability = 33f;
	[Range(0,100)] public float HardNextSpawnProbability = 33f;

    public GameObject[] AllItems = new GameObject[] { };

    public AudioSource DecisionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEasy()
    {
		CurrentLevel.GameStarted = false;
		CurrentLevel.GamePaused = false;
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
        CurrentLevel.CondidateItems = EasyCondidateItems;
		CurrentLevel.PurchaseOrderTimeLimitMin = EasyPurchaseOrderTimeLimitMin;
		CurrentLevel.PurchaseOrderTimeLimitByItem = EasyPurchaseOrderTimeLimitByItem;
		CurrentLevel.AnotherSpawnProbability = EasyAnotherSpawnProbability;
		CurrentLevel.NextSpawnProbability = EasyNextSpawnProbability;
		CurrentLevel.OrderMax = EasyOrderMax;
        CurrentLevel.AllItems = AllItems;
    }

    public void OnClickNormal()
    {
		CurrentLevel.GameStarted = false;
		CurrentLevel.GamePaused = false;
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
        CurrentLevel.CondidateItems = NormalCondidateItems;
		CurrentLevel.PurchaseOrderTimeLimitMin = NormalPurchaseOrderTimeLimitMin;
		CurrentLevel.PurchaseOrderTimeLimitByItem = NormalPurchaseOrderTimeLimitByItem;
		CurrentLevel.AnotherSpawnProbability = NormalAnotherSpawnProbability;
		CurrentLevel.NextSpawnProbability = NormalNextSpawnProbability;
		CurrentLevel.OrderMax = NormalOrderMax;
        CurrentLevel.AllItems = AllItems;
    }

    public void OnClickHard()
    {
		CurrentLevel.GameStarted = false;
		CurrentLevel.GamePaused = false;
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
        CurrentLevel.CondidateItems = HardCondidateItems;
		CurrentLevel.PurchaseOrderTimeLimitMin = HardPurchaseOrderTimeLimitMin;
		CurrentLevel.PurchaseOrderTimeLimitByItem = HardPurchaseOrderTimeLimitByItem;
		CurrentLevel.AnotherSpawnProbability = HardAnotherSpawnProbability;
		CurrentLevel.NextSpawnProbability = HardNextSpawnProbability;
		CurrentLevel.OrderMax = HardOrderMax;
        CurrentLevel.AllItems = AllItems;
    }
}
