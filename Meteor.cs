using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class Meteor : MonoBehaviour
{
    public GameObject circle;
    public Rigidbody2D circleRigid;
    public Collider2D circleCollider;
    public GameObject square;
    public Rigidbody2D squareRigid;
    public Collider2D squareCollider;

    public GameObject player;
    public Collider2D playerCollider;

    float bounceYVelocity = 0;
    float bounceVariability = 150; // randomness of x velocity change when bouncing
    float gravity = 9.806f;
    int j = 0;
    float bounceHeight = 180;

    int primeBounces = 4;

    float startingUpperbound = 12f;
    public int maxHealth;
    public int meteorHealth;
    public TMP_Text meteorHealthDisplay;

    System.Random rnd = new System.Random();

    void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log("entered #" + i); i++;
        // Debug.Log(collision.collider.GetInstanceID());
        // Debug.Log(circleCollider.GetInstanceID());

        if (collision.gameObject.tag == "player") {
            // take dmg
            if (IsPrime(maxHealth)) {
                primeBounces--;
                if (primeBounces <= 0) {
                    Destroy(circle);
                }
            }
            Bounce();
        }

        if (collision.gameObject.tag == "Circle") {
            Physics2D.IgnoreCollision(circleCollider, collision.gameObject.GetComponent<Collider2D>());
            // Debug.Log("circle collide");
        }

        if (collision.gameObject.tag == "Square") {
            // if (bounceYVelocity == 0) {
            //     bounceYVelocity = circle.GetComponent<Rigidbody2D>().velocity.y;
            // }
            // Debug.Log(bounceYVelocity);
            // Debug.Log("touch #" + j); j++;

            // if the meteor is prime
            if (IsPrime(maxHealth)) {
                primeBounces--;
                if (primeBounces <= 0) {
                    Destroy(circle);
                }
            }
            Bounce();
        }
    }

    void RandomHealth(float modifier) {
        float upperbound = startingUpperbound + modifier;
        meteorHealth = rnd.Next(2, (int)Math.Floor(upperbound) + 1);
    }

    public int GetMaxHealth() {
        return maxHealth;
    }
    
    public int GetCurrentHealth() {
        return meteorHealth;
    }

    public bool IsPrime(int number) {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2) 
            if (number % i == 0)
                return false;

        return true;        
    }

    void Bounce() {
        CalculateBounceVelocity(circle.transform.position.y);
        Debug.Log("BOUNCE Y:" + bounceYVelocity);
        float factor = ((float)rnd.NextDouble() - 0.5f) * 2 * bounceVariability;
        // if (rnd.Next(0,2) == 1) {
        //     circleRigid.velocity = new Vector2((float)rnd.NextDouble() * -bounceVariability, bounceYVelocity);
        // }
        // else {
        //     circleRigid.velocity = new Vector2((float)rnd.NextDouble() * bounceVariability, bounceYVelocity);
        // }
        circleRigid.velocity = new Vector2(factor, bounceYVelocity);
    }

    void Ignore(string tag) { 
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects) {
            if (obj.GetComponent<Collider2D>() && obj != gameObject) {
                Physics2D.IgnoreCollision(circleCollider, obj.GetComponent<Collider2D>());
            }
        }
    }

    // meteorY MUST be smaller than bounceHeight
    void CalculateBounceVelocity(float meteorY) {
        float displacement = bounceHeight - meteorY;
        if (displacement < 0) displacement = 0;
        bounceYVelocity = (float)Math.Sqrt(2 * gravity * displacement);
    }

    void Awake() {
        // feed this parameter from GenerateMeteor.cs
        RandomHealth(0);
        Debug.Log("here");
        maxHealth = meteorHealth;
        meteorHealthDisplay.text = meteorHealth.ToString() + "/" + maxHealth.ToString();
        Debug.Log("created meteor");
    }

    // Start is called before the first frame update
    void Start() {
        gravity *= circleRigid.gravityScale;
        circleCollider.tag = "Circle";
        squareCollider.tag = "Square";
        playerCollider.tag = "player";
        // Debug.Log(circle.transform.position.y);
        // CalculateBounceVelocity(circle.transform.position.y);
        // Debug.Log("Final Velocity is: " + bounceYVelocity);
    }

    // Update is called once per frame
    void Update() {
        Ignore("Circle");
    }
}



