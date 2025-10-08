using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator _animator;
    int direction=1;
    float timeInDirection;

    public float distanceTime;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        timeInDirection = distanceTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeInDirection < 0)
        {
            direction = direction * -1;
            timeInDirection = distanceTime;
        }
        Vector2 pos = transform.position;
        pos.x = pos.x + (speed * Time.deltaTime *direction);
        transform.position = pos;
        timeInDirection -= Time.deltaTime;
    }
}
