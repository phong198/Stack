using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public SpawnDirection SpawnDirection { get; set; }

    public GameObject m_Cube;

    [SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        //float stopvalue = GetStopvalue();
        //if (Mathf.Abs(stopvalue) > LastCube.transform.position.x || Mathf.Abs(stopvalue) > LastCube.transform.position.z);
        //SceneManager.LoadScene(0);


        if (LastCube == null)
            LastCube = m_Cube.GetComponent<MovingCube>();

        CurrentCube = this;
        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float stopvalue = GetStopvalue();
        float direction = stopvalue > 0 ? 1f : -1f;
        float max = SpawnDirection == SpawnDirection.X ? LastCube.transform.localScale.x : LastCube.transform.localScale.z;

        //no penalty zone
        if (Mathf.Abs(stopvalue) < 0.01)
        {
            transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(LastCube.transform.position.x, transform.position.y, transform.position.z);
        }
        
        //lose
        else if (Mathf.Abs(stopvalue) >= max)
        {
            SceneManager.LoadScene("GameOver");
        }
        
        //chop cube direction
        if (SpawnDirection == SpawnDirection.X)
            ChopCubeX(stopvalue, direction);
        else
        {
            ChopCubeZ(stopvalue, direction);
        }
        LastCube = this;
    }

    private float GetStopvalue()
    {
        if (SpawnDirection == SpawnDirection.X)
        return transform.position.x - LastCube.transform.position.x;
        else return transform.position.z - LastCube.transform.position.z;
    }

    private void ChopCubeX(float stopvalue, float direction)
    {
        float CurrentXSize = LastCube.transform.localScale.x - Mathf.Abs(stopvalue);
        float ChoppedXSize = transform.localScale.x - CurrentXSize;
        float CurrentXPosition = LastCube.transform.position.x + (stopvalue / 2);

        transform.localScale = new Vector3(CurrentXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(CurrentXPosition, transform.position.y, transform.position.z);

        float CurrentXEdgePosition = transform.position.x + (CurrentXSize / 2f * direction);
        float ChoppedXPosition = CurrentXEdgePosition + (ChoppedXSize / 2f * direction);

        SpawnChoppedCube(ChoppedXPosition, ChoppedXSize);
    }

    private void ChopCubeZ(float stopvalue, float direction)
    {
        float CurrentZSize = LastCube.transform.localScale.z - Mathf.Abs(stopvalue);
        float ChoppedZSize = transform.localScale.z - CurrentZSize;
        float CurrentZPosition = LastCube.transform.position.z + (stopvalue / 2);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, CurrentZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, CurrentZPosition);

        float CurrentZEdgePosition = transform.position.z + (CurrentZSize / 2f * direction);
        float ChoppedZPosition = CurrentZEdgePosition + (ChoppedZSize / 2f * direction);

        SpawnChoppedCube(ChoppedZPosition, ChoppedZSize);
    }

    private void SpawnChoppedCube(float ChoppedCubePosition, float ChoppedCubeSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (SpawnDirection == SpawnDirection.X)
        {
            cube.transform.localScale = new Vector3(ChoppedCubeSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(ChoppedCubePosition, transform.position.y, transform.position.z);
        }
        else
        {
            cube.transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y, ChoppedCubeSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, ChoppedCubePosition);
        }
        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 1);
    }


    private void Update()
    {
        if (SpawnDirection == SpawnDirection.X)
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        else
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
