using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupTimerIcon : MonoBehaviour {
    public Image powerupIcon;

    public void changeImage(Sprite newImage) {
        powerupIcon.sprite = newImage;
    }

    public void DestroyTimer() {
        Destroy(gameObject);
    }
}
