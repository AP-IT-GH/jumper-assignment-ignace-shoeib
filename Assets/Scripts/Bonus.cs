using UnityEngine;

public class Bonus : MonoBehaviour
{
    private float speed;
    private void Start()
    {
        speed = Random.Range(3f, 5f);
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") == true)
        {
            Destroy(gameObject);
        }
    }
}
