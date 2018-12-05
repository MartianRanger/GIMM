using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpeechBubble : MonoBehaviour {

    private bool isPlaying = false;
    private  AudioSource thisSound;
    private AudioClip sound;
    private Rigidbody speechBubble;
    public AudioClip[] myClips;
    private float speed;
    private float damage;

    void Awake()
    {
        myClips = Resources.LoadAll<AudioClip>("Audio");

        thisSound = GetComponent<AudioSource>();
        if (isPlaying == false)
        {
            isPlaying = true;

            RandomizeSfx(myClips);
        }
        speed = 200 / thisSound.clip.length;

    }
    // Use this for initialization
    void Start () {
        myClips = Resources.LoadAll<AudioClip>("Audio");
        speechBubble = GetComponent<Rigidbody>();
        if (isPlaying == false)
        {
            isPlaying = true;
            //thisSound.clip = myClips[Random.Range(0, myClips.Length)];
            //thisSound.Play();
        }
    }

    // Update is called once per frame
    void Update () {
        if (thisSound.isPlaying)
        {
            speechBubble.isKinematic = true;
            if(transform.localScale.x <= 2f && transform.localScale.y <= 2f && transform.localScale.z <= 2f)
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }
        if (!thisSound.isPlaying)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            isPlaying = false;
            speechBubble.isKinematic = false;
            transform.parent = null;
            //thisSound.Stop();
        }
    }
    void DestroySound()
    {
        //Destroy(gameObject);
    }
    public void BuildSound(GameObject temp)
    {
        temp.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
    }
    public void RandomizeSfx(params AudioClip[] myClips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, myClips.Length);

        //Set the clip to the clip at our randomly chosen index.
        thisSound.clip = myClips[randomIndex];

        //Play the clip.
        thisSound.Play();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if(thisSound.isPlaying == false)
            {
                var hit = other.gameObject;
                var health = hit.GetComponent<Health>();
                int damage = Mathf.RoundToInt(5 * thisSound.clip.length);
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
                Debug.Log("Destroyed!");
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "SpeechBubble")
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}
