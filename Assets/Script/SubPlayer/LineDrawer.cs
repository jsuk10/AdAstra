using UnityEngine;
using System.Collections;
using Photon.Pun;

public class LineDrawer : MonoBehaviourPun
{
    public GameObject linePrefab;
    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    [SerializeField]private Texture2D cursorImg;


    private Camera cam;

    private Line currentrLine;
    [SerializeField] private ColorPaletteController palette;
    [SerializeField] private GameObject LineDraw;
    public PhotonView PV;

    private void Start()
    {
        cam = Camera.main;
        GameManager.Instance.SetDirctionary(LineDraw.name, LineDraw);
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
        if (Input.mousePosition.x < 300f && Input.mousePosition.y < 300f)
            return;
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        // currentrLine = PhotonNetwork.Instantiate(linePrefab.name, this.transform.position, Quaternion.identity).GetComponent<Line>();
        GameObject obj = PhotonNetwork.Instantiate(linePrefab.name, this.transform.position, Quaternion.identity);
        currentrLine = obj.GetComponent<Line>();
        PV = obj.GetComponent<PhotonView>();
        PV.RPC("UsePhysice", RpcTarget.All, false);
        float r = palette.SelectedColor.r;
        float g = palette.SelectedColor.g;
        float b = palette.SelectedColor.b;
        PV.RPC("SetLineColor", RpcTarget.All, r, g, b);
        PV.RPC("SetPointsDistance", RpcTarget.All, linePointsMinDistance);
        PV.RPC("SetLineWidth", RpcTarget.All, lineWidth);
        // currentrLine.UsePhysice(false);
        // currentrLine.SetLineColor(palette.SelectedColor);
        // currentrLine.SetPointsDistance(linePointsMinDistance);
        // currentrLine.SetLineWidth(lineWidth);

    }
    void Draw()
    {
        GameManager.Instance.useInk = currentrLine.totalDistance;
        UIManager.Instance.AddChange();
        //UseInk();
        if (GameManager.Instance.useInk + GameManager.Instance.usedInk > GameManager.Instance.maxInk)
            return;
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        PV.RPC("AddPoint", RpcTarget.All, mousePosition);
        // currentrLine.AddPoint(mousePosition);
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
                // currentrLine.UsePhysice(true);
                // currentrLine.SetMass();
                PV.RPC("UsePhysice", RpcTarget.All, true);
                PV.RPC("SetMass", RpcTarget.All);
                GameManager.Instance.usedInk += GameManager.Instance.useInk;
                GameManager.Instance.useInk = 0;
                UIManager.Instance.AddChange();
                //UseInk();
                currentrLine = null;
            }
        }
    }
    public void SetCursor() {
        Cursor.SetCursor(cursorImg, Vector2.down, CursorMode.ForceSoftware);
    }
}