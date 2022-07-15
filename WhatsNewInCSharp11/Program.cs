using System.Runtime.CompilerServices;
using WhatsNewInCSharp11;

//DemonstrateListPatterns();

// https://github.com/dotnet/csharplang/blob/main/proposals/param-nullchecking.md
static void DemonstrateListPatterns()
{
	Console.WriteLine(nameof(DemonstrateListPatterns));
	Console.WriteLine();

	// Note the "nameof(values)" - this is a new feature in C# 11
	// https://github.com/dotnet/csharplang/issues/373
	static void Peruse(List<int> values, [CallerArgumentExpression(nameof(values))] string valuesExpression = "")
	{
		var patternResult = values switch
		{
			[var single] => single,
			[3, var middle, 6] => middle,
			[var first, var middle, var last] => first + middle + last,
			[var first, ..] => first * 2,
			_ => 0
		};

		Console.WriteLine($"{valuesExpression} yields {patternResult}");
	}

	Peruse(new List<int> { 3 });
	Peruse(new List<int> { 3, 4, 6 });
	Peruse(new List<int> { 3, 4, 5 });
	Peruse(new List<int> { 3, 4, 5, 6, 7 });
	Peruse(new List<int> { });
}

//DemonstrateRawStringLiterals();

// https://github.com/dotnet/csharplang/blob/main/proposals/raw-string-literal.md
static void DemonstrateRawStringLiterals()
{
	Console.WriteLine(nameof(DemonstrateRawStringLiterals));
	Console.WriteLine();

	var literalCode =
		@"
		public static class StringExtensions
		{
			// Here is a ""comment"" with a triple-quote: """"""
			public static int GetLength(this string self) => self.Length;
		}";

	Console.WriteLine($"{nameof(literalCode)}");
	Console.WriteLine(literalCode);
	Console.WriteLine();

	var rawCode =
		"""
		public static class StringExtensions
		{
			public static int GetLength(this string self) => self.Length;
		}
		""";

	Console.WriteLine($"{nameof(rawCode)}");
	Console.WriteLine(rawCode);
	Console.WriteLine();

	var tripleQuoteCode =
		""""
		public static class StringExtensions
		{
			// Here is a "comment" with a triple-quote: """
			public static int GetLength(this string self) => self.Length;
		}
		"""";

	Console.WriteLine($"{nameof(tripleQuoteCode)}");
	Console.WriteLine(tripleQuoteCode);
	Console.WriteLine();

	var commentText = "My comment";

	var interpolatedLiteralCode =
		$@"public static class StringExtensions
		{{
			// {commentText}
			public static int GetLength(this string self) => self.Length;
		}}";

	Console.WriteLine($"{nameof(interpolatedLiteralCode)}");
	Console.WriteLine(interpolatedLiteralCode);
	Console.WriteLine();

	var interpolatedRawCode =
		$$$"""
		public static class StringExtensions
		{
			// {{{commentText}}}
			public static int GetLength(this string self) => self.Length;
		}
		""";

	Console.WriteLine($"{nameof(interpolatedRawCode)}");
	Console.WriteLine(interpolatedRawCode);
}

//DemonstrateGenericAttributes();

// https://github.com/dotnet/csharplang/issues/124
// https://github.com/JasonBock/Injectors
// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.typeconverterattribute
static void DemonstrateGenericAttributes()
{
	Console.WriteLine(nameof(DemonstrateGenericAttributes));
	Console.WriteLine();

	[Trace]
	static int Add(int x, int y) => x + y;

	Console.WriteLine(Add(3, 4));
}

//DemonstrateRequiredProperties();

// https://github.com/dotnet/csharplang/issues/3630
// https://blog.paranoidcoding.com/2022/04/11/lowercase-type-names.html
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/warning-waves#cs8981---the-type-name-only-contains-lower-cased-ascii-characters
static void DemonstrateRequiredProperties()
{
	Console.WriteLine(nameof(DemonstrateRequiredProperties));
	Console.WriteLine();

	// This won't work.
	//var person = new Person();

	var person = new Person() { Name = "Jason" };

	Console.WriteLine($"{person.Name}, {person.Age}");
}

DemonstrateStaticAbstractMembersInInterfaces();

// https://devblogs.microsoft.com/dotnet/dotnet-7-generic-math/
// https://github.com/dotnet/csharplang/issues/4436
// https://source.dot.net/#System.Private.CoreLib/IParsable.cs
static void DemonstrateStaticAbstractMembersInInterfaces()
{
	Console.WriteLine(nameof(DemonstrateStaticAbstractMembersInInterfaces));
	Console.WriteLine();

	static T Add<T>(T left, T right)
		where T : INumber<T> => left + right;

	Console.WriteLine(Add(3, 4));
	Console.WriteLine(Add(3.4, 4.3));
	Console.WriteLine(Add(int.Parse("3"), 4));

	// The current version of BigInteger (https://source.dot.net/#System.Runtime.Numerics/System/Numerics/BigInteger.cs)
	// implements the necessary interfaces, such as IBinaryInteger.
	// However, this isn't in .NET 6, which is what this project is referencing.
	//Console.WriteLine(Add(BigInteger.Parse("49043910940940104390"), BigInteger.Parse("59839583901984390184")));

	var customer = CreateableCustomer.Create();
	Console.WriteLine(customer);
}