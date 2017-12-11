using MessagingApiTemplate.Models.Requests.Webhook.Event;

namespace MessagingApiTemplate.Models.Requests.Webhook {

	/// <summary>
	/// WebhookRequest
	/// </summary>
	public class WebhookRequest {

		/// <summary>
		/// イベントリスト
		/// </summary>
		public EventBase[] events;

	}
}