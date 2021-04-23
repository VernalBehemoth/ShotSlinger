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
            Debug.Log(Input.GetMouseButtonDown(0));
            Ray raycast = Input.GetMouseButtonDown(0) ?  Camera.main.ScreenPointToRay(Input.mousePosition) : Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.CompareTag("Interactable"))
                {
                    if(raycastHit.collider.GetComponent<InteractableItem>())
                    {
                        raycastHit.collider.GetComponent<InteractableItem>().SendMessage("Interact", this.gameObject);
                    }else{
                        Debug.Log("Please add interactable script to: " + raycastHit.collider.gameObject.name);
                    }
                    
                }
            }
        }
    }
}
