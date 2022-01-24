using System.Runtime.Versioning;

using SFML.Graphics;
using SFML.Window;

[assembly: RequiresPreviewFeatures]

VideoMode videoMode = new(800, 600);
RenderWindow window = new(videoMode, "SFMLSharp Example");

RectangleShape shape = new();
shape.Size = new(200, 200);

while (window.IsOpen)
{
	while (window.PollEvent(out Event e))
	{
		switch (e.Type)
		{
			case EventType.Closed:
				window.Close();
				break;
		}
	}

	window.Clear(Color.Black);

	window.Draw(shape);

	window.Display();
}
