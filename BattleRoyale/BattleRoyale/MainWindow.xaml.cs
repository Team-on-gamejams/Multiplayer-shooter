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
		
		Common.GameObjectState playerState;

		Common.IClient client;

		Timer keysTimer;
		object keysLocker = new object();
		List<Key> pressedKeys;

		public MainWindow() {
			InitializeComponent();

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
						Canvas.SetLeft(image, state.Pos.x);
						Canvas.SetTop(image, state.Pos.y);
						newState.Add(image);
						image.Tag = state.Id;
					});

					if (state.Id == client.PlayerId)
						playerState = state;
				}

				this.Dispatcher.Invoke(() => {
					foreach (var image in newState) {
						foreach (UIElement c in GameCanvas.Children) {
							if ((ulong)(c as Image).Tag == (ulong)image.Tag) {
								GameCanvas.Children.Remove(c);
								break;
							}
						}

						GameCanvas.Children.Add(image);
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

			double xs = mspos.X - plposx,
				ys = mspos.Y - plposy;

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

			Console.WriteLine($"angle = {newAngle}");

			client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.PlayerChangeAngle) {
				newAngle = newAngle,
			});
		}
	}
}
