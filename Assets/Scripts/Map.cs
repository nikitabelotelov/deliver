using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    public int Columns = 3;
    public int Rows = 3;
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
                x = column * CellWidth;
                z = (Rows - 1 - row) * CellHeight;
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
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            var newPos = new Vector2Int(path.Last().x, path.Last().y - 1);
            if(path.Count > 1 && newPos == path[path.Count-2]){
                Debug.Log("Twise");
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.yellow;
                path.RemoveAt(path.Count-1);
            }
            else if(path.Last().y > 0 && !IsWall(newPos))
            {
                path.Add(newPos);
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var newPos = new Vector2Int(path.Last().x, path.Last().y + 1);
            if(path.Count > 1 && newPos == path[path.Count-2]){
                Debug.Log("Twise");
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.yellow;
                path.RemoveAt(path.Count-1);
            }
            else if (path.Last().y < Columns - 1 && !IsWall(newPos))
            {
                path.Add(newPos);
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var newPos = new Vector2Int(path.Last().x - 1, path.Last().y);
            if(path.Count > 1 && newPos == path[path.Count-2]){
                Debug.Log("Twise");
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.yellow;
                path.RemoveAt(path.Count-1);
            }
            else if (path.Last().x > 0 && !IsWall(newPos))
            {
                path.Add(newPos);
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var newPos = new Vector2Int(path.Last().x + 1, path.Last().y);
            if(path.Count > 1 && newPos == path[path.Count-2]){
                Debug.Log("Twise");
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.yellow;
                path.RemoveAt(path.Count-1);
            }
            else if (path.Last().x < Rows - 1 && !IsWall(newPos))
            {
                path.Add(newPos);
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private bool IsWall(Vector2Int next)
    {// DD: поменяла у и x местами
        if(arrMap[next.y][next.x])
        {
            return true;
        }
        return false;
    }

    private List<List<bool>> generateMap()
    {
        List<List<bool>> res = new List<List<bool>>(Rows);
        for(int i = 0; i < Rows; i++)
        {
            List<bool> row = new List<bool>(Columns);
            for(int j = 0; j < Columns; j++)
            {
                row.Add(Random.value > 0.5);
            }
            res.Add(row);
        }
        return res;
    }

}