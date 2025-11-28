using UnityEngine;

public class SoftSlime : MonoBehaviour
{
    [Header("ÉmÅ[Éhê›íË")]
    public int nodeCount = 20;
    public float radius = 0.05f;
    public float nodeMass = 0.05f;

    [Header("SpringJointê›íË")]
    public float distance = 0.1f;
    public float frequency = 5f;
    public float damping = 0.8f;

    [Header("å©ÇΩñ⁄")]
    public Color nodeColor = Color.green;

    [HideInInspector]
    public Rigidbody2D mainBody;
    [HideInInspector]
    public Rigidbody2D[] nodes;

    void Start()
    {
        nodes = new Rigidbody2D[nodeCount];

        GameObject main = new GameObject("MainBody");
        main.transform.parent = transform;
        main.transform.localPosition = Vector3.zero;
        main.transform.localScale = Vector3.one * 0.5f;

        mainBody = main.AddComponent<Rigidbody2D>();
        mainBody.mass = 0.5f;

        CircleCollider2D mainCol = main.AddComponent<CircleCollider2D>();
        mainCol.radius = radius;

        SpriteRenderer mainSr = main.AddComponent<SpriteRenderer>();
        mainSr.sprite = GenerateCircleSprite();
        mainSr.color = nodeColor;
        mainSr.sortingOrder = 1;

        for (int i = 0; i < nodeCount; i++)
        {
            float angle = (360f / nodeCount) * i;
            Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * distance;

            GameObject node = new GameObject("Node_" + i);
            node.transform.parent = transform;
            node.transform.localPosition = pos;
            node.transform.localScale = Vector3.one * 0.5f;

            Rigidbody2D rb = node.AddComponent<Rigidbody2D>();
            rb.mass = nodeMass;

            CircleCollider2D col = node.AddComponent<CircleCollider2D>();
            col.radius = radius;

            SpriteRenderer sr = node.AddComponent<SpriteRenderer>();
            sr.sprite = GenerateCircleSprite();
            sr.color = nodeColor;
            sr.sortingOrder = 1;

            SpringJoint2D sj = node.AddComponent<SpringJoint2D>();
            sj.connectedBody = mainBody;
            sj.autoConfigureDistance = false;
            sj.distance = distance;
            sj.frequency = frequency;
            sj.dampingRatio = damping;

            nodes[i] = rb;
        }

        for (int i = 0; i < nodeCount; i++)
        {
            int next = (i + 1) % nodeCount;
            SpringJoint2D sj = nodes[i].gameObject.AddComponent<SpringJoint2D>();
            sj.connectedBody = nodes[next];
            sj.autoConfigureDistance = false;
            sj.distance = distance;
            sj.frequency = frequency * 1.5f;
            sj.dampingRatio = damping;
        }
    }

    private Sprite GenerateCircleSprite()
    {
        Texture2D tex = new Texture2D(32, 32);
        Color[] pixels = new Color[32 * 32];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.white;
        tex.SetPixels(pixels);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 32, 32), Vector2.one * 0.5f, 128f);
    }
}
