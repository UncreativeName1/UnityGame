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
    public int startingHealth;
    public float fireSpeed;
    float canFire = 0f;

    private Vector2 moveDirection;

    public Slider healthSlider;
    public Image health;

    public TMP_Text healthDisplay;

    void OnCollisionEnter2D(Collision2D collision) {
        // player hits meteor
        if (collision.gameObject.tag == "Circle") {
            int meteorDamage = collision.gameObject.GetComponent<Meteor>().GetMaxHealth();
            TakeDamage(meteorDamage);
            // Debug.Log(healthValue);
            Debug.Log(healthDisplay.text);
        }
    }

    // Start is called before the first frame update
    void Start() {
        SetMaxHealth(startingHealth);
    }

    // Update is called once per frame
    void Update() 
    {
        Movement();
        Fire();
    }

    int GetCurrentHealth() {
        return (int)healthSlider.value;
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

    public void SetMaxHealth(int healthValue) {
        healthSlider.maxValue = healthValue;
        healthSlider.value = healthValue;
        healthDisplay.text = healthSlider.value.ToString();
    }

    public void TakeDamage(int damage) {
        healthSlider.value -= damage;
        healthDisplay.text = healthSlider.value.ToString();
    }

}

