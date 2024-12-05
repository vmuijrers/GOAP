using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class BeastStats  {

    public float moveSpeed;
    public float rotationSpeed;
    public float health;
    public float damage;
    public bool dead;

    [BitMask(typeof(Effects))]
    public Effects
        currentEffects;
    //public Dictionary<Effects,bool> currentEffects;

    [BitMask(typeof(Effects))]
    public Effects
        startEffects;
    //public EffectState[] startEffects;
    public void ResetStats()
    {
        health = 1f;
        dead = false;

        AddEffect(startEffects);
        //currentEffects |= startEffects;
        //foreach(EffectState s in startEffects)
        //{
        //    SetEffectValue(s.effect, s.isTrue);
        //}
        //PrintEffects();
    }



    public void PrintEffects()
    {


        Debug.Log(currentEffects.ToString());
        //foreach (KeyValuePair<Effects, bool> i in currentEffects)
        //{
        //    Debug.Log(i.Key + " " + i.Value);

        //}
    }

    public void RunEffects()
    {
        //if (/*currentEffects.TryGetValue(Effects.GetsHungry, out val)*/ IsEffectActive(Effects.GetsHungry))
        //{

        //        health -= 0.01f * Time.deltaTime;
        //        health = Mathf.Clamp01(health);
            
        //}
    }

    public bool IsEffectActive(Effects s)
    {
        return (currentEffects & s) != 0;
    }

    /// <summary>
    /// returns true if the bits are equal, this way we can explicitly check if an effect is not active 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool GetConditionEffect(EffectState s)
    {
        return s.isActive ? IsEffectActive(s.effect) : (~currentEffects & s.effect) != 0;
    }
    public void FlipFlag(Effects s)
    {
        currentEffects ^= s;
    }

    public void AddEffect(Effects s)
    {
        currentEffects |= s;
    }
    public void RemoveEffect(Effects s)
    {

        currentEffects &= ~s;
    }


    //public void SetEffectValue(Effects effect, bool value)
    //{
    //    if (currentEffects.ContainsKey(effect))
    //    {
    //        currentEffects[effect] = value;
    //    }
    //    else
    //    {
    //        currentEffects.Add(effect, value);
    //    }
    //}

    //public bool GetEffectValue(Effects effect,out bool result)
    //{
    //    result = false;
    //    if (currentEffects.ContainsKey(effect))
    //    {
    //        result = currentEffects[effect];
    //        return true;
    //    }

    //    return false;
    //}
}

