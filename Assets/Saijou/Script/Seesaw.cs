using UnityEngine;

public class Seesaw : MonoBehaviour
{
    public Rigidbody2D rbA;
    public Rigidbody2D rbB;
    public float force = 5f;

    void Start()
    {
        if (rbA == null) rbA = GameObject.Find("GroundA").GetComponent<Rigidbody2D>();
        if (rbB == null) rbB = GameObject.Find("GroundB").GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // A‚ğ‰º‚É‰Ÿ‚·—Í‚ğ‰Á‚¦‚é
            rbA.AddForce(Vector2.down * force, ForceMode2D.Impulse);
            // B‚ÍA‚ÆŒq‚ª‚Á‚Ä‚¢‚é‚Ì‚Å©“®‚Åã‚ª‚é
        }
    }
}
