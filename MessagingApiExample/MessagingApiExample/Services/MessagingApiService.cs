using System;
using System.Diagnostics;
using MessagingApiExample.Models.Request.Webhook.Body;
using MessagingApiExample.Models.Webhook.Body.Event;
using MessagingApiExample.Models.Webhook.Body.Event.Beacon;
using MessagingApiExample.Models.Webhook.Body.Event.Message;
using MessagingApiExample.Models.Webhook.Body.Event.Postback;
using MessagingApiExample.Models.Webhook.Body.Event.Postback.Parameter;
using MessagingApiExample.Models.Webhook.Body.Event.Source;
using Newtonsoft.Json.Linq;

namespace MessagingApiExample.Services {

	/// <summary>
	/// Messaging API用サービス
	/// </summary>
	public class MessagingApiService {

		/// <summary>
		/// JTokenからWebhookRequestデータモデルに変換する
		/// </summary>
		/// <param name="token">リクエストより送られたJToken</param>
		/// <returns>WebhookRequest</returns>
		public WebhookRequest ConvertJTokenToWebhookRequest( JToken token ) {

			Trace.TraceInformation( "Start Convert JToken To Webhook Request" );

			WebhookRequest webhookRequest = new WebhookRequest();

			// Event数を取得
			JArray events = (JArray)token[ "events" ];
			webhookRequest.events = new EventBase[ events.Count ];
			Trace.TraceInformation( "Events Count is : " + events.Count );
			
			// TypeからEventを割り出す
			for( int i = 0 ; i < webhookRequest.events.Length ; i++ ) {

				Trace.TraceInformation( "Event Type is : " + events[ i ][ "type" ] );
				switch( events[ i ][ "type" ].Value<string>() ) {

					// ビーコン
					case "beacon":
						webhookRequest.events[ i ] = new BeaconEvent() {
							beacon = this.ConvertBeacon( (JValue)events[ i ][ "beacon" ] ) ,
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// 友達追加またはブロック解除時
					case "follow":
						webhookRequest.events[ i ] = new FollowEvent() {
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// グループ参加時
					case "join":
						webhookRequest.events[ i ] = new JoinEvent() {
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// グループ退出時
					case "leave":
						webhookRequest.events[ i ] = new LeaveEvent();
						break;

					// メッセージ
					case "message":
						webhookRequest.events[ i ] = new MessageEvent() {
							message = this.ConvertMessage( (JValue)events[ i ][ "message" ] ) ,
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// ポストバック
					case "postback":
						webhookRequest.events[ i ] = new PostbackEvent() {
							postback = this.ConvertPostback( (JValue)events[ i ][ "postback" ] ) ,
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// ブロック時
					case "unfollow":
						webhookRequest.events[ i ] = new UnfollowEvent();
						break;

					// その他
					default:
						return null;

				}
				
				// 共通情報設定
				webhookRequest.events[ i ].source = this.ConvertSource( (JValue)events[ i ][ "source" ] );
				webhookRequest.events[ i ].timestamp = events[ i ][ "timestamp" ].Value<DateTime>();

			}
			
			return webhookRequest;

		}

		/// <summary>
		/// Beaconの変換
		/// </summary>
		/// <param name="beacon">JValueのbeacon</param>
		/// <returns>BeaconBaseのbeacon</returns>
		private BeaconBase ConvertBeacon( JValue beacon ) {

			BeaconBase beaconBase;

			// typeがenterならビーコン受信圏内に入った
			if( "enter".Equals( beacon[ "type" ].Value<string>() ) )
				beaconBase = new EnterBeacon();

			// typeがleaveならビーコン受信県外に出た
			else if( "leave".Equals( beacon[ "type" ].Value<string>() ) )
				beaconBase = new LeaveBeacon();

			// typeがenterでもleaveでもなければbannerタップ時
			else
				beaconBase = new BannerBeacon();

			// 共通情報設定
			beaconBase.hwid = beacon[ "hwid" ].Value<string>();
			beaconBase.dm = beacon[ "dm" ].Value<string>();

			return beaconBase;

		}

		/// <summary>
		/// Messageの変換
		/// </summary>
		/// <param name="message">JValueのmessage</param>
		/// <returns>MessageBaseのmessage</returns>
		private MessageBase ConvertMessage( JValue message ) {

			// typeで分岐
			switch( message[ "type" ].Value<string>() ) {

				// 音声
				case "audio":
					return new AudioMessage() {
						id = message[ "id" ].Value<string>()
					};

				// ファイル
				case "file":
					return new FileMessage() {
						id = message[ "id" ].Value<string>() ,
						fileSize = message[ "fileSize" ].Value<string>() ,
						fileName = message[ "fileName" ].Value<string>()
					};

				// 画像
				case "image":
					return new ImageMessage() {
						id = message[ "id" ].Value<string>()
					};

				// 位置情報
				case "location":
					return new LocationMessage() {
						id = message[ "id" ].Value<string>() ,
						address = message[ "address" ].Value<string>() ,
						title = message[ "title" ].Value<string>() ,
						latitude = message[ "latitude" ].Value<string>() ,
						longitude = message[ "longitude" ].Value<string>()
					};

				// スタンプ
				case "sticker":
					return new StickerMessage() {
						id = message[ "id" ].Value<string>() ,
						stickerId = message[ "stickerId" ].Value<string>() ,
						packageId = message[ "packageId" ].Value<string>()
					};

				// テキスト
				case "text":
					return new TextMessage() {
						id = message[ "id" ].Value<string>() ,
						text = message[ "text" ].Value<string>()
					};

				// 動画
				case "video":
					return new VideoMessage() {
						id = message[ "id" ].Value<string>()
					};

				// その他
				default:
					return null;

			}
			
		}

		/// <summary>
		/// Postbackの変換
		/// </summary>
		/// <param name="postback">JValueのpostback</param>
		/// <returns>PostbackDataのpostback</returns>
		private PostbackData ConvertPostback( JValue postback ) =>
			new PostbackData() {
				data = postback[ "data" ].Value<string>() ,
				parameters = new PostbackParameter() {
					date = postback[ "param" ][ "date" ].Value<DateTime>() ,
					datetime = postback[ "param" ][ "datetime" ].Value<DateTime>() ,
					time = postback[ "param" ][ "time" ].Value<TimeSpan>()
				}
			};

		/// <summary>
		/// Sourveの変換
		/// </summary>
		/// <param name="source">JValueのsource</param>
		/// <returns>SourceBaseのsource</returns>
		private SourceBase ConvertSource( JValue source ) {

			// groupIdがあればグループからの送信
			if( source[ "groupId" ].HasValues )
				return new GroupSource() {
					groupId = source[ "groupId" ].Value<string>() ,
					userId = source[ "userId" ].Value<string>()
				};

			// roomIdがあればトークルームからの送信
			else if( source[ "roomId" ].HasValues )
				return new RoomSource() {
					roomId = source[ "roomId" ].Value<string>() ,
					userId = source[ "userId" ].Value<string>()
				};

			// groupIdもroomIdもなければユーザからの送信
			else
				return new UserSource() {
					userId = source[ "userId" ].Value<string>()
				};

		}

	}

}