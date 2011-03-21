<Query Kind="Program">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	string tbResult = ""; 
	string txtCommand="192.168.1.1";
	　ProcessStartInfo start = new ProcessStartInfo("Ping.exe"); //设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
	　//如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
	　start.Arguments = txtCommand; //设置命令参数
	　start.CreateNoWindow = true; //不显示dos命令行窗口
	　start.RedirectStandardOutput = true; //
	　start.RedirectStandardInput = true; //
	　start.UseShellExecute = false; //是否指定操作系统外壳进程启动程序
	　Process p=Process.Start(start); 
	　StreamReader reader = p.StandardOutput; //截取输出流
	　string line = reader.ReadLine(); //每次读取一行
	　while (!reader.EndOfStream)
	　{
	　tbResult+=(line +" "); 
	　line = reader.ReadLine(); 
	　}
	　p.WaitForExit(); //等待程序执行完退出进程
	　p.Close(); //关闭进程
	　reader.Close(); //关闭流(责任编辑：admin) 
	tbResult.Dump();
}

// Define other methods and classes here
