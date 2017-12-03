using MessagingApiExample.Models.Request.Webhook.Body.Event.Beacon;

namespace MessagingApiExample.Models.Request.Webhook.Body.Event {

	/// <summary>
	/// ビーコンイベント
	/// </summary>
	public class BeaconEvent : EventBase {
		
		/// <summary>
		/// リプライトークン
		/// </summary>
		public string replyToken;

		/// <summary>
		/// ビーコン情報
		/// </summary>
		public BeaconBase beacon;

	}

}