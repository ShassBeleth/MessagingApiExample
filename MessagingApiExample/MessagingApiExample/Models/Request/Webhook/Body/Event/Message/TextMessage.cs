﻿namespace MessagingApiExample.Models.Webhook.Body.Event.Message {

	/// <summary>
	/// テキストメッセージ
	/// </summary>
	public class TextMessage : MessageBase {

		/// <summary>
		/// メッセージID
		/// </summary>
		public string id;

		/// <summary>
		/// メッセージ本文
		/// </summary>
		public string text;

	}

}