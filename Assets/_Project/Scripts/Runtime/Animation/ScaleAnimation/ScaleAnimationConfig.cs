using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleAnimationConfig", menuName = Constants.EditorMenuName + "/ScaleAnimation")]
public class ScaleAnimationConfig : ScriptableObject
{
    [SerializeField] private Vector3 _targetScale = Vector3.one;
    [SerializeField] private float _duration = 0.2f;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private Ease _ease = Ease.OutBack;

    public Vector3 TargetScale => _targetScale;

    public float Duration => _duration;

    public float Delay => _delay;

    public Ease Ease => _ease;
}
