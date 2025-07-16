using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }
    public GameObject player;
    public Camera playerCamera;
    public PlayerController _playerController;
    public CameraShake cameraShake;
    public PlayerHUD playerHUD;

    public Vector2 lungeDist;
    public float damage;
    public float stagger;

    public float recentEnemyMaxHealth;
    public float recentEnemyHealth;
    public float recentEnemyMaxStagger;
    public float recentEnemyStagger;


    //Player variables to save
    public float health;

    public float k = 3; //k = excitation constant (lower k (~1-2) for sluggish movement, higher k (~10) for move snappish behavior)

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerController = player.GetComponent<PlayerController>();
        playerCamera = Camera.main; //_playerController.GetComponent<Camera>();
        cameraShake = playerCamera.GetComponent<CameraShake>();
        playerHUD = _playerController.playerHUD;
    }

    private void Update()
    {
        //lungeDist.x = Mathf.Lerp(lungeDist.x, 0f, 3f * Time.deltaTime);
        lungeDist.y = Mathf.Lerp(lungeDist.y, 0f, (float)(1 - Mathf.Exp(-k * Time.deltaTime)));
        Debug.Log(lungeDist.y);
        lungeDist.x = Mathf.Lerp(lungeDist.x, 0f, (float)(1 - Mathf.Exp(-k * Time.deltaTime)));
    }

    public void SetEnemyHealth(float health)
    {
        playerHUD.SetEnemyHealth(health);
    }

    public void SetEnemyStagger(float stagger)
    {
        playerHUD.SetEnemyStagger(stagger);
    }

    public void SetEnemyMaxHealth(float health)
    {
        playerHUD.SetEnemyMaxHealth(health);
    }

    public void SetEnemyMaxStagger(float stagger)
    {
        playerHUD.SetEnemyMaxStagger(stagger);
    }
}
