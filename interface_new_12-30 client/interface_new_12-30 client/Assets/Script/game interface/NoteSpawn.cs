using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawn : MonoBehaviour {

	public GameObject melody;
	public GameObject melodybar;
	public GameObject melodyh;
	private float[] spawntime;
	private string[] in1;
	private string[] in2;
	private string[] in3;
	private float t;
	private int i;
	private int count;
	private int notes;
	private int totalnote;
	private float[] pos;
	private string[] direction;
	private GameObject Note;
	public static TextAsset dataFile;
	public static TextAsset dataFile2;
	public static TextAsset dataFile3;

	// Use this for initialization
	void Start () {
        if (difficulty.id == 0)
        {
            if (difficulty.songdiff == 0)
            {
                NoteSpawn.dataFile = Resources.Load<TextAsset>("songsetting/easystime");
                NoteSpawn.dataFile2 = Resources.Load<TextAsset>("songsetting/easyposition");
                NoteSpawn.dataFile3 = Resources.Load<TextAsset>("songsetting/easydirection");
            }
            else
            {
                NoteSpawn.dataFile = Resources.Load<TextAsset>("songsetting/stime");
                NoteSpawn.dataFile2 = Resources.Load<TextAsset>("songsetting/NotePosition");
                NoteSpawn.dataFile3 = Resources.Load<TextAsset>("songsetting/NoteDirection");
            }
        }
        else if(difficulty.id == 1)
        {
            if (difficulty.songdiff == 0)
            {
                NoteSpawn.dataFile = Resources.Load<TextAsset>("songsetting/detectiveconan_easy_stime");
                NoteSpawn.dataFile2 = Resources.Load<TextAsset>("songsetting/detectiveconan_easy_position");
                NoteSpawn.dataFile3 = Resources.Load<TextAsset>("songsetting/detectiveconan_easy_direction");
            }
            else
            {
                NoteSpawn.dataFile = Resources.Load<TextAsset>("songsetting/detectiveconan_hard_stime");
                NoteSpawn.dataFile2 = Resources.Load<TextAsset>("songsetting/detectiveconan_hard_position");
                NoteSpawn.dataFile3 = Resources.Load<TextAsset>("songsetting/detectiveconan_hard_direction");
            }
        }
		count = 0;
		t = 0;
		notes = 0;
		i = 0;
		in1 = dataFile.text.Split ('\n');
		in2 = dataFile2.text.Split ('\n');
		in3 = dataFile3.text.Split('\n');
		spawntime = new float[in1.Length];
		pos = new float[in2.Length];
		direction = new string[in3.Length];
		foreach (string line in in1) {
			spawntime [i] = float.Parse(line);
			i++;
		}
		i = 0;
		foreach (string line in in3)
		{
			direction[i] = line;
			i++;
		}
		i = 0;
		foreach (string line in in2) {
			pos [i] = float.Parse (line);
			i++;
		}
		i = 0;
		for(int x = 0 ; x < direction.Length; x++){
			if(direction[x].Contains("lbr")){
				spawntime[x] -= 1.15f;
				if (spawntime[x] < 0) spawntime[x] = 0;
				pos[i] = 6.76f*pos[i] -3.25f;
				i++;
				pos[i] = 13.03f*pos[i] - 6.14f;
				i++;
				pos[i] = 6.05f*pos[i] - 3.25f;
				i++;
			}
			else if(direction[x].Contains("lb")){
				spawntime[x] -= 1.15f;
				if (spawntime[x] < 0) spawntime[x] = 0;
				pos[i] = 6.76f*pos[i] -3.25f;
				i++;
				pos[i] = 13.03f*pos[i] - 6.14f;
				i++;
			}
			else if(direction[x].Contains("lr")){
				spawntime[x] -= 1.15f;
				if (spawntime[x] < 0) spawntime[x] = 0;
				pos[i] = 6.76f*pos[i] -3.25f;
				i++;
				pos[i] = 6.05f*pos[i] - 3.25f;
				i++;
			}
			else if (direction[x].Contains("br"))
			{
				spawntime[x] -= 1.20f;
				if (spawntime[x] < 0) spawntime[x] = 0;
				pos[i] = 13.03f * pos[i] - 6.14f;
				i++;
				pos[i] = 6.05f * pos[i] - 3.25f;
				i++;
			}
			else if(direction[x].Contains("l")){
				spawntime[x] -= 1.15f;
				if (spawntime[x] < 0) spawntime[x] = 0;
				pos[i] = 6.76f*pos[i] -3.25f;
				i++;
			}
			else if(direction[x].Contains("r")){
				spawntime[x] -= 1.20f;
				if (spawntime[x] < 0) spawntime[x] = 0;
				pos[i] = 6.05f*pos[i] - 3.25f;
				i++;
			}
			else if(direction[x].Contains("b")){
				spawntime[x] -= 0.85f;
				if (spawntime[x] < 0) spawntime[x] = 0;
				pos[i] = 13.03f*pos[i] - 6.14f;
				i++;
			}
		}
		totalnote = pos.Length;
	}

	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t >= spawntime[notes] && count <= totalnote && notes < spawntime.Length) {
			if(direction[notes].Contains("lbr")){
				Instantiate (melodybar, new Vector3 (7.16f, pos[count], 0), Quaternion.identity);
				count++;
				Instantiate (melody, new Vector3 (pos[count], 5.15f, 0), Quaternion.identity);
				count++;
				Instantiate (melodyh, new Vector3 (-6.11f, pos[count], 0), Quaternion.identity);
				count++;
			}
			else if(direction[notes].Contains("lb")){
				//Debug.Log("lb");
				Instantiate (melodybar, new Vector3 (7.16f, pos[count], 0), Quaternion.identity);
				count++;
				Instantiate (melody, new Vector3 (pos[count], 5.15f, 0), Quaternion.identity);
				count++;
			}
			else if(direction[notes].Contains("lr")){
				//Debug.Log("lr");
				Instantiate (melodybar, new Vector3 (7.16f, pos[count], 0), Quaternion.identity);
				count++;
				Instantiate (melodyh, new Vector3 (-6.11f, pos[count], 0), Quaternion.identity);
				count++;
			}
			else if(direction[notes].Contains("br")){
				//Debug.Log("rb");
				Instantiate (melodyh, new Vector3 (-6.11f, pos[count], 0), Quaternion.identity);
				count++;
				Instantiate (melody, new Vector3 (pos[count], 5.15f, 0), Quaternion.identity);
				count++;
			}
			else if(direction[notes].Contains("l")){
				//Debug.Log("l");
				Instantiate (melodybar, new Vector3 (7.16f, pos[count], 0), Quaternion.identity);
				count++;
			}
			else if(direction[notes].Contains("r")){
				//Debug.Log("r");
				Instantiate (melodyh, new Vector3 (-6.11f, pos[count], 0), Quaternion.identity);
				count++;
			}
			else if(direction[notes].Contains("b")){
				//Debug.Log("b");
				Instantiate (melody, new Vector3 (pos[count], 5.15f, 0), Quaternion.identity);
				count++;
			}
			notes++;
		}
	}
}