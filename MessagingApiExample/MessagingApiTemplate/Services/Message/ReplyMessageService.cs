using MessagingApiTemplate.Models.Requests.ReplyMessage;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Message {

	/// <summary>
	/// リプライ送信用サービス
	/// </summary>
	public static class ReplyMessageService {

		/// <summary>
		/// メッセージの返信
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="replyToken">ReplyToken</param>
		/// <param name="messageFactoryService">MessageFactoryService</param>
		public static async Task SendReplyMessage( string channelAccessToken , string replyToken , MessageFactoryService messageFactoryService ) {

			Trace.TraceInformation( "Start" );

			ReplyMessageRequest request = new ReplyMessageRequest() {
				replyToken = replyToken ,
				messages = messageFactoryService.Messages
			};
			string requestUrl = 
				ConfigurationManager.AppSettings[ "BaseUrl" ] + 
				ConfigurationManager.AppSettings[ "ReplyUrl" ];

			await MessagingApiSender.SendMessagingApi<ReplyMessageRequest , string>(
				channelAccessToken ,
				requestUrl ,
				request ,
				false
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

		}

	}

}