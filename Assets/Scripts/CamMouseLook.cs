using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothV;
    public float senstitivity = 5.0f;
    public float smoothing = 2.0f;
    public float xRotation;
    public float yRotation;
    public float currentXRotation;
    public float currentYRotation;
    public float lookSensitivity = 5f;
    public float lookSmoothDamp = 0.1f;
    public float xRotationV;
    public float yRotationV;
    GameObject character;
	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
