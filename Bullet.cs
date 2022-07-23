using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D bullet; 
    public float moveSpeed;
    // Start is called before the first frame update
    void Start() {
        bullet.velocity = new Vector3(0, moveSpeed, 0);
    }

    // Update is called once per frame
    void Update() {
        
    }
}