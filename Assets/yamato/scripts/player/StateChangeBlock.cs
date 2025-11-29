using UnityEngine;

public class StateChangeBlock : MonoBehaviour
{
    public PlayerStateManager.State changeToState; // Inspector ‚Å‘I‚Ô

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var state = collision.GetComponent<PlayerStateManager>();
        if (state == null) return;

        switch (changeToState)
        {
            case PlayerStateManager.State.Gas:
                state.ChangeToGas();
                break;
            case PlayerStateManager.State.Liquid:
                state.ChangeToLiquid();
                break;
            case PlayerStateManager.State.Solid:
                state.ChangeToSolid();
                break;
        }
    }
}
