using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MessagingApiExample.Models.Request.Webhook.Body;
using MessagingApiExample.Models.Webhook.Body.Event;
using MessagingApiExample.Models.Webhook.Body.Event.Beacon;
using MessagingApiExample.Models.Webhook.Body.Event.Message;
using MessagingApiExample.Services.Authentication;
using MessagingApiExample.Services.JTokenConverter;
using Newtonsoft.Json.Linq;

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
		public HttpResponseMessage Post( JToken requestToken ) {

			Trace.TraceInformation( "Webhook API Start" );

			// リクエストトークンをデータモデルに変換
			ConvertJTokenService convertJTokenService = new ConvertJTokenService();
			WebhookRequest webhookRequest = convertJTokenService.ConvertJTokenToWebhookRequest( requestToken );

			// TODO 署名の検証

			// チャンネルアクセストークン取得
			AuthenticationService authenticationService = new AuthenticationService();

			#region リクエスト毎にチャンネルアクセストークンを発行する
			// ChannelAccessTokenResponse channelAccessTokenResponse = await authenticationService.IssueChannelAccessToken();
			// string channelAccessToken = channelAccessTokenResponse.access_token;
			#endregion

			#region ロングタームのチャンネルアクセストークンを取得
			string channelAccessToken = authenticationService.GetLongTermAccessToken();
			#endregion

			foreach( EventBase webhookEvent in webhookRequest.events ) {

				// 友達追加時イベント
				if( webhookEvent is FollowEvent )
					this.ExecuteFollowEvent( (FollowEvent)webhookEvent );
				// ブロック時イベント
				else if( webhookEvent is UnfollowEvent )
					this.ExecuteUnfollowEvent( (UnfollowEvent)webhookEvent );
				// グループ追加時イベント
				else if( webhookEvent is JoinEvent )
					this.ExecuteJoinEvent( (JoinEvent)webhookEvent );
				// グループ退会時イベント
				else if( webhookEvent is LeaveEvent )
					this.ExecuteLeaveEvent( (LeaveEvent)webhookEvent );
				// メッセージイベント
				else if( webhookEvent is MessageEvent )
					this.ExecuteMessageEvent( (MessageEvent)webhookEvent );
				// ポストバックイベント
				else if( webhookEvent is PostbackEvent )
					this.ExecutePostbackEvent( (PostbackEvent)webhookEvent );
				// ビーコンイベント
				else if( webhookEvent is BeaconEvent )
					this.ExecuteBeaconEvent( (BeaconEvent)webhookEvent );
				else
					Trace.TraceError( "Unexpected Message Type" );

			}

			Trace.TraceInformation( "Webhook API End" );
			return new HttpResponseMessage( HttpStatusCode.OK );

		}

		/// <summary>
		/// メッセージイベント
		/// </summary>
		/// <param name="messageEvent">MessageEvent</param>
		private void ExecuteMessageEvent( MessageEvent messageEvent ) {

			// 音声
			if( messageEvent.message is AudioMessage )
				this.ExecuteAudioMessageEvent( (AudioMessage)messageEvent.message );
			// ファイル
			else if( messageEvent.message is FileMessage )
				this.ExecuteFileMessageEvent( (FileMessage)messageEvent.message );
			// 画像
			else if( messageEvent.message is ImageMessage )
				this.ExecuteImageMessageEvent( (ImageMessage)messageEvent.message );
			// 位置情報
			else if( messageEvent.message is LocationMessage )
				this.ExecuteLocationMessageEvent( (LocationMessage)messageEvent.message );
			// スタンプ
			else if( messageEvent.message is StickerMessage )
				this.ExecuteStickerMessageEvent( (StickerMessage)messageEvent.message );
			// テキスト
			else if( messageEvent.message is TextMessage )
				this.ExecuteTextMessageEvent( (TextMessage)messageEvent.message );
			// 動画
			else if( messageEvent.message is VideoMessage )
				this.ExecuteVideoMessageEvent( (VideoMessage)messageEvent.message );
			// 想定外のイベントの時は何もしない
			else
				Trace.TraceError( "Unexpected Type" );
			
		}

		/// <summary>
		/// ビーコンイベント
		/// </summary>
		/// <param name="beaconEvent">BeaconEvent</param>
		private void ExecuteBeaconEvent( BeaconEvent beaconEvent ) {
			
			// バナータップ時イベント
			if( beaconEvent.beacon is BannerBeacon )
				this.ExecuteBannerBeaconEvent( (BannerBeacon)beaconEvent.beacon );

			// バナー受信圏内に入った時のイベント
			else if( beaconEvent.beacon is EnterBeacon )
				this.ExecuteEnterBeaconEvent( (EnterBeacon)beaconEvent.beacon );

			// バナー受信圏外に出た時のイベント
			else if( beaconEvent.beacon is LeaveBeacon )
				this.ExecuteLeaveBeaconEvent( (LeaveBeacon)beaconEvent.beacon );

			// 想定外のイベントの時は何もしない
			else
				Trace.TraceError( "Unexpected Beacon Type" );
			
		}

		/// <summary>
		/// 友達追加、ブロック解除時イベント
		/// </summary>
		/// <param name="followEvent">FollowEvent</param>
		private void ExecuteFollowEvent( FollowEvent followEvent ) => Trace.TraceInformation( "Execute Follow Event" );

		/// <summary>
		/// グループ参加時イベント
		/// </summary>
		/// <param name="joinEvent">JoinEvent</param>
		private void ExecuteJoinEvent( JoinEvent joinEvent ) => Trace.TraceInformation( "Execute Join Event" );

		/// <summary>
		/// グループ退出時イベント
		/// </summary>
		/// <param name="leaveEvent">LeaveEvent</param>
		private void ExecuteLeaveEvent( LeaveEvent leaveEvent ) => Trace.TraceInformation( "Execute Leave Event" );

		/// <summary>
		/// 音声メッセージイベント
		/// </summary>
		/// <param name="audioMessage">AudioMessage</param>
		private void ExecuteAudioMessageEvent( AudioMessage audioMessage ) => Trace.TraceInformation( "Execute Audio Message Event" );

		// TODO 未確認
		/// <summary>
		/// ファイルメッセージイベント
		/// </summary>
		/// <param name="fileMessage">FileMessage</param>
		private void ExecuteFileMessageEvent( FileMessage fileMessage ) => Trace.TraceInformation( "Execute File Message Event" );

		/// <summary>
		/// 画像メッセージイベント
		/// </summary>
		/// <param name="imageMessage">ImageMessage</param>
		private void ExecuteImageMessageEvent( ImageMessage imageMessage ) => Trace.TraceInformation( "Execute Image Message Event" );

		/// <summary>
		/// 位置情報メッセージイベント
		/// </summary>
		/// <param name="locationMessage">LocationMessage</param>
		private void ExecuteLocationMessageEvent( LocationMessage locationMessage ) => Trace.TraceInformation( "Execute Location Message Event" );
		
		/// <summary>
		/// スタンプメッセージイベント
		/// </summary>
		/// <param name="stickerMessage">StickerMessage</param>
		private void ExecuteStickerMessageEvent( StickerMessage stickerMessage ) => Trace.TraceInformation( "Execute Sticker Message Event" );
		
		/// <summary>
		/// テキストメッセージイベント
		/// </summary>
		/// <param name="textMessage">TextMessage</param>
		private void ExecuteTextMessageEvent( TextMessage textMessage ) => Trace.TraceInformation( "Execute Text Message Event" );

		/// <summary>
		/// 動画メッセージイベント
		/// </summary>
		/// <param name="videoMessage">VideoMessage</param>
		private void ExecuteVideoMessageEvent( VideoMessage videoMessage ) => Trace.TraceInformation( "Execute Video Message Event" );
		
		/// <summary>
		/// ポストバックイベント
		/// </summary>
		/// <param name="postbackEvent">PostbackEvent</param>
		private void ExecutePostbackEvent( PostbackEvent postbackEvent ) => Trace.TraceInformation( "Execute Postback Message Event" );
		
		/// <summary>
		/// ブロック時イベント
		/// </summary>
		/// <param name="unfollowEvent">UnfollowEvent</param>
		private void ExecuteUnfollowEvent( UnfollowEvent unfollowEvent ) => Trace.TraceInformation( "Execute Unfollow Message Event" );
		
		/// <summary>
		/// バナータップ時イベント
		/// </summary>
		/// <param name="bannerBeacon">BannerBeacon</param>
		private void ExecuteBannerBeaconEvent( BannerBeacon bannerBeacon ) => Trace.TraceInformation( "Execute Banner Beacon Message Event" );
		
		/// <summary>
		/// ビーコン受信圏内に入った時のイベント
		/// </summary>
		/// <param name="enterBeacon">EnterBeacon</param>
		private void ExecuteEnterBeaconEvent( EnterBeacon enterBeacon ) => Trace.TraceInformation( "Execute Enter Beacon Message Event" );

		/// <summary>
		/// ビーコン受信圏外に出た時のイベント
		/// </summary>
		/// <param name="leaveBeacon">LeaveBeacon</param>
		private void ExecuteLeaveBeaconEvent( LeaveBeacon leaveBeacon ) => Trace.TraceInformation( "Execute Leave Beacon Message Event" );

	}

}