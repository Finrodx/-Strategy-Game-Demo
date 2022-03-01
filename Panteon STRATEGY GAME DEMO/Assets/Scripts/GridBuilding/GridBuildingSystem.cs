using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{

    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private BuildingPlacement temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;


    private void Awake()
    {
        current = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        string tilePath = @"Background\Tiles\Ground Tiles\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "32pxWC"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "32pxGC"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "32pxRC"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!temp)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject(0) )
            {
                return;
            }

            if (!temp.Placed)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
                worldPosition.z = -11;
                Vector3Int cellPos = gridLayout.LocalToCell(worldPosition);
                
                //Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

                //check if the click is on tilemap
                if (MainTilemap.GetTile(cellPos))
                {
                    Debug.Log(cellPos);
                    if (prevPos != cellPos)
                    {
                        temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos);
                        prevPos = cellPos;
                        FollowBuilding();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) | Input.GetKeyDown(KeyCode.Space))
        {
            if (temp.CanBePlaced())
                temp.Place();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) | Input.GetMouseButtonDown(1))
        {
            ClearArea();
            Destroy(temp.gameObject);
        }
    }

    public void InitWBuilding(GameObject building)
    {
        temp = Instantiate(building, Input.mousePosition, Quaternion.identity).GetComponent<BuildingPlacement>();
        FollowBuilding();
    }


    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v3 in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v3.x, v3.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }
    

    //Creating an array based on size of a building's area
    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }

    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        ClearArea();
        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray= GetTilesBlock(buildingArea, MainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];
        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                FillTiles(tileArray, TileType.Green);
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }

        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
        foreach (TileBase tb in baseArray)
        {
            if (tb != tileBases[TileType.White])
            {
                Debug.Log("Can not place here");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, TileType.Green, MainTilemap);
    }
    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }
}
