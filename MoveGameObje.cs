using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
# endif
public class MoveGameObje : MonoBehaviour
{

    GameObject[] gidilecekNoktalar;

    bool aradakiMefaseyiBirKereAl = true;

    Vector3 aradakiMesafe;

    int aradakiMesafeSayaci = 0;

    bool ilerimigerimi = true;

    public float speed = 5;


    void Start()
    {

        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }


    }


    void FixedUpdate()
    {
        //transform.Rotate(0, 0, 5);
        NoktalaraGit();
    }

    void NoktalaraGit()
    {
        if (aradakiMefaseyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMefaseyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);//aradaki uzaklýðý veren method
        transform.position += aradakiMesafe * Time.deltaTime * speed;
        if (mesafe < 0.5f)
        {
            aradakiMefaseyiBirKereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ilerimigerimi = false;
            }
            else if (aradakiMesafeSayaci == 0)
            {
                ilerimigerimi = true;
            }
            if (ilerimigerimi)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }


        }





    }

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }

#endif
}
#if UNITY_EDITOR//sadece editorun gördu nesler

[CustomEditor(typeof(MoveGameObje))]
[System.Serializable]

class TestereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MoveGameObje script = (MoveGameObje)target;
        if (GUILayout.Button("CREATE", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();

        }
    }
}
#endif





