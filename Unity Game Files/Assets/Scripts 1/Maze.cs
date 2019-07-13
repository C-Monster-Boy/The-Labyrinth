using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Maze : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north;
        public GameObject east;
        public GameObject west;
        public GameObject south;
    }

    public NavMeshSurface surface;
    public GameObject[] enemyBot;
    public GameObject player;
    public GameObject endPoint;
    public GameObject floor;
    public GameObject wall;
    public float wallLength = 1.0f;
    public int xSize;
    public int ySize;

    private Cell[] cells;
    private GameObject wallHolder;
    private Vector3 initialPos;
    private int totalCells;
    [SerializeField]
    private int currCell = 0;
    private int visitedCells = 0;
    private bool startedBuilding = false;
    private int currNeighbour = 0;
    private List<int> lastCells;
    private int backingUp = 0;
    private int wallToBreak = 0;
    [SerializeField]
    private float limX;
    [SerializeField]
    private float limY;

    void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            xSize = GameManagerScript.mazeComplexity;
            ySize = GameManagerScript.mazeComplexity;
        }
        catch
        {
            xSize = 6;
            ySize = 6;
        }
        totalCells = xSize * ySize;
        CreateWalls();
        SpawnAtStartPos(player);
        SpawnAtEndPos(endPoint); 
        surface.BuildNavMesh();
        SpawnEnemy(enemyBot);
    }

    public void CreateWalls()
    {
        wallHolder = new GameObject();
        wallHolder.name = "Maze";

        initialPos = new Vector3((-xSize / 2) + (wallLength / 2), 0f, (-ySize / 2) + (wallLength / 2));

        GameObject tempWall;

        // For X-axis walls
        for(int i = 0; i < ySize; i++)
        {
            for(int j = 0; j <= xSize; j++)
            {
                Vector3 myPos = new Vector3((initialPos.x + (j*wallLength) - wallLength/2), 0f, (initialPos.z + (i*wallLength) - wallLength/2));
                tempWall = Instantiate (wall, myPos,Quaternion.identity) as GameObject;
                tempWall.transform.localScale = new Vector3(tempWall.transform.localScale.x, tempWall.transform.localScale.y, wallLength);
                tempWall.name = "VerticalWall" + j + "-" + i;
                tempWall.transform.parent = wallHolder.transform;
                if(i==ySize-1 && j == xSize)
                {
                    limX = myPos.x - (wallLength/2f);
                    limY = myPos.z;
                }
            }
        }

        // For Y-Axis walls
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                Vector3 myPos = new Vector3((initialPos.x + (j * wallLength)), 0f, (initialPos.z + (i * wallLength) - wallLength));
                tempWall = Instantiate (wall, myPos, Quaternion.Euler(0f,90f,0f)) as GameObject;
                tempWall.transform.localScale = new Vector3(tempWall.transform.localScale.x, tempWall.transform.localScale.y, wallLength);
                tempWall.name = "HorizontalWall" + j + "-" + i;
                tempWall.transform.parent = wallHolder.transform;

            }
        }

        CreateCells();

        CreateFloor(xSize,ySize);
    }

    void CreateFloor(int x, int y)
    {
        GameObject f = Instantiate(floor, Vector3.zero, Quaternion.identity) as GameObject;
        f.transform.position = new Vector3(0.8f,-0.5f,0f);
        f.transform.localScale = new Vector3(x,1f,y);
    }

    void CreateCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();

        int childern = wallHolder.transform.childCount;
        GameObject[] allWalls = new GameObject[childern];
        cells = new Cell[xSize * ySize];
        int eastWestRef = 0;
       

        //Get all childern of wallHolder
        for (int i = 0; i < childern; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        //Assign walls to cells
        for(int i = 0; i < cells.Length; i++)
        {
            cells[i] = new Cell();
            if (i % xSize == 0 && i != 0)
            {
                eastWestRef++;
            }
            cells[i].east = allWalls[eastWestRef + 1];
            cells[i].west = allWalls[eastWestRef];
            cells[i].south = allWalls[((xSize+1)*ySize) + i];
            cells[i].north = allWalls[(((xSize + 1) * ySize) + i) + xSize];

            eastWestRef++;
        }

        CreateMaze();
    }

    void CreateMaze()
    {  
       while(visitedCells < totalCells)
        {
            if (startedBuilding)
            {
                GiveMeNeighbour();
                if(cells[currNeighbour].visited == false && cells[currCell].visited == true)
                {
                    BreakWall();
                    cells[currNeighbour].visited = true;
                    visitedCells++;
                    lastCells.Add(currCell);
                    currCell = currNeighbour;

                    if(lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                currCell = Random.Range(0, totalCells);
                cells[currCell].visited = true;
                visitedCells++;

                startedBuilding = true;
            }
        }

        //Debug.Log("Created Maze!");
    }

    void BreakWall()
    {
        switch (wallToBreak)
        {
            case 1:
                {
                    Destroy(cells[currCell].north);
                    break;
                }
            case 2:
                {
                    Destroy(cells[currCell].east);
                    break;
                }
            case 3:
                {
                    Destroy(cells[currCell].west);
                    break;
                }
            case 4:
                {
                    Destroy(cells[currCell].south);
                    break;
                }
        }
    }

    void GiveMeNeighbour()
    {
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWall = new int[4];
        int check = 0;

        check = ((currCell+1)/xSize);
        check -= 1;
        check *= xSize;
        check += xSize;

        //east
        if(currCell + 1 < totalCells && (currCell + 1) != check)
        {
            if(cells[currCell+1].visited == false)
            {
                neighbours[length] = currCell + 1;
                connectingWall[length] = 2;
                length++;
            }
        }

        //west
        if (currCell - 1 >= 0 && currCell  != check)
        {
            if (cells[currCell - 1].visited == false)
            {
                neighbours[length] = currCell - 1;
                connectingWall[length] = 3;
                length++;
            }
        }

        //north
        if (currCell + xSize < totalCells)
        {     
            if (cells[currCell + xSize].visited == false)
            {
                neighbours[length] = currCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }

        //south
        if (currCell - xSize >= 0)
        {
            if (cells[currCell - xSize].visited == false)
            {
                neighbours[length] = currCell - xSize;
                connectingWall[length] = 4;
                length++;
            }
        }

       if(length > 0)
        {
            int theChosenOne = Random.Range(0, length);
            currNeighbour = neighbours[theChosenOne];
            wallToBreak = connectingWall[theChosenOne];
        }
       else
        {
            if(backingUp > 0)
            {
                currCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    void SpawnAtStartPos(GameObject g)
    {
        float x, y;
        x = limX - (wallLength * (xSize - 1));
        y = limY - (wallLength * (ySize - 1));

        Vector3 pos = new Vector3(x,0f,y);
        Instantiate(g, pos, Quaternion.identity);
    }

    void SpawnAtEndPos(GameObject g)
    {
        int endXY = Random.Range(1, 20);

        if (endXY % 2 == 0)
        {
            int randPos = Random.Range(0, xSize-1);
            float xPosEndPt = limX - (randPos * wallLength); 
            Vector3 pos = new Vector3(xPosEndPt-1.3f, 0f, limY-1.3f); //1.3 for portal obj
            Instantiate(g, pos, Quaternion.Euler(0f,90f,0f));
        }
        else
        {
            int randPos = Random.Range(0, ySize - 1);
            float yPosEndPt = limY - (randPos * wallLength);
            Vector3 pos = new Vector3(limX - 1.3f, 0f, yPosEndPt - 1.3f);
            Instantiate(g, pos, Quaternion.identity);
        } 
    }

    void SpawnEnemy(GameObject[] g)
    {
        int noOfBots = xSize / 6;

        for(int i = 0; i < noOfBots; i++)
        {
            int randPosX = Random.Range(0, xSize - 2);
            int randPosY = Random.Range(0, ySize - 2);
            float xPosEndPt = limX - (randPosX * wallLength);
            float yPosEndPt = limY - (randPosY * wallLength);
            Vector3 pos = new Vector3(xPosEndPt, 0f, yPosEndPt);
            Instantiate(g[0], pos, Quaternion.identity);
        }
    }

    public Vector3 GetRandomPosOnMaze()
    {
        int randPosX = Random.Range(0, xSize - 2);
        int randPosY = Random.Range(0, ySize - 2);
        float xPosEndPt = limX - (randPosX * wallLength);
        float yPosEndPt = limY - (randPosY * wallLength);
        Vector3 pos = new Vector3(xPosEndPt, 0f, yPosEndPt);
        return pos;
    }
}
