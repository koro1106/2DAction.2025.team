using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentSavePoint;
    // Start is called before the first frame update
    void Start()
    {
        //STAGE開始地点をセーブポイントにする
        currentSavePoint = transform.position;
    }

    public void SetSavePoint (Vector3 newSavePoint)
    {
        currentSavePoint = newSavePoint;
        Debug.Log("新しいセーブポイント確保: " + newSavePoint);

    }
    // Update is called once per frame
   public  void Die()
    {
        // HP処理や演出が終わった後に呼ぶ
        transform.position = currentSavePoint;
    }
}
