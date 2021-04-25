using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            var hits = Physics.RaycastAll(raycast,5f, ~mask);
            if (hits != null)
            {
                foreach(RaycastHit raycastHit in hits)
                {
                    if (raycastHit.collider.CompareTag("Interactable"))
                    {
                        if(raycastHit.collider.GetComponent<InteractableItem>())
                        {
                            raycastHit.collider.GetComponent<InteractableItem>().SendMessage("Interact", this.gameObject);
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
