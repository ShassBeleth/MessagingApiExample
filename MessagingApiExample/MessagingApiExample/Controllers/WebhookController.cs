using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using MessagingApiTemplate.Services.Webhook;
using MessagingApiTemplate.Models.Config.Webhook;
using MessagingApiTemplate.Utils;

namespace MessagingApiExample.Controllers {

	/// <summary>
	/// Messaging APIよりコールされるWebhook API
	/// </summary>
	public class WebhookController : ApiController {
		
		/// <summary>
		/// POSTメソッド
		/// </summary>
		/// <param name="requestToken">リクエストトークン</param>
		/// <returns>常にステータス200のみを返す</returns>
		public async Task<HttpResponseMessage> Post( JToken requestToken ) {
			
			Trace.TraceInformation( "Webhook API Start" );

			// Webhook Serviceの実行
			await WebhookService.Execute(

				// Webhook Serviceの設定
				new WebhookServiceConfig() {

					RequestJToken = requestToken ,
					RequestHeaders = this.Request.Headers ,
					RequestContent = this.Request.Content ,

					// 署名の検証は行わない
					IsExecuteVerifySign = false ,

					// ロングタームチャンネルアクセストークンを使用する
					IsUseLongTermChannelAccessToken = true ,

					// フォローイベント
					FollowEventHandler = async ( channelAccessToken , replyToken ) => await this.ExecuteFollowEvent( channelAccessToken , replyToken ) ,

					// 参加イベント
					JoinEventHandler = async ( channelAccessToken , replyToken ) => await this.ExecuteJoinEvent( channelAccessToken , replyToken ) ,

					// テキストイベント
					TextMessageEventHandler = async () => await this.ExecuteTextMessageEvent()

				}

			);

			Trace.TraceInformation( "Webhook API End" );
			
			return new HttpResponseMessage( HttpStatusCode.OK );

		}

		/// <summary>
		/// 友達追加、ブロック解除イベント実行
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		private async Task ExecuteFollowEvent( 
			string channelAccessToken ,
			string replyToken
		) {
			Trace.TraceInformation( "Start" );
			await Task.CompletedTask;
			Trace.TraceInformation( "End" );
		}

		/// <summary>
		/// グループ追加イベント実行
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		private async Task ExecuteJoinEvent(
			string channelAccessToken ,
			string replyToken
		) {
			Trace.TraceInformation( "Start" );
			await Task.CompletedTask;
			Trace.TraceInformation( "End" );
		}

		private async Task ExecuteTextMessageEvent() {
			Trace.TraceInformation( "Start" );
			await Task.CompletedTask;
			Trace.TraceInformation( "End" );
		}

	}

}