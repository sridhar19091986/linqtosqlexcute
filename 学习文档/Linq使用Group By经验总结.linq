<Query Kind="Expression">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
</Query>

Linq使用Group By经验总结
http://developer.51cto.com  2009-09-08 16:02  佚名  IT168  我要评论() 


这里介绍Linq使用Group By和Count得到每个CategoryID中产品的数量，Linq使用Group By和Count得到每个CategoryID中断货产品的数量等方面。
学习Linq时，经常会遇到Linq使用Group By问题，这里将介绍Linq使用Group By问题的解决方法。

1.计数

var q =  from p in db.Products  group p by p.CategoryID into g  select new {  g.Key,  NumProducts = g.Count()  }; 语句描述：Linq使用Group By和Count得到每个CategoryID中产品的数量。

说明：先按CategoryID归类，取出CategoryID值和各个分类产品的数量。

2.带条件计数

var q =  from p in db.Products  group p by p.CategoryID into g  select new {  g.Key,  NumProducts = g.Count(p => p.Discontinued)  }; 语句描述：Linq使用Group By和Count得到每个CategoryID中断货产品的数量。

说明：先按CategoryID归类，取出CategoryID值和各个分类产品的断货数量。 Count函数里，使用了Lambda表达式，Lambda表达式中的p，代表这个组里的一个元素或对象，即某一个产品。

3.Where限制

var q =  from p in db.Products  group p by p.CategoryID into g  where g.Count() >= 10  select new {  g.Key,  ProductCount = g.Count()  }; 语句描述：根据产品的―ID分组，查询产品数量大于10的ID和产品数量。这个示例在Group By子句后使用Where子句查找所有至少有10种产品的类别。

说明：在翻译成SQL语句时，在最外层嵌套了Where条件。

4.多列(Multiple Columns)

var categories =  from p in db.Products  group p by new  {  p.CategoryID,  p.SupplierID  }  into g  select new  {  g.Key,  g  }; 语句描述：Linq使用Group By按CategoryID和SupplierID将产品分组。

说明：既按产品的分类，又按供应商分类。在by后面，new出来一个匿名类。这里，Key其实质是一个类的对象，Key包含两个Property：CategoryID、SupplierID。用g.Key.CategoryID可以遍历CategoryID的值。

5.表达式(Expression)

var categories =  from p in db.Products  group p by new { Criterion = p.UnitPrice > 10 } into g  select g; 语句描述：Linq使用Group By返回两个产品序列。第一个序列包含单价大于10的产品。第二个序列包含单价小于或等于10的产品。

说明：按产品单价是否大于10分类。其结果分为两类，大于的是一类，小于及等于为另一类。

【编辑推荐】
