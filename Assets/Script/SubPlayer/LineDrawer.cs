using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    private Camera cam;

    private Line currentrLine;
    [SerializeField] private ColorPaletteController palette;
    


    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentrLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    void BeginDraw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - new Vector3(+5.19f, 0, 0);
        if (mousePosition.x < -5.5 && mousePosition.y < -5.5f)
            return;
        currentrLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        currentrLine.UsePhysice(false);
        currentrLine.SetLineColor(palette.SelectedColor);
        currentrLine.SetPointsDistance(linePointsMinDistance);
        currentrLine.SetLineWidth(lineWidth);

    } 
    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - new Vector3(+5.19f, 0,0);
        currentrLine.AddPoint(mousePosition);
    }
    void EndDraw()
    {
        if (currentrLine != null) {
            if (currentrLine.PointsCount < 2)
            {
                //라인이 점일때
                Destroy(currentrLine.gameObject);
            }
            else {
                currentrLine.UsePhysice(true);
                currentrLine.SetMass();
                currentrLine = null;
            }
        }
    }

}
