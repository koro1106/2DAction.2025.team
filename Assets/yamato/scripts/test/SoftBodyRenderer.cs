
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SoftBodyRenderer : MonoBehaviour
{
    public Rigidbody2D[] nodes;  // Node��S�������ꏊ
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        // Node�̐��{1�i�Ō�ōŏ��ɂȂ��j
        line.positionCount = nodes.Length + 1;

        // ���̑���
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        // �������[�v������
        line.loop = true;
        line.useWorldSpace = true;
    }

    void Update()
    {
        // Node�̈ʒu�����Ԃɐ��Ō���
        for (int i = 0; i < nodes.Length; i++)
        {
            line.SetPosition(i, nodes[i].position);
        }

        // �Ō�ɍŏ���Node�ɖ߂�
        line.SetPosition(nodes.Length, nodes[0].position);
    }
}