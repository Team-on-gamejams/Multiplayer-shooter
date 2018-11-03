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

using static BattleRoyale.Extensions;

namespace BattleRoyale {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		Common.IClient client;

		public MainWindow() {
			InitializeComponent();

			client = new TCPClient();
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
					});
				}

				this.Dispatcher.Invoke(() => {
					GameCanvas.Children.Clear();
					foreach (var image in newState)
						GameCanvas.Children.Add(image);
				});

			};
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

		}

		private void Window_Closed(object sender, EventArgs e) {

		}

		private void Window_KeyDown(object sender, KeyEventArgs e) {
			switch (e.Key) {
				case Key.None:
					break;
				case Key.Cancel:
					break;
				case Key.Back:
					break;
				case Key.Tab:
					break;
				case Key.LineFeed:
					break;
				case Key.Clear:
					break;
				case Key.Enter:
					break;
				case Key.Pause:
					break;
				case Key.Capital:
					break;
				case Key.KanaMode:
					break;
				case Key.JunjaMode:
					break;
				case Key.FinalMode:
					break;
				case Key.HanjaMode:
					break;
				case Key.Escape:
					break;
				case Key.ImeConvert:
					break;
				case Key.ImeNonConvert:
					break;
				case Key.ImeAccept:
					break;
				case Key.ImeModeChange:
					break;
				case Key.Space:
					break;
				case Key.PageUp:
					break;
				case Key.PageDown:
					break;
				case Key.End:
					break;
				case Key.Home:
					break;
				case Key.Left:
					break;
				case Key.Up:
					break;
				case Key.Right:
					break;
				case Key.Down:
					break;
				case Key.Select:
					break;
				case Key.Print:
					break;
				case Key.Execute:
					break;
				case Key.PrintScreen:
					break;
				case Key.Insert:
					break;
				case Key.Delete:
					break;
				case Key.Help:
					break;
				case Key.D0:
					break;
				case Key.D1:
					break;
				case Key.D2:
					break;
				case Key.D3:
					break;
				case Key.D4:
					break;
				case Key.D5:
					break;
				case Key.D6:
					break;
				case Key.D7:
					break;
				case Key.D8:
					break;
				case Key.D9:
					break;
				case Key.A:
					client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveLeft));
					break;
				case Key.B:
					break;
				case Key.C:
					break;
				case Key.D:
					client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveRight));
					break;
				case Key.E:
					break;
				case Key.F:
					break;
				case Key.G:
					break;
				case Key.H:
					break;
				case Key.I:
					break;
				case Key.J:
					break;
				case Key.K:
					break;
				case Key.L:
					break;
				case Key.M:
					break;
				case Key.N:
					break;
				case Key.O:
					break;
				case Key.P:
					break;
				case Key.Q:
					break;
				case Key.R:
					break;
				case Key.S:
					client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveBackward));
					break;
				case Key.T:
					break;
				case Key.U:
					break;
				case Key.V:
					break;
				case Key.W:
					client.SentPlayerAction(new Common.BasePlayerAction(Common.PlayerActionType.MoveForward));
					break;
				case Key.X:
					break;
				case Key.Y:
					break;
				case Key.Z:
					break;
				case Key.LWin:
					break;
				case Key.RWin:
					break;
				case Key.Apps:
					break;
				case Key.Sleep:
					break;
				case Key.NumPad0:
					break;
				case Key.NumPad1:
					break;
				case Key.NumPad2:
					break;
				case Key.NumPad3:
					break;
				case Key.NumPad4:
					break;
				case Key.NumPad5:
					break;
				case Key.NumPad6:
					break;
				case Key.NumPad7:
					break;
				case Key.NumPad8:
					break;
				case Key.NumPad9:
					break;
				case Key.Multiply:
					break;
				case Key.Add:
					break;
				case Key.Separator:
					break;
				case Key.Subtract:
					break;
				case Key.Decimal:
					break;
				case Key.Divide:
					break;
				case Key.F1:
					break;
				case Key.F2:
					break;
				case Key.F3:
					break;
				case Key.F4:
					break;
				case Key.F5:
					break;
				case Key.F6:
					break;
				case Key.F7:
					break;
				case Key.F8:
					break;
				case Key.F9:
					break;
				case Key.F10:
					break;
				case Key.F11:
					break;
				case Key.F12:
					break;
				case Key.F13:
					break;
				case Key.F14:
					break;
				case Key.F15:
					break;
				case Key.F16:
					break;
				case Key.F17:
					break;
				case Key.F18:
					break;
				case Key.F19:
					break;
				case Key.F20:
					break;
				case Key.F21:
					break;
				case Key.F22:
					break;
				case Key.F23:
					break;
				case Key.F24:
					break;
				case Key.NumLock:
					break;
				case Key.Scroll:
					break;
				case Key.LeftShift:
					break;
				case Key.RightShift:
					break;
				case Key.LeftCtrl:
					break;
				case Key.RightCtrl:
					break;
				case Key.LeftAlt:
					break;
				case Key.RightAlt:
					break;
				case Key.BrowserBack:
					break;
				case Key.BrowserForward:
					break;
				case Key.BrowserRefresh:
					break;
				case Key.BrowserStop:
					break;
				case Key.BrowserSearch:
					break;
				case Key.BrowserFavorites:
					break;
				case Key.BrowserHome:
					break;
				case Key.VolumeMute:
					break;
				case Key.VolumeDown:
					break;
				case Key.VolumeUp:
					break;
				case Key.MediaNextTrack:
					break;
				case Key.MediaPreviousTrack:
					break;
				case Key.MediaStop:
					break;
				case Key.MediaPlayPause:
					break;
				case Key.LaunchMail:
					break;
				case Key.SelectMedia:
					break;
				case Key.LaunchApplication1:
					break;
				case Key.LaunchApplication2:
					break;
				case Key.Oem1:
					break;
				case Key.OemPlus:
					break;
				case Key.OemComma:
					break;
				case Key.OemMinus:
					break;
				case Key.OemPeriod:
					break;
				case Key.Oem2:
					break;
				case Key.Oem3:
					break;
				case Key.AbntC1:
					break;
				case Key.AbntC2:
					break;
				case Key.Oem4:
					break;
				case Key.Oem5:
					break;
				case Key.Oem6:
					break;
				case Key.Oem7:
					break;
				case Key.Oem8:
					break;
				case Key.Oem102:
					break;
				case Key.ImeProcessed:
					break;
				case Key.System:
					break;
				case Key.OemAttn:
					break;
				case Key.OemFinish:
					break;
				case Key.OemCopy:
					break;
				case Key.OemAuto:
					break;
				case Key.OemEnlw:
					break;
				case Key.OemBackTab:
					break; ;
				case Key.Attn:
					break;
				case Key.CrSel:
					break;
				case Key.ExSel:
					break;
				case Key.EraseEof:
					break;
				case Key.Play:
					break;
				case Key.Zoom:
					break;
				case Key.NoName:
					break;
				case Key.Pa1:
					break;
				case Key.OemClear:
					break;
				case Key.DeadCharProcessed:
					break;
				default:
					break;
			}
		}
	}
}
