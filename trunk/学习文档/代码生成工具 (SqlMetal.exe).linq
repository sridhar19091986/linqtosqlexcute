<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

SqlMetal 命令行工具可为 .NET Framework 的 LINQ to SQL 组件生成代码和映射。通过应用本主题后面出现的选项，可以指示 SqlMetal 执行若干种不同的操作，其中包括：

从数据库生成源代码和映射属性或映射文件。

从数据库生成供自定义使用的中间数据库标记语言 (.dbml) 文件。

从 .dbml 文件生成代码和映射属性或映射文件。

默认情况下， SQLMetal 文件位于 drive:\Program Files\Microsoft SDKs\Windows\vn.nn\bin 下。
C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\SqlMetal.exe

Linq2Sql：使用Sqlmetal.exe
Sqlmetal.exe是微软对Linq2Sql提供的代码生成工具。我们除了使用VS设计器中的Linq To SQL Classes来生成代码外，还可以采用Sqlmetal工具。使用这个工具可以提供更好的灵活性。

工具说明：http://msdn.microsoft.com/zh-cn/library/bb386987.aspx

几个常用的：

/dbml[:文件]
 以 .dbml 扩展名发送输出。不能与 /map 选项一起使用。
 
/code[:文件]
 以源代码形式发送输出。不能与 /dbml 选项一起使用。
 
/map[:文件]
 生成 XML 映射文件而不是属性。不能与 /dbml 选项一起使用。
 


/pluralize
 自动为类和成员名称应用复数或单数形式。
 


/serialization:<选项>
 生成可序列化的类（如：WCF中使用）
 


生成dbml：

SqlMetal /conn:"server='myserver'; database='northwind'" /dbml:northwind.dbml

分开生成类文件和映射：

SqlMetal /conn:"server='myserver'; database='northwind'" /code:northwind.cs /map:northwind.map

序列化成xml：

SqlMetal /conn:"server='myserver'; database='northwind'"  /pluralize /xml:Northwind.xml

通过xml生成cs：

SqlMetal /namespace:nwind  /code:Northwind.cs  Northwind.xml

参考：http://blog.csdn.net/soudog/archive/2007/06/21/1660680.aspx

http://zhidao.baidu.com/question/68911292.html

 


.net工具
http://www.pconline.com.cn/pcedu/empolder/net/cs/0509/699905_1.html

1、代码段编译工具：Snippet complier

2、正则表达式工具：Regulator

3、代码生成工具：CodeSmith

4、编写单元测试工具：Nuit

5、程序集分析检测工具：Reflector

参考：

http://www.cnblogs.com/bluesky521/archive/2008/11/19/1336688.html

http://dev.yesky.com/463/8188463.shtml

 
