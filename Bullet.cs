using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public Rigidbody2D bulletRigid; 

    public float moveSpeed;
    public int bulletDamage = 1;

    public int GetBulletDamage() {
        return bulletDamage;
    }

    void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject.tag == "Circle") {
            Destroy(bullet);
        }
        
        // if (collision.gameObject.tag == "")
    }

    // Start is called before the first frame update
    void Start() {
        bullet.tag = "Projectile";
        bulletRigid.velocity = new Vector3(0, moveSpeed, 0);
    }

    // Update is called once per frame
    void Update() {
        
    }
}