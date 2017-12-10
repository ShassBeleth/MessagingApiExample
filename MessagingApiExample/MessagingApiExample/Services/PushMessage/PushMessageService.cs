using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiExample.Models.Request.PushMessage;
using MessagingApiExample.Services.MessageFactory;
using Newtonsoft.Json;

namespace MessagingApiExample.Services.PushMessage {

	/// <summary>
	/// Push通知を送るService
	/// </summary>
	public class PushMessageService {

		/// <summary>
		/// プッシュ送信
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="to">送信先ID</param>
		/// <param name="messageFactoryService">MessageFactoryService</param>
		public static async Task SendReplyMessage( string channelAccessToken , string to , MessageFactoryService messageFactoryService ) {

			Trace.TraceInformation( "Start Send Push Message" );

			PushMessageRequest request = new PushMessageRequest() {
				to = to ,
				messages = messageFactoryService.messages
			};

			string jsonRequest = JsonConvert.SerializeObject( request );
			Trace.TraceInformation( "Push Message Request is : " + jsonRequest );

			StringContent content = new StringContent( jsonRequest );
			content.Headers.ContentType = new MediaTypeHeaderValue( "application/json" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl = ConfigurationManager.AppSettings[ "BaseUrl" ] + ConfigurationManager.AppSettings[ "PushUrl" ];

			try {

				HttpResponseMessage response = await client.PostAsync( requestUrl , content );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				content.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Send Push Message Response is : " + resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Send Push Message is Argument Null Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Send Push Message is Http Request Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( Exception ) {
				Trace.TraceError( "Send Push Message is Unexpected Exception" );
				content.Dispose();
				client.Dispose();
			}

		}

	}

}