using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomVRControls : MonoBehaviour {
    public Rigidbody bullet;
    private float nextFireTime;
    public Transform fireTransform;
    public float launchForce = 20f;
    public GameObject bomb;
    public GameObject bombPrefab;
    public Rigidbody player;
    private bool speechBubble = true;
    private float bombForce = 5000.0f;
    public Transform speechBubbleSpawn;
    public Transform bombSpawn;
    public GameObject speechPrefab;
    private GameObject temp;
    public Slider audioSlider;
    // Use this for initialization
    void Start () {
        player = GetComponent<Rigidbody>();
    }
	public void Fire(float launchForce, float fireRate)
    {
        if(Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Rigidbody bulletInstance = Instantiate(bullet, fireTransform.position, fireTransform.rotation) as Rigidbody;
            bulletInstance.velocity = launchForce * fireTransform.forward;
        }
    }
	// Update is called once per frame
	void Update () {
        // returns true if the primary button (typically “A”) is currently pressed.
        if(OVRInput.GetDown(OVRInput.Button.One) && speechBubble == true)
        {
            temp = Instantiate(speechPrefab, speechBubbleSpawn.position, speechBubbleSpawn.rotation);
            temp.transform.parent = transform;
            audioSlider.direction = Slider.Direction.LeftToRight;
            audioSlider.minValue = 0;
            audioSlider.maxValue = temp.GetComponent<AudioSource>().clip.length;
            audioSlider.value = temp.GetComponent<AudioSource>().time;
            Debug.Log("Why");
        }
        if (OVRInput.GetUp(OVRInput.Button.One) && temp.GetComponent<AudioSource>().isPlaying)
        {
            audioSlider.value = 0;
            temp.GetComponent<AudioSource>().Stop();
            temp.transform.parent = null;
            Destroy(temp);
            Debug.Log("Temp Destroyed");
        }
        if (OVRInput.GetUp(OVRInput.Button.One) && !temp.GetComponent<AudioSource>().isPlaying)
        {
            temp.transform.parent = null;
            audioSlider.value = temp.GetComponent<AudioSource>().time;
            Debug.Log("DIDI" + " " + temp.GetComponent<Rigidbody>().velocity);

            temp.GetComponent<AudioSource>().Stop();
            temp.transform.parent = null;

        }
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Fire(launchForce, 1);
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            bomb = Instantiate(bombPrefab, bombSpawn.position, player.rotation);
            bomb.GetComponent<Rigidbody>().useGravity = true;
            bomb.GetComponent<Rigidbody>().AddForce(Vector3.forward * bombForce);
            Debug.Log(bomb.GetComponent<Rigidbody>().velocity);
            speechBubble = true;
        }
        if (OVRInput.Get(OVRInput.Button.Four))
        {
            Fire(launchForce, 1);
        }
        // returns true if the primary button (typically “A”) was pressed this frame.
        OVRInput.GetDown(OVRInput.Button.One);

        // returns true if the “X” button was released this frame.
        OVRInput.GetUp(OVRInput.RawButton.X);

        // returns a Vector2 of the primary (typically the Left) thumbstick’s current state. 
        // (X/Y range of -1.0f to 1.0f)
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // returns true if the primary thumbstick is currently pressed (clicked as a button)
        OVRInput.Get(OVRInput.Button.PrimaryThumbstick);

        // returns true if the primary thumbstick has been moved upwards more than halfway.  
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);

        // returns a float of the secondary (typically the Right) index finger trigger’s current state.  
        // (range of 0.0f to 1.0f)
        OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        // returns a float of the left index finger trigger’s current state.  
        // (range of 0.0f to 1.0f)
        OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

        // returns true if the left index finger trigger has been pressed more than halfway.  
        // (Interpret the trigger as a button).
        OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

        // returns true if the secondary gamepad button, typically “B”, is currently touched by the user.
        OVRInput.Get(OVRInput.Touch.Two);

        // returns true after a Gear VR touchpad tap
        OVRInput.GetDown(OVRInput.Button.One);

        // returns true on the frame when a user’s finger pulled off Gear VR touchpad controller on a swipe down
        OVRInput.GetDown(OVRInput.Button.DpadDown);

        // returns true the frame AFTER user’s finger pulled off Gear VR touchpad controller on a swipe right
        OVRInput.GetUp(OVRInput.RawButton.DpadRight);

        // returns true if the Gear VR back button is pressed
        OVRInput.Get(OVRInput.Button.Two);

        // Returns true if the the Gear VR Controller trigger is pressed down
        OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);

        // Queries active Gear VR Controller touchpad click position 
        // (normalized to a -1.0, 1.0 range, where -1.0, -1.0 is the lower-left corner)
        OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);

        // If no controller is specified, queries the touchpad position of the active Gear VR Controller
        OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        // returns true if the Gear VR Controller back button is pressed
        OVRInput.Get(OVRInput.Button.Back);

        // recenters the active Gear VR Controller. Has no effect for other controller types.
        OVRInput.RecenterController();
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "platform")
        {
            transform.parent = collider.transform;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "SpeechBubble")
        {
            speechBubble = false;

        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "platform")
        {
            transform.parent = null;

        }
    }

}
