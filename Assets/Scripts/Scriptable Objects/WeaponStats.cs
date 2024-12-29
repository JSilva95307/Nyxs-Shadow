using UnityEngine;


[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
    //all the basic attribute stats for the different weapon attacks
    public float P_damage;
    public float P_critMult;
    public float P_cooldown;
    public float S_damage;
    public float S_critMult;
    public float S_cooldown;
    public float A1_damage;
    public float A1_critMult;
    public float A1_cooldown;
    public float A2_damage;
    public float A2_critMult;
    public float A2_cooldown;
    //all the variables for things like animations and whatnot will go here.
    bool isComplete;
}
