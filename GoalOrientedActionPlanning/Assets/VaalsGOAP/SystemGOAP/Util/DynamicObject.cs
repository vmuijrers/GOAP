using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicObject : MonoBehaviour {

    public virtual void Start()
    {
        RegisterObject(this);
    }

    public virtual void OnDisable()
    {
        UnRegisterObject(this);
    }

    public virtual void RegisterObject(MonoBehaviour obj) {
        RegisterObjectMessage msg = new RegisterObjectMessage(obj);
        Message.Send<RegisterObjectMessage>(msg);
    }
    public virtual void UnRegisterObject(MonoBehaviour obj)
    {
        UnRegisterObjectMessage msg = new UnRegisterObjectMessage(obj);
        Message.Send<UnRegisterObjectMessage>(msg);
    }

}

public class RegisterObjectMessage : Message
{
    public MonoBehaviour component;
    public RegisterObjectMessage(MonoBehaviour component)
    {
        this.component = component;
    }
    
}

public class UnRegisterObjectMessage : Message
{
    public MonoBehaviour component;
    public UnRegisterObjectMessage(MonoBehaviour component)
    {
        this.component = component;
    }

}