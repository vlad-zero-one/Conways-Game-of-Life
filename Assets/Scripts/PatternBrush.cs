using UnityEngine;
using UnityEngine.UI;

public class PatternBrush : MonoBehaviour
{
    public string patternName = "";

    public Sprite patternCursorSprite;
    private GameObject objectForPattern;
    private SpriteRenderer spriteRenderer;


    void OnEnable()
    {
        Start();
    }

    void Start()
    {
        if (patternName != "")
        {
            foreach (var button in GetComponentsInChildren<Button>())
            {
                if (button.name == System.String.Format("Button{0}", patternName))
                {
                    patternCursorSprite = button.GetComponent<Image>().sprite;
                    break;
                }
            }
            objectForPattern = new GameObject("Brush");
            spriteRenderer = objectForPattern.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = patternCursorSprite;
            spriteRenderer.transform.position = Vector2.zero;
        }
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (patternName != "")
        {

            var patternPosition = new Vector2((int)mousePos.x, (int)mousePos.y);
            objectForPattern.transform.position = patternPosition;

            if (Input.GetMouseButtonDown(0))
            {
                var prefabPattern = Resources.Load<GameObject>("Prefabs/Patterns/" + patternName) as GameObject;
                Instantiate(prefabPattern, patternPosition, Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(objectForPattern);
                this.enabled = false;
            }

            CameraZoom.Zoom();

        }
    }

    public void SetPatternName(string nameFromInspector)
    {
        patternName = nameFromInspector;
    }
}
