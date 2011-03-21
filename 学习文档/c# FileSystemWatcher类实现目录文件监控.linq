<Query Kind="Expression">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

c# FileSystemWatcher类实现目录文件监控 

--------------------------------------------------------------------------------

2008-11-12 17:32:27　标签：C# 文件监控 目录监控　　　[推送到技术圈] 


版权声明：原创作品，允许转载，转载时请务必以超链接形式标明文章 原始出处 、作者信息和本声明。否则将追究法律责任。http://seanli888.blog.51cto.com/345958/112276 
.Net提供了FileSystemWatcher类用于实现文件监控功能。
FileSystemWatcher位于System.IO名称空间下，使用前需using System.IO;
 
FileSystemWatcher可以监控指定目录下的文件删除，创建，重命名等操作。在其构造函数中可以指定需要监控的目录以及需要监控的特定文件类型。
其Created事件在指定目录下创建文件的时候触发。
然而在实际应用中我们常常需要在需要监控的目录中文件创建完成时才作出相应的处理，而这个事件是在有文件创建的时候就触发的，这在处理大文件的时候就容易出错，因为文件还没创建完成，比如复制一个大文件。这时候我们需要对创建的文件进行访问的时候就会出现无法打开文件的异常。
很多网友都是通过循环检查创建的文件大小变化来判断文件是否完成的，这样带来了很大的系统性能损耗，而且不稳定。
其实我们可以使用一个变通的办法，那就是在创建完大文件的时候创建一个同名的特定类型的小文件，前面我们已经说到FileSystemWatcher类是可以指定监控特定类型的文件的，所以我们就可以安全的处理创建的文件了。
FileSystemWatcher fsw = new FileSystemWatcher(@"D:\aaa");
fsw.Created += new FileSystemEventHandler(fsw_Created);
具体的实现过程很简单，这里就不贴代码了，大家实践实践就可以领会了。 
