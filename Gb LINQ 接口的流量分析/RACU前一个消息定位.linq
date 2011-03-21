<Query Kind="Program">
  <Connection>
    <ID>27d89e94-3492-4017-9db8-6f859e59aa6c</ID>
    <Server>localhost</Server>
    <Persist>true</Persist>
    <Database>IP_Stream</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
		{
//			var n=JishiTongXin_qq_Length ().Sum (e=>e.mLen);
//			n.Dump();
			var a=JishiTongXin_qq_Length ();
			a.Dump ();
//			var b=IP_streams.Where (e=>e.Ip_length !=null).Sum (e=>e.Ip_length );
		}
       

		public  IEnumerable<messageLocatingType> JishiTongXin_qq_Length()
		{
			var m = IP_streams.Where (e=>e.Link =="down").Where (e=>e.Tcp_s =="80");
			var _msIMEI=update_msIMEI().ToList ();
			var _ciBVCI=update_ciBVCI().ToList ();
			var _mUri=update_mUri().ToList ();
			foreach (var p in m)
			{
			     messageLocatingType  down=new messageLocatingType();
				 down.fileNum=p.FileNum ;
				 down.frame =p.PacketNum ;
				 down.bvci =p.Bvci ;
				 down.tlli =p.Tlli ;
				 down.mainType =p.Http_type;
				 down.typeFlag =p.Tcp_s;
				 down.mLen=p.Ip_length;
				 
			var ci=_ciBVCI.Where (e=>e.fileNum ==down.fileNum )
			.Where (e=>e.bvci == down.bvci ).FirstOrDefault ();
			if(ci !=null)
			down.lacCI =ci.lacCi ;
			//还需要定位 ciType
			
			var imei=_msIMEI.Where (e=>e.fileNum ==down.fileNum )
			.Where (e=>e.tlli == down.tlli ).FirstOrDefault ();
			if(imei !=null)
			down.imei =imei.imei  ;
			//还需要定位imeiType
			
			var uri=_mUri.Where (e=>e.fileNum ==down.fileNum )
			.Where (e=>e.tlli   ==down.tlli )
			.Where (e=>e.sport ==p.Tcp_d ).Where (e=>e.dport ==p.Tcp_s ).FirstOrDefault ();
			if(uri !=null)
			down.mainType  +=uri.uri  ;
			//还需要定位streamType
			
			yield return down;
			
			}
		}

		public IEnumerable<msIMEI> update_msIMEI()
		{
		var m = IP_streams.Where (e=>e.Imei  !=null).Where (e=>e.Tlli  !=null);
		foreach(var ms in m.Distinct ())
		{
		msIMEI a=new msIMEI();
		a.fileNum =ms.FileNum ;
		a.tlli=ms.Tlli ;
		a.imei  =ms.Imei ;
		yield return a;
		}
		}
		public class msIMEI
		{
		   public int? fileNum;
		   public string tlli;
		   public string imei;
		}
		public IEnumerable<mUri> update_mUri()
		{
		var m = IP_streams.Where (e=>e.Http_uri!=null).Where (e=>e.Tlli  !=null);
		foreach(var ms in m)
		{
		mUri a=new mUri();
		a.fileNum =ms.FileNum ;
		a.tlli=ms.Tlli ;
		a.sport   =ms.Tcp_s;
		a.dport =ms.Tcp_d ;
		a.uri =ms.Http_uri ==null?"-":ms.Http_uri;
		a.uri +=ms.Wsp_uri==null?"-":ms.Wsp_uri ;
		a.uri +=ms.Http_x_online==null?"-":ms.Http_x_online;
		a.uri +=ms.Http_host ==null?"-":ms.Http_host ;
//		+null? "":ms.Http_host +"::";
		yield return a;
		}
		}
		public class mUri
		{
		   public int? fileNum;
		   public string tlli;
		   public string sport;
		   public string dport;
		   public string uri;
		}
		public IEnumerable<ciBVCI> update_ciBVCI()
		{
		var m = IP_streams.Where (e=>e.Ci !=null).Where (e=>e.Bvci !=null);
		foreach(var ci in m.Distinct ())
		{
		ciBVCI a=new ciBVCI();
		a.fileNum =ci.FileNum ;
		a.bvci=ci.Bvci ;
		a.lacCi =ci.Lac+"-"+ci.Ci ;
		yield return a;
		}
		
		}
		public class ciBVCI
		{
		    public int? fileNum;
		    public string lacCi;
			public string bvci;	
		}
		
		//每1条消息 帧号、小区、业务、终端、包大小
		public class messageLocatingType 
		{
		  //消息帧号，方便回查
		   public int? fileNum;
		   public int? frame;
		   //小区类型
		   public string ciType;
		   public string lacCI;
		   public string bvci;
		   //用户类型，上网本、终端
		   public string msType;
		   public string tlli;
		   public string imei;
//		   public string imsi;
		   //业务类型
		   public string mainType;
		   public string typeFlag;
		   //消息长度
		   public int? mLen;
		}