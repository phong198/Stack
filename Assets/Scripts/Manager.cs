using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private SpawnCubeX[] spawners;
    private int spawnerIndex;
    private SpawnCubeX currentSpawner;

    private void Awake()
    {
        spawners = FindObjectsOfType<SpawnCubeX>();
    }
    public GameObject m_MainCamera;

    public static event Action OnCubeSpawned; 

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];
            currentSpawner.SpawnCube();

            OnCubeSpawned();
            
            m_MainCamera.transform.position = new Vector3(
                m_MainCamera.transform.position.x, 
                m_MainCamera.transform.position.y + 0.1f, 
                m_MainCamera.transform.position.z);
        }
    }
}
