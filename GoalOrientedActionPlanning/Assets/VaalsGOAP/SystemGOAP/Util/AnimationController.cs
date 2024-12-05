using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VaalsGOAP
{
    public class AnimationController : MonoBehaviour
    {

        internal Animator anim;
        internal RuntimeAnimatorController normalController;
        public AnimatorOverrideController overrideController;
        // Use this for initialization
        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            normalController = anim.runtimeAnimatorController;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnStateChanged(State state)
        {
            if (state.CheckIfEffectIsPresent(Effect.HasWeapon))
            {
                anim.runtimeAnimatorController = overrideController;
            }
            else
            {
                anim.runtimeAnimatorController = normalController;
            }
        }

        public void FadeToState(string name, float fadeTime)
        {
            anim.CrossFadeInFixedTime(name, fadeTime);
        }
    }
}

