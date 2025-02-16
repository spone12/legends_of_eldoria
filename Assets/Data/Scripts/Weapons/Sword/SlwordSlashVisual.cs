using UnityEngine;

public class SwordSlashVisual : MonoBehaviour
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
     * Trigger sword attack
    */
    private void Sword_OnSwordSwing(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }
}
