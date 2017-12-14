using System.Diagnostics;
using MessagingApiTemplate.Models.Requests.Webhook;
using MessagingApiTemplate.Models.Requests.Webhook.Event;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Beacon;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Message;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Postback;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Postback.Parameter;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Source;
using Newtonsoft.Json.Linq;

namespace MessagingApiTemplate.Utils {

	/// <summary>
	/// JToken変換用クラス
	/// </summary>
	internal static class JTokenConverter {

		/// <summary>
		/// JTokenからWebhookRequestデータモデルに変換する
		/// </summary>
		/// <param name="token">リクエストより送られたJToken</param>
		/// <returns>WebhookRequest</returns>
		internal static WebhookRequest ConvertJTokenToWebhookRequest( JToken token ) {

			WebhookRequest webhookRequest = new WebhookRequest();

			// Event数を取得
			JArray events = (JArray)token[ "events" ];
			webhookRequest.events = new EventBase[ events.Count ];

			// TypeからEventを割り出す
			for( int i = 0 ; i < webhookRequest.events.Length ; i++ ) {
				
				switch( events[ i ][ "type" ].Value<string>() ) {

					// ビーコン
					case "beacon":
						// TODO 未確認
						webhookRequest.events[ i ] = new BeaconEvent() {
							beacon = ConvertBeacon( (JObject)events[ i ][ "beacon" ] ) ,
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// 友達追加またはブロック解除時
					case "follow":
						// TODO 未確認
						webhookRequest.events[ i ] = new FollowEvent() {
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// グループ参加時
					case "join":
						// TODO 未確認
						webhookRequest.events[ i ] = new JoinEvent() {
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						break;

					// グループ退出時
					case "leave":
						// TODO 未確認
						webhookRequest.events[ i ] = new LeaveEvent();
						break;

					// メッセージ
					case "message":
						webhookRequest.events[ i ] = new MessageEvent() {
							message = ConvertMessage( (JObject)events[ i ][ "message" ] ) ,
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						Trace.TraceInformation( "Reply Token is : " + ( webhookRequest.events[ i ] as MessageEvent ).replyToken );
						break;

					// ポストバック
					case "postback":
						// TODO 未確認
						webhookRequest.events[ i ] = new PostbackEvent() {
							postback = ConvertPostback( (JObject)events[ i ][ "postback" ] ) ,
							replyToken = events[ i ][ "replyToken" ].Value<string>()
						};
						Trace.TraceInformation( "Reply Token is : " + ( webhookRequest.events[ i ] as PostbackEvent ).replyToken );
						break;

					// ブロック時
					case "unfollow":
						// TODO 未確認
						webhookRequest.events[ i ] = new UnfollowEvent();
						break;

					// その他
					default:
						// TODO 未確認
						Trace.TraceInformation( "Event Type Couldn't Be Identified" );
						return null;

				}

				// 共通情報設定
				webhookRequest.events[ i ].source = ConvertSource( (JObject)events[ i ][ "source" ] );
				webhookRequest.events[ i ].timestamp = events[ i ][ "timestamp" ].Value<long>();
				Trace.TraceInformation( "Timestamp is : " + webhookRequest.events[ i ].timestamp );

			}

			return webhookRequest;

		}

		/// <summary>
		/// Beaconの変換
		/// </summary>
		/// <param name="beacon">JValueのbeacon</param>
		/// <returns>BeaconBaseのbeacon</returns>
		private static BeaconBase ConvertBeacon( JObject beacon ) {

			BeaconBase beaconBase;

			Trace.TraceInformation( "Beacon Type is : " + beacon[ "type" ].Value<string>() );
			switch( beacon[ "type" ].Value<string>() ) {

				case "enter":
					beaconBase = new EnterBeacon();
					break;

				case "leave":
					beaconBase = new LeaveBeacon();
					break;

				case "banner":
					beaconBase = new BannerBeacon();
					break;

				default:
					Trace.TraceInformation( "Don't Convert Beacon" );
					return null;

			}

			// 共通情報設定
			beaconBase.hwid = beacon[ "hwid" ].Value<string>();
			Trace.TraceInformation( "Hard Were Id is : " + beaconBase.hwid );
			beaconBase.dm = beacon[ "dm" ].Value<string>();
			Trace.TraceInformation( "DM is : " + beaconBase.dm );

			return beaconBase;

		}

		/// <summary>
		/// Messageの変換
		/// </summary>
		/// <param name="message">JValueのmessage</param>
		/// <returns>MessageBaseのmessage</returns>
		private static MessageBase ConvertMessage( JObject message ) {

			// typeで分岐
			Trace.TraceInformation( "Message Type is : " + message[ "type" ].Value<string>() );
			switch( message[ "type" ].Value<string>() ) {

				// 音声
				case "audio":
					AudioMessage audioMessage = new AudioMessage() {
						id = message[ "id" ].Value<string>()
					};
					Trace.TraceInformation( "Id is : " + audioMessage.id );
					return audioMessage;

				// ファイル
				case "file":
					// TODO 未確認
					FileMessage fileMessage = new FileMessage() {
						id = message[ "id" ].Value<string>() ,
						fileSize = message[ "fileSize" ].Value<string>() ,
						fileName = message[ "fileName" ].Value<string>()
					};
					Trace.TraceInformation( "Id is : " + fileMessage.id );
					Trace.TraceInformation( "File Size is : " + fileMessage.fileSize );
					Trace.TraceInformation( "File Name is : " + fileMessage.fileName );
					return fileMessage;

				// 画像
				case "image":
					ImageMessage imageMessage = new ImageMessage() {
						id = message[ "id" ].Value<string>()
					};
					Trace.TraceInformation( "Id is : " + imageMessage.id );
					return imageMessage;

				// 位置情報
				case "location":
					LocationMessage locationMessage = new LocationMessage() {
						id = message[ "id" ].Value<string>() ,
						address = message[ "address" ].Value<string>() ,
						title = message[ "title" ].Value<string>() ,
						latitude = message[ "latitude" ].Value<decimal>() ,
						longitude = message[ "longitude" ].Value<decimal>()
					};
					Trace.TraceInformation( "Id is : " + locationMessage.id );
					Trace.TraceInformation( "Address is : " + locationMessage.address );
					Trace.TraceInformation( "Title is :" + locationMessage.title );
					Trace.TraceInformation( "Latitude is : " + locationMessage.latitude );
					Trace.TraceInformation( "Longitude is : " + locationMessage.longitude );
					return locationMessage;

				// スタンプ
				case "sticker":
					StickerMessage stickerMessage = new StickerMessage() {
						id = message[ "id" ].Value<string>() ,
						stickerId = message[ "stickerId" ].Value<string>() ,
						packageId = message[ "packageId" ].Value<string>()
					};
					Trace.TraceInformation( "Id is : " + stickerMessage.id );
					Trace.TraceInformation( "Sticker Id is : " + stickerMessage.stickerId );
					Trace.TraceInformation( "Package Id is :" + stickerMessage.packageId );
					return stickerMessage;


				// テキスト
				case "text":
					TextMessage textMessage = new TextMessage() {
						id = message[ "id" ].Value<string>() ,
						text = message[ "text" ].Value<string>()
					};
					Trace.TraceInformation( "Id is : " + textMessage.id );
					Trace.TraceInformation( "Textis : " + textMessage.text );
					return textMessage;

				// 動画
				case "video":
					VideoMessage videoMessage = new VideoMessage() {
						id = message[ "id" ].Value<string>()
					};
					Trace.TraceInformation( "Id is : " + videoMessage.id );
					return videoMessage;

				// その他
				default:
					// TODO 未確認
					Trace.TraceWarning( "Don't Convert Message" );
					return null;

			}

		}

		/// <summary>
		/// Postbackの変換
		/// </summary>
		/// <param name="postback">JValueのpostback</param>
		/// <returns>PostbackDataのpostback</returns>
		private static PostbackData ConvertPostback( JObject postback ) {
			PostbackData postbackData = new PostbackData() {
				data = postback[ "data" ].Value<string>() ,
				parameters = new PostbackParameter() {
					date = postback[ "param" ][ "date" ].Value<string>() ,
					datetime = postback[ "param" ][ "datetime" ].Value<string>() ,
					time = postback[ "param" ][ "time" ].Value<string>()
				}
			};
			Trace.TraceInformation( "Postback Data is :" + postbackData.data );
			Trace.TraceInformation( "Postback Parameter Date is : " + postbackData.parameters.date );
			Trace.TraceInformation( "Postback Parameter Date Time is : " + postbackData.parameters.datetime );
			Trace.TraceInformation( "Postback Parameter Time is : " + postbackData.parameters.time );
			return postbackData;
		}

		/// <summary>
		/// Sourceの変換
		/// </summary>
		/// <param name="source">JValueのsource</param>
		/// <returns>SourceBaseのsource</returns>
		private static SourceBase ConvertSource( JObject source ) {

			Trace.TraceInformation( "Source Type is : " + source[ "type" ].Value<string>() );
			switch( source[ "type" ].Value<string>() ) {

				// グループ
				case "group":
					// TODO 未確認
					GroupSource groupSource = new GroupSource() {
						groupId = source[ "groupId" ]?.Value<string>() ,
						userId = source[ "userId" ]?.Value<string>()
					};
					Trace.TraceInformation( "Group Id is : " + groupSource.groupId );
					Trace.TraceInformation( "User Id is : " + groupSource.userId );
					return groupSource;

				// トークルーム
				case "room":
					// TODO 未確認
					RoomSource roomSource = new RoomSource() {
						roomId = source[ "roomId" ]?.Value<string>() ,
						userId = source[ "userId" ]?.Value<string>()
					};
					Trace.TraceInformation( "Room Id is : " + roomSource.roomId );
					Trace.TraceInformation( "User Id is : " + roomSource.userId );
					return roomSource;

				// ユーザ
				case "user":
					UserSource userSource = new UserSource() {
						userId = source[ "userId" ]?.Value<string>()
					};
					Trace.TraceInformation( "User Id is : " + userSource.userId );
					return userSource;

				default:
					// TODO 未確認
					Trace.TraceWarning( "Don't Convert Source" );
					return null;
			}

		}
		
	}

}