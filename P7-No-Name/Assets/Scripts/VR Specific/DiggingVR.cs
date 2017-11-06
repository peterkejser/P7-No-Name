using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiggingVR : MonoBehaviour {

    public Terrain TerrainMain;
    int xRes;
    int yRes;
    float[,] heights;
    float reset;
    int sceneNumber;
    float[] lowerTerAmount;
    int i;
    GameObject earthParticles;
    bool inDigRange = false;

    void Start()
    {
        earthParticles = GameObject.FindGameObjectWithTag("EarthParticles");
        i = 0;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        reset = 0.001678569f;
        lowerTerAmount = new float[6] { 0.00157f, 0.0014f, 0.0012f, 0.001f, 0.0007f, 0.0005f };
        xRes = TerrainMain.terrainData.heightmapWidth;
        yRes = TerrainMain.terrainData.heightmapHeight;
        heights = TerrainMain.terrainData.GetHeights(0, 0, xRes, yRes);
    }

    void Update()
    {
        Debug.Log(i);
        if (Input.GetKeyDown("r"))
        {
            i = 0;
            //right beneath the wire
            heights[221, 240] = reset;
            heights[221, 241] = reset;
            heights[221, 239] = reset;
            //one point backwards
            heights[220, 240] = reset;
            heights[220, 239] = reset;
            heights[220, 241] = reset;
            // one point forwards
            heights[222, 240] = reset;
            heights[222, 241] = reset;
            heights[222, 239] = reset;
            TerrainMain.terrainData.SetHeights(0, 0, heights);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Snout")
        {
            inDigRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Snout")
        {
            inDigRange = false;
        }
    }
    public void VRdigging()
    {
        if (inDigRange)
        {
            earthParticles.GetComponent<ParticleSystem>().Play();
            LowerTerrain();
        }
    }

    void LowerTerrain()
    {
        //right beneath the wire
        Debug.Log("new");
        heights[221, 240] = lowerTerAmount[i];
        heights[221, 241] = lowerTerAmount[i];
        heights[221, 239] = lowerTerAmount[i];
        //one point backwards
        heights[220, 240] = lowerTerAmount[i];
        heights[220, 239] = lowerTerAmount[i];
        heights[220, 241] = lowerTerAmount[i];
        // one point forwards
        heights[222, 240] = lowerTerAmount[i];
        heights[222, 241] = lowerTerAmount[i];
        heights[222, 239] = lowerTerAmount[i];
        TerrainMain.terrainData.SetHeights(0, 0, heights);
        i += 1;
    }
}
