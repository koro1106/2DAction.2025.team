using UnityEngine;

public class SeesawTrigger : MonoBehaviour
{
    public SeesawManager manager;
    public bool isABox;
    // プレイヤーが箱に乗ったとき
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isABox) manager.SetAOccpied(true);
            else manager.SetBOccpied(true);
            Debug.Log(isABox);
        }
    }

    // プレイヤーが箱から降りたとき
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isABox) manager.SetAOccpied(false);
            else manager.SetBOccpied(false);
        }
    }
}
