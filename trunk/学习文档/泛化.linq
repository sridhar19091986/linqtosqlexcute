<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
  </Connection>
</Query>

	public interface ITarget { void Request();}
	public interface IAdaptee { void SpecifiedRequest();}

	public abstract class GenericAdapterBase<T> : ITarget
		where T : IAdaptee, new()
	{
		public virtual void Request()
		{
			new T().SpecifiedRequest();
		}
	}
	void Main()
	{
//	IAdaptee Adaptee;
	  			IAdaptee adaptee ;
//				= new Adaptee();
			ITarget target;
//			= adaptee;
	Console.WriteLine (adaptee.Code);
			Assert.AreEqual<int>(adaptee.Code, target.Data);
			List<Target> targets = new List<Target>();
			targets.Add(adaptee);
			targets.Add(adaptee);
			Assert.AreEqual<int>(adaptee.Code, targets[1].Data);
	}


// Define other methods and classes here
