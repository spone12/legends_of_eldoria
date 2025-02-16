using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Sword sword;

    private const string ATTACK = "Attack";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    /**
     * Set animator trigger ATTACK
    */
    private void Sword_OnSwordSwing(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }

    /**
     * Trigger end attack animation
    */
    public void TriggerEndAttackAnimation() {
        sword.AttackColliderTurnOff();
    }
}
