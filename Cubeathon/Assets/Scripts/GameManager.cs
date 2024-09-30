using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public GameObject player;
    bool replay = false;
    float playbackTime;
    public Renderer playerRenderer;
    //public  Material replayMaterial;
    public Material glowMaterial;


    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;
        playerRenderer = player.GetComponent<MeshRenderer>();



        if (CommandLog.logs.Count > 0)
        {
            replay = true;
            playerRenderer.material.color = new Color(.09f, .31f, .3f, .7f);
            playbackTime = Time.timeSinceLevelLoad;
        }
    }

    void FixedUpdate()
    {
        if (replay)
        {
            Replay();
        }
    }
    public void CompleteLevel ()
    {
        completeLevelUI.SetActive(true);
    }
    public void EndGame(Collision collisionInfo)
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Replay()
    {
        if (CommandLog.logs.Count == 0) 
        {
            return;
        }

        Command command = CommandLog.logs.Peek();

        if (Time.timeSinceLevelLoad >= command.time)
        {
            command = CommandLog.logs.Dequeue();
            command._player = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExecuteCommmand();
        }
    }

    private void OnEnable()
    {
        PlayerCollision.OnHitObstacle += EndGame;
    }
    private void OnDisable()
    {
        PlayerCollision.OnHitObstacle -= EndGame;
    }
}
