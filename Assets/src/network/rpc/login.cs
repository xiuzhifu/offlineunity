//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
public class login
{
	public protoProtocol.login l;
	public login ()
	{
		l = new protoProtocol.login();
		l.request = new protoType.login.request();

	}
	public void loginToServer(string username, string password){
		l.request.username = username;
		l.request.password = password;
	}
}

