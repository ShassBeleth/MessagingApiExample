using MessagingApiTemplate.Models.Requests.Authentication;
using MessagingApiTemplate.Models.Responses.Authentication;
using MessagingApiTemplate.Utils;
using System;
using System.Configuration;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Webhook {

	/// <summary>
	/// 認証についてのService
	/// </summary>
	internal static class AuthenticationService {

		// TODO OKにならない
		/// <summary>
		/// 署名の検証
		/// </summary>
		/// <param name="xLineSignature">署名</param>
		/// <param name="content">リクエストコンテント</param>
		/// <returns>検証の合否</returns>
		internal static async Task<bool> VerifySign( string xLineSignature , HttpContent content ) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( xLineSignature == null ) {
				Trace.TraceWarning( "X Line Signature is Null" );
				return false;
			}
			if( content == null ) {
				Trace.TraceWarning( "Content is Null" );
				return false;
			}

			try {

				// シークレットチャンネルアクセストークンをキーにSHA256ハッシュを作成
				HMACSHA256 hmacSha256 = new HMACSHA256( Encoding.UTF8.GetBytes( ConfigurationManager.AppSettings[ "SecretChannelToken" ] ) );

				// リクエストコンテンツをハッシュ化
				byte[] computeHash = hmacSha256.ComputeHash( Encoding.UTF8.GetBytes( await content.ReadAsStringAsync() ) );

				// base64文字列に変換
				string base64Content = Convert.ToBase64String( computeHash );

				// ヘッダにある署名と暗号情報が等しければOK
				if( xLineSignature.Equals( base64Content ) ) {
					Trace.TraceInformation( "Verify Sign is OK" );
					Trace.TraceInformation( "End" );
					return true;
				}
				else {
					Trace.TraceInformation( "Verify Sign is NG" );
					Trace.TraceInformation( "End" );
					return false;
				}

			}
			catch( ArgumentNullException ) {
				System.Diagnostics.Trace.TraceInformation( "Verify Sign is Argument Null Exception" );
				Trace.TraceInformation( "End" );
				return false;
			}

		}

		// TODO 取得できない
		/// <summary>
		/// チャンネルアクセストークンの発行
		/// </summary>
		/// <returns>レスポンス</returns>
		public static async Task<IssueChannelAccessTokenResponse> IssueChannelAccessToken() {

			Trace.TraceInformation( "Start" );

			IssueChannelAccessTokenRequest request = new IssueChannelAccessTokenRequest() {
				grant_type = "client_credentials" ,
				client_id = ConfigurationManager.AppSettings[ "ChannelId" ] ,
				client_secret = ConfigurationManager.AppSettings[ "SecretChannelToken" ]
			};
			string requestUrl = 
				ConfigurationManager.AppSettings[ "BaseUrl" ] + 
				ConfigurationManager.AppSettings[ "IssueChannelAccessTokenUrl" ];

			IssueChannelAccessTokenResponse response = await MessagingApiSender.SendMessagingApi<IssueChannelAccessTokenRequest , IssueChannelAccessTokenResponse>(
				null ,
				requestUrl ,
				request ,
				false ,
				"application/x-www-form-urlencoded"
			);

			Trace.TraceInformation( "End" );

			return response;

		}

		// TODO 未確認
		/// <summary>
		/// チャンネルアクセストークンを取り消す
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <returns>レスポンス</returns>
		public static async Task RevokeChannelAccessToken( string channelAccessToken ) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				System.Diagnostics.Trace.TraceWarning( "Channel Access Token is Null" );
				return;
			}

			RevokeChannelAccessTokenRequest request = new RevokeChannelAccessTokenRequest() {
				access_token = channelAccessToken
			};
			string requestUrl = 
				ConfigurationManager.AppSettings[ "BaseUrl" ] + 
				ConfigurationManager.AppSettings[ "RevokeChannelAccessTokenUrl" ];

			await MessagingApiSender.SendMessagingApi<RevokeChannelAccessTokenRequest , string>(
				null ,
				requestUrl ,
				request ,
				false ,
				"application/x-www-form-urlencoded"
			);

			Trace.TraceInformation( "End" );

		}

	}

}