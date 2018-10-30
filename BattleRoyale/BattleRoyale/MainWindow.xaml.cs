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
using System.Windows.Shapes;

using System.Runtime.InteropServices;

namespace BattleRoyale {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		Common.IClient client;
		//List<Common.GameObjectState> statesHashed;

		public MainWindow() {
			InitializeComponent();

			client = new TCPClient();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			AllocConsole();

			client.Connect("127.0.0.1", 65000);
			bool readed = false;
			client.OnWorldUpdate += (states) => {
				if (readed)
					return;
				readed = true;

				Console.WriteLine($"Recieve {states.Length * Common.GameObjectState.OneObjectSize} bytes {new DateTime(states[0].ticks).ToLongTimeString()}    {states.Length}");

				//if (statesHashed == null)
				//	statesHashed = new List<Common.GameObjectState>(states);

				this.Dispatcher.Invoke(() => {
					GameCanvas.Children.Clear();
				});

				int i = 0;
				foreach (var state in states) {
					string path = @"D:\code\Battle-Royale\BattleRoyale\BattleRoyale\Resources\textures\";

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
					Console.WriteLine(path + $" {++i}");

					this.Dispatcher.Invoke(() => {
						Image image = new Image();
						image.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
						image.Width = state.Size.width;
						image.Height = state.Size.height;
						
						GameCanvas.Children.Add(image);

						Canvas.SetLeft(image, state.Pos.x);
						Canvas.SetTop(image, state.Pos.y);
					});
				}
			};
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

		}

		private void Window_Closed(object sender, EventArgs e) {

		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AllocConsole();
	}
}
