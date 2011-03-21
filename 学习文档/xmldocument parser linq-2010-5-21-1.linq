<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
</Query>

    public static List<metacdr> listmetacdr=new List<metacdr>();
	public static Composite root0 = new Composite();
	public static Composite comp = new Composite();
	
	static void Main()
	{
//	 listmetacdr=new List<metacdr>();
		string path = "";
		string file=@"F:\LenovoHP\ICDR_EDGE\Temp\test.xml";
		metacdr m = new metacdr(path,file);
		root0 = new Composite(m);
		XmlDocument doc = new XmlDocument();
		doc.Load(file);
		XmlElement root = doc.DocumentElement;
		Get(root, path);
		root0.Diaplay (0);
//			root0.listmetacdr ;
//		root0.listmetacdr .Dump ();
//listmetacdr.SingleOrDefault(n=>n.path.Length >10);
    	var q =            
    	from p in listmetacdr
    	group p by p.path  into g
    	select new {g.Key ,a=g.Count ()};
    	q.Dump ();
//		listmetacdr.ToDictionary
	}

	public static void Get(XmlNode node, string path)
	{
		if (!node.HasChildNodes)
		{
			metacdr m = new metacdr(path, node.InnerText);
			if (node.InnerText.IndexOf("message") != -1)
			{
				comp = new Composite(m);
				root0.Add(comp);
			}
			else
				comp.Add(new Leaf(m));
		}
		else
		{
			path += node.Name + "/";
			foreach (XmlNode n in node.ChildNodes)
			{
				Get(n, path);
			}
		}
	}
	public class metacdr
	{
		private string _path;
		private string _innertext;
		public string path
		{
			get { return _path; }
			set { _path = value; }
		}
		public string innertext
		{
			get { return _innertext; }
			set { _innertext = value; }
		}

		public metacdr(string path, string innertext)
		{
			this._path = path;
			this._innertext = innertext;
		}
	}
	public abstract class Component
	{			
		protected metacdr name;

		public Component(metacdr name)
		{
			this.name = name;
		}
		public Component()
		{

		}

		public abstract void Add(Component c);
		public abstract void Remove(Component c);
		public abstract void Diaplay(int depth);
	}
	public class Composite : Component 
	{		
		 List<Component> children;	
		public Composite(metacdr name)
			: base(name)
		{
			if (children == null)
			{
				children = new List<Component>();
			}
		}
		public Composite()
		{

		}
		public override void Add(Component c)
		{
			this.children.Add(c);
		}

		public override void Remove(Component c)
		{
			this.children.Remove(c);
		}

		public override void Diaplay(int depth)
		{
//			Console.WriteLine(new string('-', depth) + name.path + name.innertext);
			listmetacdr.Add (name);
			foreach (Component component in children)
			{
				component.Diaplay(depth + 2);
			}
		}

//		{
//					foreach (Component component in children)
//			  yield return name;
//		}
	}
	public class Leaf : Component
	{

		public Leaf(metacdr name)
			: base(name)
		{

		}

		public override void Add(Component c)
		{
			Console.WriteLine("不能向叶子节点添加子节点");
		}

		public override void Remove(Component c)
		{
			Console.WriteLine("叶子节点没有子节点");
		}

		public override void Diaplay(int depth)
		{
				listmetacdr.Add (name);
//			Console.WriteLine(new string('-', depth) + name.path + name.innertext);
		}

	}