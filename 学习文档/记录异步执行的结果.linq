<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
  </Connection>
</Query>

	

void Main()
{

				AsyncInvoker asyncInvoker = new AsyncInvoker();
				System.Threading.Thread.Sleep(3000);
				Console.WriteLine(asyncInvoker.Output[0]);
				Console.WriteLine(asyncInvoker.Output[1]);
				Console.WriteLine(asyncInvoker.Output[2]);
//				Assert.AreEqual<string>("method", asyncInvoker.Output[0]);
//				Assert.AreEqual<string>("fast", asyncInvoker.Output[1]);
//				Assert.AreEqual<string>("slow", asyncInvoker.Output[2]);

}
public class AsyncInvoker
		{
			// 记录异步执行的结果
			private IList<string> output = new List<string>();
	
			public AsyncInvoker()
			{
				Timer slowTimer = new Timer(new TimerCallback(OnTimerInterval), 
					"slow", 2500, 2500);
				Timer fastTimer = new Timer(new TimerCallback(OnTimerInterval), 
					"fast", 2000, 2000);
				output.Add("method");
			}
	
			private void OnTimerInterval(object state)
			{
				output.Add(state as string);
			}
	
			public IList<string> Output { get { return output; } }
		}
		

// Define other methods and classes here
