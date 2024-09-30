using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Light lighting;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }

    private void OnEnable()
    {
        //PlayerCollision.OnHitObstacle += ChangeColor;
        PlayerCollision.OnHitObstacle += Dark;
    }
    private void OnDisable()
    {
        //PlayerCollision.OnHitObstacle -= ChangeColor;
        PlayerCollision.OnHitObstacle -= Dark;
    }

    void Dark(Collision Collisioninfo)
    {
        lighting.color = Color.black;
       // lighting.intensity = Mathf.PingPong(Time.time, 8);
    }

}
