using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {
    public Slider healthSlider;
    public Image health;
    public TMP_Text healthDisplay;

    public GameObject player;

    public void DisplayHealth(int healthValue) {
        healthSlider.value = healthValue;
        healthDisplay.text = healthSlider.value.ToString();
    }

    // Start is called before the first frame update
    void Start() {
        int health = player.GetComponent<Player>().maxHealth;
        Instantiate(player, new Vector3(0, -134, 0), Quaternion.identity);
        player.GetComponent<Player>().SetHealth(health);
        healthSlider.maxValue = health;
    }

    // Update is called once per frame
    void Update() {
        DisplayHealth(player.GetComponent<Player>().GetCurrentHealth());
    }
}
