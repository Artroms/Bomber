using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "ScriptableObjects/CharacterSettings", order = 0)]
public class CharacterSettings : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private int health;
    public float Speed => speed;
    public int Health => health;
}
