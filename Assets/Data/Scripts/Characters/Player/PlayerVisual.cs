using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        PlayerHealth.Instanse.OnPlayerDeath += Instanse_OnPlayerDeath;
    }

    private void Update() {

        if (Player.Instanse.IsPlayerAlive()) {
            animator.SetBool(IS_RUNNING, Player.Instanse.IsRunning());
            AdjustPlayerFacingDirection();
        }
    }

    /**
     * Event handler on die player
     */
    private void Instanse_OnPlayerDeath(object sender, System.EventArgs e) {
        animator.SetBool(IS_DIE, true);
    }

    /**
* Rotation of the player sprite depending on the mouse
*/
    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instanse.GetPlayerScreenPostion();

        if (mousePos.x < playerPos.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }
}
