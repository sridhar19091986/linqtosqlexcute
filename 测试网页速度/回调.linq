<Query Kind="Statements">
  <Connection>
    <ID>114e7c14-f3fa-4b35-9a8b-364c9e7f21bf</ID>
    <Server>localhost</Server>
    <Persist>true</Persist>
    <Database>RNC681</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

百度空间 | 百度首页 | 登录
若水如兰山涧的味道,如水的芬芳主页博客相册个人档案好友
 	
查看文章
 
[转载]经典的讲故事谈.NET委托: 一个C#睡前故事
2009年03月27日 星期五 22:55
紧耦合

从前，在南方一块奇异的土地上，有个工人名叫彼得，他非常勤奋，对他的老板总是百依百顺。但是他的老板是个吝啬的人，从不信任别人，坚决要求随时知道彼得的工作进度，以防止他偷懒。但是彼得又不想让老板呆在他的办公室里站在背后盯着他，于是就对老板做出承诺：无论何时，只要我的工作取得了一点进展我都会及时让你知道。彼得通过周期性地使用“带类型的引用”(原文为：“typed reference” 也就是delegate？？)“回调”他的老板来实现他的承诺，如下：

class Worker {
 public void Advise(Boss boss) { _boss = boss; }
 public void DoWork() {
 Console.WriteLine(“工作: 工作开始”);
 if( _boss != null ) _boss.WorkStarted();

 Console.WriteLine(“工作: 工作进行中”);
 if( _boss != null ) _boss.WorkProgressing();

 Console.WriteLine("“工作: 工作完成”");
 if( _boss != null ) {
 int grade = _boss.WorkCompleted();
 Console.WriteLine(“工人的工作得分＝” + grade);
 }
}
private Boss _boss;
}

class Boss {
 public void WorkStarted() { }
 public void WorkProgressing() { }
 public int WorkCompleted() {
 Console.WriteLine(“时间差不多！”);
 return 2;
 }
}

class Universe {
 static void Main() {
 Worker peter = new Worker();
 Boss boss = new Boss();
 peter.Advise(boss);
 peter.DoWork();

 Console.WriteLine(“Main: 工人工作完成”);
 Console.ReadLine();
 }
}

接口

　　现在，彼得成了一个特殊的人，他不但能容忍吝啬的老板，而且和他周围的宇宙也有了密切的联系，以至于他认为宇宙对他的工作进度也感兴趣。不幸的是，他必须也给宇宙添加一个特殊的回调函数Advise来实现同时向他老板和宇宙报告工作进度。彼得想要把潜在的通知的列表和这些通知的实现方法分离开来，于是他决定把方法分离为一个接口：

interface IWorkerEvents {
 void WorkStarted();
 void WorkProgressing();
 int WorkCompleted();
}

class Worker {
 public void Advise(IWorkerEvents events) { _events = events; }
 public void DoWork() {
 Console.WriteLine(“工作: 工作开始”);
 if( _events != null ) _events.WorkStarted();

 Console.WriteLine(“工作: 工作进行中”);
 if(_events != null ) _events.WorkProgressing();

 Console.WriteLine("“工作: 工作完成”");
 if(_events != null ) {
 int grade = _events.WorkCompleted();

 Console.WriteLine(“工人的工作得分＝” + grade);
 }
 }
 private IWorkerEvents _events;
}

class Boss : IWorkerEvents {
 public void WorkStarted() { }
 public void WorkProgressing() { }
 public int WorkCompleted() {
 Console.WriteLine(“时间差不多！”);
 return 3;
 }
}

委托

　　不幸的是，每当彼得忙于通过接口的实现和老板交流时，就没有机会及时通知宇宙了。至少他应该忽略身在远方的老板的引用，好让其他实现了IWorkerEvents的对象得到他的工作报告。（”At least he'd abstracted the reference of his boss far away from him so that others who implemented the IWorkerEvents interface could be notified of his work progress” 原话如此，不理解到底是什么意思 ）

　　他的老板还是抱怨得很厉害。“彼得！”他老板吼道，“你为什么在工作一开始和工作进行中都来烦我？！我不关心这些事件。你不但强迫我实现了这些方法，而且还在浪费我宝贵的工作时间来处理你的事件，特别是当我外出的时候更是如此！你能不能不再来烦我？”

