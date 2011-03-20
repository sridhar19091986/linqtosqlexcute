<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

知道 > 电脑/网络 > 程序设计 > C#/.NET添加到搜藏已解决 
c# 文件关联问题
 悬赏分：50 - 解决时间：2009-9-3 13:14 
我自己编写一个播放器，可以打开.rm、.wmv等视频文件，我现想双击这些文件将用我的软件打开，且这些文件的图标为我自己编写软件的图标，怎么样实现？我用的是vs2008编译器 

提问者： wcxah1225 - 四级最佳答案这个要改注册表,不过使用程序也可以更改到注册表,如段代码你参考下

 private void button1_Click(object sender, EventArgs e)
		{
			string fileType = textBox1.Text;
			if (!fileType.StartsWith("."))
			{
				fileType = "." + fileType;
				textBox1.Text = fileType;
			}
			 //Application.StartupPath 这个只是路径
			 // Environment.CommandLine ; 这个包含参数
			string appName = Application.ExecutablePath;
		  
			RegistryKey hk_root = Registry.ClassesRoot; //HKEY_CLASSES_ROOT
		   

			//注册自己程序的the ProgID
			if (null == hk_root.OpenSubKey("widebright.mp3")) //The proper format of a ProgID key name is [Vendor or Application].[Component].[Version], separated by periods and with no spaces, as in Word.Document.6. The Version portion is optional but strongly recommended (see Using Versioned PROGIDs).
			{
				RegistryKey progID = hk_root.CreateSubKey("widebright.mp3");
				if (progID == null) 
				{
					MessageBox.Show("创建widebright.mp3 类型ProgID时失败");
					hk_root.Close();
					return;
				}
				//创建（默认）键值，显示给用户看的文件类型描述
				progID.SetValue("", "widebright专用的mp3文件 ", RegistryValueKind.String );

				//文件显示的图标
				RegistryKey defaultIcon = progID.CreateSubKey("DefaultIcon");
				if (defaultIcon == null)
				{
					MessageBox.Show("创建widebright.mp3 类型的DefaultIcon时失败");
					progID.Close();
					hk_root.Close();
					return;
				}
				//创建（默认）键值，指定文件显示图标
				defaultIcon.SetValue("", appName + ",0", RegistryValueKind.String);
				defaultIcon.Close();
			   
				//指定文件动作
				RegistryKey shell = progID.CreateSubKey("shell");
				if (shell == null)
				{
					MessageBox.Show("创建widebright.mp3 类型的shell键时失败");
					progID.Close();
					hk_root.Close();
					return;
				}
				//open 动作------------------------
				RegistryKey open = shell.CreateSubKey("open");
				if (open == null)
				{
					MessageBox.Show("创建widebright.mp3 类型的open键时失败");
					shell.Close();
					progID.Close();
					hk_root.Close();
					return;
				}
							
				RegistryKey command = open.CreateSubKey("command");
				if (command == null)
				{
					MessageBox.Show("创建widebright.mp3 类型的command键时失败");
					open.Close();
					shell.Close();
					progID.Close();
					hk_root.Close();
					return;
				}
				command.SetValue("", "\"" + appName + "\" \"%1\"" , RegistryValueKind.String);
				command.Close();
				open.Close();
				//play动作---------------------
				RegistryKey play = shell.CreateSubKey("play");
				if (play == null)
				{
					MessageBox.Show("创建widebright.mp3 类型的play键时失败");
					shell.Close();
					progID.Close();
					hk_root.Close();
					return;
				}
				play.SetValue("", "在widebright的程序中播放", RegistryValueKind.String);
				command = play.CreateSubKey("command");
				if (command == null)
				{
					MessageBox.Show("创建widebright.mp3 类型的 play->command键时失败");
					play.Close();
					shell.Close();
					progID.Close();
					hk_root.Close();
					return;
				}
				command.SetValue("", "\"" + appName + "\" \"%1\"", RegistryValueKind.String);
				command.Close();
				open.Close();
				progID.Close();

			}

			//修改对应文件类型的默认的关联程序
			//因为在系统里面一把mp3都是已经注册的了，所以这里只是简单修改一下关联

			RegistryKey mp3 = hk_root.OpenSubKey(".mp3", true);
			if (mp3 !=null)
			{
				mp3.SetValue("", "widebright.mp3", RegistryValueKind.String); //指定用我们 上面的ProgID打开mp3文件
				mp3.Close();
			}
		  
			hk_root.Close();
			//通知系统，文件关联已经是作用，不然可能要等到系统重启才看到效果
			SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED,HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
			MessageBox.Show("mp3文件的关联已经修改成功了！");
		} 
