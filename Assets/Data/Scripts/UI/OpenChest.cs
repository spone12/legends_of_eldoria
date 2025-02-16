using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public GameObject tooltip;

    public bool isNearPlayer;

    public InputEventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {

        eventHandler = Player.Instanse.GetComponent<InputEventHandler>();
        if (tooltip != null) {
            tooltip.SetActive(false);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {

            eventHandler.EventHandlerManagment(true);
            if (tooltip != null) {
                tooltip.SetActive(true);
            }

            // The player near the object
            isNearPlayer = true;
            InputEventHandler.OnActionKeyPressed += OpenChest;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            if (tooltip != null) {
                tooltip.SetActive(false);
            }

            isNearPlayer = false;
            InputEventHandler.OnActionKeyPressed -= OpenChest;
            eventHandler.EventHandlerManagment();
        }
    }

    private void OpenChest() {

        if (isNearPlayer) {
            Debug.Log ("Open chest");
        }
    }
}