　　于是，彼得意识到接口虽然在很多情况都很有用，但是当用作事件时，“粒度”不够好。他希望能够仅在别人想要时才通知他们，于是他决定把接口的方法分离为单独的委托，每个委托都像一个小的接口方法：

delegate void WorkStarted();
delegate void WorkProgressing();
delegate int WorkCompleted();

class Worker {
 public void DoWork() {
 Console.WriteLine(“工作: 工作开始”);
 if( started != null ) started();

 Console.WriteLine(“工作: 工作进行中”);
 if( progressing != null ) progressing();

 Console.WriteLine("“工作: 工作完成”");
 if( completed != null ) {
 int grade = completed();
 Console.WriteLine(“工人的工作得分＝” + grade);
 }
 }
 public WorkStarted started;
 public WorkProgressing progressing;
 public WorkCompleted completed;
}

class Boss {
 public int WorkCompleted() {
 Console.WriteLine("Better...");
 return 4;
}
}

class Universe {
 static void Main() {
 Worker peter = new Worker();
 Boss boss = new Boss();
 peter.completed = new WorkCompleted(boss.WorkCompleted);
 peter.DoWork();

 Console.WriteLine(“Main: 工人工作完成”);
 Console.ReadLine();
 }
}

静态监听者

　　这样，彼得不会再拿他老板不想要的事件来烦他老板了，但是他还没有把宇宙放到他的监听者列表中。因为宇宙是个包涵一切的实体，看来不适合使用实例方法的委托（想像一下，实例化一个“宇宙”要花费多少资源…..），于是彼得就需要能够对静态委托进行挂钩，委托对这一点支持得很好：

class Universe {
 static void WorkerStartedWork() {
 Console.WriteLine("Universe notices worker starting work");
 }

 static int WorkerCompletedWork() {
 Console.WriteLine("Universe pleased with worker's work");
 return 7;
 }

 static void Main() {
 Worker peter = new Worker();
 Boss boss = new Boss();
 peter.completed = new WorkCompleted(boss.WorkCompleted);
 peter.started = new WorkStarted(Universe.WorkerStartedWork);
 peter.completed = new WorkCompleted(Universe.WorkerCompletedWork);
 peter.DoWork();

 Console.WriteLine(“Main: 工人工作完成”);
 Console.ReadLine();
 }
}

事件

　　不幸的是，宇宙太忙了，也不习惯时刻关注它里面的个体，它可以用自己的委托替换了彼得老板的委托。这是把彼得的Worker类的的委托字段做成public的一个无意识的副作用。同样，如果彼得的老板不耐烦了，也可以决定自己来激发彼得的委托（真是一个粗鲁的老板）：

// Peter's boss taking matters into his own hands
if( peter.completed != null ) peter.completed();

　　彼得不想让这些事发生，他意识到需要给每个委托提供“注册”和“反注册”功能，这样监听者就可以自己添加和移除委托，但同时又不能清空整个列表也不能随意激发彼得的事件了。彼得并没有来自己实现这些功能，相反，他使用了event关键字让C#编译器为他构建这些方法：

class Worker {
...
 public event WorkStarted started;
 public event WorkProgressing progressing;
 public event WorkCompleted completed;
}

　　彼得知道event关键字在委托的外边包装了一个property，仅让C#客户通过+= 和 -=操作符来添加和移除，强迫他的老板和宇宙正确地使用事件。

static void Main() {
 Worker peter = new Worker();
 Boss boss = new Boss();
 peter.completed += new WorkCompleted(boss.WorkCompleted);
 peter.started += new WorkStarted(Universe.WorkerStartedWork);
 peter.completed += new WorkCompleted(Universe.WorkerCompletedWork);
 peter.DoWork();

 Console.WriteLine(“Main: 工人工作完成”);
 Console.ReadLine();
}

“收获”所有结果

　　到这时，彼得终于可以送一口气了，他成功地满足了所有监听者的需求，同时避免了与特定实现的紧耦合。但是他注意到他的老板和宇宙都为它的工作打了分，但是他仅仅接收了一个分数。面对多个监听者，他想要“收获”所有的结果，于是他深入到代理里面，轮询监听者列表，手工一个个调用：

public void DoWork() {
 ...
 Console.WriteLine("“工作: 工作完成”");
 if( completed != null ) {
 foreach( WorkCompleted wc in completed.GetInvocationList() ) {
 int grade = wc();
 Console.WriteLine(“工人的工作得分＝” + grade);
 }
 }
}

