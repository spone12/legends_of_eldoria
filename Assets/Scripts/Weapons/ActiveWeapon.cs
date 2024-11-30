using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private Sword sword;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        FollowMousePosition();
    }

    public Sword GetActiveWeapon() {
        return sword;
    }

    private void FollowMousePosition() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instanse.GetPlayerScreenPostion();

        if (mousePos.x < playerPos.x) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
