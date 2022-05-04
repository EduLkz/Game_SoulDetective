using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string place;
    public Collider trigger;

    public void Interact() {
        Dialog.Instance.ShowText($"You found {gameObject.name}", DialogType.Notification);
        transform.GetChild(0).gameObject.SetActive(false);
        trigger.enabled = false;
        
    }
}