using UnityEngine;

public class AttackFunctionHolder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public OgreBehavior ogre;
    


    void JumpAttackStart()
    {
        ogre.jumpQueued = true;
    }

    public void SetAttackingTrue()
    {
        ogre.SetAttacking(true);
    }

    public void SetAttackingFalse()
    {
        ogre.SetAttacking(false);
    }
}
