using UnityEngine;
using UnityEngine.Playables;

public class PlayerStateManager : MonoBehaviour
{

    public PlayerState currentState = PlayerState.Liquid;

    // 液体・気体・固体のオブジェクト（Meshやスプライト）

    public GameObject liquidForm;

    public GameObject gasForm;

    public GameObject solidForm;

    void Start()

    {

        UpdateStateVisual();
    }

    public void ChangeState(PlayerState newState)

    {

        currentState = newState;

        UpdateStateVisual();

    }

    private void UpdateStateVisual()
    {

        liquidForm.SetActive(currentState == PlayerState.Liquid);

        gasForm.SetActive(currentState == PlayerState.Gas);

        solidForm.SetActive(currentState == PlayerState.Solid);

    }

}

