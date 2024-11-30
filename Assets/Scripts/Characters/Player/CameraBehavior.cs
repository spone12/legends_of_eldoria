using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /**
     * Camera following the player's movement
     */
    private void LateUpdate() {
        Vector3 pos = transform.position;
        pos.x = player.position.x;
        pos.y = player.position.y;

        transform.position = pos;
    }
}
