using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkReciever : MonoBehaviour
{
    public ItemType itemType;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Recieve(GameObject currentItem)
    {
        if(currentItem.GetComponent<InteractableItem>() == null)
        {
            Debug.LogError("Recieved item wich is not interactable");
            return;
        }

        var item = currentItem.GetComponent<InteractableItem>();

        if(item.itemType == itemType)
        {
            animator.SetBool("hasDrink", true);
            //RECIEVED!
        }else{
            // DONT WANT IT
        }
    }
}
