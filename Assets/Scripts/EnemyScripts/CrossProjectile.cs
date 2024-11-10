using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossProjectile : MonoBehaviour
{
    // The tag of the object this game object will attach to upon collision.
    private string targetTag = "Vehicle";
    private bool hasHitVehicle = false;
    public float damage = 15.0f;
    [SerializeField] Collider collider;

    public void InitializeProjectile(Vector3 position)
    {
        Debug.Log("InitializeProjectile");
        transform.LookAt(position);
        iTween.MoveTo(gameObject, iTween.Hash("x", position.x, "y", position.y, "z", position.z, "time", 1.0f, "islocal", false, "easetype", iTween.EaseType.easeInCubic, "onupdatetarget", gameObject, "onupdate", "UpdateCounter"));
        StartCoroutine(DestroyProjectile());
    }
    void UpdateCounter()
    {
        if (hasHitVehicle)
        {
            iTween.Stop(gameObject);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the specified tag
        if (other.gameObject.CompareTag(targetTag))
        {
            Debug.Log("The vehicle has been hit");
            // Attach this game object to the collided object
            hasHitVehicle = true;
            collider.enabled = false;
            AttachToObject(other.gameObject);
            Destroy(gameObject, 15);
        }
    }*/


    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("The vehicle has been hit");
            // Attach this game object to the collided object
            hasHitVehicle = true;
            collider.enabled = false;
            AttachToObject(collision.gameObject);
            collision.gameObject.GetComponent<Vehicle>().DealDamageToVehicle(damage);
            Destroy(gameObject, 15);
        }
    }

    private void AttachToObject(GameObject targetObject)
    {
        // Store the world position, rotation, and scale of the game object
        Vector3 worldPosition = transform.position;
        Quaternion worldRotation = transform.rotation;
        Vector3 worldScale = transform.lossyScale;

        // Set this object's parent to the target object
        transform.SetParent(targetObject.transform);

        // Restore the world position, rotation, and scale
        transform.position = worldPosition;
        transform.rotation = worldRotation;

        // Note: Scaling needs special handling because lossyScale includes the parent's scale
        // To preserve the exact world scale, you might need to calculate it based on the parent
        // Here we're assuming uniform scale, but you may need more complex logic depending on your needs
        transform.localScale = new Vector3(
            worldScale.x / targetObject.transform.lossyScale.x,
            worldScale.y / targetObject.transform.lossyScale.y,
            worldScale.z / targetObject.transform.lossyScale.z
        );
    }

    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(6.0f);
        if (!hasHitVehicle)
        {
            Destroy(gameObject);
        }
    }
}
