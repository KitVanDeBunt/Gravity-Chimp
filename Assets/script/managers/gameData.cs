using UnityEngine;
using System.Collections;

public class gameData : MonoBehaviour {
	
	
	static GameObject[] levelSpawnPoints;
	static GameObject activeSpawn;
	static GameObject player;
	static int activeSpawnNumber;
	static bool inLevel;
	static bool restarted = false;
	
	static ArrayList levelArray = new ArrayList();
	static ArrayList levelInfo = new ArrayList();
	
	//float aspectRatio = 1.7777777777f;
	
	void Awake () {
		DontDestroyOnLoad (this);
	}
	
	
     
    void Start()
    {
       // float difference = Mathf.Abs(aspectRatio-Camera.main.aspect);
       // if (difference > 0.01f){
			
		//	Camera.main.rect = new Rect (0, difference*0.5f, 1, (1-difference));
		//}
    }
	
	//function calld by PlayerHealt.cs Start();
	public static void init(){
		player = GameObject.FindGameObjectWithTag("Player");
		
		levelSpawnPoints = null;
		levelSpawnPoints = GameObject.FindGameObjectsWithTag("levelspawnPoint");
		//check if there is data of this level avalible
		if(restarted){
			for(int i = 0; i < levelArray.Count ; i++){
				ArrayList checkLevel = levelArray[i] as ArrayList;
				int checkLevelNum = (int)checkLevel[0];
					if(checkLevelNum == Application.loadedLevel){ 
						//set restart point
						int restartPointNum = (int)checkLevel[2];
						setActiveSpawn(restartPointNum);
						restarted = false;
						player.GetComponent<PlayerHealth>().movePlayerToSpawn();
					}
			}
		}
		levelInfo.Add(0);
		levelInfo.Add(0);
		levelInfo.Add(0);	
		updateActiveSpawn();
		updateLevelInfo();
		player.GetComponent<PlayerHealth>().movePlayerToSpawn();
	}
	
	public static void restart(){
		restarted = true;
	}
	
	public static void addLevel(ArrayList addedLevel){
		// (level number,level name,current spawn point);
		int newLevelNumber = (int)addedLevel[0];
		bool levelIsNew = true;
		//check if level exist if level exist overright;
		for (int i = 0;i<levelArray.Count;i++){
			ArrayList checkLevel = (ArrayList)levelArray[i];
			int checkLevelNumber = (int)checkLevel[0];
			if(checkLevelNumber == newLevelNumber){
				levelArray[i] = addedLevel;
				levelIsNew = false;
			}
		}
		//addlevel if new
		if(levelIsNew){
			levelArray.Add(addedLevel);
		}
		
		int latestLevelInt = levelArray.Count;
		ArrayList newestLevelAdded = levelArray[latestLevelInt-1] as ArrayList;
		string levelName = newestLevelAdded[1] as string;
		//Debug.Log(levelName+" level #: "+newestLevelAdded[0]+" _ Point: "+newestLevelAdded[2]+"\n"+"levelArray lenght:"+levelArray.Count);
	}
	
	public static void updateLevelInfo(){
		levelInfo[0] = Application.loadedLevel;
		levelInfo[1] = Application.loadedLevelName;
		levelInfo[2] = activeSpawnNumber;
		gameData.addLevel(levelInfo);	
	}
	
	public static void setAllSpawnsInactive(){
		for (int i = 0;i < levelSpawnPoints.Length;i++){
			bool isActive = levelSpawnPoints[i].GetComponent<levelSpawnPoint>().active;
			if(isActive){
				levelSpawnPoints[i].GetComponent<levelSpawnPoint>().active = false;
			}
		}
	}
	
	static void setActiveSpawn(int setNumber){
		setAllSpawnsInactive();
		for (int i = 0;i < levelSpawnPoints.Length;i++){
			if(setNumber == levelSpawnPoints[i].GetComponent<levelSpawnPoint>().Number){
				levelSpawnPoints[i].GetComponent<levelSpawnPoint>().active = true;
			}
		}
	}
	
	public static void updateActiveSpawn(){
		int activeSpawnCount = 0;
		for (int i = 0;i < levelSpawnPoints.Length;i++){
			bool isActive = levelSpawnPoints[i].GetComponent<levelSpawnPoint>().active;
			if(isActive){
				activeSpawnCount += 1;
				activeSpawn = levelSpawnPoints[i];
				activeSpawnNumber = levelSpawnPoints[i].GetComponent<levelSpawnPoint>().Number;
				
				player.GetComponent<PlayerHealth>().spawnLocation.x = activeSpawn.transform.position.x;	
				player.GetComponent<PlayerHealth>().spawnLocation.y = activeSpawn.transform.position.y;
				
			}
			if(activeSpawnCount>1){
				throw new System.ArgumentException("You have more than one active spawn point!!");
			}
		}	
		for (int i = 0;i < levelSpawnPoints.Length;i++){
			levelSpawnPoints[i].GetComponent<levelSpawnPoint>().setFrame();
		}
		updateLevelInfo();
	}
}
