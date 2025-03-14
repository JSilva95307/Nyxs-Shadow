using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OgreBehavior : BaseEnemy
{
    public Animator animator;
    public Transform projectileSpawn1;
    public Transform projectileSpawn2;
    public GameObject shockwave;

    //public float timeBetweenAttacks = 2f;
    private float timer = 0f;
    private Health health;

    private bool dead = false;

    public bool jumpQueued = false;
    private bool queueShockwave = false;


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

        if(PlayerManager.Instance.player.transform.position.x > transform.position.x)
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
            jumpQueued = true;
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

        queueShockwave = true;
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


    public void SpawnShockwaves()
    {
        //rotate proj 1
        Instantiate(shockwave, projectileSpawn1.transform.position, projectileSpawn1.transform.rotation);
        Instantiate(shockwave, projectileSpawn2.transform.position, projectileSpawn2.transform.rotation);
    }
   
}
