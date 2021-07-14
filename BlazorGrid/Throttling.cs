using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorGrid
{
	public static class Throttling
	{
		/// <summary>Ignores events that are followed by another event within
		/// a specified relative time duration.</summary>
		public static EventHandler<TEventArgs> Throttle<TEventArgs>(
			EventHandler<TEventArgs> handler,
			TimeSpan dueTime)
		{
			Timer timer = null;
			return (s, e) =>
			{
				var newTimer = new Timer(
					_ => handler(s, e), null, dueTime, Timeout.InfiniteTimeSpan);
				var previousTimer = Interlocked.Exchange(ref timer, newTimer);
				previousTimer?.Dispose();
			};
		}
	}



}