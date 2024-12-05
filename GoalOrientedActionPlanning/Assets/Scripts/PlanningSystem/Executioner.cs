using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ParallelFinishType { AllDone, FirstDone, Continue, AnyDone };

public class Executioner {


    public static IEnumerator Sequence(params IEnumerator[] funcs)
    {
        foreach(IEnumerator co in funcs)
        {
            while (co.MoveNext())
            {
                yield return co.Current;
            }
        }

        
    }

    public static IEnumerator Parallel(BaseBeast caller, ParallelFinishType finishType, params IEnumerator[] funcs)
    {

        List<bool> isDone = new List<bool>();
        int batchNumber = ++caller.batchNumber;
        for (int i =0;i < funcs.Length; i++)
        {
            isDone.Add(false);
            caller.StartCoroutine(ParallelHelper(caller,funcs[i], batchNumber, i));
        }
        caller.parallelListener.Add(batchNumber, isDone);

        bool returnType = false;
        switch (finishType)
        {
            case ParallelFinishType.AnyDone:

                while (!returnType)
                {

                    for (int i = 0; i < funcs.Length; i++)
                    {
                        returnType |= caller.parallelListener[batchNumber][i];

                    }
                    yield return null;
                }

                break;
            case ParallelFinishType.FirstDone:
                while (!returnType)
                {
                    returnType = caller.parallelListener[batchNumber][0];
                    yield return null;

                }
                break;
            case ParallelFinishType.AllDone:
                while (!returnType)
                {
                    returnType = true;
                    for (int i = 0; i < funcs.Length; i++)
                    {
                        returnType &= caller.parallelListener[batchNumber][i];

                    }
                    yield return null;
                }
                break;
            case ParallelFinishType.Continue:

                yield return null;
                break;
        }
        caller.parallelListener.Remove(batchNumber);

        
    }

    public static IEnumerator ParallelHelper(BaseBeast caller, IEnumerator func, int batchNumber, int indexNumber)
    {


        yield return caller.StartCoroutine(func);


        if (caller.parallelListener.ContainsKey(batchNumber))
        {
            caller.parallelListener[batchNumber][indexNumber] = true;
        }



    }


    #region Util
    public static MonoBehaviour GetClosestGameObjectOfType<T>(GameObject caller, bool notMe)
    {

        //GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
        MonoBehaviour[] objs = UnityEngine.GameObject.FindObjectsOfType(typeof(T)) as MonoBehaviour[];
        float minDist = 100000;
        MonoBehaviour returnObject = null;

        foreach (MonoBehaviour obj in objs)
        {
            if (notMe)
            {

                if (obj.gameObject.Equals(caller))
                {
                    continue;
                }
            }
            if (obj.enabled)
            {
                float dist = Vector3.Distance(caller.transform.position, obj.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    returnObject = obj;
                }
            }
        }


        return returnObject;
    }
    #endregion

}

