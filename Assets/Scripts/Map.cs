using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour {
    public int Columns = 3;
    public int Rows = 3;
    public float CellWidth = 1;
    public float CellHeight = 1;
    //public bool[,] Map = new bool[Rows, Columns];
    public Vector2 StartPoint = new Vector2(0,0);
    public Vector2[] CheckPoint;
    
    public bool[,] arrMap = new bool[3, 3] { { true, false, false }, { true, true, true }, { true, false, false}};

	public GameObject Road = null;
    public GameObject PlayerDelivery = null;

    float _vx;
	float _vy;

    void Start () {

        GameObject tmp;
		float x;
		float z;

        for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
                x = column*CellWidth;
				z = row*CellHeight;
                tmp = Instantiate(Road,new Vector3(x,z,0), Quaternion.Euler(0,0,0)) as GameObject;
                if(arrMap[row,column])
                    tmp.GetComponent<SpriteRenderer>().color = Color.red;
                else
                    tmp.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }

        tmp = Instantiate(PlayerDelivery,new Vector3(StartPoint.x,StartPoint.y,0), Quaternion.Euler(0,0,0)) as GameObject;

    }

    void Update()
	{
		// determine horizontal velocity change based on the horizontal input
		_vx = Input.GetAxisRaw ("Horizontal");
		_vy = Input.GetAxisRaw ("Vertical");


    }

}