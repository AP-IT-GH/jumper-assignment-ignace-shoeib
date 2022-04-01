using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed = 5f;
    void Start()
    {
        speed = Random.Range(3f, 5f);
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall") == true)
        {
            Destroy(gameObject);
        }
    }
}
