using UnityEngine;

public class ProjecTile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out EnemyController2 e))
            e.Fix();
        
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);
        Destroy(gameObject);
    }
    
    private void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
}
