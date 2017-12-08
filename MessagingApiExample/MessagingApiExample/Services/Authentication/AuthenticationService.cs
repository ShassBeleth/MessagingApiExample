using System;
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

		/// <summary>
		/// チャンネルシークレット
		/// </summary>
		/// <returns></returns>
		public string GetChannelSecret()
			=> "fbaa1452535d03f7bfc0596f0749834b";

		/// <summary>
		/// ロングタームのチャンネルアクセストークンを取得する
		/// </summary>
		/// <returns></returns>
		public string GetLongTermAccessToken()
			=> "N2m51pI1iWtf2kPc+6QfDPMWnSPDevD3O1qbelrRXyAZnTNuCeZw+533rh2/om6pBkDQp2Z/VmeLehoqGBZirPm1zg/7mPGUUraGyWcRiFqKkWaY9CBx3MdI2gAf+JjqTJG/8WU3yTnouopT435dWgdB04t89/1O/w1cDnyilFU=";
		
		/// <summary>
		/// 署名の検証
		/// </summary>
		/// <param name="xLineSignature">署名</param>
		/// <returns>検証の合否</returns>
		public bool VerifySign( string xLineSignature , object obj ) {

			Trace.TraceInformation( "Start Verify Sign" );

			try {

				byte[] channelSecretBytes = Encoding.UTF8.GetBytes( this.GetChannelSecret() );
				Trace.TraceInformation( "Encoding Secret Key" );

				HMACSHA256 hmacsha256 = new HMACSHA256( channelSecretBytes );
				Trace.TraceInformation( "Create HMAC-SHA256" );

				byte[] requestBodyBytes = null;
				Trace.TraceInformation( "Get Request Body Byte" );

				byte[] requestBodyDigest = hmacsha256.ComputeHash( requestBodyBytes );
				Trace.TraceInformation( "Get Request Body Digest" );

				string encodedRequestBodyDigest = Convert.ToBase64String( requestBodyDigest );
				Trace.TraceInformation( "Encoded Request Body Digest is :" + encodedRequestBodyDigest );

				Trace.TraceInformation( "X-Line Signature is :" + xLineSignature );
				
				return true;

			}
			catch( ArgumentNullException ) {

				return false;

			}
			
		}

		// TODO 未確認
		public async Task<ChannelAccessTokenResponse> IssueChannelAccessToken() {

			string jsonRequest = JsonConvert.SerializeObject(
				new ChannelAccessTokenRequest() {
					grant_type = "client_credentials" ,
					client_id = "" ,
					client_secret = ""
				}
			);
			Trace.TraceInformation( "Issue Channel Access Token Request is : " + jsonRequest );

			StringContent content = new StringContent( jsonRequest );
			content.Headers.ContentType = new MediaTypeHeaderValue( "application/x-www-form-urlencoded" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/x-www-form-urlencoded" ) );

			try {
				
				HttpResponseMessage response = await client.PostAsync( "https://api.line.me/v2/oauth/accessToken" , content );
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
		
	}

}