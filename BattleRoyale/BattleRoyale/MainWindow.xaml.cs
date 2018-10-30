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

		public MainWindow() {
			InitializeComponent();

			client = new TCPClient();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			AllocConsole();

			client.Connect("127.0.0.1", 65000);
			client.OnWorldUpdate += (states) => {
				Console.WriteLine($"Recieve {states.Length * Common.GameObjectState.OneObjectSize} bytes {new DateTime(states[0].ticks).ToLongTimeString()}");
				foreach (var state in states) {
					
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
