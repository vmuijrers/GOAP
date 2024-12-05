using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{

    void TakeHit(RaycastHit hit, Vector3 force);
}
