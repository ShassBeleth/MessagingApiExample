using MessagingApiExample.Models.Webhook.Event.Postback.Parameter;

namespace MessagingApiExample.Models.Webhook.Event.Postback {

	/// <summary>
	/// ポストバック
	/// </summary>
	public class PostbackData {

		/// <summary>
		/// ポストバックで返されるデータ
		/// </summary>
		public string data;

		/// <summary>
		/// 日付選択時ポストバックで返されるデータ
		/// </summary>
		public PostbackParameter parameters;
		
	}

}