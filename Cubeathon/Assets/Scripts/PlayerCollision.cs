using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    public delegate void HitObstacle(Collision collisionInfo);
    public static event HitObstacle OnHitObstacle;
    void OnCollisionEnter (Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            OnHitObstacle?.Invoke(collisionInfo);

            movement.enabled = false;
            FindAnyObjectByType<GameManager>().EndGame(collisionInfo);
        }
    }
}
