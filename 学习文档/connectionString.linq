<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
</Query>



DataConnectionDialog dialog = new DataConnectionDialog();
dialog.DataSources.Add(DataSource.SqlDataSource);
dialog.SelectedDataProvider = DataProvider.SqlDataProvider;

//dialog.DataSources.Add(DataSource.OdbcDataSource);

//dialog.SelectedDataSource = DataSource.OdbcDataSource;
//dialog.SelectedDataProvider = DataProvider.OdbcDataProvider;

dialog.StartPosition = FormStartPosition.CenterScreen;
dialog.Title = "Connect to SqlServer";
dialog.ConnectionString = "server=127.0.0.1;database=YarnNew;user id=sa;password=aaa;";

if (DataConnectionDialog.Show(dialog, this) == DialogResult.OK)
{
	MessageBox.Show(dialog.ConnectionString);
} 


Data Connection Dialog
	  winform程序，在发布时往往需要更改数据库连接字符串，而数据库采用附加的方式配置数据库连接字符串，可以使用VS2005的配置界面来处理 
引用C:\Program Files\Microsoft Visual Studio 8\Common7\IDE 里面的Microsoft.Data.ConnectionUI.Dialog.dll。
using Microsoft.Data.ConnectionUI;
		private void button1_Click(object sender, EventArgs e)
		{
			DataConnectionDialog dia=new DataConnectionDialog ();

			dia.DataSources.Add(DataSource.SqlDataSource);
			dia.SelectedDataProvider = DataProvider.SqlDataProvider;
			if (DataConnectionDialog.Show(dia, this) == DialogResult.OK )
			{
				string myConnect = dia.ConnectionString;
				MessageBox.Show(myConnect);
			 }
		   
		}

为了保存数据库字符串，我们需要更改VS的配置文档（程序的connectString在配置文件内）。.exe文件的配置文件为.exe.config；在同一目录下
XmlDocument myDoc = new XmlDocument();
				XmlElement myXmlElement;
				myDoc.Load(Application.ExecutablePath + ".config");
				XmlNode myNode = myDoc.SelectSingleNode("//connectionStrings");
				myXmlElement = (XmlElement)myNode.SelectSingleNode("//add [@name='NXY.Properties.Settings.nxyInfoConnectionString']");
				myXmlElement.SetAttribute("connectionString", myConnect);
				myDoc.Save(Application.ExecutablePath + ".config");

相对应的XML文件
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<configSections>
	</configSections>
	<connectionStrings>
		<add name="NXY.Properties.Settings.nxyInfoConnectionString" connectionString="Data Source=SC440;Initial Catalog=nxyInfo;Persist Security Info=True;User ID=sa;Password=sa"
			providerName="System.Data.SqlClient" />
	</connectionStrings>
</configuration>