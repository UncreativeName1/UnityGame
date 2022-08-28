using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public GameObject player;
    public GameObject pointer; // will show where you're gonna shoot    
    // public GameObject scriptHolder;

    public float pointerY = 200;

    void Movement() {
        transform.position = new Vector3(player.transform.position.x, pointerY, 0f);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Movement();
    }
}
