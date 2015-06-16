using Opencoding.CommandHandlerSystem;
using UnityEngine;

public class ConsoleOutputTest : MonoBehaviour
{
	private void Start()
	{
		for (int i = 0; i < 10; ++i)
		{
			Debug.Log("Log message here " + i);
			Debug.Log("Log message here " + i);
			Debug.Log("Log message here " + i);
			Debug.LogError("Error message here " + i);
			Debug.LogWarning("Warning message here " + i);
		}

		Convolution();

		CommandHandlers.RegisterCommandHandlers(this);
	}

	private void Convolution()
	{
		Something();
	}

	private void Something()
	{
		Debug.Log("Something method");
		SomethingElse();
	}

	private void SomethingElse()
	{
		Debug.Log("Some message");
		Debug.Log("Another message");
		Debug.Log("A two line message\nThis is the second line");
		Debug.Log("A long long long long message that takes up quite a lot of space! Is this long enough yet?");
	}

	public bool _Output;


	public void Update()
	{
		if(_Output)
			Debug.Log("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus facilisis, nisl id venenatis lacinia, diam metus eleifend felis, ac aliquam lacus sapien eget justo. Donec auctor consequat posuere. Integer efficitur blandit accumsan. Cras vehicula dictum sapien, sed ultricies leo laoreet nec. Integer egestas ex tempus metus vulputate lacinia. Cras ut rutrum leo, quis ornare tellus. Sed dolor odio, volutpat a dignissim a, eleifend vel urna. Nam pellentesque enim ac mauris eleifend, et ultrices augue mollis. Etiam eget neque facilisis, tempor odio nec, feugiat turpis.\n\nEtiam nunc augue, finibus id consequat id, iaculis ac diam. Pellentesque condimentum dolor id quam finibus, id ultrices felis finibus. Proin faucibus nibh vitae sapien aliquet, sit amet sagittis nunc viverra. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In vestibulum eleifend pulvinar. Phasellus at congue erat. Fusce at orci egestas, congue est posuere, facilisis ante. Donec semper justo non porta hendrerit. Cras interdum, ipsum non interdum vulputate, sem ipsum imperdiet felis, eget euismod orci tortor vitae nunc.\n\nNam ornare tempor nisi sed posuere. Etiam gravida porttitor fringilla. Donec id dui lacinia, elementum magna et, posuere metus. Proin sed neque risus. Curabitur nunc tortor, rhoncus ac varius a, pellentesque sit amet elit. Duis tempor rhoncus leo, eu lacinia ligula consectetur in. Praesent ut pellentesque ligula. Curabitur tempus mauris lobortis semper hendrerit. Proin mattis, quam nec elementum placerat, arcu erat posuere lacus, ac lobortis est magna quis neque. Sed at mauris sed libero facilisis mattis. Maecenas eget elementum ex. Maecenas vitae sollicitudin est, a elementum sem. Fusce hendrerit felis sit amet pellentesque dapibus.\n\nNullam at maximus quam. Etiam id metus accumsan, condimentum enim sed, malesuada est. Nulla ut urna consequat, porttitor ligula at, malesuada turpis. Quisque egestas lectus non pretium tincidunt. Ut erat ligula, dignissim a pellentesque nec, convallis in orci. Vivamus eu efficitur turpis. Pellentesque luctus ex nec feugiat pharetra. Duis cursus nulla non quam sollicitudin dictum.\n\nMaecenas mauris mauris, tempus sit amet ligula eget, convallis congue arcu. Morbi vel posuere ante. Integer justo odio, tristique ac lorem at, congue congue velit. Maecenas mattis magna lacus, eget rhoncus quam gravida eu. Vestibulum vitae tristique tellus. Etiam sit amet ligula quis arcu tempor laoreet eu eu erat. Nunc ultrices ligula ipsum. Ut eget purus imperdiet, laoreet neque ac, ultrices nunc. Morbi urna nisi, maximus vitae gravida vel, luctus et turpis. Etiam id vestibulum ligula. Quisque tempus sit amet nulla sit amet fermentum. Aenean luctus enim id ex suscipit, eu maximus tortor accumsan. Sed aliquam ex sit amet erat aliquet, vitae finibus ipsum laoreet. Nam luctus in tortor id tristique. Phasellus placerat neque a nibh scelerisque dignissim. Donec in urna ipsum.");
		_Output = false;
	}

	
}