异步通知：激发 & 忘掉

　　同时，他的老板和宇宙还要忙于处理其他事情，也就是说他们给彼得打分所花费的事件变得非常长：

class Boss {
 public int WorkCompleted() {
 System.Threading.Thread.Sleep(3000);
 Console.WriteLine("Better..."); return 6;
 }
}

class Universe {
 static int WorkerCompletedWork() {
 System.Threading.Thread.Sleep(4000);
 Console.WriteLine("Universe is pleased with worker's work");
 return 7;
 }
 ...
}

　　很不幸，彼得每次通知一个监听者后必须等待它给自己打分，现在这些通知花费了他太多的工作事件。于是他决定忘掉分数，仅仅异步激发事件：

public void DoWork() {
 ...
 Console.WriteLine("“工作: 工作完成”");
 if( completed != null ) {
 foreach( WorkCompleted wc in completed.GetInvocationList() )
 {
 wc.BeginInvoke(null, null);
 }
 }
}

异步通知：轮询

　　这使得彼得可以通知他的监听者，然后立即返回工作，让进程的线程池来调用这些代理。随着时间的过去，彼得发现他丢失了他工作的反馈，他知道听取别人的赞扬和努力工作一样重要，于是他异步激发事件，但是周期性地轮询，取得可用的分数。

public void DoWork() {
 ...
 Console.WriteLine("“工作: 工作完成”");
 if( completed != null ) {
 foreach( WorkCompleted wc in completed.GetInvocationList() ) {
 IAsyncResult res = wc.BeginInvoke(null, null);
 while( !res.IsCompleted ) System.Threading.Thread.Sleep(1);
 int grade = wc.EndInvoke(res);
 Console.WriteLine(“工人的工作得分＝” + grade);
 }
 }
}

异步通知：委托

　　不幸地，彼得有回到了一开始就想避免的情况中来，比如，老板站在背后盯着他工作。于是，他决定使用自己的委托作为他调用的异步委托完成的通知，让他自己立即回到工作，但是仍可以在别人给他的工作打分后得到通知：

 public void DoWork() {
 ...
 Console.WriteLine("“工作: 工作完成”");
 if( completed != null ) {
 foreach( WorkCompleted wc in completed.GetInvocationList() ) {
 wc.BeginInvoke(new AsyncCallback(WorkGraded), wc);
 }
 }
 }

 private void WorkGraded(IAsyncResult res) {
 WorkCompleted wc = (WorkCompleted)res.AsyncState;
 int grade = wc.EndInvoke(res);
 Console.WriteLine(“工人的工作得分＝” + grade);
 }

宇宙中的幸福

　　彼得、他的老板和宇宙最终都满足了。彼得的老板和宇宙可以收到他们感兴趣的事件通知，减少了实现的负担和非必需的往返“差旅费”。彼得可以通知他们，而不管他们要花多长时间来从目的方法中返回，同时又可以异步地得到他的结果。彼得知道，这并不*十分*简单，因为当他异步激发事件时，方法要在另外一个线程中执行，彼得的目的方法完成的通知也是一样的道理。但是，迈克和彼得是好朋友，他很熟悉线程的事情，可以在这个领域提供指导。

　　他们永远幸福地生活下去……<完>


类别：asp.net(c#) |  | 添加到搜藏 | 分享到i贴吧 | 浏览(86) | 评论 (0)
 
上一篇：verify your "SMTP" and "smtp_p...    下一篇：生きれる
 
相关文章：
•	讲故事谈.NET委托:一个C#睡前故...　　　　　　　　　	•	讲故事谈.NET委托:一个C#睡前故...
•	讲故事谈 皮特的故事 .NET委托:(...　　　　　　　　　	•	讲故事谈 皮特的故事 .NET委托:(...
•	讲故事谈.NET委托:一个C#睡前故...　　　　　　　　　	•	睡前讲故事
•	睡前讲故事--The Wolf And The L...　　　　　　　　　	•	睡前让爸爸给我讲故事吧
•	门徒|睡前讲故事.　　　　　　　　　	 	 
 
最近读者：
	登录后，您就出现在这里。					
 	 	ELLApurple	天堂人间晓	胡冰玲	烟雨寒潭	
 
网友评论：
发表评论：
内　容：	
 	    

 	 	 

©2010 Baidu
