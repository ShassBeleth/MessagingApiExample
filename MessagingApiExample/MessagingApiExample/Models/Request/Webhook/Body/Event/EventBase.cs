using System;
using MessagingApiExample.Models.Webhook.Body.Event.Source;

namespace MessagingApiExample.Models.Webhook.Body.Event {

	/// <summary>
	/// イベント
	/// </summary>
	public abstract class EventBase {

		/// <summary>
		/// イベント発生時刻
		/// </summary>
		public DateTime timestamp;

		/// <summary>
		/// 送信元
		/// </summary>
		public SourceBase source;

	}

}