using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour {

    public List<GameObject> FoodList;
    public GameObject FoodPrefab;
    public int numFood = 20;
    public float areaRange = 10;
	// Use this for initialization
	void Awake () {
        FoodList = new List<GameObject>();
        for(int i =0; i < numFood; i++)
        {
            GameObject f = (GameObject)Instantiate(FoodPrefab, new Vector3(Random.Range(-areaRange, areaRange), 0.5f, Random.Range(-areaRange, areaRange)),Quaternion.identity);
            FoodList.Add(f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
