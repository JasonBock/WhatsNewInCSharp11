using System.Numerics;

namespace WhatsNewInCSharp11;

public interface ICreatable<TSelf>
	where TSelf : ICreatable<TSelf>
{
#pragma warning disable CA1000 // Do not declare static members on generic types
   static abstract TSelf Create();
#pragma warning restore CA1000 // Do not declare static members on generic types
}

public sealed class CreateableCustomer 
	: ICreatable<CreateableCustomer>,
	IAdditionOperators<CreateableCustomer, CreateableCustomer, CreateableCustomer>
{
	public static CreateableCustomer Create() =>
		new()
		{
			Id = Guid.NewGuid(),
			Name = "Jason"
		};

	private CreateableCustomer() { }

	public override string ToString() => $"{this.Id}, {this.Name}";

	public Guid Id { get; init; }	
	public string? Name { get; init; }

   public static CreateableCustomer operator +(CreateableCustomer left, CreateableCustomer right) => 
		new() { Id = Guid.NewGuid(), Name = left?.Name + right?.Name };
}