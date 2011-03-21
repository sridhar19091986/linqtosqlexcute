<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

public void GetCustomerOrder()

{

	DataClasses1DataContext dc = new DataClasses1DataContext();

 

	var q= (from orders in dc.GetTable<Order>()

			from orderDetails in dc.GetTable<Order_Detail>()

			from prods in dc.GetTable<Product>()

			where ((orderDetails.OrderID == orders.OrderID) &&

				 (prods.ProductID == orderDetails.ProductID) &&

				 (orders.EmployeeID == 1))

			orderby orders.ShipCountry

			select new CustomerOrderResult

			{

				CustomerID = orders.CustomerID,

				CustomerContactName = orders.Customer.ContactName,

				CustomerCountry = orders.Customer.Country,

				OrderDate = orders.OrderDate,

				EmployeeID = orders.Employee.EmployeeID,

				EmployeeFirstName = orders.Employee.FirstName,

				EmployeeLastName = orders.Employee.LastName,

				ProductName = prods.ProductName

			}).ToList<CustomerOrderResult>();

 

	dataGridView1.DataSource = q;

}       
