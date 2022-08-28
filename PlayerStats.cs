using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour {
    public GameObject canvasObject;

    public Slider healthSlider;
    public TMP_Text healthDisplay;

    public Slider shieldSlider;
    public TMP_Text shieldDisplay;

    public Vector3 sliderInstantiatePosition = new Vector3(377, -210, 0);
    public Slider powerupTimerSlider;
    public Image powerupIcon;

    [SerializeField]
    List<Tuple<Slider, int>> activePowerups = new List<Tuple<Slider, int>>();
    [SerializeField]
    List<bool> isPowerupActive = new List<bool>();

    float powerupDisplayGapFactor = 0.4f; // space between timers relative to timer width

    public GameObject player;

    public void DisplayHealth(int healthValue, int shieldValue) {
        healthSlider.value = healthValue;
        healthDisplay.text = healthSlider.value.ToString();

        shieldSlider.value = shieldValue;
        shieldDisplay.text = shieldSlider.value.ToString();
    }
    
    public void UpdatePowerupDisplay() {
        // Debug.Log(isPowerupActive.Count); /* =7 */
        // indexing player powerupTimers
        int index = 0;
        // check for new powerups
        foreach (float timer in player.GetComponent<Player>().powerupTimers) {
            if (timer > Time.time && !isPowerupActive[index]) {
                // offset all current powerup sliders by width + space
                foreach (Tuple<Slider, int> activePowerup in activePowerups) {
                    activePowerup.Item1.transform.position = new Vector3(activePowerup.Item1.transform.position.x - activePowerup.Item1.GetComponent<RectTransform>().rect.width * (1 + powerupDisplayGapFactor), activePowerup.Item1.transform.position.y, activePowerup.Item1.transform.position.z);
                }

                // create & set new timer
                Slider newPowerupSlider = Instantiate(powerupTimerSlider, sliderInstantiatePosition, Quaternion.identity);
                newPowerupSlider.GetComponent<PowerupTimerIcon>().changeImage(player.GetComponent<Player>().powerupIcons[index]);
                newPowerupSlider.transform.SetParent(canvasObject.transform, false);
                // NEXT: HOW TO ADJUST COORDS IN CANVAS. too high currently
                // newPowerupSlider.transform.position = sliderInstantiatePosition;
                newPowerupSlider.maxValue = player.GetComponent<Player>().powerupData[index].duration;

                activePowerups.Add(new Tuple<Slider, int> (newPowerupSlider, index));
                isPowerupActive[index] = true;
            }
            index++;
        }
    }

    public void UpdatePowerupTimers() {
        // Debug.Log(activePowerups);
        // Debug.Log(activePowerups.Count);
        if (activePowerups.Count <= 0) return;
        for (int i = 0; i < activePowerups.Count; i++) {
            // Debug.Log("HERE: index:" + activePowerups[i].Item2 + " timer:" + player.GetComponent<Player>().powerupTimers[activePowerups[i].Item2] + " current time:" + Time.time);
            // if a powerup is over remove the timer object
            if (player.GetComponent<Player>().powerupTimers[activePowerups[i].Item2] <= Time.time) {
                // when object is destroyed, return offset all sliders in front of it
                isPowerupActive[activePowerups[i].Item2] = false;
                activePowerups[i].Item1.GetComponent<PowerupTimerIcon>().DestroyTimer();
                for (int j = 0; j < i; j++) {
                    activePowerups[j].Item1.transform.position = new Vector3(activePowerups[j].Item1.transform.position.x + (activePowerups[i].Item1.GetComponent<RectTransform>().rect.width * (1 + powerupDisplayGapFactor)), activePowerups[j].Item1.transform.position.y, activePowerups[j].Item1.transform.position.z);
                }
                activePowerups.RemoveAt(i);
                
                i--;
                continue;
            }
            activePowerups[i].Item1.value = player.GetComponent<Player>().powerupTimers[activePowerups[i].Item2] - Time.time;
        }
    }

    // Start is called before the first frame update
    void Start() {
        int health = player.GetComponent<Player>().maxHealth;
        healthSlider.maxValue = health;
        int shield = player.GetComponent<Player>().maxShieldHealth;
        shieldSlider.maxValue = shield;
        Debug.Log("here. " + player.GetComponent<Player>().powerupIcons.Count);
        
        for (int i = 0; i < player.GetComponent<Player>().powerupIcons.Count; i++) {
            isPowerupActive.Add(false);
            // Debug.Log(isPowerupActive[i]);
        }
    }

    // Update is called once per frame
    void Update() {
        DisplayHealth(player.GetComponent<Player>().playerHealth, player.GetComponent<Player>().shieldHealth);
        UpdatePowerupDisplay();
        UpdatePowerupTimers();
    }
}
