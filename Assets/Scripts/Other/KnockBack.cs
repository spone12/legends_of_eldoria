using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour {
    [SerializeField] private float _knockBackForce = 3f;
    [SerializeField] private float _knockBackTimeMax = 0.3f;

    private float _knockBackMovingTime;

    private Rigidbody2D _rb;

    public bool IsKnockedBack { get; private set; }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _knockBackMovingTime -= Time.deltaTime;
        if (_knockBackMovingTime < 0 ) {
            StopKnockBackMovement();
        }
    }

    /**
     * 
     */
    public void GetKnockedBack(Transform damageSource) {

        IsKnockedBack = true;
        _knockBackMovingTime = _knockBackTimeMax;
        // Subtract the position of the source object from our object, multiply by the knock force / mass body
        Vector2 diff = (transform.position - damageSource.position).normalized * _knockBackForce / _rb.mass;
        _rb.AddForce(diff, ForceMode2D.Impulse);
    }

    /**
     * Stop knock back movement
     */
    public void StopKnockBackMovement() {
        _rb.velocity = Vector2.zero;
        IsKnockedBack = false;
    }
}
