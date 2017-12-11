using MessagingApiTemplate.Models.Requests.Webhook.Event.Source;

namespace MessagingApiTemplate.Models.Requests.Webhook.Event {

	/// <summary>
	/// イベント
	/// </summary>
	public abstract class EventBase {

		/// <summary>
		/// イベント発生時刻
		/// </summary>
		public long timestamp;

		/// <summary>
		/// 送信元
		/// </summary>
		public SourceBase source;

	}

}