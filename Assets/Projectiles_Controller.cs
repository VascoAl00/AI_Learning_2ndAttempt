using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles_Controller : MonoBehaviour
{
    public GameObject capsuleProjectilePrefab; // Reference to the prefab of the capsule projectile
    public Transform projectileLauncher; // Reference to the transform of the projectile launcher object
    public float fallSpeed = 10f; // The speed at which the projectile falls towards the ground
    private bool projectileExists = false; // Flag to track if a projectile is currently active
    private float projectileTimer = 0f; // Timer to track how long the projectile has been active

    void Update()
    {
        if (!projectileExists) // Only spawn a new projectile if one is not currently active
        {
            SpawnPurple();
            projectileExists = true;
        }
        else // If a projectile is active, increment the timer and delete the projectile after one second
        {
            projectileTimer += Time.deltaTime;
            if (projectileTimer >= 2f)
            {
                Destroy(GameObject.FindGameObjectWithTag("PurpleShit"));
                projectileExists = false;
                projectileTimer = 0f;
            }
        }
    }

    void SpawnPurple()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-5f, 5f), projectileLauncher.position.y, projectileLauncher.position.z); // Random Y position along the launcher's position
        GameObject capsuleProjectile = Instantiate(capsuleProjectilePrefab, spawnPos, Quaternion.identity); // Instantiate the projectile prefab
        capsuleProjectile.tag = "PurpleShit"; // Set the tag of the projectile to easily identify it for deletion
        Rigidbody capsuleProjectileRb = capsuleProjectile.GetComponent<Rigidbody>(); // Get the rigidbody component of the projectile
        capsuleProjectileRb.velocity = Vector3.down * fallSpeed; // Set the velocity of the projectile to move downwards at the given speed
    }
}