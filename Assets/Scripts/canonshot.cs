using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonshot : MonoBehaviour
{
    // Start is called before the first frame update
    public float _speed; //Tốc độ bay của đạn
    void Start()
    {
        _speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed *Time.deltaTime * Vector3.left);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (other.transform.DotTest(transform, Vector2.down)) {
                Flatten();
            } else {
                player.Hit();
            }
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

}
