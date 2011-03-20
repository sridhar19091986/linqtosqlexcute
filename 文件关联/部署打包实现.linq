<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

利用NET的部署打包很容易实现： 

创建文件关联 
此步骤为“我的记事本”添加文件关联，以便双击   .vbn   文件时启动“我的记事本”应用程序。 

为   Windows   应用程序创建文件关联   

1、在解决方案资源管理器中选择“我的记事本安装程序”项目。在“视图”菜单上指向“编辑器”，然后选择“文件类型”。   
2、在“文件类型编辑器”中选择“目标计算机上的文件类型”节点。在“操作”菜单上，选择“添加文件类型”。   
	将添加一个“新文档类型   #1”节点，而且该节点将打开，以便您重命名。   

3、将“新文档类型   #1”重命名为   Vbn.doc。   
4、在“属性”窗口中，将文件类型的   Extension   属性设置为   vbn。   
5、选择   Command   属性并单击“省略号”()   按钮。在“选择项目中的项”对话框中，定位到“应用程序文件夹”，并选择“主输出来自‘我的记事本’”。   
6、单击“确定”关闭对话框。


C#让文件关联到自己做的WinForm，从文件打开WinForm 
　　刚学c#的时候自己做了个记事本，当时就想双击记事本文件然后打开自己做的记事本。头脑发热，就右键－＞打开方式－＞选择程序－＞自己做的记事本。当然，结果是不行的。后来没搞出来，也就放着了。最近想起来了，查了资料。是要用到获取进程命令行参数。在窗体的Load事件里获取。

	  //窗体的Load事件 
		private void Form1_Load(object sender, EventArgs e)
		{
			string command = Environment.CommandLine;//获取进程命令行参数
			string[] para = command.Split('\"');
			if (para.Length > 3)
			{
				string pathC = para[3];//获取打开的文件的路径
				  //下面就可以自己编写代码使用这个pathC参数了
				//FileStream fs = new FileStream(pathC, FileMode.Open, FileAccess.Read);

			}

		}
