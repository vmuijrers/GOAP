using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class ObjectFinder : MonoBehaviour {

    public static Dictionary<System.Type, List<MonoBehaviour>> allDynamicObjects;

    void OnEnable()
    {
        allDynamicObjects = new Dictionary<System.Type, List<MonoBehaviour>>();
        Message.AddListener<RegisterObjectMessage>(RegisterObject);
        Message.AddListener<UnRegisterObjectMessage>(UnRegisterObject);
    }

    private void RegisterObject(RegisterObjectMessage msg)
    {
        
        System.Type type = msg.component.GetType();
        if (!allDynamicObjects.ContainsKey(type))
        {
            allDynamicObjects.Add(type, new List<MonoBehaviour>());
        }
        Debug.Log("Object Registered: " + msg.component);
        allDynamicObjects[type].Add(msg.component);
    }

    private void UnRegisterObject(UnRegisterObjectMessage msg)
    {
        Debug.Log("Object UnRegistered!: " + msg.component);
        System.Type type = msg.component.GetType();
        if (allDynamicObjects.ContainsKey(type))
        {
            allDynamicObjects[type].Remove(msg.component);
        }
        
    }

    public static T FindNearestObjectByType<T>(GameObject sender, float range = float.MaxValue, params OptionalParameter[] optionalParameters) where T : MonoBehaviour
    {
        T returnObj = null;
        if (allDynamicObjects.ContainsKey(typeof(T)))
        {
            float minDist = float.MaxValue;

            Debug.Log("Num Objects: " + allDynamicObjects[typeof(T)].Count);
            foreach(T obj in allDynamicObjects[typeof(T)])
            {
                float sqrDist = Vector3.SqrMagnitude(obj.transform.position - sender.transform.position);

                if (sqrDist <= minDist && sqrDist <= range * range)
                {
                    bool objIsValid = true;
                    foreach(OptionalParameter op in optionalParameters)
                    {
                        FieldInfo info = obj.GetType().GetField(op.nameField);
                        objIsValid &= info.GetValue(obj).Equals(op.value);
                    }
                    if (objIsValid)
                    {
                        minDist = sqrDist;
                        returnObj = obj;
                    }
                }
            }

        }
        return returnObj;
    }

}
public class OptionalParameter
{
    public OptionalParameter(string nameField, object value)
    {
        this.nameField = nameField;
        this.value = value;
    }
    public string nameField;
    public object value;
}