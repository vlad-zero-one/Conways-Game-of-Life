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

    public bool[][] points = new bool[defaultLength][];
    public bool[][] points_new = new bool[defaultLength][];

    private readonly (int, int)[] neighs_coor = new (int, int)[] {
            (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0)
        };

    void OnEnable()
    {
        limitLength = BorderCreation.borderSize;
        System.Array.Resize(ref points, limitLength);
        for (int i = 0; i < limitLength; i++)
        {
            points[i] = new bool[limitLength];
        }
        GameObject[] startingCells = GameObject.FindGameObjectsWithTag("Cell");
        foreach (var cell in startingCells)
        {
            points[(int)cell.transform.position.x][(int)cell.transform.position.y] = true;
        }
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
            points_new = DeepCopyArrayOfArrays(points);
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
            
            for (int i = 0; i < limitLength; i++)
            {
                System.Array.Clear(points[i], 0, limitLength);
            }
            points = DeepCopyArrayOfArrays(points_new);

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
        var result = 0;

        foreach (var (corX, corY) in neighs_coor)
        {
            if (i + corX >= 0 && i + corX < points.Length)
            {
                if (j + corY >= 0 && j + corY < points[i + corX].Length)
                {
                    if (points[i + corX][j + corY]) result++;
                }
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