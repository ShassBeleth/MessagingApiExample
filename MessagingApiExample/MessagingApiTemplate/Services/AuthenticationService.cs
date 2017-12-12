using MessagingApiTemplate.Models.Requests.Authentication;
using MessagingApiTemplate.Models.Responses.Authentication;
using MessagingApiTemplate.Utils;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services {

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

			Trace.TraceInformation( "Start Verify Sign" );

			// 引数のnullチェック
			if( xLineSignature == null ) {
				Trace.TraceWarning( "X Line Signature Of Verify Sign is Null" );
				return false;
			}
			if( content == null ) {
				Trace.TraceWarning( "Content Of Verify Sign is Null" );
				return false;
			}

			try {

				HMACSHA256 hmacSha256 = new HMACSHA256( Encoding.UTF8.GetBytes( ConfigurationManager.AppSettings[ "SecretChannelToken" ] ) );
				byte[] computeHash = hmacSha256.ComputeHash( Encoding.UTF8.GetBytes( await content.ReadAsStringAsync() ) );
				string base64Content = Convert.ToBase64String( computeHash );
				if( xLineSignature.Equals( base64Content ) ) {
					Trace.TraceInformation( "Verify Sign is OK" );
					return true;
				}
				else {
					Trace.TraceInformation( "Verify Sign is NG" );
					return false;
				}

			}
			catch( ArgumentNullException ) {
				Trace.TraceInformation( "Verify Sign is Argument Null Exception" );
				return false;
			}

		}

		// TODO 取得できない
		/// <summary>
		/// チャンネルアクセストークンの発行
		/// </summary>
		/// <returns>レスポンス</returns>
		public static async Task<IssueChannelAccessTokenResponse> IssueChannelAccessToken() {

			Trace.TraceInformation( "Start Issue Channel Access Token" );

			IssueChannelAccessTokenRequest request = new IssueChannelAccessTokenRequest() {
				grant_type = "client_credentials" ,
				client_id = ConfigurationManager.AppSettings[ "ChannelId" ] ,
				client_secret = ConfigurationManager.AppSettings[ "SecretChannelToken" ]
			};
			string requestUrl = 
				ConfigurationManager.AppSettings[ "BaseUrl" ] + 
				ConfigurationManager.AppSettings[ "IssueChannelAccessTokenUrl" ];

			return await MessagingApiSender.SendMessagingApi<IssueChannelAccessTokenRequest , IssueChannelAccessTokenResponse>(
				null ,
				requestUrl ,
				request ,
				false ,
				"application/x-www-form-urlencoded"
			);

		}

		// TODO 未確認
		/// <summary>
		/// チャンネルアクセストークンを取り消す
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <returns>レスポンス</returns>
		public static async Task RevokeChannelAccessToken( string channelAccessToken ) {

			Trace.TraceInformation( "Start Revoke Channel Access Token" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Revoke Channel Access Token is Null" );
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

		}

	}

}