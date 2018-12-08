﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpeechBubble : MonoBehaviour {

    private bool isPlaying = false;
    private  AudioSource thisSound;
    private AudioClip sound;
    private Rigidbody speechBubble;
    public AudioClip[] myClips;
    public float damageFactor;
    private float speed;
    private float damage;
    private Vector3 destinationScale;
    private bool isScaling = false;
    private Vector3 smallScale = new Vector3(0, 0, 0);

    void Awake()
    {
        myClips = Resources.LoadAll<AudioClip>("Audio");
        transform.localPosition = transform.position;
        thisSound = GetComponent<AudioSource>();
        if (isPlaying == false)
        {
            isPlaying = true;

            RandomizeSfx(myClips);
        }
        speed = 400 / thisSound.clip.length;
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
            {
                //transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                destinationScale = transform.localScale;
            }

        }
        if (!thisSound.isPlaying)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            isPlaying = false;
            speechBubble.isKinematic = false;
            transform.parent = null;
            Destroy(gameObject, thisSound.clip.length);
            //DestroySound(thisSound.clip.length);
            //Invoke("DestroySound", 0.09f * thisSound.clip.length);
            //transform.localScale *= 0.25f;
            //thisSound.Stop();
        }
    }
    private void DestroySound() //Method to decrease size based on the amount of time
    {
        //float scaleFactor = 0.99f + (0.5 / time);
        transform.localScale *= .9f;
        
        if (transform.localScale == smallScale)
        {
            Destroy(gameObject);
            Debug.Log("SPEECH BUBBLE DESTROYED");
        }
        //Destroy(gameObject);
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
                int damage = Mathf.RoundToInt(damageFactor * thisSound.clip.length);
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
