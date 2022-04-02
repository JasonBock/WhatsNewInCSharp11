using System.Reflection;

namespace WhatsNewInCSharp11;

// Eventually, the example could use 
// https://github.com/JasonBock/Injectors,
// but that hasn't been updated.
public abstract class InjectorAttribute<T>
	: Attribute
	where T : ICustomAttributeProvider
{
	public void Inject(T target!!) =>
		this.OnInject(target);

	protected abstract void OnInject(T target);
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class TraceAttribute
	: InjectorAttribute<MethodInfo>
{
	protected override void OnInject(MethodInfo target)
	{
		// do something with the target...
	}
}