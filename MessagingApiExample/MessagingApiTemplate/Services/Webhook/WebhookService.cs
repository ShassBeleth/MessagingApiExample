using System.Collections.Generic;
using System.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiTemplate.Models.Requests.Webhook.Event;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Beacon;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Message;
using MessagingApiTemplate.Models.Responses.Authentication;
using MessagingApiTemplate.Utils;
using MessagingApiTemplate.Models.Config.Webhook;
using MessagingApiTemplate.Models.Requests.Webhook;

namespace MessagingApiTemplate.Services.Webhook {

	/// <summary>
	/// WebhookについてのService
	/// </summary>
	public static class WebhookService {
		
		/// <summary>
		/// サービス実行
		/// </summary>
		/// <param name="config">設定項目</param>
		public static async Task Execute( WebhookServiceConfig config ) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( config == null ) {
				Trace.TraceInformation( "Config is Null" );
				return;
			}
						
			// 署名の検証を行う場合
			if( config.IsExecuteVerifySign ) {

				// 署名の検証
				bool verifySignResult = await AuthenticationService.VerifySign( GetSignature( config.RequestHeaders ) , config.RequestContent ).ConfigureAwait( false );

				// NGなら以下の処理を行わない
				if( !verifySignResult ) {
					Trace.TraceInformation( "Verify Sign Result is NG" );
					return;
				}

			}

			// リクエストトークンをデータモデルに変換
			WebhookRequest request = JTokenConverter.ConvertJTokenToWebhookRequest( config.RequestJToken ) ?? new WebhookRequest();
			
			// チャンネルアクセストークンの取得
			string channelAccessToken;

			// ロングタームチャンネルアクセストークンを使用する場合
			if( config.IsUseLongTermChannelAccessToken )
				channelAccessToken = ConfigurationManager.AppSettings[ "LongTermChannelAccessToken" ];

			// ロングタームチャンネルアクセストークンを使用しない場合はチャンネルアクセストークンを発行する
			else {
				IssueChannelAccessTokenResponse channelAccessTokenResponse = await AuthenticationService.IssueChannelAccessToken().ConfigureAwait( false );
				channelAccessToken = channelAccessTokenResponse?.access_token;
			}

			// イベント毎に分岐
			foreach( EventBase webhookEvent in request.events ) {

				// 友達追加時イベント
				if( webhookEvent is FollowEvent )
					config.FollowEventHandler?.Invoke( channelAccessToken , ( webhookEvent as FollowEvent )?.replyToken );

				// ブロック時イベント
				else if( webhookEvent is UnfollowEvent )
					config.UnfollowEventHandler?.Invoke();

				// グループ追加時イベント
				else if( webhookEvent is JoinEvent )
					config.JoinEventHandler?.Invoke( channelAccessToken , ( webhookEvent as JoinEvent )?.replyToken );

				// グループ退会時イベント
				else if( webhookEvent is LeaveEvent )
					config.LeaveEventHandler?.Invoke();

				// メッセージイベント
				else if( webhookEvent is MessageEvent ) {

					MessageEvent messageEvent = webhookEvent as MessageEvent;

					// 音声
					if( messageEvent?.message is AudioMessage )
						config.AudioMessageEventHandler?.Invoke();

					// ファイル
					else if( messageEvent?.message is FileMessage )
						config.FileMessageEventHandler?.Invoke();

					// 画像
					else if( messageEvent?.message is ImageMessage )
						config.ImageMessageEventHandler?.Invoke();

					// 位置情報
					else if( messageEvent?.message is LocationMessage )
						config.LocationMessageEventHandler?.Invoke();

					// スタンプ
					else if( messageEvent?.message is StickerMessage )
						config.StickerMessageEventHandler?.Invoke();

					// テキスト
					else if( messageEvent?.message is TextMessage ) {
						TextMessage textMessage = messageEvent?.message as TextMessage;
						config.TextMessageEventHandler?.Invoke( channelAccessToken , messageEvent?.source , messageEvent?.replyToken , textMessage?.text );
					}

					// 動画
					else if( messageEvent?.message is VideoMessage )
						config.VideoMessageEventHandler?.Invoke();

					// 想定外のイベントの時は何もしない
					else
						Trace.TraceError( "Unexpected Type" );

				}

				// ポストバックイベント
				else if( webhookEvent is PostbackEvent )
					config.PostbackMessageEventHandler?.Invoke();

				// ビーコンイベント
				else if( webhookEvent is BeaconEvent ) {

					// バナータップ時イベント
					if( ( (BeaconEvent)webhookEvent )?.beacon is BannerBeacon )
						config.BannerBeaconEventHandler?.Invoke();

					// バナー受信圏内に入った時のイベント
					else if( ( (BeaconEvent)webhookEvent )?.beacon is EnterBeacon )
						config.EnterBeaconEventHandler?.Invoke();

					// バナー受信圏外に出た時のイベント
					else if( ( (BeaconEvent)webhookEvent )?.beacon is LeaveBeacon )
						config.LeaveBeaconEventHandler?.Invoke();

					// 想定外のイベントの時は何もしない
					else
						Trace.TraceError( "Unexpected Beacon Type" );

				}

				else
					Trace.TraceError( "Unexpected Message Type" );

			}

			Trace.TraceInformation( "End" );

		}

		/// <summary>
		/// シグネチャ取得
		/// </summary>
		/// <returns>シグネチャ</returns>
		private static string GetSignature( HttpRequestHeaders headers ) {

			Trace.TraceInformation( "Start" );

			if( headers == null ) {
				Trace.TraceWarning( "Headers Of Get Signature is Null" );
				return null;
			}

			IEnumerable<string> signatures = headers.GetValues( "X-Line-Signature" );

			string signature = "";
			foreach( string item in signatures ) {
				signature = item;
				Trace.TraceInformation( "Signature is " + item );
			}

			Trace.TraceInformation( "End" );

			return signature;

		}

	}

}