using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = this.GetComponent<PlayerInventory>();

        if(playerInventory == null)
            Debug.LogError("Player Raycaster must know about player inventory to be able to interact with NPC");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Ray raycast = Input.GetMouseButtonDown(0) ?  Camera.main.ScreenPointToRay(Input.mousePosition) : Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
            // Bit shift the index of the layer (8) to get a bit mask
            int scenary = 1 << LayerMask.NameToLayer("Walkable");
            int player = 1 << LayerMask.NameToLayer("Ignore Raycast");
            int mask = scenary | player;
            var hits = Physics.RaycastAll(raycast,5f);
            Debug.Log("Ray");
            if (hits != null)
            {
                foreach(RaycastHit raycastHit in hits)
                {
                    Debug.Log(raycastHit.collider.tag);
                    if (raycastHit.collider.CompareTag("Interactable"))
                    {
                        if(raycastHit.collider.GetComponent<InteractableItem>())
                        {
                            raycastHit.collider.GetComponent<InteractableItem>().SendMessage("Interact", this.gameObject);
                        }else{
                            Debug.Log("Please add interactable script to: " + raycastHit.collider.gameObject.name);
                        }
                        break;
                    }else  if (raycastHit.collider.CompareTag("NPC")){
                        if(raycastHit.collider.GetComponent<DrinkReciever>())
                        {
                            raycastHit.collider.GetComponent<DrinkReciever>().SendMessage("Recieve", playerInventory.currentlySelected);
                        }else{
                            Debug.Log("Please add interactable script to: " + raycastHit.collider.gameObject.name);
                        }
                        break;
                    }
                }
               
            }
        }
    }
}
