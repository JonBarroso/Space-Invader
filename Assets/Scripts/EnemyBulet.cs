using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] 
public class EnemyBulet : MonoBehaviour
{
  private Rigidbody2D myRigidbody2D;

  public float speed = 5;
    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();
    }
    private void Fire()
    {
      GetComponent<Rigidbody2D>().velocity = Vector2.down * speed; 
    }
}
