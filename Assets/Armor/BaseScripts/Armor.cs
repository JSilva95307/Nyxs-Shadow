using UnityEngine;

[CreateAssetMenu(menuName = "Armor")]

public class Armor : ScriptableObject
{
    public ArmorType armorType;
    public ArmorSet armorSet;

    public bool unlocked;

    //Stats are % based
    public float attack;
    public float defense;
    public float health;
    public float speed;
}
