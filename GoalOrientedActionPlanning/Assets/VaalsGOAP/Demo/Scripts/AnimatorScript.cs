using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour {

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        anim.CrossFade("Idle", 0.1f);
	}

    void OnEnable()
    {
        GetComponent<Kicking>().OnKickAction += KickAnimation;
    }
    void OnDisable()
    {
        GetComponent<Kicking>().OnKickAction -= KickAnimation;
    }
    
	
	// Update is called once per frame
	void Update () {

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                anim.CrossFade("Run", 0f);
            }
            else
            {
                anim.CrossFade("Idle", 0f);
            }
        }
        
	}

    public void KickAnimation()
    {
        StartCoroutine(EnqueueAnimation("Kicking", "Idle", 2.133f));
    }

    public IEnumerator EnqueueAnimation(string animName, string goBackToAnimNameAfterwards, float delay)
    {
        anim.CrossFade(animName, 0f);
        yield return new WaitForSeconds(delay);
        Debug.Log("Hhelo");
        anim.CrossFade(goBackToAnimNameAfterwards, 0.1f);
    }
}
