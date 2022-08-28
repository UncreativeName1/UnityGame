using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public GameObject player;
    public GameObject pointer;
    public GameObject instantiatedPlayer;
    public GameObject instantiatedPointer;
    // Start is called before the first frame update
    void Start() {
        instantiatedPlayer = Instantiate(player, new Vector3(0, -146, 0), Quaternion.identity);
        instantiatedPlayer.GetComponent<Player>().player = player;
        // player.GetComponent<Player>().SetHealth(player.GetComponent<Player>().maxHealth);

        instantiatedPointer = Instantiate(pointer, new Vector3(instantiatedPlayer.transform.position.x, pointer.GetComponent<Pointer>().pointerY, 0), Quaternion.identity);
        instantiatedPointer.GetComponent<Pointer>().player = instantiatedPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
