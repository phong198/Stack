using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubeX : MonoBehaviour
{
    public GameObject m_Cube;
    [SerializeField]
    private MovingCube cubePrefab;
    [SerializeField]
    private SpawnDirection spawnDirection;

    public void SpawnCube()
    {
        var spawner = Instantiate(cubePrefab);

        spawner.SpawnDirection = spawnDirection;

        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != m_Cube)
        {
            float x = spawnDirection == SpawnDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float z = spawnDirection == SpawnDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;

            spawner.transform.position = new Vector3(x, MovingCube.LastCube.transform.position.y + 0.1f, z);
        }
        else spawner.transform.position = transform.position;
        }

    }

public enum SpawnDirection
{
    X,Z
}
