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
    public bool tryingToDig = false;
    public GameObject pawRight;
    public GameObject pawLeft;
    float timer;
    float rpFirstPos;
    float lpFirstPos;
    float rpSecondPos;
    float lpSecondPos;
    float rpPosDif;
    float lpPosDif;
    bool initialPosCheck;

    void Start()
    {
        Debug.Log("checkche");
        timer = 0;
        earthParticles = GameObject.FindGameObjectWithTag("EarthParticles");
        i = 0;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        reset = 0.001678569f;
        lowerTerAmount = new float[6] { 0.00157f, 0.0014f, 0.0012f, 0.001f, 0.0007f, 0.0005f };
        xRes = TerrainMain.terrainData.heightmapWidth;
        yRes = TerrainMain.terrainData.heightmapHeight;
        heights = TerrainMain.terrainData.GetHeights(0, 0, xRes, yRes);
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

    void Update()
    {
        if (tryingToDig)
        {
            if (initialPosCheck)
            {
                rpFirstPos = pawRight.transform.position.z;
                lpFirstPos = pawLeft.transform.position.z;
                initialPosCheck = false;
            }
            if (timer <0.5f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                rpSecondPos = pawRight.transform.position.z;
                lpSecondPos = pawLeft.transform.position.z;
                rpPosDif = Mathf.Abs(rpFirstPos - rpSecondPos);
                lpPosDif = Mathf.Abs(lpFirstPos - lpSecondPos);
                Tempstuff(rpPosDif,lpPosDif);
            }

        }
        if (Input.GetKeyDown("r"))
        {

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
    void Tempstuff(float rpPosDif, float lpPosDif)
    {
        if(rpPosDif>0.1 || lpPosDif>0.1)
        {
            VRdigging();
        }
        timer = 0;
        rpSecondPos = 0;
        lpSecondPos = 0;
        initialPosCheck = true;
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
        if (i < 5)
        {
         //right beneath the wire
            heights[221, 240] = lowerTerAmount[i];
            //heights[221, 241] = lowerTerAmount[i];
            //heights[221, 239] = lowerTerAmount[i];
         //Behind Fence
            heights[220, 240] = lowerTerAmount[i];
            //heights[220, 239] = lowerTerAmount[i];
            //heights[220, 241] = lowerTerAmount[i];
         // Infront of fence
            heights[222, 240] = lowerTerAmount[i];
            //heights[222, 241] = lowerTerAmount[i];
            //heights[222, 239] = lowerTerAmount[i];
            TerrainMain.terrainData.SetHeights(0, 0, heights);
            i += 1;
        }
    }
}
