using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VaalsGOAP;
public class HitHandler : MonoBehaviour, IHittable

{

    Rigidbody[] allRigids;
    // Use this for initialization
    void Start()
    {
        allRigids = GetComponentInParent<GOAPAgent>().gameObject.GetComponentsInChildren<Rigidbody>();
        SetRigid(true, true);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInChildren<Animator>().enabled = false;
            SetRigid(false, true);
        }
    }

    void SetRigid(bool isKinematic, bool useGravity)
    {

        foreach (Rigidbody rb in allRigids)
        {
            rb.isKinematic = isKinematic;
            rb.useGravity = useGravity;
        }
    }

    public void TakeHit(RaycastHit hit, Vector3 force)
    {
        GetComponentInParent<Animator>().enabled = false;
        GetComponentInParent<NavMeshAgent>().enabled = false;
        GetComponentInParent<GOAPAgent>().state.AddEffect(Effect.IsDead);
        SetRigid(false, true);
        
        hit.collider.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, hit.point, ForceMode.Impulse);
    }
}
