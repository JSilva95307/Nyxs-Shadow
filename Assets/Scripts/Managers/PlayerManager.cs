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
}
