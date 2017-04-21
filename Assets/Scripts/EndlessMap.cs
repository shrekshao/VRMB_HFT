using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EndlessMap : MonoBehaviour {

    //public const int mapChunkSize = 241;    // 240: 2, 4, 6, 8, 10, 12  (LOD)

    [SerializeField]
    GameObject MapChunkPrefab;

    [SerializeField]
    public static Vector2 viewerPosition;

    //[SerializeField]
    public const float maxViewDst = 450;

    const float maxViewDisSqr = maxViewDst * maxViewDst;

    [SerializeField]
    public Transform viewer;

    [SerializeField]
    int chunkSize = 10;

    int chunksVisibleInViewDst;


    Dictionary<Vector2, MapChunk> position2MapChunk = new Dictionary<Vector2, MapChunk>();
    List<MapChunk> mapChunksVisibleLastUpdate = new List<MapChunk>();


    // Use this for initialization
    void Start () {
        //chunkSize = (int) MapChunkPrefab.GetComponent<Mesh>().bounds.size.x;  // assume the prefab is square
        //chunkSize = 240;
        chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
    }
	
	// Update is called once per frame
	void Update () {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks()
    {

        for (int i = 0; i < mapChunksVisibleLastUpdate.Count; i++)
        {
            mapChunksVisibleLastUpdate[i].SetVisible(false);
        }
        mapChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
        {
            //for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
            //int xOffset = 0;
            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (position2MapChunk.ContainsKey(viewedChunkCoord))
                {
                    position2MapChunk[viewedChunkCoord].UpdateMapChunk();
                    if (position2MapChunk[viewedChunkCoord].IsVisible())
                    {
                        mapChunksVisibleLastUpdate.Add(position2MapChunk[viewedChunkCoord]);
                    }
                }
                else {
                    position2MapChunk.Add(viewedChunkCoord, new MapChunk(viewedChunkCoord, chunkSize, transform, MapChunkPrefab));
                }

            }
        }
    }





    public class MapChunk
    {

        GameObject meshObject;
        Vector2 position;
        Bounds bounds;


        public MapChunk(Vector2 coord, int size, Transform parent, GameObject prefab)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            //meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject = Instantiate(prefab, positionV3, Quaternion.identity);
            //meshObject.transform.position = positionV3;
            //meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);
        }

        public void UpdateMapChunk()
        {
            //float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            //bool visible = viewerDstFromNearestEdge <= maxViewDst;

            bool visible = bounds.SqrDistance(viewerPosition) <= maxViewDisSqr;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }

    }
}
