using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class StableBlob : MonoBehaviour
{
    [Header("Blob Settings")]
    public GameObject particlePrefab;
    public int particleCount = 24;
    public float radius = 1.5f;
    public float springFrequency = 6f;
    public float springDamping = 0.7f;
    public float moveSpeed = 5f;

    private List<Rigidbody2D> particles = new List<Rigidbody2D>();
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    private Vector2 centerPosition;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        lineRenderer.loop = true;

        // �������S�ʒu
        centerPosition = transform.position;

        CreateBlob();
    }

    void Update()
    {
        // ���S�ʒu�𒼐ڍX�V�iKinematic �I�Ɂj
        float move = Input.GetAxis("Horizontal");
        centerPosition += Vector2.right * move * moveSpeed * Time.deltaTime;

        // ���q�𒆐S�ɖ߂���
        foreach (var p in particles)
        {
            Vector2 dir = centerPosition - (Vector2)p.position;
            p.AddForce(dir * 3f); // ���S�Ɉ����߂���
        }

        UpdateLineAndCollider();
    }

    void CreateBlob()
    {
        for (int i = 0; i < particleCount; i++)
        {
            float angle = i * Mathf.PI * 2f / particleCount;
            Vector2 pos = centerPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            GameObject p = Instantiate(particlePrefab, pos, Quaternion.identity);

            Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
            if (rb == null) rb = p.AddComponent<Rigidbody2D>();
            rb.mass = 0.3f;
            rb.drag = 0.5f;
            rb.angularDrag = 0.5f;
            rb.gravityScale = 0f;

            // ���S�Ƃ̃X�v�����O
            SpringJoint2D joint = p.AddComponent<SpringJoint2D>();
            joint.connectedBody = null; // ���S Rigidbody �Ȃ�
            joint.autoConfigureDistance = false;
            joint.distance = radius;
            joint.frequency = springFrequency;
            joint.dampingRatio = springDamping;

            particles.Add(rb);
        }
    }

    void UpdateLineAndCollider()
    {
        if (particles.Count < 2) return;

        Vector3[] points = new Vector3[particles.Count + 1];
        for (int i = 0; i < particles.Count; i++)
            points[i] = particles[i].position;
        points[particles.Count] = particles[0].position;

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);

        Vector2[] colliderPoints = new Vector2[particles.Count + 1];
        for (int i = 0; i < particles.Count; i++)
            colliderPoints[i] = particles[i].position;
        colliderPoints[particles.Count] = particles[0].position;

        edgeCollider.points = colliderPoints;
    }
}
