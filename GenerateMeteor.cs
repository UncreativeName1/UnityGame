using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMeteor : MonoBehaviour {
    public GameObject meteor;
    System.Random rnd = new System.Random();
    int counter = 0;
    double random;
    double meteorSpawnFrequency = 2; // between 0 and 100
    int maxMeteors = 10;
    float spawnRadius = 300f;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(meteor, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        random = rnd.NextDouble();
        if (random < meteorSpawnFrequency / 10000) {
            if (counter < maxMeteors) {
                float factor = ((float)rnd.NextDouble() - 0.5f) * 2 * spawnRadius;
                // meteor.GetComponent<Meteor>().RandomHealth();
                Instantiate(meteor, new Vector3(factor, 250, 0), Quaternion.identity);
                counter++;
            }
        }
    }
}