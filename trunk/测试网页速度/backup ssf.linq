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

ConnDb.IDBAccess Acc = ConnDb.DBAccessFactory.Create(ConnDb.DBType.Access);

 bool liveChange;
 	  toolStripTextBox1.Text = matches.live_Table.Select(p => p.s_date).Min().ToString();
	  			forFileLength(new DirectoryInfo(appPath));//遍历文件按路径下文件生成树
			Thread trd = new Thread(new ThreadStart(this.LoadTree));
			trd.IsBackground = true;
			trd.Start();
			this.LoadTree(treeView1);
			this.LoadTree(treeView5);
			LoadDataToTree.LoadTree(treeView5);
			
			  treeView5.Nodes.Add(loaddatatree.TreeViewMatch("type"));
			  			loaddatatree.TreeViewMatch(treeView5,"type")
						
								  textBox1.Text = "http://live2.7m.cn/lbpk_ft.aspx?view=all&amp;match=&amp;line=no&amp;ordType=";
liveChange = false;
	   liveChange = false;
	   				Thread.Sleep(2000);
				while (insertComplete == false)
				{
				    Application.DoEvents();
				}
							this.webBrowser1 = new WebBrowser();
			this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
			
 webBrowser1不能自动销毁对象
			DataTable dt = TableHtmlAgilityPack.GetHtmlTable(html, "//table[@id='live_Table']", 0);
			return TableHtmlAgilityPack.InsertLiveHtmlTableToDB(dt);
			DataTable dt = TableHtmlAgilityPack.GetHtmlTable(html, "//table[@id='result_tb']", 1);
			return TableHtmlAgilityPack.InsertLastHtmlTableToDB(dt);
			DialogResult result; //Messagebox所属于的类
			result = MessageBox.Show(this, "YesOrNo", "你确定要执行查询吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)//Messagebox返回的值
			{
			    try
			    {
			        string strdp = DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒");
			        StreamWriter writer = new StreamWriter(appPath + "\\sqlLog\\sqlCode" + strdp + ".txt");
			        writer.Write(richTextBox2.Text);
			        writer.Close();
			        DataTable dt = Acc.RunQuery(richTextBox2.Text);
			        this.dataGridView1.AutoGenerateColumns = true;
			        this.dataGridView1.DataSource = dt.DefaultView;
			        this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			        this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			        this.dataGridView1.BorderStyle = BorderStyle.Fixed3D;
			        this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
			    }
			    catch { }
			}


		运用List容器的方法生成treeView的各级节点

		DataTable dt_treeView;

		/ <summary>
		/ 遍历文件夹下文件.并组成树
		/ </summary>
		/ <param name="directory">文件夹路径</param>
		private void forFileLength(DirectoryInfo directory)
		{
		    DirectoryInfo[] directorys = directory.GetDirectories();//获取所给目录的所有文件夹
		    FileInfo[] files;
		    TreeNode tnRoot = new TreeNode("Soccer Score Forecast");//生成根节点
		    treeView2.Nodes.Add(tnRoot);
		    foreach (DirectoryInfo di in directorys)//遍历问加价
		    {
		        if (di.Name == "sqlLog--" || di.Name == "sqlCmd")
		        {
		            bool bHasAspx = false;
		            files = di.GetFiles();//获取文件夹内所有文件
		            foreach (FileInfo file in files)
		            {
		                bHasAspx = true;
		                break;
		            }
		            if (bHasAspx)
		            {
		                TreeNode tnParent = new TreeNode(di.Name);
		                tnRoot.Nodes.Add(tnParent);//添加文件夹节点
		                files = di.GetFiles();
		                foreach (FileInfo file in files)//遍历文件夹下所有文件
		                {
		                    TreeNode tnSon = new TreeNode(file.Name);
		                    tnSon.Text = file.Name;
		                    tnParent.Nodes.Add(tnSon);//添加文件节点
		                }
		            }
		        }
		    }
		}
			if (c.Node.Level != 2) { return; }
			string selectMatch = c.Node.Text.ToString();
			richTextBox3.Text = selectMatch; 
			string[] ar = selectMatch.Split(Convert.ToChar(','));
			int id = Int32.Parse(ar[0].ToString());

			DataClassesMatchDataContext matches = new DataClassesMatchDataContext();
			var l = matches.live_Table_lib.Where(o => o.live_table_lib_id == id).First();

			var top20h = matches.result_tb_lib.Where(e => e.home_team_big == l.home_team_big || e.away_team_big == l.away_team_big);
			var top20a = matches.result_tb_lib.Where(e => e.home_team_big == l.away_team_big || e.away_team_big == l.home_team_big);
			var top20 = top20h.Union(top20a).OrderByDescending(e => e.match_time).Take(40);

			dataGridView2.DataSource = top20;

			var r = matches.match_analysis_result.Where(o => o.live_table_lib_id == id).FirstOrDefault();
			dataGridView3.DataSource = r;

			List<MatchPoint<int>> realmatch = DrawMatchScore.RealMatch(id);
			DrawMatchScore.DrawCoordinate(pictureBox1, realmatch);

			var jz = matches.result_tb_lib.Where(e => e.home_team_big == l.home_team_big && e.away_team_big == l.away_team_big).OrderByDescending(e => e.match_time);
			dataGridView3.DataSource = jz;
			DataTable dt = Acc.GetColumns();
			dataGridView1.DataSource = dt.DefaultView;
			LoadDataToTree l = new LoadDataToTree();
			loaddatatree.LoadTyepTree(treeView5);
			LoadTree();
			loaddatatree.LoadTypeTree();
			treeView5.Nodes.Add(loaddatatree.TreeViewMatch);

			loaddatatree.TreeViewMatch(treeView5, "type");
			treeView5.Nodes.Add(loaddatatree.TreeViewMatch("type"));
			dt_treeView.Clear();
			dt_treeView = Acc.RunQuery("select  * from  MatchToday");
			LoadTree();

			UpdateSqldata.UpdateLastMatch(); 
 UpdateSqldata.UpdateTodayMatch();
			FileStream fs = new FileStream(appPath + "\\urlLog\\liveOddslistTable.txt", FileMode.Create);//创建一个Log文件
			StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
			StreamReader reader = new StreamReader(appPath + "\\urlLog\\liveOddsurlCode.txt");//读入Log文件
			string urlCode = richTextBox4.Text;
			LiveOdds md = new LiveOdds(richTextBox4.Text);
			this.listView1.View = View.Details;
			int j = 0;
			listView1.Items.Clear();
			toolStripProgressBar1.Maximum = md.liveodds.Count;
			foreach (string m in md.liveodds)
			{
			    toolStripProgressBar1.Value = j; j++; Application.DoEvents();
			    string[] ar = m.Split(Convert.ToChar(','));
			    ListViewItem lv = new ListViewItem(ar[0]);
			    for (int i = 1; i < ar.Length; i++)
			    {
			        lv.SubItems.Add(ar[i]);
			    }
			    listView1.Items.Add(lv);
			}

			reader.Close();
			insertLiveOdds();

		正则表达式和普通替换的结合
		private string CleanHtml(string html)
		{

		    html = Regex.Replace(html, @"<TD[^>]*?>", " ", RegexOptions.IgnoreCase);
		    html = html.Replace("<SPAN>", " ");
		    html = html.Replace("</SPAN>", " ");
		    html = html.Replace("<STRONG>", " ");
		    html = html.Replace("</STRONG>", " ");
		    html = html.Replace("\"", "");
		    html = html.Replace("A href=javascript:", " ");
		    html = html.Replace("</A>", " ");
		    html = html.Replace("<script", " ");
		    html = html.Replace("</script>", " ");
		    html = html.Replace("<BR>", " ");
		    html = html.Replace("<A class=main_team href=javascript:", " ");
		    html = html.Replace(";", " ");
		    html = html.Replace("<", " ");
		    html = html.Replace(">", " ");

		    return html;
		}
		private void insertLiveOdds()
		{
			foreach (ListViewItem lvItem in listView1.Items)
			{
			    if (lvItem.SubItems.Count > 12)
			    {
			        string strMatch = lvItem.SubItems[2].Text;
			        strMatch = CleanHtml(strMatch);

			        string strDate = DateTime.Now.ToString("yyyy年MM月dd");

			        string strTeam = lvItem.SubItems[3].Text;
			        strTeam = CleanHtml(strTeam);

			        string strLiveOdds = lvItem.SubItems[4].Text + lvItem.SubItems[5].Text + lvItem.SubItems[6].Text +
			            lvItem.SubItems[7].Text + lvItem.SubItems[8].Text + lvItem.SubItems[9].Text + lvItem.SubItems[10].Text +
			             lvItem.SubItems[11].Text + lvItem.SubItems[12].Text;
			        strLiveOdds = CleanHtml(strLiveOdds);

			        string insertNow = "insert into LiveOdds([Match], [DateTTime],[Team],[LiveOdds]) values('" +
			                                       strMatch + "','" + strDate + "','" + strTeam + "','" + strLiveOdds + "')";
			        Acc.RunNoQuery(insertNow);
			    }
			}
		}
		运用递归过程遍历treeView的所有的节点
		public void selectNode(TreeNodeCollection tc)
		{
		    foreach (TreeNode TNode in tc)
		    {
		        if (TNode.Level == 2)
		        {
		            TNode.Parent.Expand(); TNode.EnsureVisible(); TNode.Parent.EnsureVisible();
		            Application.DoEvents();//调整窗口焦点   TNode.ForeColor = Color.Blue;
		            treeView1.SelectedNode = TNode;//treeView选中事件
		            while (!copyBmpComplete)
		            {
		                Application.DoEvents();//等待程序执行完成
		            }
		            Thread.Sleep(100);//线程休息100毫秒
		            TNode.Parent.Collapse();//收缩tree
		            TNode.ForeColor = Color.Red;
		        }
		        Application.DoEvents();
		        selectNode(TNode.Nodes);
		    }
		}
		获取最大记录数放到窗口
		private void getMatchid()
		{
			DataTable dt = Acc.RunQuery("select top 1 DDate,id from MatchData order by id desc");
			toolStripLabel2.Text = dt.Rows[0][1].ToString();
		}
			LoadDataToTree l = new LoadDataToTree();
			treeView5 = loaddatatree.TreeViewMatch("time");

			loaddatatree.TreeViewMatch(treeView5, "time");

		运用List容器获取DataSet容器所有种类
		然后把List容器中的一一列出寻找在DataSet容器中相同属性的内容
		运用List生成多级树
			treeView5.Nodes.Add(loaddatatree.TreeViewMatch("time"));
		string strTableRow;
	  运用递归过程遍历treeView的所有的节点
		public void GetNode(TreeNodeCollection tc)
		{
		    foreach (TreeNode TNode in tc)
		    {
		        if (TNode.Level > 3)
		        {
		            string strTT = TNode.Text.ToString().Trim();
		            if (strTT.IndexOf("#") != -1)
		            {
		                strTT = strTT.Replace("#text", "");
		                strTableRow += strTT + ",";
		            }
		            if (TNode.Text.ToString().Trim().IndexOf("BODY") != -1 && strTableRow != null)
		            {
		                listBox3.Items.Add(strTableRow);
		                strTableRow = null;
		            }
		        }
		        GetNode(TNode.Nodes);
		    }
		}
		把listBox的内容插入数据库的方法
		private void insertLiveOdds2()
		{
		    foreach (string m in listBox3.Items)
		    {
		        Application.DoEvents();
		        string[] ar = m.Split(Convert.ToChar(','));
		        int s = ar.Length;
		        if (s > 5 && s < 50)
		        {
		            string strMatch = ar[0].ToString();
		            string strDate = DateTime.Now.ToString("yyyy年MM月dd");
		            string strTime = ar[1].ToString();
		            string strHomeTeam = ar[3].ToString().Trim();
		            string strAwayTeam = ar[4].ToString().Trim();
		            string strLiveOdds = null;
		            for (int i = 5; i < s; i++)
		            {
		                strLiveOdds += ar[i].ToString().Trim() + ",";
		            }
		            string insertNow = "insert into LiveOddsTeamName([Match], [DDate],[TTime],[HomeTeam],[AwayTeam],[LiveOdds]) values('" +
		                       strMatch + "','" + strDate + "','" + strTime + "','" +
		                       strHomeTeam + "','" + strAwayTeam + "','" + strLiveOdds + "')";

		            if (strTime.IndexOf(":") != -1)
		            {
		                Acc.RunNoQuery(insertNow);
		            }
		        }
		    }
		}
		bool copyBmpComplete;
		GDI+绘图中把窗口的图像转成图像文件
		private void copyBmpToFile()
		{
		    try
		    {
		        Application.DoEvents();
		        string strDate = DateTime.Now.ToString("yyyy-MM-dd");
		        string[] ar = toolStripStatusLabel3.Text.Split(Convert.ToChar(","));
		        string strMatch = ar[2].ToString() + "_" + ar[0].ToString();
		        strDate = strDate + "_" + strMatch;
		        Rectangle rect = Screen.AllScreens[0].Bounds;
		        Image image = new Bitmap(rect.Width, rect.Height);
		        Graphics g = Graphics.FromImage(image);
		        g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(rect.Width, rect.Height));
		        Rectangle rect2 = this.Bounds;
		        Image image2 = new Bitmap(rect2.Width, rect2.Height);
		        Graphics g2 = Graphics.FromImage(image2);
		        g2.DrawImage(image, new Rectangle(0, 0, rect2.Width, rect2.Height), rect2, GraphicsUnit.Pixel);
		        string strBmpFile = appPath + "\\forecaseHistoryView\\" + strDate + ".bmp";
		        先删除重复的文件
		        if (File.Exists(strBmpFile))
		        {
		            File.Delete(strBmpFile);
		            Application.DoEvents();
		        }
		        再创建文件直到成功，那么 copyBmpComplete 才等于真
		        image2.Save(strBmpFile, System.Drawing.Imaging.ImageFormat.Bmp);
		        while (!File.Exists(strBmpFile))
		        {
		            Application.DoEvents();
		        }
		        copyBmpComplete = true;
		    }
		    catch { }
		}
		运用递归过程遍历treeView的所有的节点
		public void GetNode2(TreeNodeCollection tc)
		{
		    foreach (TreeNode TNode in tc)
		    {
		        if (TNode.Level > 4)
		        {
		            string strTT = TNode.Text.ToString().Trim();
		            取消掉字符前头的符号
		            if (strTT.IndexOf("#") != -1)
		            {
		                strTT = strTT.Replace("#text", "");
		                strTableRow += strTT + ",";
		            }
		            TD
		            TR
		            if (TNode.Text.ToString().Trim() == "TR" && strTableRow != null)
		            {
		                strTableRow += "===";
		            }
		            BODY
		            if (TNode.Text.ToString().Trim().IndexOf("BODY") != -1 && strTableRow != null)
		            {
		                listBox3.Items.Add(strTableRow);
		                strTableRow = null;
		            }
		        }
		        GetNode2(TNode.Nodes);
		    }
		}
			listBox3.Items.Clear();
			TreeNodeCollection tc = treeView3.Nodes;
			GetNode2(tc);
			listBox3.Items.Clear();
			TreeNodeCollection tc = treeView3.Nodes;
			GetNode(tc);
			insertLiveOdds2();
			treeView3.Nodes.Clear();
			IHTMLDocument2 doc = (IHTMLDocument2)axWebBrowser2.Document;
			IHTMLFramesCollection2 frames = (IHTMLFramesCollection2)doc.frames;
			IHTMLDOMNode rootDomNode = (IHTMLDOMNode)HTMLDocument.documentElement;
			TreeNode root = treeView3.Nodes.Add("HTML");
			InsertDOMNodes2(doc, root);
		如何遍历当前页面中所有的frame?
		getframes->IHTMLFramesCollection2->item()->IHTMLWindow2->IHTMLDocument2   
		循环这样的过程，可以得到frame   的IHTMLWindow2/IHTMLDocument2接口   
		把html生成tree的方法
		运用递归过程把HTML转成DOC，然后生成treeView
		private void InsertDOMNodes2(IHTMLDocument2 doc, TreeNode tree_node)
		{
			mshtml.HTMLDocument doc = myDoc;
			mshtml.HTMLDocument doc = (mshtml.HTMLDocument)browser.Document;
			mshtml.FramesCollection frames = (mshtml.FramesCollection)doc.frames;
			mshtml.IHTMLWindow2 mainFrame = null;
			for (int i = 0; i < frames.length; i++)
			{
			    object refIndex = i;
			    mshtml.IHTMLWindow2 frame = (mshtml.IHTMLWindow2)frames.item(ref refIndex);
			    if (frame.name == "main")
			        mainFrame = frame;
			}
			if (mainFrame != null)
			{
			    mshtml.IHTMLDocument2 mainFrameDoc = mainFrame.document;
			}
		}
			StreamReader reader = new StreamReader(appPath + "\\urlLog\\liveOddsurlCode.txt");//读入Log文件
			string urlCode = reader.ReadToEnd();
			LiveOdds md = new LiveOdds(richTextBox4.Text);
			this.listView1.View = View.Details;
			int j = 0;
			listView1.Items.Clear();
			toolStripProgressBar1.Maximum = md.liveodds.Count;
			foreach (string m in md.liveodds)
			{
			    toolStripProgressBar1.Value = j; j++; Application.DoEvents();
			    string[] ar = m.Split(Convert.ToChar(','));
			    ListViewItem lv = new ListViewItem(ar[0]);
			    for (int i = 1; i < ar.Length; i++)
			    {
			        lv.SubItems.Add(ar[i]);
			    }
			    listView1.Items.Add(lv);
			}
			reader.Close();
			foreach (ListViewItem lvItem in listView1.Items)
			{
			    if (lvItem.SubItems.Count > 8)
			    {
			        string strMatch = lvItem.SubItems[3].Text.Trim();
			        string strDate = DateTime.Now.ToString("yyyy年MM月dd");
			        string strTime = lvItem.SubItems[4].Text.Trim();
			        string strHomeTeam = lvItem.SubItems[6].Text.Trim();
			        string strAwayTeam = lvItem.SubItems[8].Text.Trim();
			        string strLiveOdds = lvItem.SubItems[2].Text.Trim();
			        string insertNow = "insert into LiveOddsBeiJingSingle([Match], [DDate],[TTime],[HomeTeam],[AwayTeam],[LiveOdds]) values('" +
			                                       strMatch + "','" + strDate + "','" + strTime + "','" +
			                                       strHomeTeam + "','" + strAwayTeam + "','" + strLiveOdds + "')";

			        if (strTime.IndexOf(":") != -1)
			        {
			            Acc.RunNoQuery(insertNow);
			        }
			    }
			}
			DataTable dtbjs = Acc.RunQuery("select * from LiveOddsBeiJingSingle");
			DataTable dtmt = Acc.RunQuery("select * from MatchToday");
			int maxbjs = dtbjs.Rows.Count; int maxmt = dtmt.Rows.Count;
			for (int i = 0; i < maxbjs; i++)
			{
			    for (int j = 0; j < maxmt; j++)
			    {
			        if (dtmt.Rows[j][5].ToString().IndexOf(dtbjs.Rows[i][4].ToString()) != -1
			            && dtmt.Rows[j][7].ToString().IndexOf(dtbjs.Rows[i][5].ToString()) != -1)
			        {
			            Acc.RunNoQuery("update MatchToday  set WinDLoss='" + dtbjs.Rows[i][6].ToString()
			                + "' where id=" + dtmt.Rows[j][0].ToString());
			        }
			    }
			}

			while (axWebBrowser2.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
			{
			    Application.DoEvents();
			    MessageBox.Show("waiting for document state complete. Last state was '" + axWebBrowser1.ReadyState + "'");  //弹出确认框可以达到要求的效果，后台axWebBrowser1继续执行，而程序暂停

			}
			DAL层，主要是负责数据的采集工作，
			while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
			richTextBox4.Text = webBrowser1.Document.Body.InnerHtml;
			while (richTextBox1.Text == null) return;
			MessageBox.Show("ok");
			treeView3.Nodes.Clear();
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(richTextBox4.Text);
			HtmlNode rootDomNode = doc.DocumentNode;
			TreeNode root = treeView3.Nodes.Add("HTML");
			InsertDOMNodes(rootDomNode, root);
			listBox3.Items.Clear();
			TreeNodeCollection tc = treeView3.Nodes;
			GetKeyWoredNode(tc, "TR", "-");
			把listBox的内容插入数据库的方法
			foreach (string m in listBox3.Items)
			{
			    Application.DoEvents();
			    string[] ar = m.Split(Convert.ToChar(','));
			    int s = ar.Length;
			    if (s > 5 && s < 19)
			    {
			        string strMatch = ar[0].ToString();
			        string strDate = ar[1].ToString();
			        string strTime = ar[2].ToString();
			        string strHomeTeam = ar[3].ToString().Trim();
			        string strAwayTeam = ar[11].ToString().Trim();
			        string strLiveOdds = null;
			        for (int i = 4; i < s; i++)
			        {
			            if (i != 11)
			            {
			                strLiveOdds += ar[i].ToString().Trim() + ",";
			            }
			        }
			        string insertNow = "insert into LiveOddsTeamName([Match], [DDate],[TTime],[HomeTeam],[AwayTeam],[LiveOdds]) values('" +
			                   strMatch + "','" + strDate + "','" + strTime + "','" +
			                   strHomeTeam + "','" + strAwayTeam + "','" + strLiveOdds + "')";
			        string insertNow = "insert into LiveOddsTeamName([Match], [DDate],[TTime],[HomeTeam]) values('" +
			                                       strMatch + "','" + strDate + "','" + strTime + "','" +  strLiveOdds + "')";

			        if (strTime.IndexOf(":") != -1)
			        {
			            Acc.RunNoQuery(insertNow);
			        }
			    }
			}
		运用递归过程遍历treeView的所有的节点
		public void GetKeyWoredNode(TreeNodeCollection tc, string keyWord, string filterWord)
		{
		    foreach (TreeNode TNode in tc)
		    {
		        if (TNode.Level > 3)
		        {
		            string strTT = TNode.Text.ToString().Trim();
		            if (strTT.IndexOf("#") != -1)
		            {
		                strTT = strTT.Replace("#text", "");
		                strTableRow += strTT + ",";
		            }

		            if (TNode.Text.ToString().Trim().IndexOf(keyWord) != -1 && strTableRow != null)
		            {
		                if (strTableRow.IndexOf(filterWord) == -1)
		                {
		                    strTableRow = null;
		                }
		                else
		                {
		                    listBox3.Items.Add(strTableRow);
		                    strTableRow = null;
		                }
		            }
		        }
		        GetKeyWoredNode(TNode.Nodes, keyWord, filterWord);
		    }
		}
			toto = true;
			getMatchid();
			liveChange = false;
			dt_treeView.Clear();
			dt_treeView = Acc.RunQuery("select  ID, StartPosition, Match,DateTTime, HomeTeam,AwayTeam from  MatchToday where (match='德甲' or match='意甲' or match='英超' or match='西甲') and links is not null");
			LoadTree();
bool toto = false;
			treeView1.Nodes[0].Expand();
			dt_treeView.Clear();
			dt_treeView = Acc.RunQuery("select  * from  MatchToday");
			List<string> list = new List<string>();
			foreach (DataRow dr in dt_treeView.Rows)
			{
			    if (!list.Contains(dr["Match"].ToString()))
			        list.Add(dr["Match"].ToString());
			}
			foreach (string s in list)
			    checkedListBox1.Items.Add(s);

			dt_treeView.Clear();
			dt_treeView = Acc.RunQuery("select  * from  MatchToday");
			DataTable dt = new DataTable();
			for (int i = 0; i < checkedListBox1.Items.Count; i++)
			{
			    if (checkedListBox1.GetItemChecked(i))
			    {
			        DataTable dt2 = dt_treeView;
			        DataView dv = dt2.DefaultView;
			        dv.RowFilter = "Match='" + checkedListBox1.Items[i].ToString() + "'";
			        DataTable dt1 = dv.ToTable();
			        dt.Merge(dt1);
			    }
			}
			dt_treeView = dt;
			LoadTree();
			treeView1.ExpandAll();
 liveChange = true;
			Thread thread = new Thread(CrossThreadFlush);
			thread.IsBackground = true;
			thread.Start();
			Acc.RunNoQuery("update MatchToday  a,SoccerChange b  set a.Result=b.Result   where a.HomeTeam_big=b.HomeTeam_big");   //数据库更新预测结果
			toolStripStatusLabel1.Text = "Update OK";
			toolStripStatusLabel1.Text = "download table......";
			liveLib = false;
			liveChange = true;
			Acc.RunNoQuery("delete from SoccerChange");
			DateTime dt = DateTime.Now.AddDays(-1);
			textBox1.Text = "http://data1.7m.cn/Result_data/default_big.shtml?date=" + dt.ToString("yyyy-MM-dd");
   deleteFile(new DirectoryInfo(appPath + "\\forecaseHistoryView"));
			richTextBox2.Text = "select id,HomeTeam_big,AwayTeam_big,ShowAnalyse_big "
			    + "from MatchAnalysis where DDate is not null and Status is null and DDate>'" +
			                              toolStripTextBox1.Text + "' order by id desc";    //取需要计算的ID
		private void forecastNoResultID(string result)
		{
			string[] ar = result.Split(Convert.ToChar(","));
			int size = ar.Length;
			if (ar[8].Length == 0)
			{
			    Acc.RunNoQuery("delete from   historyToday where  id=" + ar[0].ToString());
			}
		}
		运用递归过程搜寻treeView的所有的节点  
		private void searchTree(TreeNodeCollection tc, string keyWord)
		{
			foreach (TreeNode TNode in tc)
			{
				if (TNode.Level == 1 && keyWord.IndexOf(TNode.Text) != -1)
				{
					TNode.Expand(); TNode.EnsureVisible();
					TNode.ForeColor = Color.Blue;
					Application.DoEvents();//调整窗口焦点   TNode.ForeColor = Color.Blue;
				}
				searchTree(TNode.Nodes, keyWord);
			}
		}

			if (treeView1.SelectedNode == null) { return; }
			treeView4.CollapseAll();
			searchTree(treeView4.Nodes, treeView1.SelectedNode.Text);

			loaddatatree.TreeViewMatch(treeView5, "type");
			treeView5.Nodes.Clear();
			treeView5.Nodes.Add(loaddatatree.TreeViewMatch("type"));

			loaddatatree.TreeViewMatch(treeView5, "type");
			treeView5.Nodes.Clear();
			treeView5.Nodes.Add(loaddatatree.TreeViewMatch("type"));
		private int forecast_w(string result)
		{
		    int w = 0;
		    string[] ar = result.Split(Convert.ToChar(","));
		    int i = 6;
		    if (ar[i].IndexOf("-") != -1 && ar[i].Length > 3)
		    {
		        int l = ar[i].Length;
		        int m = ar[i].IndexOf("-");
		        int h = Int16.Parse(ar[i].Substring(0, m));
		        int a = Int16.Parse(ar[i].Substring(m + 1, l - m - 1));
		        w = h - a;
		    }
		    return w;
		}
			while (axWebBrowser2.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
			{
			    Application.DoEvents();
			}
			MessageBox.Show("ok");
			treeView3.Nodes.Clear();
			IHTMLDocument3 HTMLDocument = (IHTMLDocument3)axWebBrowser2.Document;
			IHTMLDOMNode rootDomNode = (IHTMLDOMNode)HTMLDocument.documentElement;
			TreeNode root = treeView3.Nodes.Add("HTML");
			InsertDOMNodes(rootDomNode, root);
			while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
			richTextBox4.Text = webBrowser1.Document.Body.InnerHtml;
			while (richTextBox1.Text == null) return;
			MessageBox.Show("ok");
			treeView3.Nodes.Clear();
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(richTextBox4.Text);
			HtmlNode rootDomNode = doc.DocumentNode;
			TreeNode root = treeView3.Nodes.Add("HTML");
			InsertDOMNodes(rootDomNode, root);
			listBox3.Items.Clear();
			TreeNodeCollection tc = treeView3.Nodes;
			GetKeyWoredNode(tc, "TR", "-");

			listBox3.Items.Clear();
			TreeNodeCollection tc = treeView3.Nodes;
			GetKeyWoredNode(tc, "TBODY", ":");
			把listBox的内容插入数据库的方法
			insertLiveOdds2();
			button5.PerformClick();//html->txt
			Thread.Sleep(500);
			button14.PerformClick();//access->bjs
			Thread.Sleep(500);
			button15.PerformClick();//bjs->today
			Thread.Sleep(500);
using System.Windows.Forms.DataVisualization.Charting;

	   //历史比赛数据的更新  result_tb


		//最新比赛数据的更新   live_Table
			//Console.WriteLine("HTTP Response is ");
	//数据库更新比较简单的方法
 //webbrowse的方法获取html，解析后写入数据库

			//Console.WriteLine("Sending an HTTP GET request to " + uri);
			//Console.WriteLine("HTTP Response is ");
			//Console.WriteLine("Sending an HTTP GET request to " + uri);
			//Console.WriteLine("HTTP response is: ");
