using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class Actions {

    #region Actions

    public static IEnumerator MoveAndLookToPositionRigidBody(BaseBeast go, Vector3 targetPosition, float moveSpeed, float rotationSpeed, float dist)
    {

        while (Vector3.Distance(go.transform.position, targetPosition) > dist )
        {

            go.rb.MovePosition(go.transform.position + go.transform.forward * moveSpeed * Time.deltaTime);
            go.transform.rotation = Quaternion.RotateTowards(go.transform.rotation, Quaternion.LookRotation(targetPosition - go.transform.position), rotationSpeed);
            yield return null;
        }
    }
    public static IEnumerator MoveAndLookToObjectRigidBody(BaseBeast go, GameObject target, float moveSpeed, float rotationSpeed, float dist)
    {
        while (Vector3.Distance(go.transform.position, target.transform.position) > dist)
        {

            
            go.rb.MovePosition(go.transform.position + go.transform.forward * moveSpeed * Time.deltaTime);
            go.transform.rotation = Quaternion.RotateTowards(go.transform.rotation, Quaternion.LookRotation(target.transform.position - go.transform.position), rotationSpeed);
            yield return null;
        }
    }
    public static IEnumerator MoveAndLookToPosition(BaseBeast go, Vector3 targetPosition, float moveSpeed, float rotationSpeed, float dist)
    {

        while (Vector3.Distance(go.transform.position, targetPosition) > dist && Quaternion.Angle(go.transform.rotation, Quaternion.LookRotation(targetPosition - go.transform.position)) > rotationSpeed * Time.deltaTime)
        {

            go.transform.position += (targetPosition - go.transform.position).normalized * moveSpeed * Time.deltaTime;
            go.transform.rotation = Quaternion.RotateTowards(go.transform.rotation, Quaternion.LookRotation(targetPosition - go.transform.position), rotationSpeed);
            yield return null;
        }


    }
    public static IEnumerator MoveAndLookToObject(BaseBeast go, GameObject target, float moveSpeed,float rotationSpeed, float dist)
    {
        while (Vector3.Distance(go.transform.position, target.transform.position) > dist && Quaternion.Angle(go.transform.rotation, Quaternion.LookRotation(target.transform.position - go.transform.position)) > rotationSpeed * Time.deltaTime)
        {

            go.transform.position += (target.transform.position - go.transform.position).normalized * moveSpeed * Time.deltaTime;
            go.transform.rotation = Quaternion.RotateTowards(go.transform.rotation, Quaternion.LookRotation(target.transform.position - go.transform.position), rotationSpeed);
            yield return null;
        }
    }
    public static IEnumerator MoveToPosition(BaseBeast go,Vector3 targetPosition,float moveSpeed, float dist)
    {

        while(Vector3.Distance(go.transform.position, targetPosition) > dist)
        {
            
            go.transform.position += (targetPosition - go.transform.position).normalized * moveSpeed * Time.deltaTime;

            yield return null;
        }
        

    }

    public static IEnumerator MoveToObject(BaseBeast go, GameObject target, float moveSpeed, float dist)
    {
        while (Vector3.Distance(go.transform.position, target.transform.position) > dist)
        {

            go.transform.position += (target.transform.position - go.transform.position).normalized * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }

    public static IEnumerator LookAtPosition(BaseBeast go, Vector3 position, float rotationSpeed)
    {
        while (Quaternion.Angle(go.transform.rotation, Quaternion.LookRotation(position - go.transform.position)) > rotationSpeed * Time.deltaTime)
        {
            go.transform.rotation = Quaternion.RotateTowards(go.transform.rotation, Quaternion.LookRotation(position - go.transform.position), rotationSpeed);
            yield return null;
        }
        
    }
    public static IEnumerator LookAtObject(BaseBeast go, GameObject obj, float rotationSpeed)
    {
        while (Quaternion.Angle(go.transform.rotation, Quaternion.LookRotation(obj.transform.position - go.transform.position)) > rotationSpeed * Time.deltaTime)
        {
            go.transform.rotation =  Quaternion.RotateTowards(go.transform.rotation, Quaternion.LookRotation(obj.transform.position - go.transform.position), rotationSpeed);
            yield return null;
        }

    }
    public static IEnumerator FinishedTask(BaseBeast go, Task finishedTask)
    {
        yield return null;
        go.currentTask = null;
        go.CheckForNewTask();
        Debug.Log("Finished Task: " + finishedTask);
    }
    public static IEnumerator Wait(float secs)
    {

        yield return new WaitForSeconds(secs);
        Debug.Log("Done Waiting!");
    }
    public static IEnumerator GoUpTree(BaseBeast go, TreeComponent closestTree)
    {

        yield return go.StartCoroutine(Actions.MoveToPosition(go, closestTree.transform.position + new Vector3(0, 5, 0), go.stats.moveSpeed, 0.1f));
        yield return go.StartCoroutine(SetEffectValue(go, Effects.InTree, true));
    }

    public static IEnumerator DescendFromTree(BaseBeast go, TreeComponent closestTree)
    {


        yield return go.StartCoroutine(MoveToPosition(go, closestTree.transform.position + (Vector3)Random.insideUnitCircle.normalized,go.stats.moveSpeed, 0.1f));
        yield return go.StartCoroutine(SetEffectValue(go, Effects.InTree, false));
    }

    public static IEnumerator EatFood(BaseBeast go, FoodComponent closestFood)
    {
        if (closestFood.gameObject.activeSelf)
        {
            //closestFood.gameObject.SetActive(false);
            closestFood.nutrition -= 1;
            go.stats.health += closestFood.nutrition;
        }

        yield return null;
    }

    public static IEnumerator SetEffectValue(BaseBeast go, Effects effect, bool value)
    {
        if (value)
        {
            go.stats.AddEffect(effect);
        }
        else
        {
            go.stats.RemoveEffect(effect);
        }

        //go.stats.SetEffectValue(effect, value);
        yield return null;
        go.stats.PrintEffects();

    }


    public static IEnumerator JumpFromTreeToTree(BaseBeast go, TreeComponent currentTree, TreeComponent jumpToTree)
    {

       // RaycastHit hit;
      //  if (Physics.Raycast(go.transform.position, (jumpToTree.transform.position + new Vector3(0,3,0) - go.transform.position).normalized, out hit, 10, LayerMask.NameToLayer("Tree")))
        {
            Vector3 targetPos = jumpToTree.transform.position+ new Vector3(0,5,0);
            Vector3 startPos = go.transform.position;
            float dist = (targetPos - go.transform.position).magnitude;
            float totalJumpTime = dist / go.stats.moveSpeed;
            float jumpHeight = dist*2;

            float t = 0;
            Vector3 inBetweenPos = go.transform.position + (targetPos - go.transform.position) / 2 + new Vector3(0, jumpHeight, 0);
            while(t<= 1){


                t += Time.deltaTime * 1 / totalJumpTime *2;
                go.transform.position = Vector3.Slerp(startPos, inBetweenPos, t);



                yield return null;
            }
            t=0;
            while(t<= 1){


                t += Time.deltaTime * 1 / totalJumpTime *2;
                go.transform.position = Vector3.Slerp(inBetweenPos, targetPos, t);



                yield return null;
            }

        }
        yield return null;

    }

    public static IEnumerator AttackBeast(BaseBeast caller, BaseBeast other)
    {

        other.GetComponent<IDamageable>().TakeDamage(caller,caller.stats.damage);
        yield return null;

    }

    public static IEnumerator ChargeAttack(BaseBeast caller, float chargeSpeed, float chargeDistance)
    {
        caller.stats.AddEffect(Effects.ChargeAbilityActive);
        float t = 0;
        while(t < chargeDistance)
        {
            caller.rb.MovePosition(caller.transform.position + caller.transform.forward * chargeSpeed * Time.deltaTime);
            t += Time.deltaTime * chargeSpeed;
            yield return null;
        }
        caller.stats.RemoveEffect(Effects.ChargeAbilityActive);
    }

    public static IEnumerator StartCooldownEffect(BaseBeast caller,Task task, Effects effect,float cooldownTime)
    {

        task.StartCoroutine(CooldownEffect(caller, effect, cooldownTime));
        yield return null;
    }

    private static IEnumerator CooldownEffect(BaseBeast caller, Effects effect, float cooldownTime)
    {
        caller.stats.AddEffect(effect);
        yield return new WaitForSeconds(cooldownTime);
        caller.stats.RemoveEffect(effect);

    }

    //public IEnumerator AddEffect(BaseBeast go, Effects effect, bool value)
    //{
    //    if(go.stats.currentEffects.add
    //        public void addEffect(string key, object value)
    //{
    //    effects.Add(new KeyValuePair<string, object>(key, value));
    //}


    //public void removeEffect(string key)
    //{
    //    KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
    //    foreach (KeyValuePair<string, object> kvp in effects)
    //    {
    //        if (kvp.Key.Equals(key))
    //            remove = kvp;
    //    }
    //    if (!default(KeyValuePair<string, object>).Equals(remove))
    //        effects.Remove(remove);
    //}
}

    #endregion



