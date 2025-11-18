using UnityEngine;


public class SeesawManager : MonoBehaviour
{
    public float moveDistance = 1f;  // ã‰º‚ÌˆÚ“®‹——£
    public float moveSpeed = 2f;     // “®‚­‘¬‚³
    public Rigidbody2D rbA;
    public Rigidbody2D rbB;

    private Vector2 startPosA;  // ‰ŠúˆÊ’u
    private Vector2 startPosB;

    private bool isAOccupied = false; // ã‚Éæ‚Á‚Ä‚é‚©
    private bool isBOccupied = false; 

    void Start()
    {
        // Å‰‚ÌˆÊ’u‚ð‹L˜^
        startPosA = rbA.position;
        startPosB = rbB.position;
    }
    private void FixedUpdate()
    {
        Vector2 targetPosA = startPosA;
        Vector2 targetPosB = startPosB;

        if (isAOccupied) // A‚Éæ‚Á‚½‚ç
        {
            // A‚Í‰ºŒÀ‚Ü‚Å‰º‚°‚é
            targetPosA.y = startPosA.y - moveDistance;
            // ”½‘Î‚ÍãŒÀ‚Ü‚Åã‚°‚é
            targetPosB.y = startPosB.y + moveDistance;
        }
        else if(isBOccupied) // B‚Éæ‚Á‚½‚ç
        {
            // B‚Í‰ºŒÀ‚Ü‚Å‰º‚°‚é
            targetPosB.y = startPosB.y - moveDistance;
            // ”½‘Î‚ÍãŒÀ‚Ü‚Åã‚°‚é
            targetPosA.y = startPosA.y + moveDistance;
        }

        rbA.MovePosition(Vector2.MoveTowards(rbA.position, targetPosA, moveSpeed * Time.fixedDeltaTime));
        rbB.MovePosition(Vector2.MoveTowards(rbB.position, targetPosB, moveSpeed * Time.fixedDeltaTime));
    }

    public void SetAOccpied(bool occupied) => isAOccupied = occupied;
    public void SetBOccpied(bool occupied) => isBOccupied = occupied;
   
}
