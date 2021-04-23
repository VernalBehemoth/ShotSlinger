using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Interact(GameObject gameObject){
        gameObject.GetComponent<PlayerInventory>().SendMessage("EquipThis", this.gameObject);
        Debug.Log("INTERACTABLE ITERACTED :" + this.gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
