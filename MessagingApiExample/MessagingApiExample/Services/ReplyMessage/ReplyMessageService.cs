using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiExample.Models.Request.ReplyMessage;
using MessagingApiExample.Services.MessageFactory;
using Newtonsoft.Json;

namespace MessagingApiExample.Services.ReplyMessage {

	/// <summary>
	/// リクエスト送信用サービス
	/// </summary>
	public class ReplyMessageService {

		/// <summary>
		/// メッセージの返信
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="replyToken">ReplyToken</param>
		/// <param name="messageFactoryService">MessageFactoryService</param>
		public static async Task SendReplyMessage( string channelAccessToken , string replyToken , MessageFactoryService messageFactoryService ) {

			Trace.TraceInformation( "Start Send Reply Message" );

			ReplyMessageRequest request = new ReplyMessageRequest() {
				replyToken = replyToken ,
				messages = messageFactoryService.Messages
			};

			string jsonRequest = JsonConvert.SerializeObject( request );
			Trace.TraceInformation( "Reply Message Request is : " + jsonRequest );

			StringContent content = new StringContent( jsonRequest );
			content.Headers.ContentType = new MediaTypeHeaderValue( "application/json" );
			
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );
			
			string requestUrl = ConfigurationManager.AppSettings[ "BaseUrl" ] + ConfigurationManager.AppSettings[ "ReplyUrl" ];

			try {

				HttpResponseMessage response = await client.PostAsync( requestUrl , content );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				content.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Send Reply Message Response is : " + resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Send Reply Message is Argument Null Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Send Reply Message is Http Request Exception" );
				content.Dispose();
				client.Dispose();
			}
			catch( Exception ) {
				Trace.TraceError( "Send Reply Message is Unexpected Exception" );
				content.Dispose();
				client.Dispose();
			}

		}
		
	}

}