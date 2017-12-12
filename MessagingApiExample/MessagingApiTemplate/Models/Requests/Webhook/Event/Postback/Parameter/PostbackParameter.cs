using System;

namespace MessagingApiTemplate.Models.Requests.Webhook.Event.Postback.Parameter {

	/// <summary>
	/// 日付選択時ポストバックで返されるデータ
	/// </summary>
	public class PostbackParameter {

		/// <summary>
		/// 日付
		/// </summary>
		public string date;

		/// <summary>
		/// 時刻
		/// </summary>
		public string time;

		/// <summary>
		/// 日時
		/// </summary>
		public string datetime;

	}

}