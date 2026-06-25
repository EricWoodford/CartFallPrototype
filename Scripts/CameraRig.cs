using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform cart;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float followSpeed = 8f;

    void LateUpdate()
    {
        if (cart == null) return;

        Vector3 targetPos = cart.position + cart.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        transform.LookAt(cart.position + Vector3.up * 1.0f);
    }
}
