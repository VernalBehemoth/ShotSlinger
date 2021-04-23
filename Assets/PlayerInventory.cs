using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();   

    public GameObject currentlySelected;
    public GameObject throwableObject;
    float throwForce = 600;
    Vector3 objectPos;
    float distance;
    public bool isHolding = false;
    public bool throwable = false;
    void Start()
    {
    }
    public void EquipThis(GameObject gameObject){
        foreach(GameObject go in inventory)
        {
            go.SetActive(false);

            if(gameObject.name.Contains(go.name) && currentlySelected != go)
            {
                go.SetActive(true);
                currentlySelected = go;
                throwableObject = gameObject;    
                throwableObject.transform.SetParent(currentlySelected.transform);
                throwableObject.transform.position = currentlySelected.transform.position;
                throwableObject.transform.rotation = currentlySelected.transform.rotation;            
                StartCoroutine(PickUpCoolDown());
            }else if(gameObject.name.Contains(go.name) && currentlySelected == go){
                
                go.SetActive(false);                
                currentlySelected= null;
            }
        }

    }
    IEnumerator PickUpCoolDown()
    {
        throwable = false;
        yield return new WaitForSeconds(1F);
        throwable = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(currentlySelected != null && throwableObject != null && throwable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                throwable = false;


                Debug.Log("THROW DAMN IT");
                isHolding = true;
                           

                distance = Vector3.Distance(throwableObject.transform.position, transform.position);

                // if (distance >= 1f) 
                // {
                //     isHolding = false;
                //     Debug.Log("SET TO FALSE");
                // }
                //Check if isholding
                if (isHolding == true) {
                    throwableObject.GetComponent<Rigidbody>().useGravity = true;
                    throwableObject.GetComponent<Rigidbody>().isKinematic = false;
                    throwableObject.GetComponent<Rigidbody>().detectCollisions = true; 
                    throwableObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    throwableObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    throwableObject.transform.SetParent (transform);

                    if (Input.GetMouseButtonDown(0)) {
                        throwableObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
                        isHolding = false;
                    }
                }
                else 
                {
                    objectPos = throwableObject.transform.position;
                    throwableObject.transform.SetParent(null);
                    throwableObject.GetComponent<Rigidbody>().useGravity = false;
                    throwableObject.transform.position = objectPos;
                }
                isHolding = false;   
                currentlySelected.SetActive(false);         
                currentlySelected = null; 
            }
        }        
    }
   
}
