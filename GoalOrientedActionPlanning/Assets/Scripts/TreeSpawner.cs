using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TreeSpawner : MonoBehaviour {

    public List<GameObject> TreeList;
    public GameObject TreePrefab;
    public int numTrees = 20;
    public float areaRange = 10;

    void Awake()
    {
        TreeList = new List<GameObject>();
        for (int i = 0; i < numTrees; i++)
        {
            GameObject f = (GameObject)Instantiate(TreePrefab, new Vector3(Random.Range(-areaRange, areaRange), 0.5f, Random.Range(-areaRange, areaRange)), Quaternion.identity);
            TreeList.Add(f);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
	
	}
}
