namespace MessagingApiExample.Models.Webhook.Body.Event.Beacon {

	/// <summary>
	/// ビーコン情報
	/// </summary>
	public class BeaconBase {

		/// <summary>
		/// 発見したビーコンデバイスのハードウェアID
		/// </summary>
		public string hwid;

		/// <summary>
		/// 発見したビーコンデバイスのデバイスメッセージ
		/// </summary>
		public string dm;

	}

}