using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ADD GUARANTEE SPAWN EVERY 8 SEC

public class GenerateMeteor : MonoBehaviour {
    public GameObject meteor;
    public GameObject instantiatedMeteor;
    System.Random rnd = new System.Random();
    int counter = 0;
    double random;
    public double meteorSpawnFrequency = 4; // between 0 and 100
    int maxMeteors = 10000000;
    float spawnRadius = 300f;

    float spawnGuaranteeTracker = 0;
    public float spawnGuaranteeInterval = 8;

    public List<Sprite> defaultMeteorSprites;
    public List<Sprite> customMeteorSprites; // custom meteor skins

    int randomSpriteIndex;

    // Start is called before the first frame update
    void Start() {
        // so it doesnt spawn immediately
        spawnGuaranteeTracker = spawnGuaranteeInterval;
        // Instantiate(meteor, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {
        random = rnd.NextDouble();
        if (random < meteorSpawnFrequency / 10000 || spawnGuaranteeTracker < Time.time) {
            if (counter < maxMeteors) {
                float factor = ((float)rnd.NextDouble() - 0.5f) * 2 * spawnRadius;
                spawnGuaranteeTracker = Time.time + spawnGuaranteeInterval;
                // meteor.GetComponent<Meteor>().RandomHealth();
                instantiatedMeteor = Instantiate(meteor, new Vector3(factor, 250, 0), Quaternion.identity);
                // if skins active
                if (customMeteorSprites.Count > 0) {
                    randomSpriteIndex = rnd.Next(0, customMeteorSprites.Count);
                    instantiatedMeteor.GetComponent<Meteor>().setImage.sprite = customMeteorSprites[randomSpriteIndex];
                } else {
                    randomSpriteIndex = rnd.Next(0, defaultMeteorSprites.Count);
                    instantiatedMeteor.GetComponent<Meteor>().setImage.sprite = defaultMeteorSprites[randomSpriteIndex];
                }
                
                counter++;
            }
        }
    }
}