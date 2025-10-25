
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SoftBodyRenderer : MonoBehaviour
{
    public Rigidbody2D[] nodes;  // Nodeを全部入れる場所
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        // Nodeの数＋1（最後で最初につなぐ）
        line.positionCount = nodes.Length + 1;

        // 線の太さ
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        // 線をループさせる
        line.loop = true;
        line.useWorldSpace = true;
    }

    void Update()
    {
        // Nodeの位置を順番に線で結ぶ
        for (int i = 0; i < nodes.Length; i++)
        {
            line.SetPosition(i, nodes[i].position);
        }

        // 最後に最初のNodeに戻る
        line.SetPosition(nodes.Length, nodes[0].position);
    }
}