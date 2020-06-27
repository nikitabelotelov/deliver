using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    public int Columns = 6;
    public int Rows = 6;
    public float CellWidth = 1;
    public float CellHeight = 1;
    //public bool[,] Map = new bool[Rows, Columns];
    public Vector2Int StartPoint = new Vector2Int(0, 0);
    public Vector2Int[] CheckPoint;

    private List<List<bool>> arrMap;
    private List<List<GameObject>> buildedMap;
    public List<Vector2Int> path;

    public GameObject Road = null;
    public GameObject PlayerDelivery = null;

    float _vx;
    float _vy;

    private float cellSize = 0.3f;

    void Start()
    {
        arrMap = generateMap();
        path = new List<Vector2Int>();
        path.Add(StartPoint);
        GameObject tmp;
        float x;
        float z;

        buildedMap = new List<List<GameObject>>(Rows);

        for (int row = 0; row < arrMap.Count; row++)
        {
            List<GameObject> buildedMapRow = new List<GameObject>(Rows);
            for (int column = 0; column < arrMap[row].Count; column++)
            {
                x = column * CellWidth - (CellWidth * Columns / 2);
                z = (Rows - 1 - row) * CellHeight - (CellHeight * Rows / 2);
                tmp = Instantiate(Road, new Vector3(x, z, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                if (arrMap[row][column])
                    tmp.GetComponent<SpriteRenderer>().color = Color.red;
                else
                    tmp.GetComponent<SpriteRenderer>().color = Color.yellow;
                buildedMapRow.Add(tmp);
            }
            buildedMap.Add(buildedMapRow);
        }
        buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.green;
    }

    void Update()
    {
        Vector2Int newPos = new Vector2Int(0, 0);
        bool hasKeyDown = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            hasKeyDown = true;
            newPos = new Vector2Int(path.Last().x, path.Last().y - 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            hasKeyDown = true;
            newPos = new Vector2Int(path.Last().x, path.Last().y + 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            hasKeyDown = true;
            newPos = new Vector2Int(path.Last().x - 1, path.Last().y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            hasKeyDown = true;
            newPos = new Vector2Int(path.Last().x + 1, path.Last().y);
        }
        if (hasKeyDown)
        {
            if (isBackwardStep(newPos))
            {
                Vector2Int a = new Vector2Int(path.Last().x, path.Last().y);
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.yellow;
                path.RemoveAt(path.Count - 1);
                if(path.Contains(a))
                    buildedMap[a.y][a.x].GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (canGo(newPos))
            {
                path.Add(newPos);
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private bool isBackwardStep(Vector2Int next)
    {
        if (path.Count > 1 && next == path[path.Count - 2])
        {
            return true;
        }
        return false;
    }

    private bool canGo(Vector2Int next)
    {
        if (next.x < Columns && next.x >= 0 && next.y < Rows && next.y >= 0)
        {
            if (!IsWall(next))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsWall(Vector2Int next)
    {// DD: поменяла у и x местами
        if (arrMap[next.y][next.x])
        {
            return true;
        }
        return false;
    }

    private List<List<bool>> generateMap()
    {
        List<List<bool>> res = new List<List<bool>>(Rows);
        for (int i = 0; i < Rows; i++)
        {
            List<bool> row = new List<bool>(Columns);
            for (int j = 0; j < Columns; j++)
            {
                row.Add((i % 2 != 1) && (j % 2 != 1));
            }
            res.Add(row);
        }
        Debug.Log("ROWS: " + Rows);
        return res;
    }

}