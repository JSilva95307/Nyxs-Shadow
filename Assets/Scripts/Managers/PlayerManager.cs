using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }
    public GameObject player;

    public Vector2 lungeDist;
    public float damage = 10f;


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
    }

    private void Update()
    {
        //lungeDist.x = Mathf.Lerp(lungeDist.x, 0f, 3f * Time.deltaTime);
        lungeDist.x = Mathf.Lerp(lungeDist.x, 0f, (float)(1 - Mathf.Exp(-k * Time.deltaTime)));
    }
}
