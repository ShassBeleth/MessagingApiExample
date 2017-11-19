using MessagingApiExample.Models.Webhook.Event;

namespace MessagingApiExample.Models.Request.Webhook.Body {

	/// <summary>
	/// WebhookRequest
	/// </summary>
	public partial class WebhookRequest {

		/// <summary>
		/// イベントリスト
		/// </summary>
		public EventBase[] events;

	}

}