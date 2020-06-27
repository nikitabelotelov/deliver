using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGo : MonoBehaviour
{
    public GameObject player;
    private GameObject playerInstance;
    private List<Vector2Int> path;
    private float position;
    private int count = 0;
    public float cellSize = 1f;
    public float cellSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        playerInstance = Instantiate(player, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (position <= 1)
        {
            position += (Time.deltaTime * cellSpeed) / path.Count;
        }
        Vector3 newPos = calcNewPosition();
        //Debug.Log(newPos.ToString());
        playerInstance.transform.position = newPos;
    }

    private Vector3 calcNewPosition()
    {
        //Debug.Log(count);
        count++;
        float step = 1f / path.Count;
        int positionIndex = Mathf.FloorToInt(path.Count * position);
        float interpol = (position - (positionIndex * step)) / step * cellSize;
        float x = path[positionIndex].x * cellSize + interpol * (path[positionIndex + 1].x - path[positionIndex].x);
        float y = -path[positionIndex].y * cellSize - interpol * (path[positionIndex + 1].y - path[positionIndex].y);
        return new Vector3(x, y, playerInstance.transform.position.z);
    }

    public void SetPath(List<Vector2Int> path)
    {
        this.path = path;
    }
}
