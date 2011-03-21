<Query Kind="Expression">
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
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Security</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Security.Principal</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

				if (a.imei == null)
				{
				    var im = m.Where(e => e.imsi == a.imsi).Where(e => e.imei != null).FirstOrDefault();
				    var imei = n[a.imsi].Where(e => e.imei != null).Select(e => e.imei).SingleOrDefault();
				    if (imei != null)
				    {
				        a.imei = imei;
				    }
				}
									#region 还需要定位imeiType


					var imeitype = mess.imeiType
					    .Where(e => a.imei.IndexOf(e.imei)!=-1)
					    .Where(e=>e.imeiModel.IndexOf ("未知")==-1)
					    .Select(e => e.imeiModel).FirstOrDefault();
					
					

					#endregion
								var n = m.ToLookup(e => e.imsi);
								

			即时通信
			d.Add("14000", (int)trType.IM); //QQ
			d.Add("8040", (int)trType.IM);  //飞信

			//游戏类
			//d.Add("13001", (int)trType.GameCategory );  //？
			d.Add("6666", (int)trType.GameCategory);  //？不确定
			d.Add("6667", (int)trType.GameCategory);  //？不确定
			d = new Dictionary<string, int>();
			d.Add("video", (int)trType.StreamingMedia);
			d.Add("audio", (int)trType.StreamingMedia);
			d.Add("multipart", (int)trType.StreamingMedia);
			
		public enum trType
		{
		    P2P = 0,
		    StreamingMedia = 1,
		    BrowseCategory = 2,
		    MMS = 3,
		    GeneralDownloads = 4,
		    IM = 5,
		    GameCategory = 6,
		    OtherCategory = 7,
		    Null = 100
		}
		
					Dictionary<string, int> d = new Dictionary<string, int>();

			/*
			 * P2P流媒体
			使用P2P技术的流媒体业务。包括PPLive、PPStream、QQLive等。
			GGmusic:ggmusic.3g.cn
			ad.3g.net.cn
			imupdate.3g.cn
			QQmusic:qqmusic
			百灵鸟:lark
			移动随身听:218.200.160.29
			天天动听:wap.ttpod.
			好听:wap.haoting.
			乐天:ltmp3.cn
			 * */

			//P2P流媒体
			d.Add("ggmusic.", (int)trType.P2P);
			d.Add("ad.3g.net.cn", (int)trType.P2P);
			d.Add("qqmusic", (int)trType.P2P);
			d.Add("lark", (int)trType.P2P);
			d.Add("218.200.160.29", (int)trType.P2P);
			d.Add("wap.ttpod.", (int)trType.P2P);
			d.Add("wap.haoting.", (int)trType.P2P);
			d.Add("ltmp3.cn", (int)trType.P2P);

			/*
			流媒体
			包括Flash视频、RTSP流媒体、手机电视等。
			"移动电视:211.136.163.36
			211.136.165.57
			GG视频:video
			万花筒:218.204.254.237"
			**/

			//流媒体
			d.Add("211.136.163.36", (int)trType.StreamingMedia);
			d.Add("211.136.165.57", (int)trType.StreamingMedia);
			d.Add("video", (int)trType.StreamingMedia);
			d.Add("218.204.254.237", (int)trType.StreamingMedia);

			//浏览类
			//d.Add("http", (int)trType.BrowseCategory);

			/*
			普通下载
			FTP等非P2P下载类
			*/
			//普通下载
			d.Add(".mp3", (int)trType.GeneralDownloads);
			d.Add(".avi", (int)trType.GeneralDownloads);
			d.Add(".zip", (int)trType.GeneralDownloads);
			d.Add(".swf", (int)trType.GeneralDownloads);
			d.Add(".3gp", (int)trType.GeneralDownloads);

			/*
			 * 即时通信
			即时的消息交互，不包含语音/视频、游戏等
			"MSN:211.99.191.229
			QQ:211.137
			121.14
			211.139
			221.130
			119.147"
			**/
			//即时通信
			d.Add("211.99.191.229", (int)trType.IM);  //MSN
			d.Add("211.137.", (int)trType.IM);        //QQ
			d.Add("121.14.", (int)trType.IM);        //QQ
			d.Add("211.139.", (int)trType.IM);        //QQ
			d.Add("221.130.", (int)trType.IM);        //QQ
			d.Add("119.147.", (int)trType.IM);        //QQ

			/*
			 * 游戏类
			包括手机QQ游戏等。
			 * */
			//游戏类
			d.Add("farm", (int)trType.GameCategory);

			/*
			其它类
			以上业务之外的为其它业务
			GG财神爷:61.145.124.185
			大智慧:222.73.34.5:12345
			222.73.34.8:12346
			222.73.34.172:12345
			移动证券:211.136.107.88:8080
			同花顺:hexin"
			 * */

								
