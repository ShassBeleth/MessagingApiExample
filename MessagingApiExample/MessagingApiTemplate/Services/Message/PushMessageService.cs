using MessagingApiTemplate.Models.Requests.PushMessage;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Message {

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
				messages = messageFactoryService.Messages
			};
			string requestUrl = ConfigurationManager.AppSettings[ "BaseUrl" ] + ConfigurationManager.AppSettings[ "PushUrl" ];

			await MessagingApiSender.SendMessagingApi<PushMessageRequest , string>(
				channelAccessToken ,
				requestUrl ,
				request ,
				false
			);

		}

		/// <summary>
		/// 複数人同時プッシュ送信
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="to">送信先IDの配列</param>
		/// <param name="messageFactoryService">MessageFactoryService</param>
		public static async Task SendMultiCastPushMessage( string channelAccessToken , string[] to , MessageFactoryService messageFactoryService ) {

			Trace.TraceInformation( "Start Send Multi Cast Push Message" );

			MultiCastMessageRequest request = new MultiCastMessageRequest() {
				to = to ,
				messages = messageFactoryService.Messages
			};
			string requestUrl = ConfigurationManager.AppSettings[ "BaseUrl" ] + ConfigurationManager.AppSettings[ "MultiCastUrl" ];

			await MessagingApiSender.SendMessagingApi<MultiCastMessageRequest , string>(
				channelAccessToken ,
				requestUrl ,
				request ,
				false
			);

		}

	}

}