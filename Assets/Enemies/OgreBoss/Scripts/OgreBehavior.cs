using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OgreBehavior : BaseEnemy
{
    private Health health;

    private bool byWall;
    

    private float timer = 0f;
    private float chargeTimer = 0f;

    private bool dead = false;

    private bool jumpQueued = false;
    private bool queueShockwave = false;
    private bool chargeQueued = false;
    
    public Animator animator;
    public Transform wallCheck;

    [Space(10)]

    [Header("Jump Attack Vars")]
    public AnimationCurve curve; // The animation curve for vertical offset
    public float jumpHeight = 5f; // Height of the parabola
    public float duration = 1f;

    [Space(10)]

    [Header("Charge Attack Vars")]
    public float chargeAttackTimeLimit = 3;
    public float chargeSpeed = 3;

    [Space(10)]

    [Header("Shockwave Vars")]
    public Transform projectileSpawn1;
    public Transform projectileSpawn2;
    public GameObject shockwave;

    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        //Keeps the ogre from turning around mid charge
        if(!chargeQueued)
            FacePlayer();


        //Starts charge attack
        if(!byWall && chargeQueued)
        {
            chargeTimer += Time.deltaTime;
            transform.position += transform.right * Time.deltaTime * chargeSpeed;
        }
        

        //Stops the charge attack when a wall is hit or when the time limt is reached
        if(byWall && chargeQueued)
        {
            StopCharge();
            StartCoroutine(PlayerManager.Instance.cameraShake.Shake(0.1f));
        }
        else if(chargeTimer >= chargeAttackTimeLimit)
        {
            StopCharge();
        }


        //Checking if there is a wall in front
        if (transform.rotation.y == 0)
        {
            if (Physics2D.Raycast(wallCheck.position, Vector2.right, 1.7f, groundLayerMask))
            {
                byWall = true;
            }
            else
            {
                byWall = false;
            }
        }
        else
        {
            if (Physics2D.Raycast(wallCheck.position, Vector2.right, -1.7f, groundLayerMask))
            {
                byWall = true;
            }
            else
            {
                byWall = false;
            }
        }

        
        //Rotates the shockwave spawn location when the ogre rotates
        if (PlayerManager.Instance.player.transform.position.x > transform.position.x)
        {
            projectileSpawn1.transform.eulerAngles = new Vector3(0, 180, 0);
            projectileSpawn2.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            projectileSpawn1.transform.eulerAngles = new Vector3(0, 0, 0);
            projectileSpawn2.transform.eulerAngles = new Vector3(0, 180, 0);
        }


        CheckGround();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnShockwaves();
        }


        //Starts jumping attack
        if(jumpQueued)
        {
            jumpQueued = false;
            StartCoroutine(JumpAttack(transform.position, PlayerManager.Instance.player.transform.position));
            
        }


        //Spawns shockwaves
        if (queueShockwave && grounded)
        {
            SpawnShockwaves();
            queueShockwave = false;
        }



        if (health.GetCurrentHealth() <= 0 && dead == false)
        {
            dead = true;
        }
    }



    public IEnumerator JumpAttack(Vector3 start, Vector3 finish)
    {
        var timePast = 0f;


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

        queueShockwave = true;
    }

    public IEnumerator ChargeAttack()
    {
        float time = 0;
        GetComponent<AfterimageGenerator>().Play();


        while(time < chargeAttackTimeLimit && !byWall)
        {
            time += Time.deltaTime;
            transform.position += transform.right * Time.deltaTime;// * chargeSpeed;
        }

        GetComponent<AfterimageGenerator>().Stop();
        yield return null;
    }

   

    public override void Attack()
    {
        //Large Swiping attack
    }


    public override void Attack2()
    {
        //Jumping attack that spawns shockwave projectiles on landing
        jumpQueued = true;
    }


    public override void Attack3()
    {
        //Charging attack
        chargeTimer = 0f;
        chargeQueued = true;
        GetComponent<AfterimageGenerator>().Play();
        
    }


    public void StopCharge()
    {
        chargeQueued = false;
        GetComponent<AfterimageGenerator>().Stop();
        chargeTimer = 0f;
    }


    public void SpawnShockwaves()
    {
        //rotate proj 1
        Instantiate(shockwave, projectileSpawn1.transform.position, projectileSpawn1.transform.rotation);
        Instantiate(shockwave, projectileSpawn2.transform.position, projectileSpawn2.transform.rotation);
    }
}
