﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float moveSpeed;
    public Rigidbody2D theRB;

    public Transform bottomLeftLimit, topRightLimit;

    public Transform shotPoint;
    public GameObject shot;

    public float timeBetweenShots = .1f;
    private float shotCounter;

    private float normalSpeed;
    public float boostSpeed;
    public float boostLength;
    private float boostCounter;

    public bool doubleShotActive;
    public float doubleShotOffset;

    public bool stopMovement;

    private Rigidbody2D rb;

    private AudioSource myAudio;
    public AudioClip myClip;
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        normalSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayOneShot(myClip);

    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement)
        {


            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

            transform.position =
                new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.position.x, topRightLimit.position.x),
                    Mathf.Clamp(transform.position.y, bottomLeftLimit.position.y, topRightLimit.position.y),
                    transform.position.z);

            if (Input.GetButtonDown("Fire1"))
            {
                if (!doubleShotActive)
                {
                    Instantiate(shot, shotPoint.position, shotPoint.rotation);

                }
                else
                {
                    Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
                    Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
                }

                shotCounter = timeBetweenShots;
            }

            if (Input.GetButton("Fire1"))
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    if (!doubleShotActive)
                    {
                        Instantiate(shot, shotPoint.position, shotPoint.rotation);

                    }
                    else
                    {
                        Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f),
                            shotPoint.rotation);
                        Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f),
                            shotPoint.rotation);
                    }

                    shotCounter = timeBetweenShots;
                }


            }
            
          

            if (boostCounter > 0)
            {
                boostCounter -= Time.deltaTime;
                if (boostCounter <= 0)
                {
                    moveSpeed = normalSpeed;
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero; //setting up velocity to be zero when stopMovement happens
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            myAudio.Play();
            
        }
    }

    public void ActivateSpeedBoost()
        {
            boostCounter = boostLength;
            moveSpeed = boostSpeed;
        }
        
        
    
    
}
    

