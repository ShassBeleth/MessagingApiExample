using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiTemplate.Models.Requests.Webhook;
using MessagingApiTemplate.Models.Requests.Webhook.Event;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Beacon;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Message;
using MessagingApiTemplate.Models.Responses.Authentication;
using MessagingApiTemplate.Utils;
using Newtonsoft.Json.Linq;

namespace MessagingApiTemplate.Services {

	/// <summary>
	/// WebhookについてのService
	/// </summary>
	public class WebhookService {

		/// <summary>
		/// リクエストz
		/// </summary>
		private WebhookRequest request;

		/// <summary>
		/// シグネチャ
		/// </summary>
		private string signature;

		/// <summary>
		/// 署名の検証結果
		/// </summary>
		private bool verifySignResult;

		/// <summary>
		/// チャンネルアクセストークン
		/// </summary>
		private string channelAccessToken;

		/// <summary>
		/// コンストラクタは隠す
		/// </summary>
		private WebhookService() { }

		public static async Task ExecuteService(
			JToken request ,
			HttpRequestHeaders headers ,
			HttpContent content ,
			bool isExecuteVerifySign = true ,
			bool isUseLongTermChannelAccessToken = false ,
			Action followEventHandler = null,
			Action joinEventHandler = null ,
			Action leaveEventHandler = null ,
			Action audioMessageEventHandler = null ,
			Action fileMessageEventHandler = null ,
			Action ImageMessageEventHandler = null ,
			Action locationMessageEventHandler = null ,
			Action stickerMessageEventHandler = null ,
			Action textMessageEventHandler = null ,
			Action videoMessageEventHandler = null ,
			Action postbackMessageEventHandler = null ,
			Action unfollowEventHandler = null ,
			Action bannerBeaconEventHandler = null ,
			Action enterBeaconEventHandler = null ,
			Action leaveBeaconEventHandler = null
		) {

			WebhookService webhookService = new WebhookService() {

				// リクエストトークンをデータモデルに変換
				request = JTokenConverter.ConvertJTokenToWebhookRequest( request ) ,

				// シグネチャ取得
				signature = GetSignature( headers ) ,

				// 署名の検証
				verifySignResult = await AuthenticationService.VerifySign( GetSignature( headers ) , content ) ,

			};

			Trace.TraceInformation( "Signature is :" + webhookService.signature );

			// 署名の検証
			if( isExecuteVerifySign ) {
				if( !( webhookService.verifySignResult ) ) {
					Trace.TraceWarning( "Verify Sign is NG" );
					Trace.TraceInformation( "Webhook API End" );
					return;
				}
			}

			// チャンネルアクセストークン取得
			if( isUseLongTermChannelAccessToken )
				webhookService.channelAccessToken = ConfigurationManager.AppSettings[ "LongTermChannelAccessToken" ];

			else {
				IssueChannelAccessTokenResponse channelAccessTokenResponse = await AuthenticationService.IssueChannelAccessToken();
				webhookService.channelAccessToken = channelAccessTokenResponse.access_token;
			}

			// イベント毎に分岐
			foreach( EventBase webhookEvent in webhookService.request.events ) {

				// 友達追加時イベント
				if( webhookEvent is FollowEvent )
					followEventHandler?.Invoke();

				// ブロック時イベント
				else if( webhookEvent is UnfollowEvent )
					unfollowEventHandler?.Invoke();

				// グループ追加時イベント
				else if( webhookEvent is JoinEvent )
					joinEventHandler?.Invoke();

				// グループ退会時イベント
				else if( webhookEvent is LeaveEvent )
					leaveEventHandler?.Invoke();

				// メッセージイベント
				else if( webhookEvent is MessageEvent ) {

					// 音声
					if( ( (MessageEvent)webhookEvent ).message is AudioMessage )
						audioMessageEventHandler?.Invoke();

					// ファイル
					else if( ( (MessageEvent)webhookEvent ).message is FileMessage )
						fileMessageEventHandler?.Invoke();

					// 画像
					else if( ( (MessageEvent)webhookEvent ).message is ImageMessage )
						ImageMessageEventHandler?.Invoke();


					// 位置情報
					else if( ( (MessageEvent)webhookEvent ).message is LocationMessage )
						locationMessageEventHandler?.Invoke();

					// スタンプ
					else if( ( (MessageEvent)webhookEvent ).message is StickerMessage )
						stickerMessageEventHandler?.Invoke();

					// テキスト
					else if( ( (MessageEvent)webhookEvent ).message is TextMessage )
						textMessageEventHandler?.Invoke();

					// 動画
					else if( ( (MessageEvent)webhookEvent ).message is VideoMessage )
						videoMessageEventHandler?.Invoke();


					// 想定外のイベントの時は何もしない
					else
						Trace.TraceError( "Unexpected Type" );

				}

				// ポストバックイベント
				else if( webhookEvent is PostbackEvent )
					postbackMessageEventHandler?.Invoke();

				// ビーコンイベント
				else if( webhookEvent is BeaconEvent ) {

					// バナータップ時イベント
					if( ( (BeaconEvent)webhookEvent ).beacon is BannerBeacon )
						bannerBeaconEventHandler?.Invoke();

					// バナー受信圏内に入った時のイベント
					else if( ( (BeaconEvent)webhookEvent ).beacon is EnterBeacon )
						enterBeaconEventHandler?.Invoke();

					// バナー受信圏外に出た時のイベント
					else if( ( (BeaconEvent)webhookEvent ).beacon is LeaveBeacon )
						leaveBeaconEventHandler?.Invoke();

					// 想定外のイベントの時は何もしない
					else
						Trace.TraceError( "Unexpected Beacon Type" );

				}

				else
					Trace.TraceError( "Unexpected Message Type" );

			}

		}

		/// <summary>
		/// シグネチャ取得
		/// </summary>
		/// <returns>シグネチャ</returns>
		private static string GetSignature( HttpRequestHeaders headers ) {
			IEnumerable<string> signatures = headers.GetValues( "X-Line-Signature" );
			string signature = "";
			foreach( string item in signatures ) {
				signature = item;
				Trace.TraceInformation( "signature : " + item );
			}
			return signature;
		}

	}

}