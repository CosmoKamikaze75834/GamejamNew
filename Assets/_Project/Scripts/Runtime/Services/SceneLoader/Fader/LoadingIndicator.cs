using System.Collections;
using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
    [SerializeField] private int _jumps = 8;
    [SerializeField] private float _cycleDuration = 0.4f;

    private Transform _transform;
    private float _anglePerJump;
    private float _delayBetweenJumps;
    private Coroutine _rotationCoroutine;
    private WaitForSecondsRealtime _sleepTime;

    private void Awake()
    {
        _transform = transform;
        _anglePerJump = 360f / _jumps;
        _delayBetweenJumps = _cycleDuration / _jumps;
        _sleepTime = new(_delayBetweenJumps);
    }

    private void OnEnable()
    {
        _rotationCoroutine = StartCoroutine(RotateByJumpsRoutine());
    }

    private void OnDisable()
    {
        if (_rotationCoroutine != null)
            StopCoroutine(_rotationCoroutine);
    }

    private IEnumerator RotateByJumpsRoutine()
    {
        while (true)
        {
            for (int i = 0; i < _jumps; i++)
            {
                _transform.Rotate(0, 0, -_anglePerJump);

                yield return _sleepTime;
            }
        }
    }
}
