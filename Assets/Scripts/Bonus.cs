using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Agent") == true)
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall") == true)
        {
            Destroy(this.gameObject);
        }
    }
}
