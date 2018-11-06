namespace Phema.Validation
{
	public delegate ValidationMessage<TArgument1> Selector<TArgument1>();
	public delegate ValidationMessage<TArgument1, TArgument2> Selector<TArgument1, TArgument2>();
}