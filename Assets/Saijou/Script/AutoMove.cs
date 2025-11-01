using UnityEngine;

public class AutoMove : MonoBehaviour
{
    private Camera cam;
    public float moveSpeed = 10.0f;
    public float frequency = 1f;//syuuki
    private float timeOffset;//randamna taiminguno zure
    private Vector3 startPos;
    private Rigidbody2D rb;

    [SerializeField] private GameObject player;

    private Vector3 oldPos;//maeno freamnoiti
    private Vector3 deltaPos;//idouryou
    void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
        timeOffset = Random.Range(0f, Mathf.PI * 2f);
        rb = GetComponent<Rigidbody2D>();
        oldPos = transform.position;
    }


    //Auto Move Grpound
    void FixedUpdate()
    {
        Vector3 newPos;
      if(cam.transform.position.x >= -470)
      {
         if (CompareTag("UpDownGround"))
         {
            newPos = 
                new Vector2(startPos.x, startPos.y + Mathf.Sin(Time.time * frequency + timeOffset) * moveSpeed);
         }
         else
         {
            newPos =
                new Vector2(startPos.x + Mathf.Sin(Time.time * frequency + timeOffset) * moveSpeed, startPos.y);
         }
         rb.MovePosition(newPos);

         deltaPos = newPos - oldPos;
         oldPos = newPos;
      }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position += deltaPos;
        }
    }
}
