using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
	public class Util : MonoBehaviour {

        public static IEnumerator Sequence(params IEnumerator[] input)
        {
            foreach(IEnumerator coroutine in input)
            {
                while (coroutine.MoveNext())
                {
                    yield return coroutine.Current;
                }
            }
        }
	}
}
