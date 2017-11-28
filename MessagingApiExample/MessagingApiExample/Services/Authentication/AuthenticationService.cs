using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiExample.Models.Request.Authentication;
using MessagingApiExample.Models.Response.Authentication;
using Newtonsoft.Json;

namespace MessagingApiExample.Services.Authentication {

	/// <summary>
	/// 認証用Service
	/// </summary>
	public class AuthenticationService {
		
		// TODO 未確認
		public async Task<ChannelAccessTokenResponse> IssueChannelAccessToken() {

			string jsonRequest = JsonConvert.SerializeObject(
				new ChannelAccessTokenRequest() {
					grant_type = "client_credentials" ,
					client_id = "" ,
					client_secret = ""
				}
			);
			Trace.TraceInformation( "Request is : " + jsonRequest );

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
				Trace.TraceInformation( "Response is : " +resultAsString );
				return JsonConvert.DeserializeObject<ChannelAccessTokenResponse>( resultAsString );

			}
			catch( ArgumentNullException e ) {
				content.Dispose();
				client.Dispose();
				return null;
			}
			catch( HttpRequestException e ) {
				content.Dispose();
				client.Dispose();
				return null;
			}
			catch( Exception e ) {
				content.Dispose();
				client.Dispose();
				return null;
			}

		}
		
	}

}