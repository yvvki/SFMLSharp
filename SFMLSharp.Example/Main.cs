using System.Runtime.Versioning;

using SFML.Graphics;
using SFML.Window;

[assembly: RequiresPreviewFeatures]

RenderWindow window = new();

EventWindow eventWindow = new(window);
eventWindow.Closed += () => window.Close();

RectangleShape shape = new();

while (window.IsOpen)
{
	window.Clear();

	window.Draw(shape);
}
