using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class Message
{
    /// <summary>
    /// Simple Messaging system, messages can be send by string or MessageType;Use AddListeners to respond to Event;Use RemoveListener to Remove listening to event
    /// </summary>


    private static Dictionary<string, UnityEvent> allMessages = new Dictionary<string, UnityEvent>();

    //Invokes the message
    public static void Send(string messageName)
    {
        if (!allMessages.ContainsKey(messageName))
        {
            allMessages.Add(messageName, new UnityEvent());
        }
        if (allMessages[messageName] != null)
        {
            allMessages[messageName].Invoke();
        }
    }


    public static void Send<T>()
    {
        string messageName = typeof(T).ToString();
        Send(messageName);
    }
    public static void AddListener(string messageName, UnityAction func)
    {
        if (!allMessages.ContainsKey(messageName))
        {
            allMessages.Add(messageName, new UnityEvent());
        }
        allMessages[messageName].AddListener(func);
    }

    public static void RemoveListener(string messageName, UnityAction func)
    {
        if (allMessages.ContainsKey(messageName))
        {
            if (allMessages[messageName] != null)
            {
                allMessages[messageName].RemoveListener(func);
            }
        }
    }
    public static void AddListener<T>(UnityAction func)
    {
        string messageName = typeof(T).ToString();
        if (!allMessages.ContainsKey(messageName))
        {
            allMessages.Add(messageName, new UnityEvent());
        }
        allMessages[messageName].AddListener(func);
    }

    public static void RemoveListener<T>(UnityAction func)
    {

        string messageName = typeof(T).ToString();
        if (allMessages.ContainsKey(messageName))
        {
            if (allMessages[messageName] != null)
            {
                allMessages[messageName].RemoveListener(func);
            }
        }

    }



    //Messages for 1 parameter messages
    private static Dictionary<string, UnityEvent<Message>> messageDictionary = new Dictionary<string, UnityEvent<Message>>();



    public static void Send<T>(string messageName, T arg) where T : Message
    {
        if (!messageDictionary.ContainsKey(messageName))
        {
            messageDictionary.Add(messageName, new InternalMessageEvent());
        }
        if (messageDictionary[messageName] != null)
        {
            messageDictionary[messageName].Invoke(arg);
        }
    }

    public static void AddListener<T>(string messageName, UnityAction<T> func) where T : Message
    {
        if (!messageDictionary.ContainsKey(messageName))
        {
            messageDictionary.Add(messageName, new InternalMessageEvent());
        }
        messageDictionary[messageName].AddListener((t) => { func((T)t); });
    }

    public static void RemoveListener<T>(string messageName, UnityAction<T> func) where T : Message
    {
        if (messageDictionary.ContainsKey(messageName))
        {
            if (messageDictionary[messageName] != null)
            {
                messageDictionary[messageName].RemoveListener((t) => { func((T)t); });
            }
        }
    }


    public static void Send<T>(Message messageObject) where T : Message
    {
        string messageName = typeof(T).ToString();
        if (messageDictionary.ContainsKey(messageName))
        {
            messageDictionary[messageName].Invoke(messageObject);
        }
    }

    public static void AddListener<T>(UnityAction<T> func) where T : Message
    {
        string messageName = typeof(T).ToString();
        if (!messageDictionary.ContainsKey(messageName))
        {
            messageDictionary.Add(messageName, new InternalMessageEvent());
        }
        messageDictionary[messageName].AddListener((t) => { func((T)t); });
    }

    public static void RemoveListener<T>(UnityAction<Message> func) where T : Message
    {
        string messageName = typeof(T).ToString();
        if (messageDictionary.ContainsKey(messageName))
        {
            messageDictionary[messageName].RemoveListener(func);
        }

    }
    [System.Serializable]
    private class InternalMessageEvent : UnityEvent<Message> { }
}

