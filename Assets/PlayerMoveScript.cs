using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float speed=1;

    LastOrder LastOrder;
    public Rigidbody2D playerRB2D;

    public bool canUp = false;
    public bool canDown = false;
    public bool canLeft = false;
    public bool canRight = false;



    void Start()
    {
        playerRB2D = this.GetComponent<Rigidbody2D>();

   
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

        }
        else
        {
            canRight = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);

        }

        //Check left

         direction = new Vector3(-1, 0, 0);
         hit = Physics2D.Raycast(this.transform.position + new Vector3(0, BoxSize, 0), direction, 0.6f);
         hit2 = Physics2D.Raycast(this.transform.position + new Vector3(0, -BoxSize,  0), direction, 0.6f);

        if (hit.collider == null && hit2.collider == null)
        {
            canLeft = true;
            Debug.DrawRay(this.transform.position, direction, Color.green);

        }
        else
        {
            canLeft = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);

        }

        //Check UP

        direction = new Vector3(0, 1, 0);
        hit = Physics2D.Raycast(this.transform.position + new Vector3(BoxSize, 0,0), direction, 0.6f);
        hit2 = Physics2D.Raycast(this.transform.position + new Vector3(-BoxSize, 0,0), direction, 0.6f);

        if (hit.collider == null && hit2.collider == null)
        {
            canUp = true;
            Debug.DrawRay(this.transform.position, direction, Color.green);

        }
        else
        {
            canUp = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);

        }
        //Check Down

        direction = new Vector3(0, -1, 0);
        hit = Physics2D.Raycast(this.transform.position + new Vector3(BoxSize, 0,0), direction, 0.6f);
        hit2 = Physics2D.Raycast(this.transform.position + new Vector3( -BoxSize, 0, 0), direction, 0.6f);
     
        if (hit.collider == null && hit2.collider == null)
        {
            canDown = true;
            Debug.DrawRay(this.transform.position, direction, Color.green);

        }
        else
        {
            canDown = false;
            Debug.DrawRay(this.transform.position, direction, Color.red);

        }

    }
    private void LateUpdate()
    {

      
    }
    private void FixedUpdate()
    {
        CheckDirections();
        CheckOrder();


        if (LastOrder == LastOrder.Up && canUp)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
            GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, 90);

        }
        if (LastOrder == LastOrder.Down && canDown)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
            GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, -90);

        }
        if (LastOrder == LastOrder.Right && canRight)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * speed;
            GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, -0);

        }
        if (LastOrder == LastOrder.Left && canLeft)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * speed;
            GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, 180);

        }
    }

    public void CheckOrder()
    {
        if (Input.GetKey(KeyCode.W))
        {
            LastOrder = LastOrder.Up;

        }
        if (Input.GetKey(KeyCode.S))
        {
            LastOrder = LastOrder.Down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            LastOrder = LastOrder.Right;


        }
        if (Input.GetKey(KeyCode.A))
        {
            LastOrder = LastOrder.Left;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            PlayZoneGenerator.FoodCounter--;
            if (PlayZoneGenerator.FoodCounter <= 0)
            {
                MainServerManager.SendMQTTMess("PackManPhaseEnd");
            }
            Destroy(collision.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(DieScene());


        }

    }

    public GameObject dieEffect;
    public GameObject DefeatScreen;
    IEnumerator DieScene()
    {

        speed = 0;
        this.GetComponentInChildren<Animator>().enabled = false;
        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(item.GetComponent<EnemyMoveSC>());
            Destroy(item.GetComponent<Rigidbody2D>());

        }
        yield return new WaitForSeconds(1);
        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(item);

        }
        yield return new WaitForSeconds(1);
        {

            this.GetComponentInChildren<SpriteRenderer>().enabled = false;
            Instantiate(dieEffect, this.transform.position, this.transform.rotation);
        }
        yield return new WaitForSeconds(1);

        Instantiate(DefeatScreen, this.transform);
        yield  break;
}

}

enum LastOrder
{
    Up,Down,Right,Left


}
