using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSorter : MonoBehaviour
{
    [SerializeField] private float _offset = 0.4f;
    private int _sortingOrderBase = 0;
    private SpriteRenderer _renderer;

    private bool _isStatic = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _renderer.sortingOrder = (int)(_sortingOrderBase - transform.position.y + _offset);

        if(_isStatic)
        {
            Destroy(this);
        }
    }
}