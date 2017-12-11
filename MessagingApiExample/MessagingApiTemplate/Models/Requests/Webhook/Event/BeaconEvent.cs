using MessagingApiTemplate.Models.Requests.Webhook.Event.Beacon;

namespace MessagingApiTemplate.Models.Requests.Webhook.Event {

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