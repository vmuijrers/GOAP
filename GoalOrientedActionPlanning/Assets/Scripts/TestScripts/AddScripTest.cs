using UnityEngine;
using System.Collections;

public class AddScripTest : MonoBehaviour {
    public string scriptName;
	// Use this for initialization
	void Start () {

        
        gameObject.AddComponent(System.Type.GetType(scriptName));

    }

    // Update is called once per frame
    void Update () {
	
	}
}
