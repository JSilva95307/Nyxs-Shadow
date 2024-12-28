using UnityEngine;

public class WeaponStats : ScriptableObject
{
    //all the basic attribute stats for the different weapon attacks
    float P_damage;
    float P_critMult;
    float P_cooldown;
    float S_damage;
    float S_critMult;
    float S_cooldown;
    float A1_damage;
    float A1_critMult;
    float A1_cooldown;
    float A2_damage;
    float A2_critMult;
    float A2_cooldown;
    [Space(20)]

    //all the variables for things like animations and whatnot will go here.
    bool isComplete;
}
