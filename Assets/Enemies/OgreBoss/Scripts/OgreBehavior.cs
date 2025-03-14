using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OgreBehavior : BaseEnemy
{
    public Animator animator;

    //public float timeBetweenAttacks = 2f;
    private float timer = 0f;
    private Health health;

    private bool dead = false;

    public bool jumpQueued = false;


    public AnimationCurve curve; // The animation curve for vertical offset
    public float jumpHeight = 5f; // Height of the parabola
    public float duration = 1f;




    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        FacePlayer();

        CheckGround();
        ApplyGravity();


        if(jumpQueued)
        {
            jumpQueued = false;
            StartCoroutine(JumpAttack(transform.position, PlayerManager.Instance.player.transform.position));
        }




        if (health.GetCurrentHealth() <= 0 && dead == false)
        {
            dead = true;
        }
    }

    

    public IEnumerator JumpAttack(Vector3 start, Vector3 finish)
    {
        var timePast = 0f;


        //temp vars
        while (timePast < duration)
        {
            timePast += Time.deltaTime;

            var linearTime = timePast / duration; //0 to 1 time
            var heightTime = curve.Evaluate(linearTime); //value from curve

            var height = Mathf.Lerp(0f, jumpHeight, heightTime); //clamped between the max height and 0

            transform.position =
                Vector3.Lerp(start, finish, linearTime) + new Vector3(0f, height, 0f); //adding values on y axis

            yield return null;
        }
    }

   

    public override void Attack()
    {
        //Large Swiping attack
    }

    public override void Attack2()
    {
        //Jumping attack that spawns shockwave projectiles on landing

    }

    public override void Attack3()
    {
        //Charging attack
    }

   
}
