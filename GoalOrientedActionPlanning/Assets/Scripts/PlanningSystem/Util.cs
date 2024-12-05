using UnityEngine;
using System.Collections;

public static class Global{
    
    public static float DistanceToObject(GameObject me, GameObject other)
    {

        return Vector3.Distance(me.transform.position, other.transform.position);
    }

    public static float Remap(float val, float oldMin,float oldMax,float newMin, float newMax)
    {

        return (val - oldMin) / (Mathf.Abs(oldMax - oldMin)) * (Mathf.Abs(newMax - newMin)) + newMin;
    }

}
