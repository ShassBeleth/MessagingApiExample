using System;

namespace MessagingApiExample.Models.Request.Webhook.Body.Event.Postback.Parameter {

	/// <summary>
	/// 日付選択時ポストバックで返されるデータ
	/// </summary>
	public class PostbackParameter {

		/// <summary>
		/// 日付
		/// </summary>
		public DateTime date;

		/// <summary>
		/// 時刻
		/// </summary>
		public TimeSpan time;

		/// <summary>
		/// 日時
		/// </summary>
		public DateTime datetime;

	}

}