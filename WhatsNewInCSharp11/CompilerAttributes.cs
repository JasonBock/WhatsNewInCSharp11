// These types are only here because in the current preview version, 
// they need to be added manually.
// I currently don't have .NET 7 installed, so once that's "officially"
// installed through VS Installer, then these can be removed (probably).

// https://source.dot.net/#System.Private.CoreLib/RequiredMemberAttribute.cs
// https://source.dot.net/#System.Private.CoreLib/CompilerFeatureRequiredAttribute.cs

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that a type has required members or that a member is required.</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
#if SYSTEM_PRIVATE_CORELIB
    public
#else
	internal
#endif
		 sealed class RequiredMemberAttribute : Attribute
	{ }

	/// <summary>
	/// Indicates that compiler support for a particular feature is required for the location where this attribute is applied.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	public sealed class CompilerFeatureRequiredAttribute : Attribute
	{
		public CompilerFeatureRequiredAttribute(string featureName) => this.FeatureName = featureName;

		/// <summary>
		/// The name of the compiler feature.
		/// </summary>
		public string FeatureName { get; }

		/// <summary>
		/// If true, the compiler can choose to allow access to the location where this attribute is applied if it does not understand <see cref="FeatureName"/>.
		/// </summary>
		public bool IsOptional { get; init; }

		/// <summary>
		/// The <see cref="FeatureName"/> used for the ref structs C# feature.
		/// </summary>
		public const string RefStructs = nameof(RefStructs);

		/// <summary>
		/// The <see cref="FeatureName"/> used for the required members C# feature.
		/// </summary>
		public const string RequiredMembers = nameof(RequiredMembers);
	}
}