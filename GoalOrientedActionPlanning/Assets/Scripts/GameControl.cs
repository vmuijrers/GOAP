using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    private FoodSpawner foodSpawner;
    private TreeSpawner treeSpawner;
    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        foodSpawner = GetComponent<FoodSpawner>();
        treeSpawner = GetComponent<TreeSpawner>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
