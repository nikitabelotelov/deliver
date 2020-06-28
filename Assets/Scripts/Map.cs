using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    public int Columns = 6;
    public int Rows = 6;
    public float CellWidth = 0.5f;
    public float CellHeight = 0.5f;
    //public bool[,] Map = new bool[Rows, Columns];
    public Vector2Int StartPoint = new Vector2Int(0, 0);
    public Vector2Int[] CheckPoint;
    public Text pathTextField;

    private List<List<bool>> arrMap;
    private List<List<GameObject>> buildedMap;
    public List<Vector2Int> path;
    public List<Vector2Int> points;

    public GameObject Road = null;
    public GameObject Wall = null;
    public GameObject Point = null;
    public GameObject StartPointPrefab = null;
    public GameObject PlayerDelivery = null;
    public Vector3 StartPointCoords;

    public UnityAction<List<Vector2Int>> pathBuildAction;
    void Start()
    {
        arrMap = generateMap();
        path = new List<Vector2Int>();
        path.Add(StartPoint);
        GameObject tmp;
        float x;
        float y;

        SetPoints();
        buildedMap = new List<List<GameObject>>(Rows);

        for (int row = 0; row < arrMap.Count; row++)
        {
            List<GameObject> buildedMapRow = new List<GameObject>(Rows);
            for (int column = 0; column < arrMap[row].Count; column++)
            {
                x = (column * CellWidth + this.transform.position.x) * this.transform.localScale.x;
                y = ((Rows - 1 - row) * CellHeight + this.transform.position.y) * this.transform.localScale.y;
                if (arrMap[row][column])
                {
                    tmp = Instantiate(Wall, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0), this.transform) as GameObject;
                }
                else
                {
                    if (points.IndexOf(new Vector2Int(column, row)) != -1)
                    {
                        tmp = Instantiate(Point, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0), this.transform) as GameObject;
                    }
                    else if(column == StartPoint.x && row == StartPoint.y)
                    {
                        tmp = Instantiate(StartPointPrefab, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0), this.transform) as GameObject;
                        StartPointCoords = new Vector3(x, y, 0);
                    }
                    else 
                    {
                        tmp = Instantiate(Road, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 0), this.transform) as GameObject;
                    }
                }
                buildedMapRow.Add(tmp);
            }
            buildedMap.Add(buildedMapRow);
        }
    }

    private void SetPoints()
    {
        points.Add(new Vector2Int(3, 1));
        points.Add(new Vector2Int(6, 3));
        points.Add(new Vector2Int(4, 7));
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
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = Color.white;
                path.RemoveAt(path.Count - 1);
                if (path.Contains(a))
                    buildedMap[a.y][a.x].GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (canGo(newPos))
            {
                path.Add(newPos);
                buildedMap[path.Last().y][path.Last().x].GetComponent<SpriteRenderer>().color = new Color(144f / 255, 238f / 255, 144f / 255);
            }
            pathTextField.text = "Path: " + path.Count;
        }
    }

    public void SetPathGoAction(UnityAction<List<Vector2Int>> action)
    {
        pathBuildAction += action;
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
        return res;
    }

}