using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathGo : MonoBehaviour
{
    public GameObject player;
    private GameObject playerInstance;
    private List<Vector2Int> path;
    private UnityAction pathEndAction;
    private float position;
    private int count = 0;
    private Vector3 StartPoint;
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
        } else
        {
            pathEndAction();
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
        return new Vector3(x + this.transform.position.x, y + this.transform.position.y, playerInstance.transform.position.z);
    }

    public void SetPath(List<Vector2Int> path)
    {
        this.path = path;
    }

    public void SetStartPoint(Vector3 coords)
    {
        this.StartPoint = coords;
    }

    public void SetPathEndAction(UnityAction action)
    {
        pathEndAction += action;
    }

    public void OnDestroy()
    {
        GameObject.Destroy(playerInstance);
    }
}
