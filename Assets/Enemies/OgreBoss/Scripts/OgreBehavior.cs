using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OgreBehavior : BaseEnemy
{
    public Animator animator;
    public Transform projectileSpawn1;
    public Transform projectileSpawn2;
    public GameObject shockwave;
    public Transform wallCheck;
    public bool byWall;
    //public float timeBetweenAttacks = 2f;
    private float timer = 0f;
    public float chargeTimer = 0f;
    private Health health;

    private bool dead = false;

    public bool jumpQueued = false;
    private bool queueShockwave = false;
    private bool chargeQueued = false;


    public AnimationCurve curve; // The animation curve for vertical offset
    public float jumpHeight = 5f; // Height of the parabola
    public float duration = 1f;
    public float chargeAttackTimeLimit = 3;
    public float chargeSpeed = 3;



    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(!chargeQueued)
            FacePlayer2();

        if(!byWall && chargeQueued)
        {
            chargeTimer += Time.deltaTime;
            transform.position += transform.right * Time.deltaTime * chargeSpeed;

        }
        
        if(byWall && chargeQueued)
        {
            StopCharge();
        }
        else if(chargeTimer >= chargeAttackTimeLimit)
        {
            StopCharge();
        }




        if (Physics2D.Raycast(wallCheck.position, Vector2.right, 1.7f, groundLayerMask))
        {
            byWall = true;
        }
        else
        {
            byWall = false;
        }

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
            //SpawnShockwaves();
            //jumpQueued = true;
            //Attack3();
            Attack3();
        }

        if(jumpQueued)
        {
            jumpQueued = false;
            StartCoroutine(JumpAttack(transform.position, PlayerManager.Instance.player.transform.position));
            
        }

        if (queueShockwave && grounded)
        {
            SpawnShockwaves();
            queueShockwave = false;
        }

        if (chargeQueued)
        {
            //chargeQueued = false;
            //StartCoroutine(ChargeAttack());
            transform.Translate(Vector2.right * Time.deltaTime);
            
        }


        if (health.GetCurrentHealth() <= 0 && dead == false)
        {
            dead = true;
        }
    }

    private void FixedUpdate()
    {
        //if (chargeQueued)
        //{
        //    //chargeQueued = false;
        //    //StartCoroutine(ChargeAttack());
        //    transform.Translate(Vector2.right * Time.deltaTime);

        //}
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

    private void FacePlayer2()
    {
        Vector3 scale = transform.localScale;
        Vector3 target = player.transform.position;
        Quaternion rotation = transform.rotation;

        if (target.x > transform.position.x)
        {
            //scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            rotation.y = 0;
        }
        else
        {
            //scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            rotation.y = 180;
        }

        //transform.localScale = scale;
        transform.localRotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + 1.7f, wallCheck.position.y, wallCheck.position.z));
    }

}
