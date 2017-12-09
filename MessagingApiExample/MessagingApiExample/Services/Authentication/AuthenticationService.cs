using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MessagingApiExample.Models.Request.Authentication;
using MessagingApiExample.Models.Response.Authentication;
using Newtonsoft.Json;

namespace MessagingApiExample.Services.Authentication {

	/// <summary>
	/// 認証用Service
	/// </summary>
	public class AuthenticationService {
		
		// TODO OKにならない
		/// <summary>
		/// 署名の検証
		/// </summary>
		/// <param name="xLineSignature">署名</param>
		/// <param name="content">リクエストコンテント</param>
		/// <returns>検証の合否</returns>
		public async Task<bool> VerifySign( string xLineSignature , HttpContent content ) {

			Trace.TraceInformation( "Start Verify Sign" );

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
		public async Task<ChannelAccessTokenResponse> IssueChannelAccessToken() {

			Trace.TraceInformation( "Start Issue Channel Access Token" );

			string jsonRequest = JsonConvert.SerializeObject(
				new ChannelAccessTokenRequest() {
					grant_type = "client_credentials" ,
					client_id = ConfigurationManager.AppSettings[ "ChannelId" ] ,
					client_secret = ConfigurationManager.AppSettings[ "SecretChannelToken" ]
				}
			);

			Trace.TraceInformation( "Issue Channel Access Token Request is : " + jsonRequest );

			StringContent content = new StringContent( jsonRequest );
			content.Headers.ContentType = new MediaTypeHeaderValue( "application/x-www-form-urlencoded" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/x-www-form-urlencoded" ) );
			string requestUrl = ConfigurationManager.AppSettings[ "BaseUrl" ] + ConfigurationManager.AppSettings[ "IssueChannelAccessTokenUrl" ];

			try {
				
				HttpResponseMessage response = await client.PostAsync( requestUrl  , content );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				content.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Issue Channel Access Token Response is : " +resultAsString );
				return JsonConvert.DeserializeObject<ChannelAccessTokenResponse>( resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Issue Channel Access Token is Argument Null Exception" );
				content.Dispose();
				client.Dispose();
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Issue Channel Access Token is Http Request Exception" );
				content.Dispose();
				client.Dispose();
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Issue Channel Access Token is Unexpected Exception" );
				content.Dispose();
				client.Dispose();
				return null;
			}

		}

		// TODO 未確認
		/// <summary>
		/// チャンネルアクセストークンを取り消す
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <returns>レスポンス</returns>
		public async Task RevokeChannelAccessToken( string channelAccessToken ) {

			Trace.TraceInformation( "Start Revoke Channel Access Token" );

			string jsonRequest = "{ \"access_token\":\"" + channelAccessToken + "\" }";
			Trace.TraceInformation( "Revoke Channel Access Token Request is : " + jsonRequest );

			StringContent content = new StringContent( jsonRequest );
			content.Headers.ContentType = new MediaTypeHeaderValue( "application/x-www-form-urlencoded" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/x-www-form-urlencoded" ) );
			string requestUrl = ConfigurationManager.AppSettings[ "BaseUrl" ] + ConfigurationManager.AppSettings[ "RevokeChannelAccessTokenUrl" ];

			try {

				HttpResponseMessage response = await client.PostAsync( requestUrl , content );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				content.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Revoke Channel Access Token Response is : OK" );
			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Revoke Channel Access Token is Argument Null Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Revoke Channel Access Token is Http Request Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( Exception ) {
				Trace.TraceError( "Revoke Channel Access Token is Unexpected Exception" );
				content.Dispose();
				client.Dispose();
			}

		}

	}

}