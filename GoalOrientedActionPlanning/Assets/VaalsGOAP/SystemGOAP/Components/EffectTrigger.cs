using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VaalsGOAP
{
    public class EffectTrigger : MonoBehaviour
    {
        public Effect effect;
        // Use this for initialization
        void Start()
        {

        }

        public void OnTriggerExit(Collider col)
        {
            GOAPAgent agent = col.gameObject.GetComponent<GOAPAgent>();
            if (agent != null)
            {
                agent.state.RemoveEffect(effect);
            }
        }
        public void OnTriggerEnter(Collider col)
        {
            GOAPAgent agent = col.gameObject.GetComponent<GOAPAgent>();
            if (agent != null)
            {
                agent.state.AddEffect(effect);
            }
        }
    }
}

