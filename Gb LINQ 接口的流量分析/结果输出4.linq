<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Namespace></Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var a=from p in MLocatingTypes
//      where p.Imei .StartsWith("8")
      group p by p.TrafficType   into tt
	  let allLen= MLocatingTypes.Sum (e=>e.MLen )
	  select new
	  {
	  	  mKey=tt.Key ,
		  
		    全网=(from q in tt
			let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending(e=>e.abcSum).Take (10),
			
//	        城中村=(tt.Where (e=>e.CiConverType =="城中村").Sum (e=>e.MLen )+0.0)/allLen,
	  
			城中村=(from q in tt.Where (e=>e.CiConverType =="城中村")
			let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending(e=>e.abcSum).Take (10),
//	        道路=(tt.Where (e=>e.CiConverType =="道路").Sum (e=>e.MLen )+0.0)/allLen,
	
			道路=(from q in tt.Where (e=>e.CiConverType =="道路")
			let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending  (e=>e.abcSum ).Take (10),
//		    高层写字楼=(tt.Where (e=>e.CiConverType =="高层写字楼").Sum (e=>e.MLen )+0.0)/allLen,
	     
			高层写字楼=(from q in tt.Where (e=>e.CiConverType =="高层写字楼")
						let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending (e=>e.abcSum ).Take (10),
//		    工业园区=(tt.Where (e=>e.CiConverType =="工业园区").Sum (e=>e.MLen )+0.0)/allLen,
	       
			工业园区=(from q in tt.Where (e=>e.CiConverType =="工业园区")
						let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending  (e=>e.abcSum).Take (10),
//	 	    集团客户=(tt.Where (e=>e.CiConverType =="集团客户").Sum (e=>e.MLen )+0.0)/allLen,
	      
			集团客户=(from q in tt.Where (e=>e.CiConverType =="集团客户")
						let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending  (e=>e.abcSum ).Take (10),
//			居民区=(tt.Where (e=>e.CiConverType =="居民区").Sum (e=>e.MLen )+0.0)/allLen,
	     
			居民区=(from q in tt.Where (e=>e.CiConverType =="居民区")
						let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending  (e=>e.abcSum ).Take (10),
//			商业中心=(tt.Where (e=>e.CiConverType =="商业中心").Sum (e=>e.MLen )+0.0)/allLen,
	   
			商业中心=(from q in tt.Where (e=>e.CiConverType =="商业中心")
						let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending  (e=>e.abcSum ).Take (10),
//			星级酒店=(tt.Where (e=>e.CiConverType =="星级酒店").Sum (e=>e.MLen )+0.0)/allLen
	      
			星级酒店=(from q in tt.Where (e=>e.CiConverType =="星级酒店")
						let t2=q.UriType.Substring (q.UriType.LastIndexOf ("-"))
			group q by t2 into qq
			select new {qq.Key ,abcSum=qq.Sum (e=>e.MLen)}).OrderByDescending  (e=>e.abcSum ).Take (10),
	  };
	  
	  a.OrderByDescending (e=>e.mKey).Dump ();