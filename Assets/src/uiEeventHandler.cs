using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sproto;
public class uiEeventHandler : MonoBehaviour {
	public Text fightinfo;
	public RPC rpc = new RPC();
	public login l = new login();
	public void onMainUIClick(int tag){
        switch (tag)
        {
		case 10:
			Debug.Log("login");
			l.loginToServer( "空气", "iloveyou11");
            rpc.call(l.l, (SprotoTypeBase resp) =>{
			        if(resp == null) Debug.Log("login1");
					
			         protoType.login.response r = (protoType.login.response)resp;
				if (r.ok) Debug.Log("login ok"); else Debug.Log("login fail"); });
			/*
			if (cinfo.Session == 10){
				protoProtocol.getplayerinfo player = new protoProtocol.getplayerinfo();
				player.request = new protoType.getplayerinfo.request();
				byte[] q = client.Request(player, 11);
				sm.send(q);
			}
			if (cinfo.Session == 11){
				protoType.getplayerinfo.response resp = (protoType.getplayerinfo.response)cinfo.Obj;
				if (resp.ok) {
					Debug.Log(resp.player.hp);
				}
				else{
					Debug.Log("getplayerinfo error");
				}
			}
			*/

			break;
		default:
			break;
        }

	}
	void Start(){
		rpc.connect ();
	}
	void Update(){
		rpc.Update ();
	}

}
