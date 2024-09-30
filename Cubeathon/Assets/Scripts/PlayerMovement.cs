using UnityEngine;
using static PlayerCollision;
using static PlayerMovement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    private MeshRenderer playerRenderer;

    public delegate void Falling();
    public static event Falling OnFalling;

    //public Material playerMaterial;
    //public Material glowMaterial;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        playerRenderer = GetComponent<MeshRenderer>();
    }
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            Command moveRight = new MoveRight(rb, sidewaysForce);
            Invoker invoker = new Invoker();
            invoker.SetCommand(moveRight);
            invoker.ExecuteCommmand();
        }

        if (Input.GetKey("a"))
        {
            Command moveLeft = new MoveLeft(rb, sidewaysForce);
            Invoker invoker = new Invoker();
            invoker.SetCommand(moveLeft);
            invoker.ExecuteCommmand();
        }

        if (rb.position.y < -1f)
        {
            OnFalling?.Invoke();
            FindObjectOfType<GameManager>().EndGame(null);
        }
    }

    private void OnEnable()
    {
        //PlayerCollision.OnHitObstacle += ChangeColor;
        PlayerCollision.OnHitObstacle += Glow;
        OnFalling += Fall;
    }
    private void OnDisable()
    {
        //PlayerCollision.OnHitObstacle -= ChangeColor;
        PlayerCollision.OnHitObstacle -= Glow;
        OnFalling -= Fall;
    }
   // void ChangeColor(Collision Collisioninfo)
   // {
  //      playerRenderer.material.color = new Color(.02f, .91f, .5f, .9f);
  //  }

    void Glow(Collision Collisioninfo)
    {
        playerRenderer.material = gameManager.glowMaterial;
    }

    void Fall()
    {
        this.gameObject.transform.localScale = new Vector3(.02f, 1.5f, .02f);
    }
}