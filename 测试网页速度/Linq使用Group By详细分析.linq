<Query Kind="Expression">
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
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

Linq使用Group By详细分析
http://developer.51cto.com  2009-09-08 15:56  佚名  博客园  我要评论(0)
这里介绍Linq使用Group By按CategoryID划分产品、Linq使用Group By和Max查找每个CategoryID的最高单价和Linq使用Group By和Min查找每个CategoryID的最低单价等方面。
Linq有很多值得学习的地方，这里我们主要介绍Linq使用Group By，包括介绍Linq简单形式等方面。

1.简单形式：

var q =  
from p in db.Products  
group p by p.CategoryID into g  
select g; 
语句描述：Linq使用Group By按CategoryID划分产品。

说明：from p in db.Products 表示从表中将产品对象取出来。group p by p.CategoryID into g表示对p按CategoryID字段归类。其结果命名为g，一旦重新命名，p的作用域就结束了，所以，最后select时，只能select g。

2.最大值

var q =  
from p in db.Products  
group p by p.CategoryID into g  
select new {  
g.Key,  
MaxPrice = g.Max(p => p.UnitPrice)  
}; 
语句描述：Linq使用Group By和Max查找每个CategoryID的最高单价。

说明：先按CategoryID归类，判断各个分类产品中单价最大的Products。取出CategoryID值，并把UnitPrice值赋给MaxPrice。

3.最小值

var q =  
from p in db.Products  
group p by p.CategoryID into g  
select new {  
g.Key,  
MinPrice = g.Min(p => p.UnitPrice)  
}; 
语句描述：Linq使用Group By和Min查找每个CategoryID的最低单价。

说明：先按CategoryID归类，判断各个分类产品中单价最小的Products。取出CategoryID值，并把UnitPrice值赋给MinPrice。

4.平均值

var q =  
from p in db.Products  
group p by p.CategoryID into g  
select new {  
g.Key,  
AveragePrice = g.Average(p => p.UnitPrice)  
}; 
语句描述：Linq使用Group By和Average得到每个CategoryID的平均单价。

说明：先按CategoryID归类，取出CategoryID值和各个分类产品中单价的平均值。

5.求和

var q =  
from p in db.Products  
group p by p.CategoryID into g  
select new {  
g.Key,  
TotalPrice = g.Sum(p => p.UnitPrice)  
}; 