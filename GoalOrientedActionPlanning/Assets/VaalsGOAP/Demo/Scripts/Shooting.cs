using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject cam;
    public float force = 1000;
    public LayerMask hitLayer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000, hitLayer, QueryTriggerInteraction.Ignore))
            {
                IHittable iHit = hit.collider.gameObject.GetComponentInParent<IHittable>();
                if (iHit != null)
                {
                    iHit.TakeHit(hit, cam.transform.forward * force);
                }
            }
        }
    }
}
