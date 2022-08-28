using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Powerup : MonoBehaviour {
    public GameObject powerup;
    public Rigidbody2D powerupRigid; 
    public Collider2D powerupCollider;
    public GameObject player;

    public float fallSpeed = -50;
    public int powerupIndex;

    public Image icon;

    System.Random rnd = new System.Random();

    public void StartPowerup(int level) {
        switch (powerupIndex) {
            case 0:
                player.GetComponent<Player>().SpeedBoost(level, player.GetComponent<Player>().powerupData[powerupIndex].duration);
                break;
            case 1:
                player.GetComponent<Player>().DamageBoost(level, player.GetComponent<Player>().powerupData[powerupIndex].duration);
                break;
            case 2:
                player.GetComponent<Player>().FireSpeedBoost(level, player.GetComponent<Player>().powerupData[powerupIndex].duration);
                break;
            case 3: 
                player.GetComponent<Player>().Shield(level);
                break;
            case 4:
                player.GetComponent<Player>().Invincibility(level, player.GetComponent<Player>().powerupData[powerupIndex].duration);
                break;
            case 5:
                player.GetComponent<Player>().Regeneration(level, player.GetComponent<Player>().powerupData[powerupIndex].duration);
                break;
            case 6:
                player.GetComponent<Player>().Healing(level);
                break;
        }
    }

    public void SetPowerupType() {
        powerupIndex = rnd.Next(0, player.GetComponent<Player>().powerupData.Count);
    }

    void Ignore(string tag) { 
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects) {
            if (obj.GetComponent<Collider2D>() && obj != gameObject) {
                Physics2D.IgnoreCollision(powerupCollider, obj.GetComponent<Collider2D>());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject.tag == "Square") {
            powerupRigid.velocity = new Vector3(0, 0, 0);
        } else if (collision.gameObject.tag == "player") {
            StartPowerup(1);
            Destroy(powerup);
        } else {
            Physics2D.IgnoreCollision(powerupCollider, collision.gameObject.GetComponent<Collider2D>());
        }
    }

    // Start is called before the first frame update
    void Start() {
        powerup.tag = "Powerup";
        powerupRigid.velocity = new Vector3(0, fallSpeed, 0);
    }

    // Update is called once per frame
    void Update() {
        Ignore("Circle");
        Ignore("Projectile");
        Ignore("Powerup");
    }
}
