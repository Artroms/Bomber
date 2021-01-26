using UnityEngine;

[CreateAssetMenu(fileName = "BombSettings", menuName = "ScriptableObjects/BombSettings", order = 1)]
public class BombSettings : ScriptableObject
{
    [SerializeField] private float plantTime;
    [SerializeField] private float timeToExplosion;
    [SerializeField] private float explosionDistance;
    [SerializeField] private int maxBombCount;
    public float PlantTime => plantTime;
    public float TimeToExplosion => timeToExplosion;
    public float ExplosionDistance => explosionDistance;
    public int MaxBombCount => maxBombCount;
}
