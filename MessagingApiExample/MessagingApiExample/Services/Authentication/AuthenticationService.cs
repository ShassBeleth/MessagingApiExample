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
		public async Task<bool> VerifySign( string xLineSignature , HttpContent content ) {

			try {

				HMACSHA256 hmacSha256 = new HMACSHA256( Encoding.UTF8.GetBytes( this.GetChannelSecret() ) );
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