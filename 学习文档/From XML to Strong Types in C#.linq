<Query Kind="Expression">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
</Query>

From XML to Strong Types in C#
Introduction
I was recently reading some articles on a well known technical publication site when I came across an article describing how to get strong C# types from an XML document. Since I had done this several times, I wanted to see what tid-bits I could glean from the article like additional XSD annotations. Anyway, after being disappointed by the article (as I�m sure some of you will be after reading this one), I began to search the net on producing strong .NET types from XML, and I found it difficult to find anything short and to the point, so I thought I would give it a try.

Article goals 
Describe the main difference between strongly and non-strongly typed languages. 
Provide an overview of strong types. 
Provide an overview of the Xsd.exe utility. 
Demonstrate how to create an XSD from an XML file using the Xsd.exe. 
Demonstrate how to create a C#.NET class file from the XSD using Xsd.exe. 
Demonstrate how to create a strongly typed DataSet from the XSD using Xsd.exe. 

First, what does it mean for a language to be strongly typed? The easiest way to explain the difference between a strongly and a non-strongly typed language is to compare how they generally handle typos. If you have a variable named myVar in a non-strongly typed language and reference it a second time in the code as myVat, the code will compile, and you will most likely find the bug (typo of myVar as myVat) during run time testing. On the other hand, if you had the same typo in C#.NET, the code would not compile, and you will find the bug at design time.

