using UnityEngine;

public class cameramovement : MonoBehaviour
{

    public Transform player;
    public float movespeed;

    private Vector3 Offset;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + Offset, movespeed * Time.deltaTime);
    }
}
