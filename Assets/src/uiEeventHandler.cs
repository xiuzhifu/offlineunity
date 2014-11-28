using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class uiEeventHandler : MonoBehaviour {
	public Text fightinfo;
	public void onMainUIClick(int tag){
        switch (tag)
        {
		case 1:
                Debug.Log("mian button click");
				Debug.Log(Screen.width);
                break;
            case 2:
				fightinfo.text = "j7";
                Debug.Log("战斗 click");
                break;
        }

	}
}
