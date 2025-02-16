using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonDataSO", menuName = "SO/Enemies/Skeleton")]
public class SkeletonDataSO : ScriptableObject
{
    [field: SerializeField] 
    public string Name { get; set; } = "Skeleton";

    [field: SerializeField] 
    public int Health { get; set; } = 20;

    [field: SerializeField] 
    public int AttackDamage { get; set; } = 2;
}
