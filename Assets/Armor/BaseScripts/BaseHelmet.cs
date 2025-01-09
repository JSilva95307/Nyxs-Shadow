using UnityEngine;

[CreateAssetMenu(menuName = "Armor")]

public abstract class BaseHelmet : ScriptableObject
{
    public ArmorType armorType;
    public ArmorSet armorSet;

    public bool equipped;
    public float attack;
    public float defense;
    public float health;
    public float speed;


}
