using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkReciever : MonoBehaviour
{
    public ItemType itemType;
    public Animator animator;

    public Vector3 axis = new Vector3( 0, 1, 0 );
    public float degrees = 150f;
    public float timespan = 1f;
   
    private float _rotated = 0;
    private Vector3 _rotationVector;
    void Start()
    {
          axis.Normalize();
        _rotationVector = axis * degrees;

    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("hasDrink") == true)
        {
            Debug.Log("Roatate");
             _rotated += degrees * (Time.deltaTime / timespan);
            if ( degrees > _rotated )
                transform.parent.Rotate( _rotationVector * (Time.deltaTime / timespan) );
        }
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
