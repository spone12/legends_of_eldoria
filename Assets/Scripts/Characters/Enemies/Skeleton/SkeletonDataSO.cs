using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonDataSO", menuName = "SO/Enemies/Skeleton")]
public class SkeletonDataSO : ScriptableObject
{
    [SerializeField] public string Name = "Skeleton";
    [SerializeField] public int Health = 20;
    [SerializeField] public int AttackDamage = 2;
}
