using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSC : MonoBehaviour
{

    public bool canUp = false;
    public bool canDown = false;
    public bool canLeft = false;
    public bool canRight = false;

    public float speed = 3;
    bool isUp = false;
    bool isDown = false;
    bool isRight = false;
    bool isLeft = false;

   
    void Start()
    {
        
    }

    public void ChangeDirection()
    {
       for (int i = 0; i<100; i++)
        
        {


            this.transform.rotation = new Quaternion(0, 0, 0,0);
            int c = Random.Range(0, 4);
            print(c);
            if (c == 0)
            {
                if (canUp && !isDown)
                {
                    {
                        isUp = true;
                        isRight = false;
                        isDown = false;
                        isLeft = false;
                        break;
                    }

                }
            }
            if (c == 1)
            {
                if (canRight && !isLeft)
                {
                    {
                        isUp = false;
                        isRight = true;
                        isDown = false;
                        isLeft = false;
                        break;
                    }

                }
            }
            if (c == 2)
            {
                if (canDown && !isUp)
                {
                    {
                        isUp = false;
                        isRight = false;
                        isDown = true;
                        isLeft = false;
                        break;
                    }

                }
            }
            if (c == 3)
            {
                if (canLeft && !isRight)
                {
                    {
                        isUp = false;
                        isRight = false;
                        isDown = false;
                        isLeft = true;
                        this.transform.rotation = new Quaternion(0, 180, 0, 0);
                        break;
                    }

                }
            }
        

        }
    }

    // Update is called once per frame
    void Update()
    {

        CheckDirections();



    }

    private void FixedUpdate()
    {
      
        if (isUp) this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) *speed;
        if(isRight) this.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * speed;
        if (isDown) this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
        if (isLeft) this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * speed;
    }
    public void CheckDirections()
    {

        float BoxSize = this.GetComponent<CircleCollider2D>().radius * this.transform.localScale.x;

        //Check Right
        Vector3 direction = new Vector3(1, 0, 0);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position + new Vector3(0, BoxSize, 0), direction, 0.6f);
        RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position + new Vector3(0, -BoxSize, 0), direction, 0.6f);

        if (hit.collider == null && hit2.collider == null)
        {
            canRight = true;
            Debug.DrawRay(this.transform.position, direction, Color.green);
            ChangeDirection();
        }
        else
        {
            canRight = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);

        }

        //Check left

        direction = new Vector3(-1, 0, 0);
        hit = Physics2D.Raycast(this.transform.position + new Vector3(0, BoxSize, 0), direction, 0.6f);
        hit2 = Physics2D.Raycast(this.transform.position + new Vector3(0, -BoxSize, 0), direction, 0.6f);

        if (hit.collider == null && hit2.collider == null)
        {
            canLeft = true;
            Debug.DrawRay(this.transform.position, direction, Color.green);
            ChangeDirection();
        }
        else
        {
            canLeft = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);

        }

        //Check UP

        direction = new Vector3(0, 1, 0);
        hit = Physics2D.Raycast(this.transform.position + new Vector3(BoxSize, 0, 0), direction, 0.6f);
        hit2 = Physics2D.Raycast(this.transform.position + new Vector3(-BoxSize, 0, 0), direction, 0.6f);

        if (hit.collider == null && hit2.collider == null)
        {
            canUp = true;
            Debug.DrawRay(this.transform.position, direction, Color.green);
            ChangeDirection();
        }
        else
        {
            canUp = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);
           
        }
        //Check Down

        direction = new Vector3(0, -1, 0);
        hit = Physics2D.Raycast(this.transform.position + new Vector3(BoxSize, 0, 0), direction, 0.6f);
        hit2 = Physics2D.Raycast(this.transform.position + new Vector3(-BoxSize, 0, 0), direction, 0.6f);

        if (hit.collider == null && hit2.collider == null)
        {
            canDown = true;
            Debug.DrawRay(this.transform.position, direction, Color.green);
            ChangeDirection();
        }
        else
        {
            canDown = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);

        }

    }

}
