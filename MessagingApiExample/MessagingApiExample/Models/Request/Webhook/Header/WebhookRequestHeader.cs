namespace MessagingApiExample.Models.Request.Webhook.Header {

	/// <summary>
	/// Webhookのリクエストに含まれるHTTPヘッダ
	/// </summary>
	public class WebhookRequestHeader {

		/// <summary>
		/// 署名の検証に使う署名
		/// </summary>
		public string xLineSignature;

	}

}