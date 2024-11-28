using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public GameObject hintText;

    public bool isNearPlayer;

    public InputEventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {
        // eventHandler = GetComponent<InputEventHandler>();
        if (hintText != null) {
            hintText.SetActive(false);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            
            if (hintText != null) {
                hintText.SetActive(true);
            }

            // The player near the object
            isNearPlayer = true;
            InputEventHandler.OnActionKeyPressed += OpenChest;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            if (hintText != null) {
                hintText.SetActive(false);
            }

            isNearPlayer = false;
            InputEventHandler.OnActionKeyPressed -= OpenChest;
            //eventHandler.enable = false;
        }
    }

    private void OpenChest() {

        if (isNearPlayer) {
            Debug.Log ("Open chest");
        }
    }
}
