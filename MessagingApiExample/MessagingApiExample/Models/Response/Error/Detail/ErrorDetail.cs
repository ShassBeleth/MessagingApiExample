namespace MessagingApiExample.Models.Response.Error.Detail {

	/// <summary>
	/// エラーレスポンスの詳細情報
	/// </summary>
	public class ErrorDetail {

		/// <summary>
		/// エラーの詳細
		/// </summary>
		public string message;

		/// <summary>
		/// エラーの発生個所
		/// </summary>
		public string property;

	}

}