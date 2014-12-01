using UnityEngine;
using System.Collections;
public class MainScene : MonoBehaviour {

	// Use this for initialization
	private SocketMgr sm;
	static void Start () {
		this.
        SocketMgr sm = new SocketMgr();
		if (sm.blockConnect ("127.0.0.1", 8888)){
			Debug.Log("登陆成功");
		}
		/*
        createaccount cc = new createaccount();

        cc.request = new createaccount.request();
        cc.request.username = "123";
        cc.request.password = "456";
        byte[] req = SprotoRpc.Client.Request(cc, 1);
        sm.send(req);
        */
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    static void Main(string[] args)
    {

        Start();
    }
}
