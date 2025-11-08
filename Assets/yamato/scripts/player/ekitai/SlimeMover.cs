using UnityEngine;

public class SlimeMover : MonoBehaviour
{
    public SoftSlime slime;
    public float moveForce = 5f;           // ˆÚ“®—Í
    public float waveAmplitude = 2f;       // ã‰º‚Ì—h‚ê‚Ì‹­‚³
    public float waveFrequency = 2f;       // —h‚ê‚Ì‘¬‚³
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;

    private float time;

    void FixedUpdate()
    {
        if (slime == null || slime.mainBody == null) return;

        time += Time.fixedDeltaTime * waveFrequency;

        // ¶‰EˆÚ“®
        if (Input.GetKey(rightKey))
        {
            slime.mainBody.AddForce(Vector2.right * moveForce);
        }
        else if (Input.GetKey(leftKey))
        {
            slime.mainBody.AddForce(Vector2.left * moveForce);
        }

        // ŠOüƒm[ƒh‚Éã‰º—h‚ê‚ğ‰Á‚¦‚é
        foreach (Rigidbody2D node in slime.nodes)
        {
            float wave = Mathf.Sin(time + node.GetInstanceID() * 0.5f) * waveAmplitude;
            node.AddForce(Vector2.up * wave);
        }
    }
}
