using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
[System.Serializable]
public class Movement
{
    public bool isdummy;
    public string desc;
    public float angle;
}
[System.Serializable]
public class JSONTemplate
{
    public Movement[] keypoints;
}
public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider[] healthBars;
    public GameObject[] fighters;
    float maxhealth = 1;
    public Dictionary<string, bool> isAlive;
    public GameObject[] Player = new GameObject[4];
    /*
     * POSENET Angles
     * 2: Left Arm
     * 3: Right Arm
     * 4: Left ForeArm
     * 5: Right ForeArm
     * */
    public string apiAddress;
    JSONTemplate parsedOBJ;
    List<Vector3> InitialAngles = new List<Vector3>();
    public IEnumerator GetData()
    {
        using (var www = UnityWebRequest.Get(apiAddress))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                Debug.Log("Network Error");
            if (www.responseCode == 200)
            {
                parsedOBJ = JsonUtility.FromJson<JSONTemplate>(www.downloadHandler.text);
                RenderAngles(parsedOBJ);
            }
        }
    }

    private void RenderAngles(JSONTemplate parsedOBJ)
    {
        if (!parsedOBJ.keypoints[2].isdummy)
            InitialAngles[0] += new Vector3(0, 0, parsedOBJ.keypoints[2].angle);
        if (!parsedOBJ.keypoints[3].isdummy)
            InitialAngles[1] += new Vector3(0, 0, parsedOBJ.keypoints[3].angle);
        if (!parsedOBJ.keypoints[4].isdummy)
            InitialAngles[4] = new Vector3(0, 0, parsedOBJ.keypoints[4].angle);
        if (!parsedOBJ.keypoints[5].isdummy)
            InitialAngles[5] = new Vector3(0, 0, parsedOBJ.keypoints[5].angle);
        
    }

    int rate = 1;
    int count = 0;
    private void Update()
    {
        //if((count=(count+1)%rate)==0)
        //StartCoroutine(GetData());
        
    }
    void Start()
    {
        healthBars[0].value = maxhealth;
        healthBars[1].value = maxhealth;
        isAlive = new Dictionary<string, bool>();
        isAlive["Player"] = true;
        isAlive["Opponent"] = true;
        foreach(GameObject obj in Player)
        {
            InitialAngles.Add(obj.transform.eulerAngles);
        }

    }
    public T findByTag<T>(T[] Array, string tag)
    {
        foreach(T v in Array)
        {
            if((string)v.GetType().GetProperty("tag").GetValue(v) == tag)
            {
                return v;
            }
        }
        return default(T);
    }
    // Update is called once per frame
    public void TakeDamage(float amount,string tag)
    {
        float value = findByTag<Slider>(healthBars, tag).value -= amount;
        if(value<=0.0)
        {
            this.Die(tag);
        }
    }

    private void Die(string tag)
    {
        isAlive[tag] = false;
        Rigidbody[] rbs = findByTag<GameObject>(fighters, tag).GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }
        foreach (Collider collider in findByTag<GameObject>(fighters, tag).GetComponentsInChildren<Collider>())
        {
            collider.isTrigger = false;
        }
        findByTag<GameObject>(fighters, tag).GetComponent<Animator>().enabled = false;
    }
}
