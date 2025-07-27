using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform playerA;
    public Transform playerB;

    void LateUpdate()
    {
        Vector3 m = (playerA.position + playerB.position) / 2f;
        //transform.position = new Vector3(m.x, m.y, transform.position.z);    
        transform.position = new Vector3(transform.position.x, m.y, transform.position.z);    
    }
}
