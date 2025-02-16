using UnityEngine;

[RequireComponent(typeof(Animator))] 
[RequireComponent(typeof(SpriteRenderer))] 
public class EnemyVisual : MonoBehaviour
{
    [SerializeField] protected EnemyEntity _enemyEntity;
    [SerializeField] protected EnemyAI _enemyAI;
    [SerializeField] protected GameObject _enemyShadow;

    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
