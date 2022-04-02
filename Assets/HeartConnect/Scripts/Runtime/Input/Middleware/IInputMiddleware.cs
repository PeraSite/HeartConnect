public interface IInputMiddleware {
	public InputState Process(InputState previous);

	public int Order();
}
