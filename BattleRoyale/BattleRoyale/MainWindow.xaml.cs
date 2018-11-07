using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Timers;

using static BattleRoyale.Extensions;

namespace BattleRoyale {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		Common.Coord camHalfFreeZone;
		Common.Coord halsScreenSize;
		Common.Coord camPos;

		Common.GameObjectState playerState;

		Common.IClient client;

		Timer keysTimer;
		object keysLocker = new object();
		List<Key> pressedKeys;

		public MainWindow() {
			InitializeComponent();

			camHalfFreeZone = new Common.Coord(50, 20);
			halsScreenSize = new Common.Coord();
			camPos = new Common.Coord();

			pressedKeys = new List<Key>();
			keysTimer = new Timer() {
				AutoReset = true,
				Enabled = true,
				Interval = 200,
			};

			keysTimer.Elapsed += (a, b) => {
				lock (keysLocker) {
					foreach (var key in pressedKeys) {
						switch (key) {
							case Key.A:
								client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveLeft));
								break;
							case Key.D:
								client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveRight));
								break;
							case Key.S:
								client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveBackward));
								break;
							case Key.W:
								client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveForward));
								break;
						}
					}
				}
			};

			client = new TCPClient();

			Console.CancelKeyPress += (a, b) => {
				client.Disconnect();
			};

			this.Closing += (a, b) => {
				client.Disconnect();
			};
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			halsScreenSize.x = (uint)Math.Round(RenderSize.Width / 2);
			halsScreenSize.y = (uint)Math.Round(RenderSize.Height / 2);

			AllocConsole();

			client.Connect("127.0.0.1", 65000);

			client.OnWorldUpdate += (states) => {
				List<Image> newState = new List<Image>();

				foreach (var state in states) {
					string path = @"Resources\textures\";

					switch (state.TextureId) {
						case Common.TextureId.None:
							path += "None";
							break;
						case Common.TextureId.Player:
							path += "Player";
							break;
						case Common.TextureId.DungeonFloor:
							path += "DungeonFloor";
							break;
						case Common.TextureId.DungeonWall:
							path += "DungeonWall";
							break;
					}
					path += ".png";

					this.Dispatcher.Invoke(() => {
						Image image = new Image {
							Source = new BitmapImage(new Uri(path, UriKind.Relative)),
							Width = state.Size.width,
							Height = state.Size.height,
							Stretch = Stretch.Fill,
						};
						newState.Add(image);
						image.Tag = state;
					});

					if (state.Id == client.PlayerId) {
						playerState = state;

						if (camPos != null) {
							if (playerState.Pos.x > camPos.x + camHalfFreeZone.x)
								++camPos.x;
							if (playerState.Pos.x < camPos.x - camHalfFreeZone.x)
								++camPos.x;
							if (playerState.Pos.y > camPos.y + camHalfFreeZone.y)
								++camPos.y;
							if (playerState.Pos.y < camPos.y - camHalfFreeZone.y)
								++camPos.y;
						}
						else {
							playerState = state;
							camPos = (Common.Coord)playerState.Pos.Clone();
						}
					}
				}

				this.Dispatcher.Invoke(() => {
					foreach (var image in newState) {
						foreach (UIElement c in GameCanvas.Children) {
							if (((c as Image).Tag as Common.GameObjectState).Id == (image.Tag as Common.GameObjectState).Id) {
								GameCanvas.Children.Remove(c);
								break;
							}
						}

						GameCanvas.Children.Add(image);
					}

					foreach (UIElement image in GameCanvas.Children) {
						Common.GameObjectState s = ((image as Image).Tag as Common.GameObjectState);
						Canvas.SetLeft(image, (int)(s.Pos.x) - camPos.x + halsScreenSize.x);
						Canvas.SetTop(image, (int)(s.Pos.y) - camPos.y + halsScreenSize.y);
					}
				});

			};
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

		}

		private void Window_Closed(object sender, EventArgs e) {

		}

		private void Window_KeyDown(object sender, KeyEventArgs e) {
			lock (keysLocker)
				if (!pressedKeys.Contains(e.Key))
					pressedKeys.Add(e.Key);
		}

		private void Window_KeyUp(object sender, KeyEventArgs e) {
			lock (keysLocker)
				pressedKeys.Remove(e.Key);
		}

		private void Window_MouseMove(object sender, MouseEventArgs e) {
			ChangeAngle(e.GetPosition(this));
		}

		void ChangeAngle(Point mspos) {
			double newAngleRad = 0;
			short newAngle = 0;

			double plposx = playerState.Pos.x + playerState.Size.width / 2,
				plposy = playerState.Pos.y + playerState.Size.height / 2;

			double xs = (mspos.X + camPos.x - halsScreenSize.x) - plposx,
				ys = (mspos.Y + camPos.y - halsScreenSize.y) - plposy;

			//Console.WriteLine($"pl = {plposx} {plposy}");
			//Console.WriteLine($"ms = {mspos.X} {mspos.Y}");
			//Console.WriteLine($"ss = {xs} {ys}");

			newAngleRad = Math.Atan(ys / xs);


			//if (ys >= 0 && xs >= 0)
			//	newAngleRad += Math.PI;
			if (ys >= 0 && xs <= 0)
				newAngleRad += Math.PI;
			if (ys <= 0 && xs <= 0)
				newAngleRad -= Math.PI;

			newAngle = (short)Math.Round(newAngleRad * (180 / Math.PI));

			//Console.WriteLine($"angle = {newAngle}");

			client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.PlayerChangeAngle) {
				newAngle = newAngle,
			});
		}



		private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
			halsScreenSize.x = (uint)Math.Round(RenderSize.Width / 2);
			halsScreenSize.y = (uint)Math.Round(RenderSize.Height / 2);
		}
	}
}
