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

    public GameObject bullet;

    float bounceYVelocity = 0;
    float bounceVariability = 150; // randomness of x velocity change when bouncing
    float splitVariability = 50; // minimum magnitude of x velocity upon separation
    float gravity = 9.806f;
    float bounceHeight = 180;

    int primeBounces = 4;

    Dictionary<int, List<Tuple<int, int>>> factors = new Dictionary<int, List<Tuple<int, int>>>() {};

    float startingUpperbound = 12f;
    public int maxHealth;
    int meteorHealth;
    public TMP_Text meteorHealthDisplay;

    System.Random rnd = new System.Random();

    void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log("entered #" + i); i++;
        // Debug.Log(collision.collider.GetInstanceID());
        // Debug.Log(circleCollider.GetInstanceID());

        if (collision.gameObject.tag == "player") {
            player.GetComponent<Player>().TakeDamage(maxHealth);
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

        if (collision.gameObject.tag == "Projectile") {
            meteorHealth -= bullet.GetComponent<Bullet>().GetBulletDamage();
            //SetHealthText();

            if (IsPrime(maxHealth)) {
                Debug.Log("DESTROYED PRIME");
                player.GetComponent<Player>().TakeDamage(maxHealth);
                Destroy(circle);
            } else if (meteorHealth <= 0) {
                // splitting nonprime meteor
                Debug.Log("bullet hit."); 
                Tuple<int, int> splitFactors = GenerateFactorPair(maxHealth);
                Debug.Log(splitFactors.Item1 + " " + splitFactors.Item2);
                Split(splitFactors.Item1, splitFactors.Item2);
                Destroy(circle);
            }
        }
    }

    void Split(int splitHealth1, int splitHealth2) {
        // create split meteor
        GameObject split1 = Instantiate(circle, new Vector3(circle.transform.position.x, circle.transform.position.y, 0), Quaternion.identity);
        // set max health of meteor
        split1.GetComponent<Meteor>().maxHealth = splitHealth1;
        // set current health of meteor
        split1.GetComponent<Meteor>().SetCurrentHealth();
        // bounce meteor
        split1.GetComponent<Meteor>().Bounce();

        // same thing with other meteor
        GameObject split2 = Instantiate(circle, new Vector3(circle.transform.position.x, circle.transform.position.y, 0), Quaternion.identity);
        split2.GetComponent<Meteor>().maxHealth = splitHealth2;
        split2.GetComponent<Meteor>().SetCurrentHealth();
        split2.GetComponent<Meteor>().Bounce();


        // make sure they split in oppposite directions
        float XSpeed1 = (float)rnd.NextDouble() * bounceVariability + splitVariability;
        float XSpeed2 = -(float)rnd.NextDouble() * bounceVariability + splitVariability;
        if (rnd.Next(0, 2) == 1) {
            XSpeed1 *= -1;
            XSpeed2 *= -1;
        }
        CalculateBounceVelocity(circle.transform.position.y);
        split1.GetComponent<Meteor>().circleRigid.velocity = new Vector2(XSpeed1, bounceYVelocity);
        split2.GetComponent<Meteor>().circleRigid.velocity = new Vector2(XSpeed2, bounceYVelocity);
    }

    void RandomHealth(float modifier) {
        float upperbound = startingUpperbound + modifier;
        maxHealth = rnd.Next(2, (int)Math.Floor(upperbound) + 1);
        SetCurrentHealth();
    }

    public int GetMaxHealth() {
        return maxHealth;
    }
    
    public int GetCurrentHealth() {
        return meteorHealth;
    }

    public void SetCurrentHealth() {
        meteorHealth = maxHealth;
    }

    public bool IsPrime(int number) {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        int boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2) 
            if (number % i == 0)
                return false;

        return true;        
    }

    void GenerateAllFactorPairs(int number) {
        if (factors.ContainsKey(number)) return;
        
        // creating empty key to exist
        var temp = new List<Tuple<int, int>>();
        factors.Add(number, temp);
        Tuple<int, int> factor;
        for (double i = 2; i <= Math.Sqrt(number); i++) {
            if ((double)number % i == 0) {
                factor = new Tuple<int, int>((int)i, (int)(number / i));
                factors[number].Add(factor);
            }
        }
    }

    public Tuple<int, int> GenerateFactorPair(int number) {
        var temp = new Tuple<int, int>(number, number);
        if (IsPrime(number)) return temp;

        // Debug.Log("000000");
        // Debug.Log(factors.ContainsKey(number));
        // Debug.Log("111111");
        if (!factors.ContainsKey(number)) {
            GenerateAllFactorPairs(number);
        }
        Debug.Log("generate factor pair 1");

        int index = rnd.Next(0, factors[number].Count);
        return factors[number][index];
    }

    public void Bounce() {
        CalculateBounceVelocity(circle.transform.position.y);
        // Debug.Log("BOUNCE Y:" + bounceYVelocity);
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

    void SetHealthText() {
        meteorHealthDisplay.text = meteorHealth.ToString() + "/" + maxHealth.ToString();
    }

    void Awake() {
        // feed this parameter from GenerateMeteor.cs
        RandomHealth(0);
        maxHealth = meteorHealth;
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
        SetHealthText();
    }
}