Another benefit of strongly typed languages is that you know the underlying type of your pointers (although they may sometimes be the most generic type supported by the language, object in C#.NET). For example, the following C#.NET code will not compile because the language is strongly typed, and the compiler knows the difference between a Hashtable and a string:

 Collapse Copy Code
Hashtable hash = new Hashtable();
string someString = hash;Sidebar: Scripting languages like JavaScript and VBScript are not strongly typed, and everything is pretty much a variant or a type that can contain any other type.

The XML file
The examples in this article will use one of the most basic XML files one can create. In fact, the XML files used with the Xsd.exe utility do not even need the XML descriptor in the file header. Our XML file is named People.xml and has the following content:

 Collapse Copy Code
<People>
	<Person>
		<FirstName>Mike</FirstName>
		<LastName>Elliott</LastName>
		<City>Dallas</City>
		<State>TX</State>
		<Company>Play For Sport, Inc.</Company>
	</Person>
</People>What is the Xsd.exe utility
The Visual Studio .NET help files describe the Xsd.exe as follows: The XML Schema Definition tool (Xsd.exe) is installed along with the .NET Framework Tools as part of the .NET Framework SDK. The tool is designed primarily for two purposes:

To generate either C# or Visual Basic class files that conform to a specific XML Schema definition language (XSD) schema. The tool takes an XML Schema as an argument and outputs a file that contains a number of classes that, when serialized with the XmlSerializer, conform to the schema. 
To generate an XML Schema document from a .dll file or .exe file. If you need to see the schema of a set of files that you have either created or one that has been modified with attributes, pass the DLL or EXE as an argument to the tool to generate the XML schema. 
On my machine, the Xsd.exe can be found in the [InstallDrive]:\Program Files\Microsoft Visual Studio .NET [2003]\SDK\[v1.1]\bin directory. The utility's default output language for source is C#, and as stated earlier in the article, we will focus on taking our People.xml file, producing an XSD, and using that XSD to produce a standard C# class and finally a strongly typed DataSet.

Sidebar: I like to copy often used command line tools to the [WinRoot]\System32 directory, so that I can get directly to them from any directory. Open the command prompt and type xsd.exe /? to see all the commands and switch options for the utility.

Creating an XSD from the XML file
OK, just to be sure I give you all the details; I have created a C:\_A\XmlToTypes directory which contains only the People.xml file (for now).

To generate the XSD from our People.xml file, do the following:

Open the command prompt. 
Change directories on the command prompt to the directory containing the People.xml file (C:\_A\XmlToTypes on my machine). 
Type the following on the command prompt: xsd.exe People.xml 
This will produce a People.xsd file. Figure 1 illustrates the input and output from the command line:

 Collapse Copy Code
Figure 1
C:\_A\XmlToTypes>xsd.exe People.xml
Microsoft (R) Xml Schemas/DataTypes support utility
[Microsoft (R) .NET Framework, Version 1.1.4322.573]
Copyright (C) Microsoft Corporation 1998-2002. All rights reserved.

Writing file 'C:\_A\XmlToTypes\People.xsd'.Figure 2 displays the content of People.xsd:

 Collapse Copy Code
Figure 2

<?xml version="1.0" encoding="utf-8"?>
	<xs:schema id="People" xmlns="" 
		 xmlns:xs="http://www.w3.org/2001/XMLSchema" 
		 xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
		<xs:element name="People" msdata:IsDataSet="true">
			<xs:complexType>
				<xs:choice maxOccurs="unbounded">
					<xs:element name="Person">
						<xs:complexType>
							<xs:sequence>
								<xs:element name="FirstName" 
									type="xs:string" minOccurs="0" />
								<xs:element name="LastName" 
									type="xs:string" minOccurs="0" />
								<xs:element name="City" 
									type="xs:string" minOccurs="0" />
								<xs:element name="State" 
									type="xs:string" minOccurs="0" />
							</xs:sequence>
						</xs:complexType>
					</xs:element>
				</xs:choice>
			</xs:complexType>
		</xs:element>
</xs:schema>Creating a C# class from the XSD
In order to produce a C# class or a strongly typed DataSet, we have to have an XSD. Well, we�re lucky, the Xsd.exe has created one for us via our XML file.

To generate the C#.NET class from our People.xsd, do the following:

Open the command prompt. 
Change directories on the command prompt to the directory containing the People.xsd file (C:\_A\XmlToTypes on my machine). 
Type the following on the command prompt: xsd.exe People.xsd /c 
This will produce a People.cs file. Figure 3 illustrates the input and output from the command line:

 Collapse Copy Code
Figure 3

C:\_A\XmlToTypes>xsd.exe People.xsd /c
Microsoft (R) Xml Schemas/DataTypes support utility
[Microsoft (R) .NET Framework, Version 1.1.4322.573]
Copyright (C) Microsoft Corporation 1998-2002. All rights reserved.

Writing file 'C:\_A\XmlToTypes\People.cs'.Figure 4 displays the content of People.cs. Notice that the Xsd.exe code generator has created a C# class named People and another class named PeoplePerson.

 Collapse Copy Code
Figure 4

//------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect 
//     behavior and will be lost if the code is 
//     regenerated.
// </autogenerated>
//------------------------------------------------

// 
// This source code was auto-generated by xsd, 
// Version=1.1.4322.2032.
// 
using System.Xml.Serialization;

/// <remarks/>
[System.Xml.Serialization.XmlRootAttribute(Namespace="", 
										IsNullable=false)]
public class People {
					
/// <remarks/>
[System.Xml.Serialization.XmlElementAttribute("Person", 
		Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public PeoplePerson[] Items;
}

/// <remarks/>
public class PeoplePerson {
	
/// <remarks/>
[System.Xml.Serialization.XmlElementAttribute(
		Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string FirstName;
					  
/// <remarks/>
[System.Xml.Serialization.XmlElementAttribute(
		Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string LastName;
	
/// <remarks/>
[System.Xml.Serialization.XmlElementAttribute(
		Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string City;

/// <remarks/>
[System.Xml.Serialization.XmlElementAttribute(
		Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string State;
}Sidebar: Make whatever code changes you need, like adding or changing the namespace or class name (there is an additional command line switch you can use to have the code generator add the proper namespace: /namespace). Just add the .cs file to your Visual Studio .NET project and off you go.

Creating a strongly typed DataSet from the XSD
Let me first say the Xsd.exe utility is not my preferred way to produce a strongly typed DataSet, but I wanted to provide a little variety. I prefer to create a C# Windows application and use the wizards to create strongly typed DataSets. I chose to do it this way because I can have a single, separate application responsible for producing all my strongly typed DataSets related to the project that can be added to source control. Also, if my database schema changes, I can simply open the project, delete the DataSet and immediately create a new up-to-date DataSet.

To generate the strongly typed DataSet from our People.xsd file, do the following:

Rename the existing People.cs file you just created to People1.cs (this is important or it will be overwritten). 
Open the command prompt. 
Change directories on the command prompt to the directory containing the People.xsd file (C:\_A\XmlToTypes on my machine). 
Type the following on the command prompt: xsd.exe People.xsd /d 
This will produce a .cs file named People.cs. Figure 5 illustrates the input and output from the command line:

 Collapse Copy Code
Figure 5

C:\_A\XmlToTypes>xsd.exe People.xsd /d
Microsoft (R) Xml Schemas/DataTypes support utility
[Microsoft (R) .NET Framework, Version 1.1.4322.573]
Copyright (C) Microsoft Corporation 1998-2002. All rights reserved.

Writing file 'C:\_A\XmlToTypes\People.cs'.Sidebar: The content of the resulting People.cs file is a little too lengthy to include here, but if you open it, you will find a C# class named People which derives from DataSet. The code generator will also have created several helpful methods and properties related to the elements in the XSD.

Summary
I hope I did an OK job making this information short and to the point. I also hope you see how easy it is to produce strong types by starting with XML. The Xsd.exe code generation utility is quite useful. Transforming static configurations and other structures you typically store in an XML file can easily be converted into strong types and turned into runtime objects. Another cool benefit is that IntelliSense becomes available on the types which should reduce the development time as spelling and reference issues will be caught during design time.

