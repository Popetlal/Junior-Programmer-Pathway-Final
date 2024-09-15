using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;



    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (gameObject.transform.position.x < -17.5)
        {
            gameObject.transform.Rotate(0, 180, 0);
            gameObject.transform.position += Vector3.right * Time.deltaTime * speed;
        }
        else if (gameObject.transform.position.x > 11.5)
        {
            gameObject.transform.Rotate(0, 180, 0);
            gameObject.transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            Vector3 pos = gameObject.transform.position;
            pos.y = 0;
        }
    }
}
