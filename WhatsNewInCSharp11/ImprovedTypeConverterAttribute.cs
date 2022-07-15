using System.ComponentModel;

namespace WhatsNewInCSharp11;

[AttributeUsage(AttributeTargets.All)]
public sealed class ImprovedTypeConverterAttribute<T>
	: Attribute
	where T : TypeConverter
{
	public string ConverterTypeName => typeof(T).AssemblyQualifiedName!;
}