using UnityEngine;
using System.Collections;
using protoProtocol;
using Sproto;


public class MainScene : MonoBehaviour {

	// Use this for initialization
	private SocketMgr sm = null;
	void Start () {
        sm = new SocketMgr();
		if (sm.blockConnect ("127.0.0.1", 8888)){
			Debug.Log("登陆成功");
		}

		createaccount cc = new createaccount();
		cc.request = new protoType.createaccount.request ();

		cc.request.username = ("123");
		cc.request.password = "456";
        byte[] req = SprotoRpc.Client.Request(cc, 1);
        sm.send(req);   
		sm.recv();
	}
	
	// Update is called once per frame
	void Update () {
		if (sm != null && sm.recvlist.Count > 0) {
			byte[] t = sm.recvlist.Dequeue();
			SprotoRpc.Service.RequestInfo sinfo = SprotoRpc.Service.Dispatch (t);
			Debug.Log(sinfo.Session);
			protoType.createaccount.response req_obj = (protoType.createaccount.response)sinfo.Obj;
			if (req_obj.ok)
			Debug.Log("recvd 111");
		}
	}
}