var k = (from p in mess.IP_stream
         select new
         {
             p.tlli,
             p.FileNum,
             p.imsi,
             p.imei
         }).Distinct();
var m = from p in k
        where p.imei != null || p.imsi != null
        group p by
         new
        {
            p.FileNum,
            p.tlli,

        }
            into tlliK
            select new
            {
                tlliKey = tlliK.Key,
                tlliIMSI = tlliK.Where(e => e.imsi != null).Select(e => e.imsi).FirstOrDefault(),
                tlliIMEI = tlliK.Where(e => e.imei != null).Select(e => e.imei).FirstOrDefault()
            };
var n = m.ToList();
			var n = m.Distinct().AsEnumerable();
			foreach (var filenum in n.Select(e => e.FileNum).Distinct())
			    foreach (var tlli in n.Select(e => e.tlli).Distinct())
			    {
			        msIMEI a = new msIMEI();
			        a.fileNum = filenum;
			        a.tlli = tlli;
			        计算imsi
			        var imsi = n.Where(e => e.tlli == tlli).Where(e => e.imsi != null).FirstOrDefault();
			        if (imsi != null) a.imsi = imsi.imsi;
			        计算imei
			        var tlliList = n.Where(e => e.imsi == a.imsi).Select(e => e.tlli);
			        string imei = null;
			        foreach (var tl in tlliList)
			            imei = n.Where(e => e.tlli == tl).Select(e => e.imei).FirstOrDefault();
			        if (imei != null) a.imei = imei;
			        yield return a;
                }
        }
        public class msIMEI
        {
            public int? fileNum;
            public string tlli;
            public string imsi;
            public string imei;
            public string imeitype;
        }
    }
}
				}

				#region 还需要定位 ciType

				var citype = mess.ciCoverType.Where(e => e.lacCI == down.lacCI).Select(e => e.ciType).FirstOrDefault();
				if (citype != null)
				    down.ciType = citype;

				#endregion
				if (p.link == "Down")
				{
				    down.imsi = p.imsi;
				}
				else
				{
				    var imsi = _imeiTypeClass.MsImeiCollection
				        .Where(e => e.fileNum == down.fileNum)
				        .Where(e => e.tlli == down.tlli)
				        .Where(e => e.imsi != null)
				        .FirstOrDefault();
				    if (imsi != null)
				    {
				        down.imsi = imsi.imsi;
				    }
				}
				
				
				
									.Where(e => e.fileNum == down.fileNum)
					.Where(e => e.tlli  == down.tlli)
					.Where(e => e.imei != null)
					.FirstOrDefault();
					if (imei != null)
					{
					
					
				#region 还需要定位imeiType

				var imeitype = mess.imeiType.Where(e => e.imei == down.imei).Select(e => e.imeiClass).FirstOrDefault();
				if (imeitype != null)
				    down.msType = imeitype;

				#endregion

				uri识别
				if (down.uriType != null && down.trafficType == trType.Null.ToString())
				    down.trafficType = ((trType)ConvertUri2trType(down.uriType)).ToString();
				
				
									var uri = _uriType.mUriCollection.Where(e => e.fileNum == down.fileNum)
					                .Where(e => e.tlli == down.tlli)
					                .Where(e => e.sport == p.tcp_d)
					                .Where(e => e.dport == p.tcp_s).FirstOrDefault();
										response = response.Substring(0, response.LastIndexOf("-") - 1);
										
															var uri = _uriType.mUriCollection.Where(e => e.fileNum == down.fileNum)
					                .Where(e => e.tlli == down.tlli)
					                .Where(e => e.sport == p.tcp_s)
					                .Where(e => e.dport == p.tcp_d).FirstOrDefault();
					
					


				#region 还需要定位trafficType
				
				
				if ()
				{
				    down.protocolType = "rtsp";
				}
				

				var ci = _ciType.CiTypeCollection.Where(e => e.fileNum == down.fileNum).Where(e => e.bvci == down.bvci).FirstOrDefault();
				
				
								if (ci != null)
				{
				
							var m = mess.IP_stream;// Where(e => e.link == "down");
			var _msIMEI = update_msIMEI().ToList();
			var _ciBVCI = update_ciBVCI().ToList();
			var _mUri = update_mUri().ToList();
			
								}

				}
				
				
				#endregion
				
						//public class ciBVCI
		//{
		//    public int? fileNum;
		//    public string lacCi;
		//    public string bvci;
		//    public string cicovertype;
		//}
//    }
//}
				//yield return a;
				
				
				
				#region 还需要定位 ciType

				var citype = mess.ciCoverType.Where(e => e.lacCI == a.lacCi).Select(e => e.ciCoverType1 ).FirstOrDefault();
				if (citype != null)
					a.ciCoverType  = citype;

				#endregion

