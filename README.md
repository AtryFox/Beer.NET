Beer.NET
========

A [Beer](https://github.com/aledjones/Beer.php) implemenation for .NET Framework.

Beer is a ciphering method first published by @aledjones. Basically it replaces every character of the 26-digit alphabet with a specific amount of "BEER".


## Prerequisites ##
- .NET Framework 2.0 or higher

## Installation ##
The simpleest way to install Beer.NET is to use our [NuGet package](https://www.nuget.org/packages/Beer.NET). Just open the context menu for your project with Visual Studio and click the option "*Manage NuGet Packages...*".

```
PM> Install-Package Beer.NET
```

You can also build Beer.NET yourself and add it as an reference to your project.

## Usage ##
Simple example in C#:

```
using System;
using DerAtrox.BeerNET;

namespace BeerTest {
	class Program {
		static void Main(string[] args) {
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			// Enable UTF-8 encoding.

			Console.WriteLine(Beer.SerializeBeer("Hi"));
			// Output: "µµµµµµµµµµµµµµµµ∫BEERBEERBEERBEERBEERBEERBEERBEER∫"

			Console.WriteLine(Beer.DeserializeBeer("µµµµµµµµµµµµµµµµ∫BEERBEERBEERBEERBEERBEERBEERBEER∫"));
			// Output: "Hi"

			Console.ReadKey();
		}
	}
}
```
