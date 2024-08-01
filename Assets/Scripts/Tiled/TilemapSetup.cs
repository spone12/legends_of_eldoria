using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSetup : MonoBehaviour {
    void Start() {
        TilemapCollider2D tilemapCollider = GetComponent<TilemapCollider2D>();
        CompositeCollider2D compositeCollider = GetComponent<CompositeCollider2D>();

        if (tilemapCollider == null) {
            Debug.LogError("TilemapCollider2D is missing on the Tilemap.");
        } else {
            tilemapCollider.usedByComposite = true;
        }

        if (compositeCollider == null) {
            Debug.LogError("CompositeCollider2D is missing on the Tilemap.");
        }
    }
}
