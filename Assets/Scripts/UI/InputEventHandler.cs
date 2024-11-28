using UnityEngine;

public class InputEventHandler : MonoBehaviour
{
    // Action button press handling event
    public static event System.Action OnActionKeyPressed; 

    private void Update()
    {
        Debug.Log ("InputEventHandler Update");
        // E key pressed
        if (Input.GetKeyDown(KeyCode.E)) {
            // Call the event
            OnActionKeyPressed?.Invoke();
        }   
    }
}
