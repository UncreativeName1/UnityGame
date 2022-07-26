using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {
    public GameObject player;
    public GameObject projectile;
    // public Collider2D meteor;
    public float moveSpeed;
    public int maxHealth;
    public int playerHealth;
    public float fireSpeed;
    float canFire = 0f;

    private Vector2 moveDirection;

    // public Slider healthSlider;
    // public Image health;
    // public TMP_Text healthDisplay;

    void OnCollisionEnter2D(Collision2D collision) {
        // player hits meteor
        // if (collision.gameObject.tag == "Circle") {
        //     int meteorDamage = collision.gameObject.GetComponent<Meteor>().GetMaxHealth();
        //     TakeDamage(meteorDamage);
        // }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() 
    {
        Movement();
        Fire();
        //Debug.Log(playerHealth);
    }

    public int GetCurrentHealth() {
        return playerHealth;
    }

    void Fire() {
        if (Input.GetButton("Fire1") && Time.time > canFire) {
            canFire = Time.time + fireSpeed;
            Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y, 0), Quaternion.identity);
        }
    }

    void Movement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(moveX, 0);
        transform.position += new Vector3(moveDirection.x * moveSpeed * Time.deltaTime, 0f, 0f);
    }

    public void SetHealth(int healthValue) {
        playerHealth = healthValue;
    }

    public void TakeDamage(int damage) {
        Debug.Log("Old player health: " + playerHealth);
        playerHealth -= damage;
        Debug.Log("DAMAGE TAKEN: " + damage);
        Debug.Log("New player health: " + playerHealth);
    }
}

