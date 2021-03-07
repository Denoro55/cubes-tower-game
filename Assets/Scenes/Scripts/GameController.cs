using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cubePosPrefab, cubePrefab, allCubes;
    private GameObject cubeToPlace;
    private Vector3 cubeToPlacePos;
    private List<Vector3> cubesPositions = new List<Vector3>{
        new Vector3(0, 2f, 0),
    };
    public float interval = 1f;
    private Rigidbody allCubesRb;
    private Coroutine spawnCubePosition;
    private bool _isLose = false;
    void Start()
    {
        allCubesRb = allCubes.GetComponent<Rigidbody>();
        cubeToPlacePos = new Vector3(0, 2f, 0);
        cubeToPlace = Instantiate(cubePosPrefab, new Vector3(0, 2f, 0), Quaternion.identity);
        spawnCubePosition = StartCoroutine(showCubePlace());
    }

    IEnumerator showCubePlace() {
        while (true) {
            shawnCubePosition();
            yield return new WaitForSeconds(interval);
        }
    }

    private void shawnCubePosition() {
        List<Vector3> emptyPositions = new List<Vector3>();

        float x = cubeToPlacePos.x;
        float y = cubeToPlacePos.y;
        float z = cubeToPlacePos.z;

        Vector3 cubePos = cubeToPlace.transform.position;

        List<Vector3> positions = new List<Vector3>{
            new Vector3(x + 1, y, z),
            new Vector3(x - 1, y, z),
            new Vector3(x, y, z - 1),
            new Vector3(x, y, z + 1),
            new Vector3(x, y + 1, z),
        };

        foreach (Vector3 pos in positions) {
            bool posIsEqual = cubePos.x == pos.x && cubePos.y == pos.y && cubePos.z == pos.z;
            if (isEmptyPosition(pos) && !posIsEqual) {
                emptyPositions.Add(pos);
                cubeToPlace.transform.position = pos;
            }
        }

        cubeToPlace.transform.position = emptyPositions[Random.Range(0, emptyPositions.Count)];
    }

    private bool isEmptyPosition(Vector3 checkPos) {
        foreach (Vector3 pos in cubesPositions) {
            if (pos.x == checkPos.x && pos.y == checkPos.y && pos.z == checkPos.z) {
                return false;
            }
        }

        return true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cubeToPlace != null) {
            Vector3 position = cubeToPlace.transform.position;
            GameObject newCube = Instantiate(cubePrefab, position, Quaternion.identity);
            newCube.transform.SetParent(allCubes.transform);
            cubeToPlacePos = position;
            cubesPositions.Add(position);

            allCubesRb.isKinematic = true;
            allCubesRb.isKinematic = false;

            shawnCubePosition();
        }

        if (!_isLose && allCubesRb.velocity.magnitude > 0.1f) {
            _isLose = true;
            StopCoroutine(spawnCubePosition);
            Destroy(cubeToPlace.gameObject);
        }
    }
}

struct Coord {
    public Vector3 pos;
    public int axis;

    public Coord(Vector3 pos, int axis) {
        this.pos = pos;
        this.axis = axis;
    }
}