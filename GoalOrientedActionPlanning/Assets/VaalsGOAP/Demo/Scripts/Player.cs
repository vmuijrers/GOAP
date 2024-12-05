using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Transform cam;
    public Transform head;
    private float angleX = 0;
    private float angleY = 0;
    public float sensY = 15f;
    public float sensX = 15f;
    public float headOffSet = 0.25f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        transform.position += (transform.forward * vert + transform.right * hor).normalized * moveSpeed * Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        angleX += mouseX * sensY;
        angleY += mouseY * sensX;
        angleY = Mathf.Clamp(angleY, -89f, 89f);
        transform.localRotation = Quaternion.Euler(0, angleX, 0);
        cam.transform.position = head.transform.position + head.transform.forward * headOffSet;
        cam.transform.localRotation = Quaternion.Euler(-angleY, 0, 0);

    }
}
