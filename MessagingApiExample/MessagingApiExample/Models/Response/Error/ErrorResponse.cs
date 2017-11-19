using MessagingApiExample.Models.Response.Error.Detail;

namespace MessagingApiExample.Models.Response.Error {

	/// <summary>
	/// エラーレスポンス
	/// </summary>
	public class MessagingApiErrorResponse {

		/// <summary>
		/// エラーの概要
		/// </summary>
		public string message;

		/// <summary>
		/// エラーの詳細
		/// </summary>
		public ErrorDetail[] details;

	}

}