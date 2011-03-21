<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Runtime.Remoting</Namespace>
  <Namespace>System.Runtime.Remoting.Contexts</Namespace>
  <Namespace>System.Runtime.Remoting.Messaging</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>


//
// Context-Bound type with Synchronization Context Attribute
//
[Synchronization()]
public class SampleSyncronized : ContextBoundObject
{
	// A method that does some work - returns the square of the given number
	public int Square(int i)
	{
		Console.Write("SampleSyncronized.Square called.  ");
		Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
		return i*i;
	}
}


//
// Async delegate used to call a method with this signature asynchronously
//
public delegate int SampSyncSqrDelegate(int i);

//Main sample class

	public static void Main()
	{
		int callParameter = 0;
		int callResult = 0;

		//Create an instance of a context-bound type SampleSynchronized
		//Because SampleSynchronized is context-bound, the object sampSyncObj 
		//is a transparent proxy
		SampleSyncronized sampSyncObj = new SampleSyncronized();


		//call the method synchronously
		Console.Write("Making a synchronous call on the object.  ");
		Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
		callParameter = 10;
		callResult = sampSyncObj.Square(callParameter);
		Console.WriteLine("Result of calling sampSyncObj.Square with {0} is {1}.\n\n", callParameter, callResult);


		//call the method asynchronously
		Console.Write("Making an asynchronous call on the object.  ");
		Console.WriteLine("The hash of the current thread is: {0}", Thread.CurrentThread.GetHashCode());
		SampSyncSqrDelegate sampleDelegate = new SampSyncSqrDelegate(sampSyncObj.Square);
		callParameter = 17;

		IAsyncResult aResult = sampleDelegate.BeginInvoke(callParameter, null, null);

		//Wait for the call to complete
		aResult.AsyncWaitHandle.WaitOne();

		callResult = sampleDelegate.EndInvoke(aResult);
		Console.WriteLine("Result of calling sampSyncObj.Square with {0} is {1}.", callParameter, callResult);
	}



