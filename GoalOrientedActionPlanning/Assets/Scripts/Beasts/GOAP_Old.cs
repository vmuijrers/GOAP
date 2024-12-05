using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GOAP_Old : MonoBehaviour {

    private HashSet<KeyValuePair<string, object>> preConditions;
    private HashSet<KeyValuePair<string, object>> effects;

    // Use this for initialization
    void Start () {
        preConditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();

	}
	

    public void addPrecondition(string key, object value)
    {
        preConditions.Add(new KeyValuePair<string, object>(key, value));
    }


    public void removePrecondition(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in preConditions)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            preConditions.Remove(remove);
    }


    public void addEffect(string key, object value)
    {
        effects.Add(new KeyValuePair<string, object>(key, value));
    }


    public void removeEffect(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in effects)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            effects.Remove(remove);
    }


    public HashSet<KeyValuePair<string, object>> Preconditions
    {
        get
        {
            return preConditions;
        }
    }

    public HashSet<KeyValuePair<string, object>> Effects
    {
        get
        {
            return effects;
        }
    }
}
