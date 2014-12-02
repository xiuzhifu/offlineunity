using System;
using System.Collections.Generic;
using Sproto;
using UnityEngine;
public class RPC {
	private SocketMgr sm;
	private SprotoRpc.Client client;
	private SprotoRpc.Service service;
	private Dictionary<int, TFunc> sessionMap;
	private int session = 1;
	public delegate void TFunc(SprotoTypeBase resp);
	public RPC(){
		client = new SprotoRpc.Client ();
		sm = new SocketMgr();
		sessionMap = new Dictionary<int, TFunc>();
		initRPC ();
	}

	public bool connect(){
		if (sm.blockConnect ("127.0.0.1", 8888)){
			Debug.Log("connect ok");
			sm.recv ();
			return true;
		}
		return false;
	}
	public void initRPC(){
		//new serverprotoProtocol.drop ();
		//new serverprotoProtocol.sysmessage ();
		new protoProtocol.changescene ();
		new protoProtocol.createaccount ();
		new protoProtocol.createplayer ();
		new protoProtocol.getfightround ();
		new protoProtocol.getplayerinfo ();
		new protoProtocol.login ();
	}

	public bool registerRPC(){
		return true;
	}
	public void call(SprotoProtocolBase sp, TFunc func){
		sessionMap [session] = func;
		byte[] tmp = client.Request (sp, session);
		sm.send (tmp);
		session ++;

	}
	public void dispatch(byte[] buf){

	}
	public void Update () {
		if (sm != null && sm.recvlist.Count > 0) {
			byte[] t = sm.recvlist.Dequeue();
			SprotoRpc.Client.ResponseInfo cinfo = client.Dispatch (t);
			//SprotoRpc.Service.RequestInfo sinfo = service.Dispatch (t);

			TFunc f;
			Debug.Log(cinfo.Session);
			if (cinfo.Session == 0){//server active sended msg
			} 
			else if (sessionMap.TryGetValue(cinfo.Session, out f)) {
				f(cinfo.Obj);
				sessionMap.Remove(cinfo.Session);
			}
		}
	}
}