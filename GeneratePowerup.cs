using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ADD GUARANTEE SPAWN EVERY 12 SEC

public class GeneratePowerup : MonoBehaviour {
    public GameObject player;
    public GameObject powerup;
    public GameObject instantiatedPowerup;
    System.Random rnd = new System.Random();
    int counter = 0;
    double random;
    public double powerupSpawnFrequency; // between 0 and 100
    int maxPowerups = 10000000;
    float spawnRadius = 300f;

    float spawnGuaranteeTracker;
    public float spawnGuaranteeInterval = 12;

    // Start is called before the first frame update
    void Start() {
        // so it doesnt spawn immediately
        spawnGuaranteeTracker = spawnGuaranteeInterval;
        // Instantiate(powerup, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        random = rnd.NextDouble();
        if (random < powerupSpawnFrequency / 10000 || spawnGuaranteeTracker < Time.time) {
            if (counter < maxPowerups) {
                float factor = ((float)rnd.NextDouble() - 0.5f) * 2 * spawnRadius;
                spawnGuaranteeTracker = Time.time + spawnGuaranteeInterval;
                // powerup.GetComponent<powerup>().RandomHealth();
                instantiatedPowerup = Instantiate(powerup, new Vector3(factor, 250, 0), Quaternion.identity);
                instantiatedPowerup.GetComponent<Powerup>().SetPowerupType();
                instantiatedPowerup.GetComponent<Powerup>().icon.sprite = player.GetComponent<Player>().powerupIcons[instantiatedPowerup.GetComponent<Powerup>().powerupIndex];
                Debug.Log("Powerup Index: " + instantiatedPowerup.GetComponent<Powerup>().powerupIndex);
                counter++;
            }
        }
    }
}