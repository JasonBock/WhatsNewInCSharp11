﻿using System.Runtime.CompilerServices;
using WhatsNewInCSharp11;

//DemonstrateParameterNullCheck();

// https://github.com/dotnet/csharplang/blob/main/proposals/param-nullchecking.md
static void DemonstrateParameterNullCheck()
{
	Console.WriteLine(nameof(DemonstrateParameterNullCheck));

	static int GetLengthExplicitNullCheck(string value)
	{
		if (value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		return value.Length;
	}

	static int GetLengthExplicitNullCheckUsingThrowIfNull(string value)
	{
		ArgumentNullException.ThrowIfNull(value);
		return value.Length;
	}

	static int GetLength(string value!!) => value.Length;

	static void CallLength(Func<string, int> action)
	{
		try
		{
			_ = action(null!);
		}
		catch (ArgumentNullException e)
		{
			Console.WriteLine(e.Message);
		}
	}

	Console.WriteLine(GetLengthExplicitNullCheck("value"));
	Console.WriteLine(GetLengthExplicitNullCheckUsingThrowIfNull("value"));
	Console.WriteLine(GetLength("value"));

	Console.WriteLine();

	CallLength(GetLengthExplicitNullCheck);
	CallLength(GetLengthExplicitNullCheckUsingThrowIfNull);
	CallLength(GetLength);
}

//DemonstrateRawStringLiterals();

// https://github.com/dotnet/csharplang/blob/main/proposals/raw-string-literal.md
static void DemonstrateRawStringLiterals()
{
	Console.WriteLine(nameof(DemonstrateRawStringLiterals));

	var literalCode =
		@"
		public static class StringExtensions
		{
			// Here is a ""comment"" with a triple-quote: """"""
			public static int GetLength(this string self!!) => self.Length;
		}";

	Console.WriteLine($"{nameof(literalCode)}");
	Console.WriteLine(literalCode);
	Console.WriteLine();

	var rawCode =
		"""
		public static class StringExtensions
		{
			public static int GetLength(this string self!!) => self.Length;
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
			public static int GetLength(this string self!!) => self.Length;
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
			public static int GetLength(this string self!!) => self.Length;
		}}";

	Console.WriteLine($"{nameof(interpolatedLiteralCode)}");
	Console.WriteLine(interpolatedLiteralCode);
	Console.WriteLine();

	var interpolatedRawCode =
		$$$"""
		public static class StringExtensions
		{
			// {{{commentText}}}
			public static int GetLength(this string self!!) => self.Length;
		}
		""";

	Console.WriteLine($"{nameof(interpolatedRawCode)}");
	Console.WriteLine(interpolatedRawCode);
}

//DemonstrateListPatterns();

// https://github.com/dotnet/csharplang/blob/main/proposals/param-nullchecking.md
static void DemonstrateListPatterns()
{
	Console.WriteLine(nameof(DemonstrateListPatterns));

	static void Peruse(List<int> values, [CallerArgumentExpression("values")] string valuesExpression = "")
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


//DemonstrateGenericAttributes();

// https://github.com/dotnet/csharplang/issues/124
// https://github.com/JasonBock/Injectors
static void DemonstrateGenericAttributes()
{
	Console.WriteLine(nameof(DemonstrateGenericAttributes));

	[Trace]
	static int Add(int x, int y) => x + y;

	Console.WriteLine(Add(3, 4));
}

//DemonstrateStaticAbstractMembersInInterfaces();

// https://devblogs.microsoft.com/dotnet/preview-features-in-net-6-generic-math/
// https://github.com/dotnet/csharplang/issues/4436
static void DemonstrateStaticAbstractMembersInInterfaces()
{
	Console.WriteLine(nameof(DemonstrateStaticAbstractMembersInInterfaces));

	static T Add<T>(T left, T right)
		where T : INumber<T> => left + right;

	Console.WriteLine(Add(3, 4));
	Console.WriteLine(Add(3.4, 4.3));

	// Sadly, this doesn't work right now,
	// hopefully BigInteger eventually implements those interfaces.
	//Console.WriteLine(Add(BigInteger.Parse("49043910940940104390"), BigInteger.Parse("59839583901984390184")));

	var customer = CreateableCustomer.Create();
	Console.WriteLine(customer);
}