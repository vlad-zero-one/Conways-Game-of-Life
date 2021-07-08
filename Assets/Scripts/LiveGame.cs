using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveGame : MonoBehaviour
{
    public int limitLength = 10;

    private static int defaultLength = 20;

    private float deltaForSpeed;
    [Range(0, 60)]
    public int generationsForSecond = 60;

    public Slider slider;
    public GameObject prefub;
    //public Camera cam;

    public bool[][] points = new bool[defaultLength][];
    public bool[][] points_new = new bool[defaultLength][];

    void Start()
    {

        //Debug.Log("sizeFromApplyButton из LiveGame: " + AreaChanger.sizeFromApplyButton);

        limitLength = BorderCreation.borderSize;

        //Debug.Log("limitLength из LiveGame: " + limitLength);



        //Debug.Log("START");

        System.Array.Resize(ref points, limitLength);
        //System.Array.Resize(ref points_new, limitLength);

        for (int i = 0; i < limitLength; i++)
        {
            points[i] = new bool[limitLength];
            //points_new[i] = new bool[limitLength];
        }

        // Glider
        /*
        points[5][5] = true;
        points[6][6] = true;
        points[7][6] = true;
        points[7][5] = true;
        points[7][4] = true;
        */

        GameObject[] startingCells = GameObject.FindGameObjectsWithTag("Cell");
        foreach (var cell in startingCells)
        {
            points[(int)cell.transform.position.x][(int)cell.transform.position.y] = true;
        }

        /*
        string formatter = "";
        for(int i = 0; i < limitLength; i++)
        {
            for (int j = 0; j < limitLength; j++)
            {
                int indSum = i + j;
                formatter += points[i][j] + " ";
            }
            formatter += "\n";
        }
        Debug.Log(formatter);
        */
        for (int i = 0; i < limitLength; i++)
        {
            for (int j = 0; j < limitLength; j++)
            {
                if (points[i][j]) Debug.Log(i + " " + j);
            }
        }


        /*
        Debug.Log(System.String.Format(
                "Массив points в старте: \n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}\n" +
                points[0][0],
                points[0][1],
                points[0][2],
                points[1][0],
                points[1][1],
                points[1][2],
                points[2][0],
                points[2][1],
                points[2][2]));
                */
                
        /*
        points[5][5] = true;
        points[6][5] = true;
        points[6][6] = true;
        points[7][6] = true;
        points[6][7] = true;
        */

        /*
        points[1][1] = true;
        points[1][2] = true;
        points[2][2] = true;
        points[2][3] = true;
        points[3][3] = true;
        points[4][4] = true;
        points[5][5] = true;
        points[6][6] = true;
        */
        /*
        points[0][0] = true;
        points[0][1] = true;
        points[1][1] = true;
        points[2][2] = true;
        */
        for (int i = 0; i < limitLength; i++)
        {
            for (int j = 0; j < limitLength; j++)
            {
                if(points[i][j])
                {
                    Instantiate(prefub, new Vector2(i, j), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {
        generationsForSecond = (int)slider.value;

        if (generationsForSecond * deltaForSpeed > 60 * Time.deltaTime)
        {
            Debug.Log("UPDATE");

            Debug.Log(System.String.Format(
                "Массив points в начале Update: \n" +
                "{0} {1} {2}\n" +
                "{3} {4} {5}\n" +
                "{6} {7} {8}\n",
                points[0][0],
                points[0][1],
                points[0][2],
                points[1][0],
                points[1][1],
                points[1][2],
                points[2][0],
                points[2][1],
                points[2][2]));

            // указывают на один и тот же элемент
            // bool[][] points_new = points.Clone() as bool[][];

            points_new = DeepCopyArrayOfArrays(points);

            //points[0][0] = false;

            Debug.Log(System.String.Format(
                "Массив points после клонирования: \n" +
                "{0} {1} {2}\n" +
                "{3} {4} {5}\n" +
                "{6} {7} {8}\n",
                points[0][0],
                points[0][1],
                points[0][2],
                points[1][0],
                points[1][1],
                points[1][2],
                points[2][0],
                points[2][1],
                points[2][2]));

            Debug.Log(System.String.Format(
                "Массив points_new после клонирования: \n" +
                "{0} {1} {2}\n" +
                "{3} {4} {5}\n" +
                "{6} {7} {8}\n",
                points_new[0][0],
                points_new[0][1],
                points_new[0][2],
                points_new[1][0],
                points_new[1][1],
                points_new[1][2],
                points_new[2][0],
                points_new[2][1],
                points_new[2][2]));

            for (int i = 0; i < limitLength; i++)
            {
                for (int j = 0; j < limitLength; j++)
                {
                    if (points[i][j])
                    {
                        if (GetNeighboursCount(i, j) < 2 || GetNeighboursCount(i, j) > 3)
                        {
                            points_new[i][j] = false;
                        }
                    }
                    else
                    {
                        if (GetNeighboursCount(i, j) == 3)
                        {
                            points_new[i][j] = true;
                        }
                    }
                }
            }

            Debug.Log(System.String.Format(
                "Массив points_new после поиска соседей: \n" +
                "{0} {1} {2}\n" +
                "{3} {4} {5}\n" +
                "{6} {7} {8}\n",
                points_new[0][0],
                points_new[0][1],
                points_new[0][2],
                points_new[1][0],
                points_new[1][1],
                points_new[1][2],
                points_new[2][0],
                points_new[2][1],
                points_new[2][2])
                );



            for (int i = 0; i < limitLength; i++)
            {
                System.Array.Clear(points[i], 0, limitLength);
            }
            //System.Array.Clear(points, 0, limitLength);

            Debug.Log(System.String.Format(
                "Массив points после чистки: \n" +
                "{0} {1} {2}\n" +
                "{3} {4} {5}\n" +
                "{6} {7} {8}\n",
                points[0][0],
                points[0][1],
                points[0][2],
                points[1][0],
                points[1][1],
                points[1][2],
                points[2][0],
                points[2][1],
                points[2][2])
                );

            points = DeepCopyArrayOfArrays(points_new);

            Debug.Log(System.String.Format(
                "Массив points_new после клонирования в points: \n" +
                "{0} {1} {2}\n" +
                "{3} {4} {5}\n" +
                "{6} {7} {8}\n",
                points_new[0][0],
                points_new[0][1],
                points_new[0][2],
                points_new[1][0],
                points_new[1][1],
                points_new[1][2],
                points_new[2][0],
                points_new[2][1],
                points_new[2][2])
                );

            Debug.Log(System.String.Format(
                "Массив points после клонирования из points_new: \n" +
                "{0} {1} {2}\n" +
                "{3} {4} {5}\n" +
                "{6} {7} {8}\n",
                points[0][0],
                points[0][1],
                points[0][2],
                points[1][0],
                points[1][1],
                points[1][2],
                points[2][0],
                points[2][1],
                points[2][2])
                );

            var gameObjects = GameObject.FindGameObjectsWithTag("Cell");

            for (var i = 0; i < gameObjects.Length; i++)
                Destroy(gameObjects[i]);

            for (int i = 0; i < limitLength; i++)
            {
                for (int j = 0; j < limitLength; j++)
                {
                    if (points[i][j])
                    {
                        Instantiate(prefub, new Vector2(i, j), Quaternion.identity);
                    }
                }
            }

            deltaForSpeed = 0;
        }
        else
        {
            deltaForSpeed += Time.deltaTime;
        }

    }

    private int GetNeighboursCount(int i, int j)
    {
        (int, int)[] neighs_coor = { (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0) };

        int result = 0;

        foreach ((int, int) neighbour in neighs_coor)
        {
            try
            {
                if (points[i + neighbour.Item1][j + neighbour.Item2]) result++;
            }
            catch (System.IndexOutOfRangeException e)
            {

            }
        }

        return result;
    }

    private bool[][] DeepCopyArrayOfArrays(bool[][] arrayOfArrays)
    {
        int lengthOfSquareArray = arrayOfArrays.Length;
        bool[][] tmpArray = new bool[lengthOfSquareArray][];

        for (int i = 0; i < lengthOfSquareArray; i++)
        {
            tmpArray[i] = new bool[lengthOfSquareArray];
            for (int j = 0; j < lengthOfSquareArray; j++)
            {
                tmpArray[i][j] = arrayOfArrays[i][j];
            }
        }

        return tmpArray;
    }
}


/*
 public class LiveGame : MonoBehaviour
{
    public int limitLength;

    public GameObject prefub;

    public bool[,] points = new bool[20, 20];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                points[i, j] = false;
            }
        }
        
        // Glider
        //points[5, 5] = true;
        //points[5, 6] = true;
        //points[5, 7] = true;
        //points[6, 7] = true;
        //points[7, 6] = true;
        
        points[5, 5] = true;
        points[6, 5] = true;
        points[6, 6] = true;
        points[7, 6] = true;
        points[6, 7] = true;

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if(points[i,j])
                {
                    Instantiate(prefub, new Vector2(i, j), Quaternion.identity);
                }
            }
        }

        Debug.Log("START!");
    }

    void FixedUpdate()
    {

        bool[,] points_new = points.Clone() as bool[,];

        for (int i = 0; i < 20; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                if (points[i,j])
                {
                    if (GetNeighboursCount(i, j) < 2 || GetNeighboursCount(i, j) > 3)
                    {
                        points_new[i, j] = false;
                    }
                }
                else
                {
                    if (GetNeighboursCount(i, j) == 3)
                    {
                        points_new[i, j] = true;
                    }
                }
            }
        }

        System.Array.Clear(points, 0, 20);
        points = points_new.Clone() as bool[,];

        var gameObjects = GameObject.FindGameObjectsWithTag("Cell");

        for (var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (points[i, j])
                {
                    Instantiate(prefub, new Vector2(i, j), Quaternion.identity);
                }
            }
        }

        Debug.Log("UPDATE!");

    }

    private int GetNeighboursCount(int i, int j)
    {
        (int, int)[] neighs_coor = { (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0) };

        int result = 0;

        foreach ((int, int) neighbour in neighs_coor)
        {
            try
            {
                if (points[i + neighbour.Item1, j + neighbour.Item2]) result++;
            }
            catch (System.IndexOutOfRangeException e)
            {

            }
        }

        return result;
    }
}

    */