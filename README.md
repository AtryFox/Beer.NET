[![Build Status Travis CI](https://travis-ci.org/DerAtrox/Beer.NET.svg?branch=master)](https://travis-ci.org/DerAtrox/Beer.NET)
[![Build status AppVeyor](https://ci.appveyor.com/api/projects/status/0h3qy885yd2dc9e0?svg=true)](https://ci.appveyor.com/project/DerAtrox/beer-net)
[![NuGet](https://img.shields.io/nuget/v/Beer.NET.svg)](https://www.nuget.org/packages/Beer.NET)

Beer.NET
========

A [Beer](https://github.com/rauhkrusche/Beer) implementation for .NET Framework.

## Prerequisites ##
- .NET Framework 2.0 or higher

## Installation ##
The simplest way to install Beer.NET is to use our [NuGet package](https://www.nuget.org/packages/Beer.NET). Just open the context menu for your project with Visual Studio and click the option "*Manage NuGet Packages...*".

```
PM> Install-Package Beer.NET
```

You can also build Beer.NET yourself and add it as an reference to your project.

## Usage ##
Simple example in C#:

```
using DerAtrox.BeerNET;

public void BeerTest() {
	string serialized = Beer.SerializeBeer("Hi");
	// Value: "µµµµµµµµµµµµµµµµ∫BEERBEERBEERBEERBEERBEERBEERBEER∫"
	
	string deserialized = Beer.DeserializeBeer("µµµµµµµµµµµµµµµµ∫BEERBEERBEERBEERBEERBEERBEERBEER∫");
	// Value: "Hi"
}
```
