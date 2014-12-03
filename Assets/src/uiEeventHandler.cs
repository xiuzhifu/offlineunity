using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sproto;
public class uiEeventHandler : MonoBehaviour {
	public Text fightinfo;
	public Text playertext;
	public RPC rpc = new RPC();
	public login l = new login();
	public void onMainUIClick(int tag){
		switch (tag)
        {
		case 10:
			Debug.Log("login");
			l.loginToServer( "空气", "iloveyou");
			rpc.registerServerRPC(protoserverProtocol.sysmessage.tag, (SprotoRpc.Client.ResponseInfo  resp) =>{
				protoserverType.sysmessage.request r = (protoserverType.sysmessage.request)resp.Obj;
				Debug.Log(r.msg);
			});
			rpc.registerServerRPC(protoserverProtocol.drop.tag, (SprotoRpc.Client.ResponseInfo  resp) =>{
				protoserverType.drop.request r = (protoserverType.drop.request)resp.Obj;
				fightinfo.text = r.exp.ToString();
			});

            rpc.call(l.l, (SprotoTypeBase resp) =>{
			    	protoType.login.response r = (protoType.login.response)resp;
					if (r.ok) 
						Debug.Log("login ok"); 
					else 
						Debug.Log("login fail"); 
			});
			protoProtocol.getplayerinfo player = new protoProtocol.getplayerinfo();
			player.request = new protoType.getplayerinfo.request();
			rpc.call(player, (SprotoTypeBase resp) =>{
				protoType.getplayerinfo.response r = (protoType.getplayerinfo.response)resp;
				if (r.ok){
					playertext.text =r.player.name + ", hp:" + r.player.hp.ToString() + 
						", exp:"  + r.player.currentexperience.ToString();
				}
			});


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
