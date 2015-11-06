using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

public class Server : MonoBehaviour {
	/* Server must last between scenes, so use DontDestroyOnLoad()
	 * and a singleton pattern
	 */
	private static Server _instance;
	private static Server instance{
		get{
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<Server>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}
	
	//Provides client connections for TCP network services.
	public static TcpClient s;
	public static Thread trd;
	static bool trdFlag = false;	

	public static bool notFinishGame = true;	
	public static bool startGame = false;
	
	//how many requests a player sends
	public static int i = 0;
	
	//client user id and password
	static string uid;
	static string pw;
		
	//get method
	public static string getUid(){return uid;}	
	public static string getPw(){return pw;}
	
	//set method
	public static void setUid(string Uid){uid = Uid;}
	public static void setPw(string Pw){pw = Pw;}	

	public static float score;

	// Use this for initialization
	void Start () {		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Awake() {
		if (_instance == null) {
			//if(first instance) then make it the singleton
			_instance = this;
			DontDestroyOnLoad (this);
		} else {
			//if(a singleton exists) then destroy it
			if(this!=_instance)
				Destroy(this.gameObject);
		}
	}

	//send message to the server
	static void Send(TcpClient socket, string sendStr, int timeout){		
		int startTickCount = Environment.TickCount;		
		byte[] data = System.Text.Encoding.ASCII.GetBytes(sendStr + "$");		
		NetworkStream writeStream = socket.GetStream();
		
		if (Environment.TickCount > startTickCount + timeout){
			throw new Exception("Timeout.");
		}
		try {
			writeStream.Write(data, 0, data.Length);
			Debug.Log("Send: "+sendStr);	
			writeStream.Flush();
		}
		catch (SocketException ex)
		{				
			if (ex.SocketErrorCode == SocketError.WouldBlock ||
			    ex.SocketErrorCode == SocketError.IOPending ||
			    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
			{
				//socket buffer is probably full, wait and try again
				Debug.Log("sleep");
				Thread.Sleep(30);
			}
			else//any serious error occur, then throw exception
				throw ex;  
		}
	}

	/*check user event is successfully done 
	 *or not and destroy thread
	 */
	IEnumerator checkTrdFlag(){
		while (true) {
			if (trdFlag) {
				try{
					trd.Abort ();
					Debug.Log ("Abort thread");
					trdFlag = false;
				}
				catch(Exception ex){
					throw ex;
				}
				break;
			}
			yield return null;
		}
	}
	
	/*once you click a login button
	 * a new thread is created
	 * to send a uid and pw to a server
	 */
	public void ClickLogin(){
		Debug.Log(uid);
		if(s==null && uid != null){
			TcpClient tcpClient = new TcpClient();
			try{
				//tcpClient.Connect("172.30.1.3", 5001);
				tcpClient.Connect("222.106.56.212", 5001);
				//tcpClient.Connect("192.168.0.235", 5001);
				s = tcpClient;
			}catch(Exception ex){
				throw ex;
			}
		}
		try{			
			string Uid = getUid ();
			string Pw = getPw ();
			if(s != null){
				i++;
				Debug.Log (i);
				Send(s, "LoginReq:"+Uid+":"+Pw, 1000); 
				Receive(s);
			}
		}
		catch(Exception ex){
			throw ex;
		}
	}

	/*once you click a Rank button
	 * a new thread is created
	 * to send a request to receive a list of top players
	 */
	public void ClickRank(){
		if(s==null){
			TcpClient tcpClient = new TcpClient();
			try{
				//tcpClient.Connect("172.30.1.3", 5001);
				tcpClient.Connect("222.106.56.212", 5001);
				s = tcpClient;
			}catch(Exception ex){
				throw ex;
			}
		}
		try{
			if(s != null){
				i++;
				Debug.Log (i);
				Send(s, "RankReq", 1000);  
				Receive(s);
			}
		}
		catch(Exception ex){
			throw ex;
		}
	}
	
	public void ClickFinishGame(){
		try{
			if(s != null){
				i++;
				Debug.Log (i);
				Send(s, "FinishReq:"+score, 1000);  
				Receive(s);
			}
		}
		catch(Exception ex){
			throw ex;
		}
	}
		
	static void Receive(TcpClient socket){
		NetworkStream readStream = socket.GetStream();		
		try {
			StreamReader readerStream = new StreamReader (readStream);
			
			string returnData;
			returnData = readerStream.ReadLine();
			Debug.Log ("Receive: "+returnData);
			
			if(returnData.Contains("LoginAck")){
				Login.OnClickLogin(returnData);
			}				
			else if(returnData.Contains("RankAck")){
				HoverAction.ParseData(returnData);
			}			
		}
		catch (SocketException ex){
			if (ex.SocketErrorCode == SocketError.WouldBlock ||
			    ex.SocketErrorCode == SocketError.IOPending ||
			    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable){
				//socket buffer is probably empty, wait and try again
				Debug.Log ("Thread Sleep!");
				Thread.Sleep(100);
			}
			else//any serious error occurr
				throw ex;  
		}
	}
}