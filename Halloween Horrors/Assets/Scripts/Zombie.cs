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
    public int health;


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
            _animator.SetInteger("Direction", direction);
            timeInDirection = distanceTime;
        }
        Vector2 pos = transform.position;
        pos.x = pos.x + (speed * Time.deltaTime *direction);
        transform.position = pos;
        timeInDirection -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerProjectile")
        {
            health--;
            Debug.Log(health);
        }
    }
}
