using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicking : MonoBehaviour {

    public float kickForce = 1000f;
    public System.Action OnKickAction;
    public LayerMask layerMask;
    // Use this for initialization
    void Start () {
        OnKickAction += Kick;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(OnKickAction != null)
            {
                OnKickAction.Invoke();
            }
            
        }
	}

    public void Kick()
    {
        Vector3 kickPos = transform.position + transform.forward + transform.up * 0.5f;

        Collider[] cols = Physics.OverlapSphere(kickPos, 0.5f, layerMask);
        Collider hit = cols[0];
        if (cols.Length == 0) return;
        if(hit != null)
        {
            Debug.Log("Kicked Something!:" + hit.gameObject.name);
            IRagDoll rag = hit.gameObject.GetComponent<IRagDoll>();
            if (rag != null)
            {
                GameObject ragDoll = rag.SpawnRagDoll();
                Rigidbody[] rbs = ragDoll.GetComponentsInChildren<Rigidbody>(); 

                float maxDist = 100000;
                Rigidbody closestRb =null;
                foreach (Rigidbody r in rbs)
                {
                    float dist = Vector3.Distance(r.transform.position, kickPos);
                    if (dist < maxDist)
                    {
                        closestRb = r;
                        maxDist = dist;
                    }

                }
                StartCoroutine(WaitThenAddForce(closestRb, hit));
            }
            else
            {
                Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
                if (rb)
                {
                    StartCoroutine(WaitThenAddForce(rb, hit));
                }
            }


        }

    }

    IEnumerator WaitThenAddForce(Rigidbody rb, Collider col)
    {
        yield return new WaitForSeconds(0.75f);
        if(rb != null)
        {
            rb.AddForceAtPosition((col.transform.position - transform.position).normalized+ transform.up+transform.forward * kickForce, Physics.ClosestPoint(transform.position, col, col.transform.position, col.transform.rotation));
        }
       
    }
}
