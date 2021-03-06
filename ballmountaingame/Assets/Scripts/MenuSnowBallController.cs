﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class MenuSnowBallController : MonoBehaviour {

    public float SnowBallSize;
    public float SnowAmountIncrease;
    public float SnowAmountDecrease;
    public float AvalancheMultiplier;
    public float MaxSnowBallSize;
    bool Grounded = true;
    public bool CanChangeSize = true;

    public Transform Snowball;
    public Transform Respawn;
    private Rigidbody2D rb;
    public UnityEvent SnowLoss;
    List<Collider2D> collidedObjects = new List<Collider2D>(2);


    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Snowball = GameObject.Find("SnowBall").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangeSize();
        collidedObjects.ToString();

    }
    void ChangeSize()
    {

        if (SnowBallSize > 0f && rb.velocity.magnitude > 35f && Grounded == false)
        {
            SnowBallSize = Mathf.Lerp(SnowBallSize, SnowBallSize - SnowAmountDecrease, Time.deltaTime * rb.velocity.magnitude * 4);
            SnowLoss.Invoke();
            Debug.Log("Shrinking");
            Snowball.transform.localScale = new Vector3(SnowBallSize, SnowBallSize, 1);
            //Debug.Log(rb.velocity.magnitude);

        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Transform Snowball = gameObject.GetComponentInChildren<Transform>().Find("SnowBall");
        if (col.gameObject.tag == "Platform")
        {
            Grounded = true;

        }
      
        if (CanChangeSize)
        {
            if (SnowBallSize <= MaxSnowBallSize)
            {


                if (col.gameObject.tag == "Snow")
                {
                    SnowBallSize = Mathf.Lerp(SnowBallSize, SnowBallSize + SnowAmountIncrease, 100 * Time.fixedDeltaTime);
                    col.gameObject.SetActive(false);
                    Snowball.transform.localScale = new Vector3(SnowBallSize, SnowBallSize, 1);
                }
               

            }
        

        if (!collidedObjects.Contains(col.collider) && col.collider.tag == "Platform")
        {
            collidedObjects.Add(col.collider);
        }

        if (SnowBallSize >= MaxSnowBallSize)
        {
            if (col.gameObject.tag == "Snow")
            {
                    col.gameObject.SetActive(false);
                }
            }

        if (Input.GetMouseButtonDown(0) && SnowBallSize > 0)
        {
            SnowBallSize = SnowBallSize * 0.8f;
            Debug.Log("Shrink");
        }
        if(SnowBallSize<= 0)
            {
                SnowBallSize = 1.0f;
                transform.position = Respawn.transform.position;
                rb.velocity = new Vector2(0, 0);
            }
        } 
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.name == "KillVolume")
        {
            SnowBallSize = 1.0f;
            rb.velocity = new Vector2(0, 0);

            transform.position = Respawn.transform.position;
        }
       
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        Grounded = false;

    }


}
